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
  BrowserRouter,
  Redirect,
  Link,
  useHistory,
  Switch
} from "react-router-dom";
import Home from "./Pages/Home";
import Login from "./Pages/Login";
import Signup from "./Pages/Signup";
import axios from "axios";
import Cookies from "universal-cookie"
import Swal from "sweetalert2";
import Profile from "./Pages/Profile";
import User from "./Pages/User"
import Users from './Pages/Users';
import UsersList from './Pages/UsersList';
import GamesList from './Pages/GamesList';
import TournamentsList from './Pages/TournamentsList';
import TeamsList from "./Pages/TeamsList";
import Teams from './Pages/Teams';
import Tournaments from './Pages/Tournaments';
import Games from './Pages/Games';
import Matches from './Pages/Matches';
import AddTeam from './Pages/AddTeam';
import Tournament from './Pages/Tournament';

const cookies = new Cookies();

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
  },
  footer: {
    marginTop: "calc(5% + 60px)",
    gutterBottom: "0"
  }
}));

function App() {
  const classes = useStyles();
  const [auth, setAuth] = React.useState(false);
  const [anchorEl, setAnchorEl] = React.useState(null);
  const [status, setStatus] = React.useState(false);
  const open = Boolean(anchorEl);

  const handleChange = (event) => {
    setAuth(false);
  };

  const handleMenu = (event) => {
    setAnchorEl(event.currentTarget);
  };

  const handleClose = () => {
    setAnchorEl(null);
  };

  const logIn = (email, password) => {
    axios.post("https://localhost:44356/login", {
      email,
      password
    }, {
      withCredentials: true
    }).then(() => {
      window.location.href="/"
    }).catch((e) => {
        Swal.fire({
          title: 'Error',
          text: e.response.data.detail,
          icon: 'error'
        })
    })
  }

  const signUp = (userName, email, password) => {
    axios.post("https://localhost:44356/users", {
      userName,
      email,
      password
    }, {
      withCredentials: true
    }).then(() => {
      window.location.href="/login"
    }).catch((e) => {
        console.log();
        Swal.fire({
          title: 'Error',
          text: e.response.data.detail,
          icon: 'error'
        })
    })
  } 

  const signOut = () => {
    axios.post("https://localhost:44356/signout", null, {
      withCredentials: true
    }).finally(() => {
      setAuth(false);
      window.location.href="/";
    })
  }

  const theme = useTheme();
  const onMobile = useMediaQuery(theme.breakpoints.down('sm'))

  const [responseData, setResponseData] = React.useState({
    userName: "User",
    email: "username@example.com",
    bio: "t",
    team: null,
    role: "Anonymous"
  });

  !status && axios.get("https://localhost:44356/users/me", {withCredentials: true}).then((res) => {
    if (res.status == 200) {
      setAuth(true);
      setStatus(true);
      setResponseData(res.data);
      console.log(res.data);
    } else {
      setAuth(false);
    }
  }).catch((e) => {
    setAuth(false);
  })

  return (
    <BrowserRouter>
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
          <Button color="inherit" component={ Link } to ="/users">Users</Button>
          <Button color="inherit" component={ Link } to ="/games">Games</Button>
          <Button color="inherit" component={ Link } to ="/tournaments">Tournaments</Button>
          <Button color="inherit" component={ Link } to ="/teams">Teams</Button>
          {responseData.role === "Admin" ? <Button color="inherit" component={ Link } to ="/matches">Matches</Button> : ""}</div>
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
                <MenuItem onClick={handleClose} component={ Link } to ="/profile">Profile</MenuItem>
                <MenuItem onClick={signOut}>Sign Out</MenuItem>
              </Menu>
            </div>
          )}
          {
            !auth && <Button color="inherit" component={ Link } to ="/login">Log in</Button>
          }
        </Toolbar>
      </AppBar>
      </header>
      <div className="content">
      <Switch>
        <Route exact path="/" component={Home}></Route>
        <Route exact path="/login" render={(props) => <Login action = {logIn} {...props} />}></Route>
        <Route exact path="/signup" render={(props) => <Signup action = {signUp} {...props} />}></Route>
        <Route exact path ="/profile" render={(props) => <Profile userName = {responseData.userName} email = {responseData.email} bio = {responseData.bio} team = {responseData.team} {...props} />}></Route>
        <Route exact path="/users/:id" render={(props) => <User {...props} />}></Route>
        <Route exact path="/users" component={responseData.role === "Admin" ? Users : UsersList} ></Route>
        <Route exact path="/games" component={responseData.role === "Admin" ? Games : GamesList}></Route>
        <Route exact path="/tournaments" component={responseData.role === "Admin" ? Tournaments : TournamentsList}></Route>
        <Route exact path="/tournaments/:id" render={(props) => <Tournament {...props} />}></Route>
        <Route exact path="/teams" render={(props) => responseData.role === "Admin" ? <Teams/> : <TeamsList {...props} anonymous={responseData.role === "Anonymous" ? true : false} />}></Route>
        <Route exact path="/teams/create" component={AddTeam} />
        <Route exact path="/matches" render={(props) => responseData.role === "Admin" ? <Matches/> : <Redirect to="/"></Redirect>}></Route>
        <Redirect to="/"></Redirect>
      </Switch>
      </div>
      {/* Footer */}
      <footer className={classes.footer}>
        <Typography variant="h6" align="center" gutterBottom>
          Proiect DAW
        </Typography>
        <Typography variant="subtitle1" align="center" color="textSecondary" component="p">
          Adam Alexandru-Vlad, Grupa 334
        </Typography>
      </footer>
      {/* End footer */}
    </div>
    </BrowserRouter>
  );
}

export default App;
