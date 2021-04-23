import React from "react";
import { BrowserRouter, Switch, Route, Link } from "react-router-dom";
import Home from "../components/Home";
import About from "../components/About";
import NavMenu from "./NavMenu";
import Auth from "./Auth";
import ProductTable from '../components/ProductTable'
import 'bootstrap/dist/css/bootstrap.min.css';

const App = () => {
  return (
    <BrowserRouter basename={"/"}>
      <NavMenu />
      <div className="row">
        <Switch>
          <Route path="/authentication" component={Auth} />
          <Route path="/about" component={About} />
          <Route path="/" component={ProductTable} />
        </Switch>
      </div>
    </BrowserRouter >
  );
};

export default App;