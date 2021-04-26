import { BrowserRouter, Switch, Route, Link } from "react-router-dom";
import About from "../components/About";
import NavMenu from "./NavMenu";
import Auth from "./Auth";
import VerticalTabs from "../components/VerticalTabs"
import ProductTable from '../components/ProductTable'
import CategoryTable from '../components/CategoryTable'
import 'bootstrap/dist/css/bootstrap.min.css';
import AddProductForm from "../components/AddProductForm";
import UpdateProductForm from "../components/UpdateProductForm";
import UpdateCategory from "../components/UpdateCategory";

const App = () => {
  return (
    <BrowserRouter basename={"/"}>
      <NavMenu />
      <div className="row">
        <VerticalTabs />
        <div className="col-sm-10 col-md-10 col-lg-10 col-xl-10">
          <Switch>
            <Route path="/update-product" component={UpdateProductForm} />
            <Route path="/add-product" component={AddProductForm} />
            <Route path="/authentication" component={Auth} />
            <Route path="/about" component={About} />
            <Route path="/category" component={CategoryTable} />
            <Route path="/update-category" component={UpdateCategory} />
            <Route path="/" component={ProductTable} />
          </Switch>
        </div>
      </div>
    </BrowserRouter >
  );
};

export default App;