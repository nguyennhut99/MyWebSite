import { useDispatch, useSelector } from "react-redux";
import { useEffect } from "react";
import { useHistory } from 'react-router-dom';
import { get_Orders, selectOrders } from "../store/user-slice";


const UserDetail = () => {

  const history = useHistory();

  const UserOrder = useSelector(selectOrders);

  const dispatch = useDispatch();
  useEffect(() => {
    var url = new URL(window.location.href);
    dispatch(get_Orders(url.searchParams.get("id")))
  }, [dispatch]);

  return (
    <div>
      <div className="table-item">
        <table className="table mt-4 ">
          <tr>
            <th>Id</th>
            <th>Order Date</th>
            <th>Total Due</th>
            <th></th>
          </tr>
          {UserOrder.map((order) => (
            <tr>
              <td>{order.id}</td>
              <td>{order.orderDate}</td>
              <td>{order.totalDue} VND</td>
              <td>
                <button className="btn btn-success" onClick={() => history.push('/order-detail?id=' + order.id)} >View</button>
              </td>
            </tr>
          ))}

        </table>
      </div>
    </div >
  );
};

export default UserDetail;