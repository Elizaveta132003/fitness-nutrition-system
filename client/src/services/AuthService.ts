import $api from '../http';
import {AxiosResponse} from 'axios';
import {IAuthRepsponse} from '../models/response/IAuthRepsponse';
import {IRegisterUser} from '../models/IRegisterUser';

export default class AuthService {
    static login(userName: string, password: string): Promise<AxiosResponse<IAuthRepsponse>> {
            return $api.post<IAuthRepsponse>('http://localhost:5198/api/Users/authorization', {userName, password});
    }

    static async registration(user:IRegisterUser): Promise<AxiosResponse<IAuthRepsponse>> {
        return $api.post<IAuthRepsponse>('http://localhost:5198/api/Users/registration', user);
    }
}