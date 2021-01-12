import { makeStyles } from '@material-ui/core/styles';
import { useTheme } from '@material-ui/core/styles';
import AppBar from '@material-ui/core/AppBar';
import Toolbar from '@material-ui/core/Toolbar';
import Typography from '@material-ui/core/Typography';
import Button from '@material-ui/core/Button';
import IconButton from '@material-ui/core/IconButton';
import MenuIcon from '@material-ui/icons/Menu';
import React from "react";
import AccountCircle from '@material-ui/icons/AccountCircle';
import MenuItem from '@material-ui/core/MenuItem';
import Menu from '@material-ui/core/Menu';
import useMediaQuery from '@material-ui/core/useMediaQuery';
import AppMobileMenu from './Components/AppMobileMenu';
import {
  Route,
  NavLink,
  HashRouter,
  Redirect,
  Link
} from "react-router-dom";
import Home from "./Pages/Home";

const useStyles = makeStyles((theme) => ({
  root: {
    flexGrow: 1,
  },
  menuButton: {
    marginRight: theme.spacing(2),
  },
  title: {
    flexGrow: 1,
  },
  edgeButton: {
    marginLeft: "200px"
  }
}));

function App() {
  const classes = useStyles();
  const [auth, setAuth] = React.useState(false);
  const [anchorEl, setAnchorEl] = React.useState(null);
  const open = Boolean(anchorEl);

  const handleChange = (event) => {
    setAuth(!auth);
  };

  const handleMenu = (event) => {
    setAnchorEl(event.currentTarget);
  };

  const handleClose = () => {
    setAnchorEl(null);
  };

  const theme = useTheme();
  const onMobile = useMediaQuery(theme.breakpoints.down('sm'))

  return (
    <HashRouter>
    <div className="App">
      <header className="App-header">
      <AppBar position="static">
        <Toolbar>
          {onMobile && 
          <AppMobileMenu />
          }
          <Typography variant="h6" className={classes.title}>
            Torneo
          </Typography>
          {!onMobile && 
          <div>
          <Button color="inherit" component={ Link } to ="/">Home</Button>
          <Button color="inherit" component={ Link } to ="/tournaments">Tournaments</Button>
          <Button color="inherit" component={ Link } to ="/teams">Teams</Button>
          <Button color="inherit" component={ Link } to ="/matches">Matches</Button></div>
          }
          {auth && (
            <div>
              <IconButton
                aria-label="account of current user"
                aria-controls="menu-appbar"
                aria-haspopup="true"
                onClick={handleMenu}
                color="inherit"
              >
                <AccountCircle />
              </IconButton>
              <Menu
                id="menu-appbar"
                anchorEl={anchorEl}
                anchorOrigin={{
                  vertical: 'top',
                  horizontal: 'right',
                }}
                keepMounted
                transformOrigin={{
                  vertical: 'top',
                  horizontal: 'right',
                }}
                open={open}
                onClose={handleClose}
              >
                <MenuItem onClick={handleClose}>Profile</MenuItem>
                <MenuItem onClick={handleChange}>Sign Out</MenuItem>
              </Menu>
            </div>
          )}
          {
            !auth && <Button color="inherit" onClick={handleChange}>Log in</Button>
          }
        </Toolbar>
      </AppBar>
      </header>
      <div className="content">
        <Route exact path="/" component={Home}></Route>
      </div>
    </div>
    </HashRouter>
  );
}

export default App;
