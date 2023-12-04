<script lang="ts">

    import type {CarrierData} from "../../../helpers/model/carrierData";
    import Endpoints from "$lib/components/Endpoints.svelte";
    import {MapPostCarrierListRequest} from "../../../helpers/methods/carrier/mapping";
    import type {ApiResource} from "../../../helpers/model/api_resources";
    import {CARRIER_URL} from "../../../helpers/api-communication/config";
    import { FileButton } from '@skeletonlabs/skeleton';

    let endpoints: ApiResource[] = [];
    let carrierName: string;
    let nickname: string;
    let carrierLogo: object;
    let carrierDataFromConfig: CarrierData

    const popupAlert = () => {
        alert('Data saved')
    };

    const addEndpoint = () => {
        endpoints = [...endpoints, {name: "", url: "", methods: [], api_resource_id:null}]
    }

    const addCarrierData = async () => {
        carrierDataFromConfig = {
            carrier_id: null,
            name: carrierName,
            nickname: nickname,
            icon: 'carrierLogo',
            api_resources: endpoints
        }
        const request = MapPostCarrierListRequest(carrierDataFromConfig);

        const res = await fetch(`${CARRIER_URL}`, {
            method: "POST",
            body: JSON.stringify(request.body),
            headers: {
                "Content-Type": "application/json"
            }
        })

        if(res.ok){
            popupAlert()
        }
    }

</script>

<div class="mt-8 sm:mx-auto sm:w-full sm:max-w-md">

    <div class="space-y-4 p-2 shadow rounded-lg card">
        <div class="card-header">
            <h1 class="flex justify-center content-center text-3xl text-surface-500">Configure mock</h1>
        </div>
        <div class="m-2 px-2">
            <label class="label">
                <span>Carrier name</span>
                <input id="carrier name" required
                       class="form-control w-full px-3 py-2 rounded-lg shadow-sm"
                       type="text" placeholder="Carrier name" bind:value={carrierName}/>
            </label>
            <label class="label">
                <span>Nickname</span>
                <input required
                       class="form-control w-full px-3 py-2 rounded-lg shadow-sm"
                       type="text" placeholder="Nickname" bind:value="{nickname}"/>
            </label>

            <span>Carrier logo</span>
            <FileButton class="pt-2 text-white" name="files" button="btn bg-surface-400" bind:value={carrierLogo}>Upload</FileButton>
        </div>

        <div class="flex flex-row justify-evenly">
            <button class="btn bg-surface-400 flex justify-end mt-5" on:click={addEndpoint} type="button">
                <span class="text-white">Add endpoint</span>
            </button>
        </div>

    </div>
    {#each endpoints as endpoint}
        <Endpoints bind:endpoint/>
    {/each}

        <div class="card-footer">
            <button type="button" class="btn bg-surface-400 flex justify-end mt-2" on:click={addCarrierData}>
                <span class="text-surface-50">Save</span>
            </button>
        </div>
    </div>
