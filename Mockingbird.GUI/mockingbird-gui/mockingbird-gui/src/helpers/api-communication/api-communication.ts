import type {AxiosRequestConfig, AxiosResponse} from 'axios';
import {Axios} from 'axios';

export const GetApiResponse = async (requestConfig: AxiosRequestConfig): Promise<AxiosResponse> => {

    const axios = new Axios(requestConfig);
    let response: AxiosResponse;

    try {
        response = await axios.request(requestConfig);
    } catch (e) {
        throw new Error(`an unexpected error occurred during api communication with message`);
    }

    return response;
};