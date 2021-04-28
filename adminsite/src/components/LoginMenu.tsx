import React from "react";
import { useSelector } from "react-redux";
import { Link } from "react-router-dom";
import { selectUser } from "../store/auth-slice";

interface IProps {
  isAuthenticated: boolean;
  userName?: string;
}


const LoginMenu = () => {
  const userName = useSelector(selectUser)?.name;
  const check = localStorage.getItem("__token")
  if (check !="") {
    return (
      <ul className="navbar-nav">
        <li className="nav-item">
          <Link className="nav-link text-dark" to="/profile">
            Hello {userName}
          </Link>
        </li>
        <li className="nav-item">
          <Link to="/authentication/logout" className="nav-link text-dark">
            Logout
          </Link>
        </li>
      </ul>
    );
  }

  return (
    <ul className="navbar-nav">
      <li className="nav-item">
        <a className="nav-link text-dark" href="/">
          Register
        </a>
      </li>
      <li className="nav-item">
        <Link to="/authentication/login" className="nav-link text-dark">
          Login
        </Link>
      </li>
    </ul>
  );
};

export default LoginMenu;