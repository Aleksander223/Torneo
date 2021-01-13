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
import { EditText } from 'react-edit-text';
import axios from "axios";
import Swal from "sweetalert2";
import { Link } from "react-router-dom";

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

const updateData = (bio) => {
    console.log(bio);
    axios.put("https://localhost:44356/users", {
      bio
    }, {
      withCredentials: true
    }).then((r) => {
        console.log(r);
    }).catch((e) => {
        
        Swal.fire({
          title: 'Error',
          text: e.response.data.detail,
          icon: 'error'
        })
    })
  }

export default function Profile(props) {

    const teamName = props.team ? props.team.name : null;
    const teamId = props.team ? props.team.id : 0;

  const [state, setState] = React.useState({
    userName: props.userName,
    email: props.email,
    bio: props.bio,
    team: teamName,
    teamId: teamId
});

const classes = useStyles();

  return (
    <Container component="main" maxWidth="xs">
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
          <EditText defaultValue={state.bio} onSave={(e) => updateData(e.value)} 
          onChange={(e) => setState({userName: state.userName, email: state.email, bio: e})} />
        </Typography>
        <Typography variant="body2" color="textSecondary" component="p" style={{wordWrap: "break-word"}}>
            Team: <Link to={"/teams/" + state.teamId}>{state.team}</Link>
        </Typography>
      </CardContent>
    </Card>
      </div>
    </Container>
  );
}