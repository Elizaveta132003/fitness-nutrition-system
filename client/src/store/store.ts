import {IUser} from '../models/IUser';
import {makeAutoObservable} from 'mobx';
import AuthService from '../services/AuthService';
import {IRegisterUser} from '../models/IRegisterUser';


export default class Store {
    user = {
        userName:"",
    } as IUser;
    isAuth = false;
    isLoading = false;

    constructor() {
        makeAutoObservable(this);
    }

    setAuth(bool: boolean) {
        this.isAuth = bool;
    }

    setUser(user: IUser) {
        this.user = user;
    }

    setLoading(bool: boolean) {
        this.isLoading = bool;
    }


    async login(userName: string, password: string) {
        try {
            this.setLoading(true);
            const response = AuthService.login(userName, password)
                .then(response => {
                console.log(response);
                localStorage.setItem('token', response.data.token);
                const user : IUser = {
                    userName: response.data.userName
                }
                this.setUser(user);
                this.setAuth(true);
                this.setLoading(false);
            });
        } catch (e: any) {
            this.setLoading(false);
            console.log(e.response?.data?.message);
        }
    }

    async registration(registerUser:IRegisterUser) {
        try {
            const response = await AuthService.registration(registerUser);
            console.log(response);
            localStorage.setItem('token', response.data.token);
            this.setAuth(true);

            const user : IUser = {
                userName: response.data.userName
            }

            this.setUser(user);
        } catch (e: any) {
            console.log(e.response?.data?.message);
        }
    }
}