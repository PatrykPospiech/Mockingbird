import {CARRIER_URL} from "../helpers/api-communication/config";

//Used by layout and page
// @ts-ignore
export async function load({fetch}) {

    const request = {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json',
            'Access-Control-Allow-Origin': '*'
        }
    };

    const resp = await fetch(CARRIER_URL, request);

    try {
        const carriers = await resp.json();

        return {
            carriers: carriers
        }
    } catch (e) {
        console.log(e)
    }
}