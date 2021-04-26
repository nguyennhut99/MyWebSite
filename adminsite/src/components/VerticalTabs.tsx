import { useHistory } from 'react-router-dom';
import '../App.css'

const VerticalTabs = () => {

    const history = useHistory();

    return (
        <div className="col-sm-2 col-md-2 col-lg-2 col-xl-2 tab">
            <button className="tablinks" onClick={()=> history.push("/")} >Product Management</button>
            <button className="tablinks" onClick={()=> history.push("/category")}>Category Management</button>
            <button className="tablinks" >Tokyo</button>
        </div>

    )
};

export default VerticalTabs;