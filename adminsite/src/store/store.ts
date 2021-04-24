import { configureStore, ThunkAction, Action } from "@reduxjs/toolkit";
import authReducer from "./auth-slice";
import productReducer from "./Product-slice";

export const store = configureStore({
  reducer: {
    auth: authReducer,
    product: productReducer,
  },
});

export type RootState = ReturnType<typeof store.getState>;
export type AppThunk<ReturnType = void> = ThunkAction<ReturnType, RootState, unknown, Action<string>>;