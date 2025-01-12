import { useEffect, useState } from "react";
import {
  AzureMap,
  AzureMapsProvider,
  IAzureMapOptions,
  AuthenticationType,
  AzureMapDataSourceProvider,
  AzureMapLayerProvider,
  AzureMapFeature,
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
  const [coords, setCoords] = useState<[number[]] | undefined>(undefined);

  useEffect(() => {
    console.debug("subdivid in defaultmap", subdivisionId);
    if (subdivisionId) {
      void getBoundaryQuery(subdivisionId, true);
    }
  }, [subdivisionId, getBoundaryQuery]);

  useEffect(() => {
    if (!result.isFetching && result.isSuccess) {
      console.debug("result in defaultmap", result);
      setCoords(result.data.coordinates);
    }
  }, [result]);

  return (
    <AzureMapsProvider>
      <div style={{ height: "400px" }}>
        <AzureMap options={option}>
          <AzureMapDataSourceProvider
            id={"polygonExample AzureMapDataSourceProvider"}
            options={{}}
          >
            <AzureMapLayerProvider
              id={"polygonExample LayerProvider"}
              options={{
                fillOpacity: 0.5,
                fillColor: "#ff0000",
              }}
              type={"PolygonLayer"}
            />
            <AzureMapFeature
              id={"polygonExample MapFeature"}
              type="Polygon"
              coordinates={coords}
            />
          </AzureMapDataSourceProvider>
        </AzureMap>
      </div>
    </AzureMapsProvider>
  );
};

export default DefaultMap;
