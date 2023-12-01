import {GetCarrierList} from "../helpers/methods/carrier/carrier";
import type {CarrierData} from "../helpers/model/carrierData";

export async function load() {

    console.log("fetching data from api");
    const carriers = GetCarrierList();
    console.log("fetched carriers", await carriers);
    console.log("loading data")

    return {
        carriers: carriers
    }
}