import React, {useContext, useState} from 'react';
import {Button, Dropdown} from 'flowbite-react';
import {observer} from 'mobx-react-lite';
import {Context} from "../../index";
import {IRegisterUser} from "../../models/IRegisterUser";
import InputField from "../input/InputField/InputField";
import {Gender} from "../../enums/Gender";

const RegisterForm = () => {
    const {store} = useContext(Context)
    const [userName, setUserName] = useState("");
    const [dateOfBirth, setDateOfBirth] = useState(new Date());
    const [password, setPassword] = useState("");
    const [gender, setGender] = useState(Gender.Female);

    function getEnumKeys<
        T extends string,
        TEnumValue extends string | number,
    >(enumVariable: { [key in T]: TEnumValue }) {
        return Object.keys(enumVariable) as Array<T>;
    }

    const onClick = (m:Gender) => {
        setGender(m)
    }

    return (
        <div className="container mx-auto w-1/3">
            <form className="flex flex-col gap-4" onSubmit={
                () => {
                    const user : IRegisterUser = {
                        userName:userName,
                        dateOfBirth:dateOfBirth,
                        password:password,
                        gender:gender,
                    }

                    store.registration(user)
                }
            }>
                <InputField id="userName"
                            type="userName"
                            required={true}
                            labelValue="Имя пользователя"
                            value={userName}
                            onChange={e=>setUserName(e.target.value)}
                />

                <InputField id="password"
                            type="password"
                            required={true}
                            labelValue="Пароль"
                            value={password}
                            onChange={e=>setPassword(e.target.value)}
                            pattern="(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,}"
                            title="Must contain at least one number and one uppercase and lowercase letter, and at least 8 or more characters"
                />

                <InputField id="dateOfBirth"
                            type="date"
                            required={true}
                            labelValue="Дата рождения"
                            value={dateOfBirth}
                            onChange={e=>setDateOfBirth(e.target.value)}

                />

                <Dropdown
                    label={`Ваш пол: ${gender}`}
                    dismissOnClick={true}
                >
                    {
                        getEnumKeys(Gender).map((key, index) =>
                            <Dropdown.Item key={index} value={Gender[key]}>
                                <Button onClick={e => onClick(Gender[key])}> {key}</Button>
                            </Dropdown.Item>
                        )
                    }

                </Dropdown>

                <Button type="submit">
                    Создать новый аккаунт
                </Button>
            </form>
        </div>
    );
};

export default observer(RegisterForm);