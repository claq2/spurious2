import React from "react";
import {
  AzureMap,
  AzureMapsProvider,
  IAzureMapOptions,
  AuthenticationType,
} from "react-azure-maps";
import { data } from "azure-maps-control";
const option: IAzureMapOptions = {
  authOptions: {
    authType: AuthenticationType.subscriptionKey,
    subscriptionKey: "UHo_yP7VRSrUF-ZA_GFnT7YOz1b-MoRMT90xMbDybzs", // Your subscription key
  },
  center: new data.Position(-83.9355468749954, 48.25394114468216),
  zoom: 4,
};

const DefaultMap: React.FC = () => (
  <AzureMapsProvider>
    <div style={{ height: "400px" }}>
      <AzureMap options={option} />
    </div>
  </AzureMapsProvider>
);

export default DefaultMap;
