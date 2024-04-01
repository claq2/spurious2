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

const pages = [
  {
    id: "top10overall",
    name: "Top 10 Overall",
  },
  { id: "top10beer", name: "Top 10 Beer" },
  { id: "top10wine", name: "Top 10 Wine" },
  { id: "top10spirits", name: "Top 10 Spirits" },
  { id: "bottom10overall", name: "Bottom 10 Overall" },
];

const NavBar = () => {
  const [anchorElNav, setAnchorElNav] = React.useState<null | HTMLElement>(
    null
  );

  const handleOpenNavMenu = (event: React.MouseEvent<HTMLElement>) => {
    setAnchorElNav(event.currentTarget);
  };

  const handleCloseNavMenu = () => {
    setAnchorElNav(null);
  };

  return (
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
              {pages.map((page) => (
                <MenuItem
                  key={page.id}
                  component={RouterLink}
                  to={`/${page.id}`}
                  onClick={handleCloseNavMenu}
                >
                  <Typography textAlign="center">{page.name}</Typography>
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
            {pages.map((page) => (
              // <>
              <Link
                sx={{
                  my: 2,
                  color: "white",
                  display: "block",
                  alignContent: "center",
                }}
                component={RouterLink}
                to={`/${page.id}`}
                key={page.id}
              >
                {page.name}
              </Link>

              // </>
            ))}
            <Link
              sx={{
                my: 2,
                color: "white",
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

          {/* <Box sx={{ flexGrow: 0 }}>
            <Tooltip title="Open settings">
              <IconButton onClick={handleOpenUserMenu} sx={{ p: 0 }}>
                <Avatar alt="Remy Sharp" src="/static/images/avatar/2.jpg" />
              </IconButton>
            </Tooltip>
            <Menu
              sx={{ mt: "45px" }}
              id="menu-appbar"
              anchorEl={anchorElUser}
              anchorOrigin={{
                vertical: "top",
                horizontal: "right",
              }}
              keepMounted
              transformOrigin={{
                vertical: "top",
                horizontal: "right",
              }}
              open={Boolean(anchorElUser)}
              onClose={handleCloseUserMenu}
            >
              {settings.map((setting) => (
                <MenuItem key={setting} onClick={handleCloseUserMenu}>
                  <Typography textAlign="center">{setting}</Typography>
                </MenuItem>
              ))}
            </Menu>
          </Box> */}
        </Toolbar>
      </Container>
    </AppBar>
  );
};
export default NavBar;
