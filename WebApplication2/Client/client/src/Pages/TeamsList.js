import React from 'react';
import AppBar from '@material-ui/core/AppBar';
import Button from '@material-ui/core/Button';
import CameraIcon from '@material-ui/icons/PhotoCamera';
import Card from '@material-ui/core/Card';
import CardActions from '@material-ui/core/CardActions';
import CardContent from '@material-ui/core/CardContent';
import CardMedia from '@material-ui/core/CardMedia';
import CssBaseline from '@material-ui/core/CssBaseline';
import Grid from '@material-ui/core/Grid';
import Toolbar from '@material-ui/core/Toolbar';
import Typography from '@material-ui/core/Typography';
import { makeStyles } from '@material-ui/core/styles';
import Container from '@material-ui/core/Container';
import {Link} from "react-router-dom";
import axios from 'axios';
import Swal from "sweetalert2";
import AddCircleIcon from '@material-ui/icons/AddCircle';

const useStyles = makeStyles((theme) => ({
  icon: {
    marginRight: theme.spacing(2),
  },
  heroContent: {
    backgroundColor: theme.palette.background.paper,
    padding: theme.spacing(8, 0, 6),
  },
  heroButtons: {
    marginTop: theme.spacing(4),
  },
  cardGrid: {
    paddingTop: theme.spacing(8),
    paddingBottom: theme.spacing(8),
  },
  card: {
    height: '100%',
    display: 'flex',
    flexDirection: 'column',
  },
  cardMedia: {
    paddingTop: '56.25%', // 16:9
  },
  cardContent: {
    flexGrow: 1,
  },
  footer: {
    backgroundColor: theme.palette.background.paper,
    padding: theme.spacing(6),
  },
}));

export default function TeamsList(props) {
  const classes = useStyles();

  const [users, setUsers] = React.useState([]);
  const [calledGet, setCalledGet] = React.useState(false);

  if (!calledGet) {
      setCalledGet(true);
      axios.get("https://localhost:44356/teams/", {
          withCredentials: true
      }).then((r) => {
        setUsers(r.data);
    }).catch((e) => {
        
        Swal.fire({
          title: 'Error',
          icon: 'error'
        })
    })
  }

  return (
    <React.Fragment>
      <CssBaseline />
      <main>
        <Container className={classes.cardGrid} maxWidth="md">
          {/* End hero unit */}

        

          <Grid container spacing={4}>
            {!props.anonymous && <Grid item xs={12} sm={6} md={4}>
                <Card className={classes.card}>
                  <CardContent className={classes.cardContent} width="100%">
                  <Typography gutterBottom variant="h5" component="h2">
                      Add a team
                    </Typography>
                    <Typography>
                      Create your own team today
                    </Typography>
                  </CardContent>
                  <CardActions>
                  <Button>Add team</Button>
                  </CardActions>
                </Card>
                
              </Grid>}

            {users.map((card) => (
              <Grid item key={card} xs={12} sm={6} md={4}>
                <Card className={classes.card}>
                  <CardContent className={classes.cardContent}>
                    <Typography gutterBottom variant="h5" component="h2">
                      {card.name}
                    </Typography>
                    <Typography>
                      {card.members.map(m => m.userName).join(", ")}
                    </Typography>
                  </CardContent>
                  <CardActions>
                    <Button size="small" color="primary" onClick={(e) => {
                        e.preventDefault(); 
                        axios.post("https://localhost:44356/teams/join/" + card.id, null, {
                            withCredentials: true
                        });
                    }}>
                      Join
                    </Button>
                  </CardActions>
                </Card>
              </Grid>
            ))}
          </Grid>
        </Container>
      </main>
    </React.Fragment>
  );
}