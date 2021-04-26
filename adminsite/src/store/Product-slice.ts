import { createSlice } from '@reduxjs/toolkit';
import api from '../api/api';
import { AppThunk, RootState } from "./store";

const initialState = {
    productList: [],
    brands: [],
    product: {}
}

export const ProductReducer = createSlice({
    name: "product",
    initialState,
    reducers: {
        getProducts: (state, { payload }) => {
            state.productList = payload.data;
        },
        getBrands: (state, { payload }) => {
            state.brands = payload.data;
        },
        getProduct: (state, { payload }) => {
            state.product = payload.data;
        },
    },
});

export const { getProducts, getBrands, getProduct } = ProductReducer.actions;


export const get_product_list = (): AppThunk => async (dispatch) => {
    try {
        const data = await (await api.Product.getAllProducts()).data;
        dispatch(getProducts({ data }));

    } catch (error) {
        console.log(error);
    }
};

export const get_product = (id: number): AppThunk => async (dispatch) => {
    try {
        const data = await (await api.Product.getProduct(id)).data;
        dispatch(getProduct({ data }));

    } catch (error) {
        console.log(error);
    }
};

export const add_product = (content: any): AppThunk => async (dispatch) => {
    try {
        await api.Product.AddProduct(content)
        const data = await (await api.Product.getAllProducts()).data;
        dispatch(getProducts({ data }));

    } catch (error) {
        console.log(error);
    }
};

export const update_product = (id: number, content: any): AppThunk => async (dispatch) => {
    try {
        await api.Product.updateProduct(id, content)
        const data = await (await api.Product.getAllProducts()).data;
        dispatch(getProducts({ data }));

    } catch (error) {
        console.log(error);
    }
};

export const delete_product = (id: number): AppThunk => async (dispatch) => {
    try {
        await api.Product.deleteProduct(id)
        const data = await (await api.Product.getAllProducts()).data;
        dispatch(getProducts({ data }));

    } catch (error) {
        console.log(error);
    }
};

export const get_Brand_List = (): AppThunk => async (dispatch) => {
    try {
        const data = await (await api.Brand.getAllBrand()).data;
        dispatch(getBrands({ data }));

    } catch (error) {
        console.log(error);
    }
};


export const selectProductList = (state: RootState) => state.product.productList;
export const selectBrandList = (state: RootState) => state.product.brands;
export const selectProduct = (state: RootState) => state.product.product;

export default ProductReducer.reducer;