import Container from "@mui/material/Container";
import { useParams } from "react-router-dom";
import NavBar from "../components/NavBar";
import { useMemo } from "react";

export interface HomeProps {
  id: string;
}

const Home = () => {
  const { id } = useParams();
  useMemo(() => {
    console.log("id in home", id);
  }, [id]);
  return (
    <>
      <NavBar />
      <Container>
        <div>
          <h1>Alcohol Density per Census Subdivision</h1>
        </div>
      </Container>

      <div className="App">
        <Container sx={{ marginY: 5 }}>Hi {id}</Container>
      </div>
    </>
  );
};

export default Home;
