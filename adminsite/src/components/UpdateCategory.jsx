import { useDispatch, useSelector } from "react-redux";
import { useState, useEffect } from "react";
import { useHistory } from 'react-router-dom';
import { selectCategory, get_Category, update_category } from "../store/category-slice";

const UpdateCategory = () => {

  const history = useHistory();

  const categoryOld = useSelector(selectCategory);

  const [category, setCategory] = useState({
    id: 0,
    name: "",
  });

  const dispatch = useDispatch();
  useEffect(() => {
    var url = new URL(window.location.href);
    dispatch(get_Category(url.searchParams.get("id")))
  }, [dispatch]);

  useEffect(() => {
    try {
      if (category.id != categoryOld.id) {
        setCategory({
          ...category,
          id: categoryOld.id,
          name: categoryOld.name
        });
      }
    } catch (error) {

    }
  });

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
    formDataSubmit.append('Name', category.name);
    dispatch(update_category(category.id, formDataSubmit))
    history.push("/category")
  };

  return (
    <div>
      <form onSubmit={handleSubmit}>
        <div className="form-group">
          <label>Category Id</label>
          <input type="text" className="form-control" name="id" value={category.id} readonly disabled />
        </div>
        <div className="form-group">
          <label>Category Name</label>
          <input type="text" className="form-control" name="name" value={category.name} onChange={handleChange} required />
        </div>
        <button type="submit" className='btn btn-primary'>Update Category</button>
      </form>
    </div >
  );
};

export default UpdateCategory;