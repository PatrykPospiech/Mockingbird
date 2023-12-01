import {CARRIER_URL} from "./config";
import type {CarrierData} from "../model/carrierData";


export const GetApiResponse = async (requestConfig: RequestInit, carrierUrl: string, fetch: any): Promise<any> => {
    let response: Response;

    try {
        response = await fetch(carrierUrl, requestConfig);

        if (!response.ok) {
            throw new Error(`Request failed with status: ${response.status}`);
        }

        const responseBody: CarrierData[] = await response.json();

        return responseBody;
    } catch (error) {
        console.log(error)
        throw new Error(`An unexpected error occurred during API communication with message: ${error} ${error}`);
    }
};