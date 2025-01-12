import { AzureMap, IAzureMapOptions } from "react-azure-maps";
import { AuthenticationType } from "azure-maps-control";

const option: IAzureMapOptions = {
  authOptions: {
    authType: AuthenticationType.anonymous,
    clientId: "6c2581ec-8018-4346-8b4e-d18b27b420b2",
    getToken: function (resolve) {
      fetch("https://spurious2.azurewebsites.net/api/azure-maps-token")
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
