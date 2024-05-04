import React, { useContext, useEffect } from "react";
import { AzureMapsContext, IAzureMapsContextProps } from "react-azure-maps";
import {
  MapMouseEvent,
  PopupOptions,
  data,
  layer,
  source,
  Shape,
} from "azure-maps-control";
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

const alcoholTemplate = "<div>{alcoholType}: {volume} L</div>";
const popupTemplate =
  '<div class="customInfobox"><div class="name">{name}</div><div class="name">{city}</div>{alcoholTypes}</div>';

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

  useEffect(() => {
    if (isMapReady && mapRef) {
      // Need to add source and layer to map on init and ready
      mapRef.sources.add(dataSourceRef);
      mapRef.layers.add(lineLayer);
      mapRef.layers.add(polygonLayer);
      mapRef.layers.add(pointLayer);

      const popup: PopupOptions = {
        pixelOffset: [0, -18],
        //closeButton: false
      };

      mapRef.events.add("click", pointLayer, (e: MapMouseEvent) => {
        //console.log('click');
        //Make sure that the point exists.
        if (e.shapes && e.shapes.length > 0) {
          //console.log(e.shapes[0]);
          //const x: data.Feature<data.Geometry, any> = e.shapes[0] as data.Feature<data.Geometry, any>;
          const s = e.shapes[0] as Shape;
          //x.properties;
          // console.log(x);
          // console.log(s);
          //console.log('x.prop');
          //console.log(x.properties['name']);
          const storeName: string = s.getProperties()["name"] as string;
          const cityName: string = s.getProperties()["city"] as string;
          const inventories: Inventory[] = s.getProperties()[
            "inventories"
          ] as Inventory[];
          let inventoryDiv = "";
          inventories.forEach((i) => {
            if (i.alcoholType && i.volume)
              return (inventoryDiv += alcoholTemplate
                .replace(/{alcoholType}/g, i.alcoholType)
                .replace(/{volume}/g, i.volume.toString()));
          });
          const content: string = popupTemplate
            .replace(/{name}/g, storeName)
            .replace(/{city}/g, cityName)
            .replace(/{alcoholTypes}/g, inventoryDiv);
          //console.log('s.getProp');
          //console.log(s.getProperties()['name']);
          //console.log(s.getProperties()['inventories']);
          //let properties = e.shapes[0].properties;// getProperties();
          //content = popupTemplate.replace(/{name}/g, properties.name).replace(/{description}/g, properties.description);
          const coordinate = s.getCoordinates();

          popup.setOptions({
            //Update the content of the popup.
            content: content,

            //Update the popup's position with the symbol's coordinate.
            position: coordinate as data.Position,
          });
          //Open the popup.
          popup.open(mapRef);
        }
      });
    }
  }, [isMapReady, mapRef]);

  useEffect(() => {
    if (subdivisionId) {
      void getBoundaryQuery(subdivisionId, true);
    }
  }, [subdivisionId, getBoundaryQuery]);

  useEffect(() => {
    if (getBoundaryResult.isSuccess) {
      // console.log("getBoundaryResult.data", getBoundaryResult.data);
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

      if (subdivisionId) {
        void getStoresQuery(subdivisionId, true);
      }
    }
  }, [getBoundaryResult, mapRef, getStoresQuery, subdivisionId]);

  useEffect(() => {
    if (getStoresResult.isSuccess) {
      getStoresResult.data.forEach((s: Store) => {
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
          //console.log('f');
          //console.log(f);
          dataSourceRef.add(storeFeature);
        }
      });
    }
  }, [getStoresResult]);

  return <MapComponent />;
};

export default MapController;
