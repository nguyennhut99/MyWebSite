import { createSlice } from '@reduxjs/toolkit';
import api from '../api/api';
import { AppThunk, RootState } from "./store";

const initialState = {
    productList: []
}

export const ProductReducer = createSlice({
    name: "product",
    initialState,
    reducers: {
        getProducts: (state, { payload }) => {
            state.productList = payload.data;
        },
    },
});

export const { getProducts } = ProductReducer.actions;

// export const completeLoginAsync = (): AppThunk => async (dispatch) => {
//     await authService.completeLoginAsync(window.location.href);
//     const user = await authService.getUserAsync();
//     console.log(user);

// };

export const get_product_list = (): AppThunk=> async  (dispatch) => {
    try {
        await api.Product.getAllProducts()
        const data = await (await api.Product.getAllProducts()).data;
        dispatch(getProducts({ data }));

    } catch (error) {
        console.log(error);
    }
};

export const delete_product = (id: number): AppThunk=> async  (dispatch) => {
    try {
        await api.Product.deleteProduct(id)
        const data = await (await api.Product.getAllProducts()).data;
        dispatch(getProducts({ data }));

    } catch (error) {
        console.log(error);
    }
};



export const selectProductList = (state: RootState) => state.product.productList;

export default ProductReducer.reducer;