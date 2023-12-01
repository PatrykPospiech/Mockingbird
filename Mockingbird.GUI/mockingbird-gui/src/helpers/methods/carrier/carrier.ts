
import {GetApiResponse} from "../../api-communication/api-communication";
import type {CarrierData} from "../../model/carrierData";
import {MapGetCarrierListRequest} from './mapping';
import {CARRIER_URL} from "../../api-communication/config";
export const GetCarrierList = async (fetch: any): Promise<CarrierData[]> => {

    try {
        const requestConfig: RequestInit = MapGetCarrierListRequest();

        const response = await callApi(requestConfig, CARRIER_URL, fetch);

        console.log("RESPONSE: " + JSON.stringify(response));

        return response;
    } catch (error) {
        console.error("Error fetching carrier list:", error);
        throw error; // Propagate the error to the caller
    }
};

// export const SendCarrierData = async (): Promise<CarrierData[]> => {
//
//     const thirdPartyRequest: AxiosRequestConfig = MapPostCarrierListRequest();
//     const response: AxiosResponse = await callApi(thirdPartyRequest);
//     console.log("RESPONSE" + JSON.stringify(response.data))
//     return JSON.parse(response.data) as CarrierData[];
// };

const callApi = async (requestConfig: RequestInit, carrierUrl: string, fetch:any): Promise<CarrierData[]> =>
    await GetApiResponse(requestConfig, carrierUrl, fetch);