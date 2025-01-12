import { useContext, useEffect, useRef, useState } from "react";
import {
  AzureMapsContext,
  IAzureMapsContextProps,
  useCreatePopup,
} from "react-azure-maps";
import { MapMouseEvent, data, layer, source, Shape } from "azure-maps-control";
import MapComponent from "./MapComponent";
import { useLazyGetStoresBySubdivisionIdQuery } from "../services/stores";
import { useLazyGetBoundaryBySubdivisionIdQuery } from "../services/subdivisions";
import { Inventory, Store } from "../services/types";

const dataSourceRef = new source.DataSource();
const lineLayer = new layer.LineLayer(dataSourceRef, undefined, {
  strokeColor: "black",
  strokeWidth: 0.7,
});
const polygonLayer = new layer.PolygonLayer(dataSourceRef, undefined, {
  fillOpacity: 0.3,
  filter: [
    "any",
    ["==", ["geometry-type"], "Polygon"],
    ["==", ["geometry-type"], "MultiPolygon"],
  ], //Only render Polygon or MultiPolygon in this layer.
});
const pointLayer = new layer.SymbolLayer(dataSourceRef, "symbol", {
  filter: ["==", ["geometry-type"], "Point"],
  iconOptions: { allowOverlap: true, image: "pin-blue" },
});

interface MapControllerProps {
  subdivisionId: number | undefined;
}

const MapController = ({ subdivisionId }: MapControllerProps) => {
  // Here you use mapRef from context
  const { mapRef, isMapReady } =
    useContext<IAzureMapsContextProps>(AzureMapsContext);
  const [getStoresQuery, getStoresResult] =
    useLazyGetStoresBySubdivisionIdQuery();
  const [getBoundaryQuery, getBoundaryResult] =
    useLazyGetBoundaryBySubdivisionIdQuery();
  const [subdivisionIdForStores, setSubdivisionIdForStores] = useState<
    number | undefined
  >(undefined);

  const [storeName, setStoreName] = useState("");
  const [storeCity, setStoreCity] = useState("");
  const [alcoholTypes, setAlcoholTypes] = useState<Inventory[]>([]);
  const popup = useRef(
    useCreatePopup({
      options: { pixelOffset: [0, -18] },
      popupContent: (
        <div className="customInfobox">
          <div className="name">{storeName}</div>
          <div className="name">{storeCity}</div>
          {alcoholTypes.map((at) => (
            <div key={at.alcoholType}>
              {at.alcoholType}: {at.volume}
            </div>
          ))}
        </div>
      ),
      isVisible: false,
    })
  );

  useEffect(() => {
    if (isMapReady && mapRef) {
      // Need to add source and layer to map on init and ready
      mapRef.sources.add(dataSourceRef);
      mapRef.layers.add(lineLayer);
      mapRef.layers.add(polygonLayer);
      mapRef.layers.add(pointLayer);

      mapRef.popups.add(popup.current);

      mapRef.events.add("mouseenter", pointLayer, (e: MapMouseEvent) => {
        if (e.shapes && e.shapes.length > 0) {
          const s = e.shapes[0] as Shape;
          const storeName: string = s.getProperties()["name"] as string;
          setStoreName(storeName);
          const cityName: string = s.getProperties()["city"] as string;
          setStoreCity(cityName);
          const inventories: Inventory[] = s.getProperties()[
            "inventories"
          ] as Inventory[];
          setAlcoholTypes(inventories);
          popup.current.setOptions({
            position: s.getCoordinates() as data.Position,
            pixelOffset: [0, -18],
          });
          popup.current.open();
        }
      });

      mapRef.events.add("mouseleave", pointLayer, () => {
        popup.current.close();
      });
    }
  }, [isMapReady, mapRef, popup]);

  useEffect(() => {
    console.debug("subdivid changed, closing popup");
    popup.current.close();
    if (subdivisionId) {
      console.debug("launching getboundaryquery");
      void getBoundaryQuery(subdivisionId, true);
      setSubdivisionIdForStores(subdivisionId);
    }
  }, [subdivisionId, getBoundaryQuery, popup]);

  useEffect(() => {
    if (
      getBoundaryResult.isSuccess &&
      !getBoundaryResult.isFetching &&
      !getBoundaryResult.isLoading
    ) {
      console.debug("clearing datasource");
      dataSourceRef.clear();
      dataSourceRef.add(getBoundaryResult.data);
      const shapes = dataSourceRef.getShapes();
      const bounds = shapes[0].getBounds();
      const centre = data.BoundingBox.getCenter(bounds);
      mapRef?.setCamera({ bounds: bounds, center: centre });
      const currentZoom = mapRef?.getCamera().zoom;
      if (currentZoom) {
        mapRef.setCamera({ zoom: currentZoom - 1 });
      }

      if (subdivisionIdForStores) {
        console.debug("launching getstoresquery");
        void getStoresQuery(subdivisionIdForStores, true);
      }
    }
    console.debug("completed shape update");
  }, [getBoundaryResult, mapRef, getStoresQuery, subdivisionIdForStores]);

  useEffect(() => {
    console.debug("getStoresResult", getStoresResult);
    if (
      getStoresResult.isSuccess &&
      !getStoresResult.isFetching &&
      !getStoresResult.isLoading
    ) {
      getStoresResult.data.forEach((s: Store) => {
        console.debug("s", s);
        if (s.locationCoordinates && s.locationCoordinates.coordinates) {
          const storeFeature = new data.Feature(
            new data.Point(
              new data.Position(
                s.locationCoordinates.coordinates[0],
                s.locationCoordinates.coordinates[1]
              )
            ),
            { name: s.name, city: s.city, inventories: s.inventories }
          );
          dataSourceRef.add(storeFeature);
          console.debug("added store", storeFeature);
        }
      });
    }
  }, [getStoresResult]);

  return <MapComponent />;
};

export default MapController;
