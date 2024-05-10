import {
  useRouteLoaderData,
  useNavigation,
  useLocation,
} from "react-router-dom";
import { Density } from "../services/types";
import { store } from "../store";
import { densityApi } from "../services/densities";
import * as React from "react";
import AppBar from "@mui/material/AppBar";
import Box from "@mui/material/Box";
import Toolbar from "@mui/material/Toolbar";
import IconButton from "@mui/material/IconButton";
import Typography from "@mui/material/Typography";
import Menu from "@mui/material/Menu";
import MenuIcon from "@mui/icons-material/Menu";
import Container from "@mui/material/Container";
import MenuItem from "@mui/material/MenuItem";
import Link from "@mui/material/Link";
import { Link as RouterLink } from "react-router-dom";

// const pages = [
//   {
//     id: "top10overall",
//     name: "Top 10 Overall",
//   },
//   { id: "top10beer", name: "Top 10 Beer" },
//   { id: "top10wine", name: "Top 10 Wine" },
//   { id: "top10spirits", name: "Top 10 Spirits" },
//   { id: "bottom10overall", name: "Bottom 10 Overall" },
// ];

const NavBar = () => {
  const result: Density[] = useRouteLoaderData("root") as Density[];
  const [anchorElNav, setAnchorElNav] = React.useState<null | HTMLElement>(
    null
  );

  const handleOpenNavMenu = (event: React.MouseEvent<HTMLElement>) => {
    setAnchorElNav(event.currentTarget);
  };

  const handleCloseNavMenu = () => {
    setAnchorElNav(null);
  };
  const navigation = useNavigation();
  const location = useLocation();

  if (navigation.state === "loading") {
    return <h1>Loading!</h1>;
  }

  return (
    <>
      <AppBar position="static">
        <Container maxWidth="xl">
          <Toolbar disableGutters>
            <Box sx={{ flexGrow: 1, display: { xs: "flex", md: "none" } }}>
              <IconButton
                size="large"
                aria-label="account of current user"
                aria-controls="menu-appbar"
                aria-haspopup="true"
                onClick={handleOpenNavMenu}
                color="inherit"
              >
                <MenuIcon />
              </IconButton>
              <Menu
                id="menu-appbar"
                anchorEl={anchorElNav}
                anchorOrigin={{
                  vertical: "bottom",
                  horizontal: "left",
                }}
                keepMounted
                transformOrigin={{
                  vertical: "top",
                  horizontal: "left",
                }}
                open={Boolean(anchorElNav)}
                onClose={handleCloseNavMenu}
                sx={{
                  display: { xs: "block", md: "none" },
                }}
              >
                {result.map((page) => (
                  <MenuItem
                    key={page.shortName}
                    component={RouterLink}
                    to={`/${page.shortName}`}
                    onClick={handleCloseNavMenu}
                  >
                    <Typography textAlign="center">{page.title}</Typography>
                  </MenuItem>
                ))}
                <MenuItem
                  key="about"
                  component={RouterLink}
                  to="/about"
                  onClick={handleCloseNavMenu}
                >
                  <Typography textAlign="center">About</Typography>
                </MenuItem>
              </Menu>
            </Box>

            <Box
              sx={{
                flexGrow: 1,
                display: { xs: "none", md: "flex" },
                "& > :not(style) ~ :not(style)": {
                  ml: 2,
                },
              }}
            >
              <Link
                variant="h5"
                sx={{ my: 2, color: "white", display: "block" }}
                component={RouterLink}
                to="/"
                key="home"
              >
                Spurious Alcohol Statistics
              </Link>
              {result.map((page) => (
                // <>
                <Link
                  sx={{
                    my: 2,
                    color:
                      location.pathname
                        .toLowerCase()
                        .endsWith(page.shortName.toLowerCase()) ||
                      (location.pathname === "/" &&
                        page.shortName.toLowerCase() ===
                          result[0].shortName.toLowerCase())
                        ? "white"
                        : "lightgray",
                    display: "block",
                    alignContent: "center",
                  }}
                  component={RouterLink}
                  to={`/${page.shortName}`}
                  key={page.shortName}
                >
                  {page.title}
                </Link>

                // </>
              ))}
              <Link
                sx={{
                  my: 2,
                  color: location.pathname.endsWith("/about")
                    ? "white"
                    : "lightgray",
                  display: "block",
                  alignContent: "center",
                }}
                component={RouterLink}
                to="/about"
                key="about"
              >
                About
              </Link>
            </Box>
          </Toolbar>
        </Container>
      </AppBar>
    </>
  );
};

export default NavBar;

export const dataLoader = async () => {
  const p = store.dispatch(densityApi.endpoints.getDensities.initiate());
  try {
    const ds = await p.unwrap();
    return ds;
  } finally {
    p.unsubscribe();
  }
};
