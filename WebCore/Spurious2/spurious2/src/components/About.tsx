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

      <div className="App">About</div>
    </>
  );
};

export default About;
