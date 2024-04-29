import React, { useContext, useEffect, useState } from "react";
import { AzureMapsContext, IAzureMapsContextProps } from "react-azure-maps";
import { data, layer, source } from "azure-maps-control";
import MapComponent from "./MapComponent";
import Button from "@mui/material/Button";

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
const getRandomPosition = () => {
  const randomLongitude = Math.floor(Math.random() * (180 - -180) + -180);
  const randomLatitude = Math.floor(Math.random() * (-90 - 90) + 90);
  return [randomLatitude, randomLongitude];
};

const styles = {
  buttonContainer: {
    display: "grid",
    gridAutoFlow: "column",
    gridGap: "10px",
    gridAutoColumns: "max-content",
    padding: "10px 0",
    alignItems: "center",
  },
  button: {
    height: 35,
    width: 80,
    backgroundColor: "#68aba3",
  },
};

const MapController = () => {
  // Here you use mapRef from context
  const { mapRef, isMapReady } =
    useContext<IAzureMapsContextProps>(AzureMapsContext);
  const [showTileBoundaries, setShowTileBoundaries] = useState(true);

  useEffect(() => {
    if (mapRef) {
      // Simple Style modification
      mapRef.setStyle({ showTileBoundaries: !showTileBoundaries });
    }
  }, [showTileBoundaries, mapRef]);

  useEffect(() => {
    if (isMapReady && mapRef) {
      // Need to add source and layer to map on init and ready
      mapRef.sources.add(dataSourceRef);
      mapRef.layers.add(lineLayer);
      mapRef.layers.add(polygonLayer);
      mapRef.layers.add(layerRef);
    }
  }, [isMapReady, mapRef]);

  const changeMapCenter = () => {
    if (mapRef) {
      // Simple Camera options modification
      mapRef.setCamera({ center: getRandomPosition() });
    }
  };

  const toggleTitleBoundaries = () => {
    setShowTileBoundaries((prev) => !prev);
  };

  const addRandomMarker = () => {
    dataSourceRef.clear();
    const randomLongitude = Math.floor(Math.random() * (180 - -180) + -180);
    const randomLatitude = Math.floor(Math.random() * (-90 - 90) + 90);
    const newPoint = new data.Position(randomLongitude, randomLatitude);

    dataSourceRef.add(new data.Feature(new data.Point(newPoint)));
  };

  const addShape = async () => {
    dataSourceRef.clear();
    await dataSourceRef.importDataFromUrl(
      "http://localhost:5207/api/subdivisions/3537001/boundary"
    );
    const shapes = dataSourceRef.getShapes();
    const bounds = shapes[0].getBounds();
    const centre = data.BoundingBox.getCenter(bounds);
    mapRef?.setCamera({ bounds: bounds, center: centre });
    const currentZoom = mapRef?.getCamera().zoom;
    if (currentZoom) {
      mapRef.setCamera({ zoom: currentZoom - 1 });
    }
  };

  return (
    <>
      <div>Map Controller</div>
      <div style={styles.buttonContainer}>
        <Button
          size="small"
          variant="contained"
          color="primary"
          onClick={toggleTitleBoundaries}
        >
          Toggle Title Boundaries
        </Button>
        <Button
          size="small"
          variant="contained"
          color="primary"
          onClick={changeMapCenter}
        >
          Change Map Center
        </Button>
        <Button
          size="small"
          variant="contained"
          color="primary"
          onClick={addRandomMarker}
        >
          Add Pin
        </Button>
        <Button
          size="small"
          variant="contained"
          color="primary"
          onClick={addShape}
        >
          Add Shape
        </Button>
      </div>
      <MapComponent />
    </>
  );
};

export default MapController;
