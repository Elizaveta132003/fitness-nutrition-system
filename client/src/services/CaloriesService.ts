import $api from '../http';

export default class CaloriesService {
    static getCalories(userName: string){
        return $api.get(`https://localhost:7113/api/calories/${userName}`)
    }
}