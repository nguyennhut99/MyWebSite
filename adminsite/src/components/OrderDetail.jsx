import { useDispatch, useSelector } from "react-redux";
import { useEffect } from "react";
import { useHistory } from 'react-router-dom';
import { get_OrderDetail, selectOrdeDetail } from "../store/user-slice";


const OrderDetail = () => {

  const history = useHistory();

  const OrderDetail = useSelector(selectOrdeDetail);

  const dispatch = useDispatch();
  useEffect(() => {
    var url = new URL(window.location.href);
    dispatch(get_OrderDetail(url.searchParams.get("id")))
  }, [dispatch]);

  return (
    <div>
      <div className="table-item">
        <table className="table mt-4 ">
          <tr>
            <th>Product Id</th>
            <th>Product Name</th>
            <th>Quantity</th>
            <th>Unit Price</th>
            <th>Total</th>
            <th></th>
          </tr>
          {OrderDetail.map((order) => (
            <tr>
              <td>{order.productId}</td>
              <td>{order.productName}</td>
              <td>{order.orderQty}</td>
              <td>{order.unitPrice}</td>
              <td>{order.total} VND</td>  
            </tr>
          ))}

        </table>
      </div>
    </div >
  );
};

export default OrderDetail;