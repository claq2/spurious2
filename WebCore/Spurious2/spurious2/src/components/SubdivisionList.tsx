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

interface Data {
  id: number;
  name: string;
  population: number;
  density: number;
}

function createData(
  id: number,
  name: string,
  population: number,
  density: number
): Data {
  return { id, name, population, density };
}

const rows: Data[] = [
  createData(10, "Peele", 159, 6.12),
  createData(2344, "Pickle Lake", 237, 9.81),
  createData(323, "White River", 262, 16.64),
  createData(44432, "Assiginack", 305, 3.75),
  createData(51, "The Archipelago", 356, 16.4),
  createData(653223, "Killarney", Math.floor(Math.random() * 100), 16.0),
  createData(37, "Westport", Math.floor(Math.random() * 100), 16.0),
  createData(81244, "Gore Bay", Math.floor(Math.random() * 100), 16.0),
  createData(91235, "Ear Falls", Math.floor(Math.random() * 100), 16.0),
  createData(1099, "James", Math.floor(Math.random() * 100), 16.0),
  createData(11, "Eleven", Math.floor(Math.random() * 100), 11.0),
];

// interface Cell {
//   cellIndex: number;
// }

const SubdivisionList = () => {
  // const tableCellClickHandler = (e: React.MouseEvent<HTMLElement>) => {
  //   console.log((e.target as Element).innerHTML);
  //   console.log("target", e.target);
  //   const x = e.target as unknown as Cell;
  //   console.log("x", x);
  //   console.log("e", e);
  // };

  const rowClick = (params: GridRowParams) => {
    console.log("rowClick", params);
  };

  const columns: GridColDef<(typeof rows)[number]>[] = [
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
      field: "density",
      headerName: "Density (L/person)",
      type: "number",
      // width: 110,
      // editable: true,
      flex: 1,
    },
  ];

  return (
    <>
      {/* <TableContainer component={Paper}>
        <Table sx={{ minWidth: 650 }} aria-label="simple table">
          <TableHead>
            <TableRow>
              <TableCell></TableCell>
              <TableCell>Subdivision&nbsp;Name</TableCell>
              <TableCell align="right">Population</TableCell>
              <TableCell align="right">Density&nbsp;(L/person)</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {rows.map((row, index) => (
              <TableRow
                onClick={tableCellClickHandler}
                key={row.name}
                sx={{ "&:last-child td, &:last-child th": { border: 0 } }}
              >
                <TableCell scope="row">{index + 1}.</TableCell>
                <TableCell>{row.name}</TableCell>
                <TableCell align="right">{row.population}</TableCell>
                <TableCell align="right">{row.density}</TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </TableContainer> */}
      <Box sx={{ width: "100%" }}>
        <DataGrid
          onRowClick={rowClick}
          rows={rows}
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
