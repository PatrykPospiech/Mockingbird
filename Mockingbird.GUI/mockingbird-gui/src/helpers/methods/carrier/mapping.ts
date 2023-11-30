import type { AxiosRequestConfig, AxiosResponse } from 'axios';
import { CARRIER_URL} from "../../api-communication/config";

export const MapCarrierListRequest = (): AxiosRequestConfig => {
    return {
        url: CARRIER_URL,
        method: 'GET',
        headers: {
            'Content-Type': 'application/json',
        }

    };
};
