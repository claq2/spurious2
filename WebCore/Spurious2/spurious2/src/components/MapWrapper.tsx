import React from "react";
import { AzureMapsProvider } from "react-azure-maps";
import MapController from "./MapController";

const MapWrapper = () => {
  return (
    <AzureMapsProvider>
      <>
        <div>Map Wrapper</div>
        <MapController />
      </>
    </AzureMapsProvider>
  );
};

export default MapWrapper;
