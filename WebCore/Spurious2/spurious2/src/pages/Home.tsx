import Container from "@mui/material/Container";
import { useParams } from "react-router-dom";
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
      <div className="App">
        <Container sx={{ marginY: 5 }}>Hi {id}</Container>
      </div>
    </>
  );
};

export default Home;
