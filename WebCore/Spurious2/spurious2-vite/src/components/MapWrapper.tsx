import { useEffect } from "react";
import { AzureMapsProvider } from "react-azure-maps";
import MapController from "./MapController";

interface MapWrapperProps {
  subdivisionId: number | undefined;
}

const MapWrapper = ({ subdivisionId }: MapWrapperProps) => {
  useEffect(() => {
    console.debug("subdivisionId in MapWrapper", subdivisionId);
  }, [subdivisionId]);
  return (
    <AzureMapsProvider>
      <MapController subdivisionId={subdivisionId} />
    </AzureMapsProvider>
  );
};

export default MapWrapper;
