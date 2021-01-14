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
import List from '@material-ui/core/List';
import ListItem from '@material-ui/core/ListItem';
import ListItemIcon from '@material-ui/core/ListItemIcon';
import ListItemText from '@material-ui/core/ListItemText';

import Grid from "@material-ui/core/Grid";
import Button from "@material-ui/core/Button";

import { Bracket, RoundProps } from 'react-brackets';

const useStyles = makeStyles((theme) => ({
  paper: {
    marginTop: theme.spacing(8),
    display: 'flex',
    flexDirection: 'column',
    alignItems: 'center',
  },
  heroContent: {
    backgroundColor: theme.palette.background.paper,
    padding: theme.spacing(8, 0, 6),
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

export default function Profile(props) {

    const [state, setState] = React.useState({
        name: "",
        description: "",
        matches: []
    });
    
    const [calledGet, setCalledGet] = React.useState(false);

    const getData = (id) => {

        axios.get("https://localhost:44356/tournaments/" + id, null, {
          withCredentials: true
        }).then((res) => {
            
            setState(res.data);
            console.log(res.data);
          
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
           

          const generateRounds = (matches) => {
              if (state.matches.length == 1) {
                  const tour =  [{
                    title: 'Round one',
                    seeds: [
                      {
                        id: 1,
                        teams: [{ name: matches[0].teamMatches[0].team.name }, { name: matches[0].teamMatches[1].team.name }],
                      }
                    ],
                  }];
                    return tour;
              } else if (state.matches.length == 3) {
                const tour =  [{
                    title: 'Round one',
                    seeds: [
                      {
                        id: 1,
                        teams: [{ name: matches[0].teamMatches[0].team.name }, { name: matches[0].teamMatches[1].team.name }],
                      },
                        {
                            id: 2,
                            teams: [{ name: matches[1].teamMatches[0].team.name }, { name: matches[1].teamMatches[1].team.name }],
                          }
                    ],
                  }, {
                    id: 3,
                    teams: [{ name: "" }, { name: "" }],
                  }];
                    return tour;
              } else {
                  const tour = [{
                    title: 'Round one',
                    seeds: [
                      {
                        id: 1,
                        teams: [{ name: 'Team A' }, { name: 'Team B' }],
                      },
                      {
                        id: 2,
                        teams: [{ name: 'Team C' }, { name: 'Team D' }],
                      },
                    ],
                  },
                  {
                    title: 'Round two',
                    seeds: [
                      {
                        id: 3,
                        teams: [],
                      },
                    ],
                  },
                ];

                return tour;
              }
          }

const classes = useStyles();

  return (
    <Container component="main" maxWidth="xs">
      <CssBaseline />
      <div className={classes.heroContent}>
          <Container maxWidth="sm">
            <Typography component="h1" variant="h2" align="center" color="textPrimary" gutterBottom>
              {state.name}
            </Typography>
            <Typography variant="h5" align="center" color="textSecondary" paragraph>
              {state.description}
            </Typography>
          </Container>
        </div>

        <Bracket rounds={generateRounds(state.matches)} />;
       
    </Container>
  );
}