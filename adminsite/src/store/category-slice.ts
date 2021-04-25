import { createSlice } from '@reduxjs/toolkit';
import api from '../api/api';
import { AppThunk, RootState } from "./store";

const initialState = {
    categories: []
}

export const   categorySlice = createSlice({
    name: "category",
    initialState,
    reducers: {
        getcategories: (state, { payload }) => {
            state.categories = payload.data;
        },
    },
});

export const { getcategories } =   categorySlice.actions;


export const getCategories = (): AppThunk=> async  (dispatch) => {
    try {
        const data = await (await api.Category.getAllCategory()).data;
        dispatch(getcategories({ data }));

    } catch (error) {
        console.log(error);
    }
};

// export const delete_product = (id: number): AppThunk=> async  (dispatch) => {
//     try {
//         await api.Product.deleteProduct(id)
//         const data = await (await api.Product.getAllProducts()).data;
//         dispatch(getProducts({ data }));

//     } catch (error) {
//         console.log(error);
//     }
// };



export const selectCategories = (state: RootState) => state.category.categories;

export default   categorySlice.reducer;