import React, { useContext, useEffect } from "react";
import { AzureMapsContext, IAzureMapsContextProps } from "react-azure-maps";
import { data, layer, source } from "azure-maps-control";
import MapComponent from "./MapComponent";
import { useLazyGetStoresBySubdivisionIdQuery } from "../services/stores";
import { useLazyGetBoundaryBySubdivisionIdQuery } from "../services/subdivisions";
import { Store } from "../services/types";

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
const layerRef = new layer.SymbolLayer(dataSourceRef, "symbol", {
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

  useEffect(() => {
    if (isMapReady && mapRef) {
      // Need to add source and layer to map on init and ready
      mapRef.sources.add(dataSourceRef);
      mapRef.layers.add(lineLayer);
      mapRef.layers.add(polygonLayer);
      mapRef.layers.add(layerRef);
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
