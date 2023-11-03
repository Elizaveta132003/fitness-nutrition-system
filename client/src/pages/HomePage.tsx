import React, {useEffect} from 'react';
import {Navigate} from "react-router-dom";
import { useNavigate } from 'react-router-dom'

const HomePage = () => {
    const navigate = useNavigate()

    useEffect(() => {
        if (!localStorage.getItem('token')) {
            navigate('/login')
        }
    },[])

    return (
        <div className="container mx-auto bg-white py-24 sm:py-32">
            <div className="mx-auto max-w-7xl px-6 lg:px-8">
                <div className="mx-auto max-w-2xl lg:mx-0">

                </div>
                <div className="mx-auto mt-10 grid max-w-2xl grid-cols-1 gap-x-8 gap-y-16 border-t border-gray-200 pt-10 sm:mt-16 sm:pt-16 lg:mx-0 lg:max-w-none lg:grid-cols-3">
                   <h1>Что-то</h1>
                </div>
            </div>
        </div>
    );
};

export default HomePage;