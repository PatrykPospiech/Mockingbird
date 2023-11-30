
import {GetApiResponse} from "../../api-communication/api-communication";
import type {CarrierData} from "../../model/carrierData";
import type { AxiosRequestConfig, AxiosResponse } from 'axios';
import {MapCarrierListRequest} from './mapping';
export const GetCarrierList = async (): Promise<CarrierData[]> => {

    const thirdPartyRequest: AxiosRequestConfig = MapCarrierListRequest();
    const response: AxiosResponse = await callApi(thirdPartyRequest);
    return JSON.parse(response.data) as CarrierData[];
};

const callApi = async (request: AxiosRequestConfig): Promise<AxiosResponse> => await GetApiResponse(request);