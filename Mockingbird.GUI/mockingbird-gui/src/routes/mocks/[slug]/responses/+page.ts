import {MapGetResourcesListRequest} from "../../../../helpers/methods/carrier/mapping";
import {RESOURCES_URL} from "../../../../helpers/api-communication/config";


// @ts-ignore
export async function load({fetch, params, }) {

    const request = MapGetResourcesListRequest();
    console.log(request)

    let res = undefined

    try
    {
        res = await fetch(`${RESOURCES_URL}?carrierId=${params.slug}`, request)
    }
    catch (e) {
        console.log(`error: ${e}`)

        throw e
    }


    if(!res.ok){
        const reason = await res.json();
        console.log(reason)

        return {
            ok: false,
            reason
        }
    }

    try {
        const carrierResources = await res.json();
        console.log('RESOURCEv121232313' + JSON.stringify(carrierResources))
        return {
            ok: true,
            carrierResources
        }
    } catch (e) {
        console.log(e)
    }
}