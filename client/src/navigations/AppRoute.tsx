import React from 'react';
import {Route, Routes} from 'react-router-dom';
import ErrorPage from "../pages/ErrorPage";
import HomePage from "../pages/HomePage";
import RegisterPage from "../pages/RegisterPage";
import LoginPage from "../pages/LoginPage";
import {observer} from "mobx-react-lite";

const AppRoute = () => {
    return (
        <Routes>
            <Route path='/'
                   element={<HomePage/>}/>
            <Route path='/register'
                   element={<RegisterPage/>}/>
            <Route path='/login'
                   element={<LoginPage/>}/>

            <Route path='*'
                   element={<ErrorPage/>}/>
        </Routes>
    );
};

export default AppRoute;