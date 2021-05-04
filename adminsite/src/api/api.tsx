import axios from "axios";
axios.defaults.baseURL = "https://localhost:44358";

const config = {
    headers: { Authorization: localStorage.getItem("__token") }
};

const Product = {
    getAllProducts: async () => await axios.get("/api/Product"),
    AddProduct: async (Content: any) => await axios.post("/api/Product", Content, config),
    deleteProduct: async (id: number) => await axios.delete(`/api/Product/${id}`, config),
    getProduct: async (id: number) => await axios.get(`/api/Product/${id}`),
    updateProduct: async (id: number, Content: any) => await axios.put(`/api/Product/${id}`, Content, config),
}

const Category = {
    getAllCategory: async () => await axios.get("/api/Category"),
    addCategory: async (Content: any) => await axios.post("/api/Category", Content, config),
    deleteCategory: async (id: number) => await axios.delete(`/api/Category/${id}`, config),
    updateCategory: async (id: number, Content: any) => await axios.put(`/api/Category/${id}`, Content, config),
    getCategory: async (id: number) => await axios.get(`/api/Category/${id}`),
}

const Brand = {
    getAllBrand: async () => await axios.get("/api/Brand"),
}

const User = {
    getAllUser: async () => await axios.get("/api/User", config),
    getOders: async (id: string) => await axios.get(`/api/User/${id}`, config),
    getOderDetail: async (id: number) => await axios.get(`/api/User/Order/${id}`, config),
}

export default { Product, Category, Brand, User };