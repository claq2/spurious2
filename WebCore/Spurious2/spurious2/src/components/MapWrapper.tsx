import React from "react";
import { AzureMapsProvider } from "react-azure-maps";
import MapController from "./MapController";

interface MapWrapperProps {
  subdivisionId: number | undefined;
}

const MapWrapper = ({ subdivisionId }: MapWrapperProps) => {
  return (
    <AzureMapsProvider>
      <MapController subdivisionId={subdivisionId} />
    </AzureMapsProvider>
  );
};

export default MapWrapper;
