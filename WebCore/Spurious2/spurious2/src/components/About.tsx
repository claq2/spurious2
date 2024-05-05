import React from "react";
import Container from "@mui/material/Container";

const About = () => {
  return (
    <>
      <div className="App">
        <Container sx={{ marginY: 5 }}>
          <h3>About</h3>
          <br />
          What does the amount of alcohol in a census subdivision mean? Nothing.
          But it's fun to map it out like it does. (For instance, there might be
          an LCBO at the border of 2 areas that serve both, but the population
          of the second area won't get counted.)
          <br />
          <br />
          Population and census subdivision data comes from Statistics Canada's
          2021 census data.
          <br />
          <br />
          Alcohol inventory comes from a custom LCBO website scraper based on{" "}
          <a href="https://github.com/heycarsten/lcbo-api">
            Carsten Nielson's LCBO API
          </a>
          . It's a series of Azure Functions. Note: this website and the scraped
          data are not used for commercial purposes. It is just a pet learning
          project.
          <br />
          <br />
          Azure SQL Server provides data storage and the geographic matching of
          LCBO locations inside subdivisions.
          <br />
          <br />
          The backend is ASP.NET Core. The front end is React written in
          TypeScript. The map is by{" "}
          <a href="https://azure.microsoft.com/en-us/services/azure-maps/">
            Azure Maps
          </a>
          . Material UI provides the good looks and data grid. It all lives in
          Azure.
          <br />
          <br />
          <a href="https://github.com/claq2/spurious2">Fork it on GitHub.</a>
          <br />
          <br />
        </Container>
      </div>
    </>
  );
};

export default About;
