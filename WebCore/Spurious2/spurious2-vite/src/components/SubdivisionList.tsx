import useTheme from "@mui/system/useTheme";
import useMediaQuery from "@mui/material/useMediaQuery";
import SubdivisionListMobile from "./SubdivisionListMobile";
import SubdivisionListDesktop from "./SubdivisionListDesktop";

export interface SubdivisionListProps {
  onSubdivisionChange: (subdivisionId: number) => void;
}

const SubdivisionList = ({ onSubdivisionChange }: SubdivisionListProps) => {
  const theme = useTheme();
  const isSmScreen = useMediaQuery(theme.breakpoints.down("sm"));

  return isSmScreen ? (
    <SubdivisionListMobile onSubdivisionChange={onSubdivisionChange} />
  ) : (
    <SubdivisionListDesktop onSubdivisionChange={onSubdivisionChange} />
  );
};

export default SubdivisionList;
