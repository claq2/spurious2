import { AzureMap, IAzureMapOptions } from "react-azure-maps";
import { AuthenticationType } from "azure-maps-control";

const option: IAzureMapOptions = {
  authOptions: {
    authType: AuthenticationType.anonymous,
    clientId: "467d3301-598b-44ba-bf81-a905711dc9f1",
    getToken: function (resolve) {
      fetch("/api/azure-maps-token")
        .then(function (response) {
          return response.text();
        })
        .then(function (token) {
          resolve(token);
        });
    },
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
