import { createSlice } from '@reduxjs/toolkit';
import api from '../api/api';
import { AppThunk, RootState } from "./store";

const initialState = {
    users: [],
    orders: [],
    orderDetail: []
}

function checkEror(e: any) {
    if (e.response.status == 401) {
        window.location.href = "/authentication/login"
    }
    if (e.response.status == 403) {
        window.alert("tài khoảng không đủ quyền");
    }
}

export const userSlice = createSlice({
    name: "user",
    initialState,
    reducers: {
        getUsers: (state, { payload }) => {
            state.users = payload.data;
        },
        getOrders: (state, { payload }) => {
            state.orders = payload.data;
        },
        orderDetail: (state, { payload }) => {
            state.orderDetail = payload.data;
        },
    },
});

export const { getUsers, getOrders, orderDetail } = userSlice.actions;


export const get_Users = (): AppThunk => async (dispatch) => {
    try {
        const data = await (await api.User.getAllUser()).data;
        dispatch(getUsers({ data }));

    } catch (error) {
        checkEror(error)
    }
};

export const get_Orders = (id: string): AppThunk => async (dispatch) => {
    try {
        const data = await (await api.User.getOders(id)).data;
        dispatch(getOrders({ data }));

    } catch (error) {
        checkEror(error)
    }
};

export const get_OrderDetail = (id: number): AppThunk => async (dispatch) => {
    try {
        const data = await (await api.User.getOderDetail(id)).data;
        dispatch(orderDetail({ data }));

    } catch (error) {
        checkEror(error)
    }
};

export const selectUsers = (state: RootState) => state.user.users;
export const selectOrders = (state: RootState) => state.user.orders;
export const selectOrdeDetail = (state: RootState) => state.user.orderDetail;

export default userSlice.reducer;