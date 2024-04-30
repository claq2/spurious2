import Container from "@mui/material/Container";
import { useParams } from "react-router-dom";
import { useMemo, useState } from "react";
import DefaultMap from "../components/DefaultMap";
import SubdivisionList from "../components/SubdivisionList";
import MapWrapper from "../components/MapWrapper";

export interface HomeProps {
  id: string;
}

const Home = () => {
  const { id } = useParams();
  useMemo(() => {
    console.log("id in home", id);
  }, [id]);

  const [selectedSubdivisionId, setSelectedSubdivisionId] = useState<
    number | undefined
  >(undefined);

  const onSubdivisionChange = (subdivisionId: number) => {
    setSelectedSubdivisionId(subdivisionId);
  };

  return (
    <>
      {/* <div className="App">
        <Container sx={{ marginY: 5 }}>Hi {id}</Container>
      </div> */}
      <Container>
        {/* <DefaultMap subdivisionId={selectedSubdivisionId} /> */}
        <MapWrapper subdivisionId={selectedSubdivisionId} />
        <SubdivisionList onSubdivisionChange={onSubdivisionChange} />
      </Container>
    </>
  );
};

export default Home;
