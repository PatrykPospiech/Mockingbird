import {GetCarrierList} from "../helpers/methods/carrier/carrier";
import type {CarrierData} from "../helpers/model/carrierData";
import {CARRIER_URL} from "../helpers/api-communication/config";

export async function load({ fetch }) {

    const request = {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json',
            'Origin': '*'
        }};

    const resp = await fetch(CARRIER_URL, request);

    try{
        const carriers = await resp.text();
        console.log("response: " + carriers)
        console.log(JSON.stringify(carriers))
        return {
            streamed: {
                carriers: carriers
            }
        }
    } catch (e) {
        console.log(e)
    }

}