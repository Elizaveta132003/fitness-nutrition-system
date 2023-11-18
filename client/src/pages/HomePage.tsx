import React, {useContext, useEffect, useMemo, useState} from 'react';
import { useNavigate } from 'react-router-dom'
import {Alert} from "flowbite-react";
import useSignalRClient from "../hooks/useSignalRClient";
import CaloriesService from "../services/CaloriesService";
import {Context} from "../index";

const HomePage = () => {
    const navigate = useNavigate()
    const { message, changeMessage, setChangeMessage } = useSignalRClient()
    const {store} = useContext(Context);

    useEffect(() => {
        if (!localStorage.getItem('token')) {
            navigate('/login')
        }

        CaloriesService.getCalories(store.user.userName)
    },[])



    return (
        <div className="container mx-auto bg-white py-24 sm:py-32">
            <div className="mx-auto max-w-7xl px-6 lg:px-8">
                <div className="mx-auto max-w-2xl lg:mx-0">
                    {
                        changeMessage
                        ?
                            <Alert color="info" onDismiss={()=>setChangeMessage(false)}>
                                <span className="font-medium">Сегодня вы наели на {message} калорий</span>
                            </Alert>
                        :
                            <div></div>
                    }
                </div>
                <div className="mx-auto mt-10 grid max-w-2xl grid-cols-1 gap-x-8 gap-y-16 border-t border-gray-200 pt-10 sm:mt-16 sm:pt-16 lg:mx-0 lg:max-w-none lg:grid-cols-3">
                   <h1>Что-то</h1>
                </div>
            </div>
        </div>
    );
};

export default HomePage;