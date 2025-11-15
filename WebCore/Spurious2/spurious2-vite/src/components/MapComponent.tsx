import { AzureMap, IAzureMapOptions } from "react-azure-maps";
import { AuthenticationType } from "azure-maps-control";

const option: IAzureMapOptions = {
  authOptions: {
    authType: AuthenticationType.anonymous,
    clientId: "44657cb2-16f2-4afa-b207-635cf196d22e",
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
