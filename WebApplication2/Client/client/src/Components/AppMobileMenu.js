import React from 'react';
import Button from '@material-ui/core/Button';
import Menu from '@material-ui/core/Menu';
import MenuItem from '@material-ui/core/MenuItem';
import { makeStyles } from '@material-ui/core/styles';
import IconButton from '@material-ui/core/IconButton';
import MenuIcon from '@material-ui/icons/Menu';
import {
  Link
} from "react-router-dom";

export default function AppMobileMenu() {
    const useStyles = makeStyles((theme) => ({
        root: {
          flexGrow: 1,
        },
        menuButton: {
          marginRight: theme.spacing(2),
        },
      }));

      const classes = useStyles();

  const [anchorEl, setAnchorEl] = React.useState(null);

  const handleClick = (event) => {
    setAnchorEl(event.currentTarget);
  };

  const handleClose = () => {
    setAnchorEl(null);
  };

  return (
    <div>
      <IconButton edge="start" className={classes.menuButton} color="inherit" aria-label="menu" aria-haspopup="true" onClick={handleClick}>
            <MenuIcon />
          </IconButton>
      <Menu
        id="simple-menu"
        anchorEl={anchorEl}
        keepMounted
        open={Boolean(anchorEl)}
        onClose={handleClose}
      >
        <MenuItem component={ Link } to ="/" onClick={handleClose}>Home</MenuItem>
        <MenuItem component={ Link } to ="/users" onClick={handleClose}>Users</MenuItem>
        <MenuItem component={ Link } to ="/games" onClick={handleClose}>Games</MenuItem>
        <MenuItem component={ Link } to ="/tournaments" onClick={handleClose}>Tournaments</MenuItem>
        <MenuItem component={ Link } to ="/teams" onClick={handleClose}>Teams</MenuItem>
        <MenuItem component={ Link } to ="/matches" onClick={handleClose}>Matches</MenuItem>
      </Menu>
    </div>
  );
}