import * as React from "react";
import {
  DataGrid,
  GridColDef,
  GridRowParams,
  gridClasses,
} from "@mui/x-data-grid";
import { Box } from "@mui/material";
import { useParams, useRouteLoaderData } from "react-router-dom";
import { useGetSubdivisionsByDensityQuery } from "../services/subdivisions";
import { useEffect, useState } from "react";
import { Density, Subdivision } from "../services/types";

interface SubdivisionListProps {
  onSubdivisionChange: (subdivisionId: number) => void;
}

const SubdivisionList = ({ onSubdivisionChange }: SubdivisionListProps) => {
  const result: Density[] = useRouteLoaderData("root") as Density[];
  const { id } = useParams();
  // Skip if the id in the route isn't one of the known densities
  const { data, isLoading, isFetching, isSuccess, isError } =
    useGetSubdivisionsByDensityQuery(id as string, {
      skip: !!!result.find(
        (r) => r.shortName.toLowerCase() === id?.toLowerCase()
      ),
    });
  const [tableData, setTableData] = useState<Subdivision[]>([]);
  const [selection, setSelection] = useState<any | undefined>(undefined);

  const rowClick = (params: GridRowParams) => {
    console.debug("rowClick", params);
    setSelection(params.row.id);
    onSubdivisionChange(params.row.id);
  };

  const columns: GridColDef<Subdivision[][number]>[] = [
    {
      field: "name",
      headerName: "Subdivision Name",
      // width: 150,
      type: "string",
      // editable: true,
      flex: 0.5,
    },
    {
      field: "population",
      headerName: "Population",
      type: "number",
      // width: 150,
      // editable: true,
      flex: 1,
    },
    {
      field: "requestedDensityAmount",
      headerName: "Density (L/person)",
      type: "number",
      // width: 110,
      // editable: true,
      flex: 1,
    },
  ];

  useEffect(() => {
    // Set selection to undefined when id changes so that it gets set when data changes because of id change
    console.debug("id in subdivlist", id);
    setSelection(undefined);
  }, [id]);

  useEffect(() => {
    if (!isLoading && !isFetching && data && isSuccess) {
      setTableData(data);
      if (selection === undefined) {
        setSelection(data[0].id);
        onSubdivisionChange(data[0].id);
      }
    }
  }, [
    data,
    isError,
    isSuccess,
    isLoading,
    isFetching,
    onSubdivisionChange,
    selection,
  ]);

  return (
    <>
      <Box sx={{ width: "100%" }}>
        <DataGrid
          autoHeight
          loading={isLoading}
          onRowClick={rowClick}
          rows={tableData}
          rowSelectionModel={selection}
          columns={columns}
          sx={{
            [`& .${gridClasses.cell}:focus, & .${gridClasses.cell}:focus-within`]:
              {
                outline: "none",
              },
            [`& .${gridClasses.columnHeader}:focus, & .${gridClasses.columnHeader}:focus-within`]:
              {
                outline: "none",
              },
          }}
          initialState={{
            pagination: {
              paginationModel: {
                pageSize: 10,
              },
            },
          }}
          pageSizeOptions={[10]}
        />
      </Box>
    </>
  );
};

export default SubdivisionList;
