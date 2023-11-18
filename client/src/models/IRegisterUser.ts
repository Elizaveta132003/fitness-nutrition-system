import {Gender} from "../enums/Gender";

export interface IRegisterUser {
    userName: string;
    dateOfBirth: Date;
    gender:Gender;
    password:string;
}