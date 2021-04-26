import { useDispatch, useSelector } from "react-redux";
import { useState, useEffect } from "react";
import { useHistory } from 'react-router-dom';
import { get_Categories, selectCategories, add_category, delete_Category, update_product } from "../store/category-slice";

const CategoryTable = () => {

  const history = useHistory();

  const CategoryList = useSelector(selectCategories);

  const [category, setCategory] = useState({
    categoryName: "",
  });

  const dispatch = useDispatch();
  useEffect(() => {
    dispatch(get_Categories());
  }, [dispatch]);

  function handleChange(evt) {
    const value = evt.target.value;
    setCategory({
      ...category,
      [evt.target.name]: value
    });
  }

  const handleSubmit = async (e) => {
    e.preventDefault();
    const formDataSubmit = new FormData();;
    formDataSubmit.append('Name', category.categoryName);
    dispatch(add_category(formDataSubmit))
  };

  return (
    <div>
      <form onSubmit={handleSubmit}>
        <div className="form-group">
          <label>Category Name</label>
          <input type="text" className="form-control" name="categoryName" value={category.categoryName} onChange={handleChange} required />
        </div>
        <button type="submit" className='btn btn-primary'>ADD Category</button>
      </form>

      <div className="table-item">
        <table className="table mt-4 ">
          <tr>
            <th>Id</th>
            <th>Name</th>
            <th></th>
          </tr>
          {CategoryList.map((cate) => (
            <tr>
              <td>{cate.id}</td>
              <td>{cate.name}</td>
              <td>
                <button className="btn btn-success" onClick={() => history.push('/update-category?id=' + cate.id)} >Cập nhật</button>

                <button className="btn btn-danger" onClick={() => dispatch(delete_Category(cate.id))} > Xóa</button>
              </td>
            </tr>
          ))}

        </table>
      </div>
    </div >
  );
};

export default CategoryTable;