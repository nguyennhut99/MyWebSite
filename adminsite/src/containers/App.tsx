import React from "react";
import { BrowserRouter, Switch, Route, Link } from "react-router-dom";
import Home from "../components/Home";
import About from "../components/About";
import NavMenu from "./NavMenu";
import Auth from "./Auth";
import VerticalTabs from "../components/VerticalTabs"
import ProductTable from '../components/ProductTable'
import 'bootstrap/dist/css/bootstrap.min.css';
import AddProductForm from "../components/AddProductForm";

const App = () => {
  return (
    <BrowserRouter basename={"/"}>
      <NavMenu />
      <div className="row">
        <VerticalTabs />
        <div className="col-sm-10 col-md-10 col-lg-10 col-xl-10">
          <Switch>
            <Route path="/add-product" component={AddProductForm} />
            <Route path="/authentication" component={Auth} />
            <Route path="/about" component={About} />
            <Route path="/" component={ProductTable} />
          </Switch>
        </div>
      </div>
    </BrowserRouter >
  );
};

export default App;