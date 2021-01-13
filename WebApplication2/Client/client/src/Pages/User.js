import React from 'react';
import CssBaseline from '@material-ui/core/CssBaseline';
import Typography from '@material-ui/core/Typography';
import { makeStyles } from '@material-ui/core/styles';
import Container from '@material-ui/core/Container';
import Card from '@material-ui/core/Card';
import CardHeader from '@material-ui/core/CardHeader';
import CardContent from '@material-ui/core/CardContent';
import Avatar from '@material-ui/core/Avatar';
import { red } from '@material-ui/core/colors';
import axios from "axios";
import Swal from "sweetalert2";
import {
    Link
} from "react-router-dom";

const useStyles = makeStyles((theme) => ({
  paper: {
    marginTop: theme.spacing(8),
    display: 'flex',
    flexDirection: 'column',
    alignItems: 'center',
  },
  submit: {
    margin: theme.spacing(3, 0, 2),
  },
  media: {
    height: 0,
    paddingTop: '56.25%', // 16:9
  },
  avatar: {
    backgroundColor: red[500],
  },
  card: {
      padding: "10px",
      minWidth: "200px",
      maxWidth: "500px"
  }
}));

export default function User(props) {

  const [state, setState] = React.useState({
    userName: "te",
    email: "T",
    bio: "aaaaaaaaaaaa",
    team: null,
    teamId: 0
});

const [calledGet, setCalledGet] = React.useState(false);

const classes = useStyles();
const getData = (id) => {

    axios.get("https://localhost:44356/users/" + id, null, {
      withCredentials: true
    }).then((res) => {
        
        if (res.data.team != null) {
            setState({
                userName: res.data.userName,
                email: res.data.email,
                bio: res.data.bio,
                team: res.data.team.name,
                teamId: res.data.teamId
          });
        } else {
            setState({
                userName: res.data.userName,
                email: res.data.email,
                bio: res.data.bio
          })
        }
      
    }).catch((e) => {
        Swal.fire({
          title: 'Error',
          icon: 'error'
        })
    })
}

    if (!calledGet) {
        setCalledGet(true);
        getData(props.match.params.id);
    }

  return (
    <Container component="main" maxWidth="md">
      <CssBaseline />
      <div className={classes.paper}>
      <Card className={classes.card}>
      <CardHeader
        avatar={
          <Avatar aria-label="recipe" className={classes.avatar}>
            {state.userName[0]}
          </Avatar>
        }
        title={state.userName}
        subheader={state.email}
      />
      <CardContent>
        <Typography variant="body2" component="p" style={{wordWrap: "break-word"}}>
            
        </Typography>
        <Typography variant="body2" color="textSecondary" component="p" style={{wordWrap: "break-word"}}>
            {state.bio}
            <hr></hr>
            Team: <Link to={"/teams/" + state.teamId}>{state.team}</Link>
        </Typography>
      </CardContent>
    </Card>
      </div>
    </Container>
  );
}