import {MapGetResourcesListRequest} from "../../../../../helpers/methods/carrier/mapping";
import {RESOURCES_URL} from "../../../../../helpers/api-communication/config";


// @ts-ignore
export async function load({fetch, params}) {

    const request = MapGetResourcesListRequest();
    console.log(request)

    const res = await fetch(`${RESOURCES_URL}?carrierId=${params.slug}&apiResourceId=${params.resource}`, request)

    if(res.ok){
        console.log('Resources loaded')
    }

    try {
        const resource = await res.json();
        console.log('RESOURCE' + JSON.stringify(resource[0]))
        return {
            resource: resource[0]
        }
    } catch (e) {
        console.log(e)
    }
}