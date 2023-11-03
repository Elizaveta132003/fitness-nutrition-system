import React, {FC, useContext, useEffect, useState} from 'react';
import {observer} from 'mobx-react-lite';
import "./LoginForm.module.css"
import {Navigate, useNavigate} from 'react-router-dom';
import {Button} from 'flowbite-react';
import {Context} from "../../index";
import InputField from "../input/InputField/InputField";

const LoginForm: FC = () => {
    const [userName, setUserName] = useState<string>('');
    const [password, setPassword] = useState('');
    const {store} = useContext(Context);
    const navigate = useNavigate();

    function handleRef() {
        return navigate(`/register`);
    }

    if (store.isAuth) {
        return <Navigate to="/" />;
    }

    return (
        <div className="container mx-auto w-1/3">
            <div className="flex flex-col gap-4 max-w-7xl">
                <InputField id="email1" type="text" required={true} labelValue="Имя пользователя"
                            onChange={(e: any) => setUserName(e.target.value)}
                            value={userName}
                />

                <InputField id="password1" type="password" required={true} labelValue="Пароль"
                            onChange={(e: any) => setPassword(e.target.value)}
                            value={password}
                />

                <Button className='w-2/3 self-center' onClick={()=>store.login(userName, password)}>Войти</Button>
                <Button className='w-2/3 self-center' onClick={handleRef} >Регистрация</Button>
            </div>
        </div>
    );
};

export default observer(LoginForm);