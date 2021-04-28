import { createSlice } from '@reduxjs/toolkit';
import api from '../api/api';
import { AppThunk, RootState } from "./store";

const initialState = {
    categories: [],
    category: {}
}

function checkEror(e: any) {
    if(e.response.status == 401){
        window.location.href="/authentication/login"
    }
    if(e.response.status == 403){
        window.alert("tài khoảng không đủ quyền");
    }
}

export const categorySlice = createSlice({
    name: "category",
    initialState,
    reducers: {
        getcategories: (state, { payload }) => {
            state.categories = payload.data;
        },
        getCategory : (state, { payload }) => {
            state.category = payload.data;
        },
    },
});

export const { getcategories, getCategory } = categorySlice.actions;


export const get_Categories = (): AppThunk => async (dispatch) => {
    try {
        const data = await (await api.Category.getAllCategory()).data;
        dispatch(getcategories({ data }));

    } catch (error) {
        checkEror(error)
    }
};

export const get_Category = (id: number): AppThunk => async (dispatch) => {
    try {
        const data = await (await api.Category.getCategory(id )).data;
        dispatch(getCategory({ data }));

    } catch (error) {
        checkEror(error) 
    }
};

export const add_category = (content: any): AppThunk => async (dispatch) => {
    try {
        await api.Category.addCategory(content)
        const data = await (await api.Category.getAllCategory()).data;
        dispatch(getcategories({ data }));

    } catch (error) {
        checkEror(error) 
    }
};

export const delete_Category = (id: number): AppThunk => async (dispatch) => {
    try {
        await api.Category.deleteCategory(id)
        const data = await (await api.Category.getAllCategory()).data;
        dispatch(getcategories({ data }));

    } catch (error) {
        checkEror(error) 
    }
};

export const update_category = (id: number, content: any): AppThunk => async (dispatch) => {
    try {
        await api.Category.updateCategory(id, content)
        const data = await (await api.Category.getAllCategory()).data;
        dispatch(getcategories({ data }));

    } catch (error) {
        checkEror(error) 
    }
};



export const selectCategories = (state: RootState) => state.category.categories;
export const selectCategory = (state: RootState) => state.category.category;

export default categorySlice.reducer;