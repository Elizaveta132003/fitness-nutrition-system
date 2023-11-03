import $api from '../http';
import {AxiosResponse} from 'axios';
import {IAuthRepsponse} from '../models/response/IAuthRepsponse';
import {IRegisterUser} from '../models/IRegisterUser';

export default class AuthService {
    static login(userName: string, password: string): Promise<AxiosResponse<IAuthRepsponse>> {
            return $api.post<IAuthRepsponse>('Users/authorization', {userName, password});
    }

    static async registration(user:IRegisterUser): Promise<AxiosResponse<IAuthRepsponse>> {
        return $api.post<IAuthRepsponse>('Users/registration', user);
    }
}