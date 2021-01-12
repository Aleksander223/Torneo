import React, { Component } from "react";
import Grid from '@material-ui/core/Grid';
import Paper from '@material-ui/core/Paper';
import HomeCarousel from "../Components/HomeCarousel";
import { makeStyles } from '@material-ui/core/styles';
 
const useStyles = makeStyles((theme) => ({
    root: {
      flexGrow: 1,
      margin: "40px"
    },
    paper: {
      padding: theme.spacing(2),
      textAlign: 'center',
      color: theme.palette.text.primary,
    }
  }));

export default function Home() {

    const classes = useStyles();

    return (
<div className={classes.root}>
      <Grid container spacing={3}>
      <Grid item xs={12}>
      
      <h1>Torneo</h1>
        </Grid>

        <Grid item xs={12}>
          <Paper className={classes.paper}><HomeCarousel /></Paper>
        </Grid>

      
      </Grid>
      </div>
    )
}
