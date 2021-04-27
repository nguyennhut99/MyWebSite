import { useDispatch, useSelector } from "react-redux";
import { useEffect } from "react";
import { useHistory } from 'react-router-dom';
import { selectUsers } from "../store/user-slice";
import { get_Users } from "../store/user-slice";

const UserManagement = () => {

  const history = useHistory();

  const Users = useSelector(selectUsers);

  const dispatch = useDispatch();
  useEffect(() => {
    dispatch(get_Users());
  }, [dispatch]);

  return (
    <div>
      <div className="table-item">
        <table className="table mt-4 ">
          <tr>
            <th>Id</th>
            <th>UserName</th>
            <th>Email</th>
            <th></th>
          </tr>
          {Users.map((user) => (
            <tr>
              <td>{user.id}</td>
              <td>{user.name}</td>
              <td>{user.email}</td>
              <td>
                <button className="btn btn-success" onClick={() => history.push('/user-detail?id=' + user.id)} >View</button>
              </td>
            </tr>
          ))}

        </table>
      </div>
    </div >
  );
};

export default UserManagement;