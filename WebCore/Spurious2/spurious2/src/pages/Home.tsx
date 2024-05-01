import Container from "@mui/material/Container";
import { useParams } from "react-router-dom";
import { useMemo, useState } from "react";
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
    console.log("subdivisionId in onSubdivisionChange", subdivisionId);
    setSelectedSubdivisionId(subdivisionId);
  };

  return (
    <>
      <Container>
        <MapWrapper subdivisionId={selectedSubdivisionId} />
        <SubdivisionList onSubdivisionChange={onSubdivisionChange} />
      </Container>
    </>
  );
};

export default Home;
