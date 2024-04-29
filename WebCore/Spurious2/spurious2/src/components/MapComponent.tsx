import React from "react";
import { AzureMap, IAzureMapOptions } from "react-azure-maps";
import { AuthenticationType } from "azure-maps-control";

const option: IAzureMapOptions = {
  authOptions: {
    authType: AuthenticationType.subscriptionKey,
    subscriptionKey: "UHo_yP7VRSrUF-ZA_GFnT7YOz1b-MoRMT90xMbDybzs", // Your subscription key
  },
};

const MapComponent = () => {
  return (
    <div style={{ height: "400px" }}>
      <AzureMap options={option} />
    </div>
  );
};

export default MapComponent;
