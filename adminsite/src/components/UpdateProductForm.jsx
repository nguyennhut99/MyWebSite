import { useState, useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { useHistory } from 'react-router-dom';
import { selectBrandList, get_Brand_List, selectProduct, get_product, update_product, getProduct } from "../store/Product-slice";
import { get_Categories, selectCategories } from "../store/category-slice";

const AddProductForm = () => {
    const history = useHistory();

    const categories = useSelector(selectCategories);
    const brands = useSelector(selectBrandList);
    const productOld = useSelector(selectProduct);

    const dispatch = useDispatch();
    useEffect(() => {
        var url = new URL(window.location.href);
        dispatch(get_product(url.searchParams.get("id")))
        dispatch(get_Categories());
        dispatch(get_Brand_List());
    }, [dispatch]);

    const [product, setProduct] = useState({
        productId: 0,
        productName: "",
        Price: 0,
        description: "",
        categoryId: 0,
        brandId: 0
    });

    useEffect(() => {
        try {
            if (product.productId != productOld.id || product.categoryId == 0 || product.brandId == 0) {
                setProduct({
                    ...product,
                    ["productId"]: productOld.id,
                    ["brandId"]: brands[0].id,
                    ["categoryId"]: categories[0].id,
                    ["productName"]: productOld.name,
                    ["Price"]: productOld.price,
                    ["description"]: productOld.description,
                });
            }

        } catch (error) {

        }
    });

    const [formData, setFormData] = useState(new FormData());

    const handleChangeFileImages = (e) => {
        const formData = new FormData();
        if (e.target.files) {
            let file = e.target.files[0];
            formData.append('ThumbnailImageUrl', file);
        }
        setFormData(formData)
    }

    function handleChange(evt) {
        const value = evt.target.value;
        setProduct({
            ...product,
            [evt.target.name]: value
        });
    }

    const handleSubmit = async (e) => {
        e.preventDefault();
        const formDataSubmit = formData;
        formDataSubmit.append('Name', product.productName);
        formDataSubmit.append('Description', product.description);
        formDataSubmit.append('Price', product.Price);
        formDataSubmit.append('CategoryId', product.categoryId);
        formDataSubmit.append('BrandId', product.brandId);
        dispatch(update_product(product.productId, formDataSubmit))
        history.push("/")
    };

    return (
        <div>
            <form onSubmit={handleSubmit}>
                <div className="form-group">
                    <label>Product Id</label>
                    <input type="text" className="form-control" name="productId" value={product.productId} onChange={handleChange} readonly disabled />
                </div>
                <div className="form-group">
                    <label>Product Name</label>
                    <input type="text" className="form-control" name="productName" value={product.productName} onChange={handleChange} required />
                </div>
                <div className="form-group">
                    <label>Price</label>
                    <input type="number" className="form-control" name="Price" value={product.Price} onChange={handleChange} />
                </div>
                <div className="form-group" >
                    <label>Description</label>
                    <textarea className="form-control" onChange={handleChange} value={product.description} name="description" rows="10" placeholder="Please type description of product"></textarea>
                </div>
                <div>
                    <label>Category </label>
                    <select className="form-control" name="categoryId" onChange={handleChange} required>
                        {
                            categories.map(item => {
                                return (
                                    <option value={item.id}>{item.name}</option>
                                )
                            })
                        }
                    </select>
                </div>
                <div>
                    <label>Brand</label>
                    <select className="form-control" name="brandId" onChange={handleChange} required>
                        {
                            brands.map(item => {
                                return (
                                    <option value={item.id}>{item.name}</option>
                                )
                            })
                        }
                    </select>
                </div>
                <div className="form-group">
                    <label>Image</label>
                    <input type='file' multiple className="form-control" onChange={handleChangeFileImages} />
                </div>
                <button type="submit" className="btn btn-primary">Update</button>
            </form>
        </div>
    );
};

export default AddProductForm;