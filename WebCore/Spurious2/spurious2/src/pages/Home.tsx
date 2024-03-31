import Container from "@mui/material/Container";
import { useParams } from "react-router-dom";

export interface HomeProps {
  id: string;
}

const Home = () => {
  const { id } = useParams();
  return (
    <div className="App">
      <Container sx={{ marginY: 5 }}>Hi {id}</Container>
    </div>
  );
};

export default Home;
