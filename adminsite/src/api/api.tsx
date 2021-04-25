import axios from "axios";
axios.defaults.baseURL = "https://localhost:44358";

const config = {
    headers: { Authorization: localStorage.getItem("__token") }
};

const Product = {
    getAllProducts: async () => await axios.get("/api/Product"),
    AddProduct: async (Content: any) => await axios.post("/api/Product",Content, config),
    deleteProduct: async (id: Number) => await axios.delete(`/api/Product/${id}`,config),

    // getProductbyId: async (id) => await axios.get(`/api/Product/${id}`),
    // addProduct: async (data) => await axios.post("/api/Product/addProduct", data, config),
    // updateProduct: async (data) => await axios.put("/api/Product/", data, config),
}

const Category = {
    getAllCategory: async () => await axios.get("/api/Category"),
    // deleteProduct: async (id: Number) => await axios.delete(`/api/Product/${id}`,config),

    // getProductbyId: async (id) => await axios.get(`/api/Product/${id}`),
    // addProduct: async (data) => await axios.post("/api/Product/addProduct", data, config),
    // updateProduct: async (data) => await axios.put("/api/Product/", data, config),
}

const Brand = {
    getAllBrand: async () => await axios.get("/api/Brand"),
    // deleteProduct: async (id: Number) => await axios.delete(`/api/Product/${id}`,config),

    // getProductbyId: async (id) => await axios.get(`/api/Product/${id}`),
    // addProduct: async (data) => await axios.post("/api/Product/addProduct", data, config),
    // updateProduct: async (data) => await axios.put("/api/Product/", data, config),
}

export default { Product, Category, Brand };