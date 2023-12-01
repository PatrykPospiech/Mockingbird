import type { AxiosRequestConfig} from 'axios';
import {CARRIER_URL} from "../../api-communication/config";
import type {CarrierData} from "../../model/carrierData";

export const MapGetCarrierListRequest = (): RequestInit => {
    return {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json',
            'Access-Control-Allow-Origin': '*'
        },
    };
};

// export const MapPostCarrierListRequest = (carrierDataFromConfig: CarrierData): RequestInit => {
//     return {
//         method: 'POST',
//         headers: {
//             'Content-Type': 'application/json',
//         },
//         data: {
//             body: GenerateBody(carrierDataFromConfig)
//         }
//     };
// };

const GenerateBody = (carrierDataFromConfig: CarrierData): CarrierData => {
    return {
        carrier_id: '',
        name: carrierDataFromConfig.name,
        nickname: carrierDataFromConfig.nickname,
        icon: '',
        options: [],
        api_resources: [{
            api_resource_id: '',
            name: '',
            url: '',
            methods: [{
                method_id: '',
                name: '',
                method_type: '',
                responses: [{
                    response_id: '',
                    is_active: true,
                    response_status_code: '',
                    response_body: '',
                    headers: [{
                        header_id: '',
                        name: '',
                        value: ''
                    }]
                }]
            }]
        }]
    }
}