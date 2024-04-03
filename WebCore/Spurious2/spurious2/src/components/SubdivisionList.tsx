import * as React from "react";
import Table from "@mui/material/Table";
import TableBody from "@mui/material/TableBody";
import TableCell from "@mui/material/TableCell";
import TableContainer from "@mui/material/TableContainer";
import TableHead from "@mui/material/TableHead";
import TableRow from "@mui/material/TableRow";
import Paper from "@mui/material/Paper";

function createData(name: string, population: number, density: number) {
  return { name, population, density };
}

const rows = [
  createData("Peele", 159, 6.12),
  createData("Pickle Lake", 237, 9.81),
  createData("White River", 262, 16.64),
  createData("Assiginack", 305, 3.75),
  createData("The Archipelago", 356, 16.4),
  createData("Killarney", Math.floor(Math.random() * 100), 16.0),
  createData("Westport", Math.floor(Math.random() * 100), 16.0),
  createData("Gore Bay", Math.floor(Math.random() * 100), 16.0),
  createData("Ear Falls", Math.floor(Math.random() * 100), 16.0),
  createData("James", Math.floor(Math.random() * 100), 16.0),
];

const SubdivisionList = () => {
  return (
    <TableContainer component={Paper}>
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
    </TableContainer>
  );
};

export default SubdivisionList;
