import React from "react";
import NavBar from "./NavBar";
import Container from "@mui/material/Container";

const About = () => {
  return (
    <>
      <NavBar />
      <Container>
        <div>
          <h1>Alcohol Density per Census Subdivision</h1>
        </div>
      </Container>
      <div className="App">
        <Container sx={{ marginY: 5 }}>About</Container>
      </div>
    </>
  );
};

export default About;
