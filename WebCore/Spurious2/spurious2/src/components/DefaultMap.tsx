import React, { useEffect } from "react";
import {
  AzureMap,
  AzureMapsProvider,
  IAzureMapOptions,
  AuthenticationType,
} from "react-azure-maps";
import { data } from "azure-maps-control";
import { useLazyGetBoundaryBySubdivisionIdQuery } from "../services/subdivisions";

const option: IAzureMapOptions = {
  authOptions: {
    authType: AuthenticationType.subscriptionKey,
    subscriptionKey: "UHo_yP7VRSrUF-ZA_GFnT7YOz1b-MoRMT90xMbDybzs", // Your subscription key
  },
  center: new data.Position(-83.9355468749954, 48.25394114468216),
  zoom: 4,
};

export interface DefaultMapProps {
  subdivisionId: number | undefined;
}

const DefaultMap = ({ subdivisionId }: DefaultMapProps) => {
  const [getBoundaryQuery, result] = useLazyGetBoundaryBySubdivisionIdQuery();

  useEffect(() => {
    console.log("subdivid in defaultmap", subdivisionId);
    if (subdivisionId) {
      getBoundaryQuery(subdivisionId);
    }
  }, [subdivisionId, getBoundaryQuery]);

  useEffect(() => {
    if (!result.isLoading && result.isSuccess) {
      console.log("result in defaultmap", result);
    }
  }, [result]);

  return (
    <AzureMapsProvider>
      <div style={{ height: "400px" }}>
        <AzureMap options={option} />
      </div>
    </AzureMapsProvider>
  );
};

export default DefaultMap;
