import axios from 'axios';

export const API_URL = "http://localhost:5198/api"

const $api = axios.create({
    withCredentials: true,
    baseURL: API_URL
});

$api.interceptors.request.use((config) => {
    config.headers.Authorization = `Bearer ${localStorage.getItem('token')}`;
    return config;
});

$api.interceptors.response.use((config) => {
    return config;
}, async (error) => {
    if (error.response.status == 401 && error.config && !error.config._isRetry) {
        console.log('Not authorized');
    }
});

export default $api;