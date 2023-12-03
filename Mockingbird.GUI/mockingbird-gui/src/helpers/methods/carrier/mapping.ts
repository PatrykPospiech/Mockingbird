import type {CarrierData} from "../../model/carrierData";

export const MapPostCarrierListRequest = (carrierDataFromConfig: CarrierData): RequestInit => {
    return {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        // @ts-ignore
        body: GeneratePostCarrierBody(carrierDataFromConfig)
    };
};

const GeneratePostCarrierBody = (carrierDataFromConfig: CarrierData): CarrierData => {
    return {
        carrier_id: null,
        name: carrierDataFromConfig.name,
        nickname: carrierDataFromConfig.nickname,
        icon: '',
        options: [],
        api_resources: carrierDataFromConfig.api_resources
    }
}

export const MapGetResourcesListRequest = (): RequestInit => {
    return {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json',
        }
    };
};