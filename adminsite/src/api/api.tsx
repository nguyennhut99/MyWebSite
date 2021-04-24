import axios from "axios";
axios.defaults.baseURL = "https://localhost:44358";

const config = {
    headers: { Authorization: localStorage.getItem("__token") }
};

const Product = {
    getAllProducts: async () => await axios.get("/api/Product"),
    deleteProduct: async (id: Number) => await axios.delete(`/api/Product/${id}`,config),

    // getProductbyId: async (id) => await axios.get(`/api/Product/${id}`),
    // addProduct: async (data) => await axios.post("/api/Product/addProduct", data, config),
    // updateProduct: async (data) => await axios.put("/api/Product/", data, config),
}

export default { Product };