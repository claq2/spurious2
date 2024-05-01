import * as React from "react";
// import Table from "@mui/material/Table";
// import TableBody from "@mui/material/TableBody";
// import TableCell from "@mui/material/TableCell";
// import TableContainer from "@mui/material/TableContainer";
// import TableHead from "@mui/material/TableHead";
// import TableRow from "@mui/material/TableRow";
// import Paper from "@mui/material/Paper";
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

// interface Data {
//   id: number;
//   name: string;
//   population: number;
//   density: number;
// }

// function createData(
//   id: number,
//   name: string,
//   population: number,
//   density: number
// ): Data {
//   return { id, name, population, density };
// }

// const rows: Data[] = [
//   createData(10, "Peele", 159, 6.12),
//   createData(2344, "Pickle Lake", 237, 9.81),
//   createData(323, "White River", 262, 16.64),
//   createData(44432, "Assiginack", 305, 3.75),
//   createData(51, "The Archipelago", 356, 16.4),
//   createData(653223, "Killarney", Math.floor(Math.random() * 100), 16.0),
//   createData(37, "Westport", Math.floor(Math.random() * 100), 16.0),
//   createData(81244, "Gore Bay", Math.floor(Math.random() * 100), 16.0),
//   createData(91235, "Ear Falls", Math.floor(Math.random() * 100), 16.0),
//   createData(1099, "James", Math.floor(Math.random() * 100), 16.0),
//   createData(11, "Eleven", Math.floor(Math.random() * 100), 11.0),
// ];

// interface Cell {
//   cellIndex: number;
// }

interface SubdivisionListProps {
  onSubdivisionChange: (subdivisionId: number) => void;
}

const SubdivisionList = ({ onSubdivisionChange }: SubdivisionListProps) => {
  const result: Density[] = useRouteLoaderData("root") as Density[];
  const { id } = useParams();
  // Skip if the id in the route isn't one of the known densities
  const { data, isLoading, isFetching, isSuccess, isError } =
    useGetSubdivisionsByDensityQuery(id as string, {
      skip: !!!result.find((r) => r.shortName === id),
    });
  const [tableData, setTableData] = useState<Subdivision[]>([]);
  const [selection, setSelection] = useState<any | undefined>(undefined);
  // const tableCellClickHandler = (e: React.MouseEvent<HTMLElement>) => {
  //   console.log((e.target as Element).innerHTML);
  //   console.log("target", e.target);
  //   const x = e.target as unknown as Cell;
  //   console.log("x", x);
  //   console.log("e", e);
  // };

  const rowClick = (params: GridRowParams) => {
    console.log("rowClick", params);
    setSelection(params.row.id);
    onSubdivisionChange(params.row.id);
  };

  const columns: GridColDef<Subdivision[][number]>[] = [
    // { field: "id", headerName: "ID", width: 90 },
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
    console.log("id in subdivlist", id);
    setSelection(undefined);
  }, [id]);

  useEffect(() => {
    console.log("isLoading", isLoading);
    console.log("isFetching", isFetching);
    if (!isLoading && !isFetching && data && isSuccess) {
      console.log("setting table data", data);
      setTableData(data);
      if (selection === undefined) {
        console.log("selection undefined setting to first item", data[0]);
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
