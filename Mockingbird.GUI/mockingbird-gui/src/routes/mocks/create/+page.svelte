<script lang="ts">

    import type {CarrierData} from "../../../helpers/model/carrierData";
    import Endpoints from "$lib/components/Endpoints.svelte";

    let endpoints: string[] = [];
    let currentEndpoint: string = "";
    let carrierName: string;
    let nickname: string;
    let carrierLogo: object;
    let carrierDataFromConfig: CarrierData;

    const popupAlert = () => {
        console.log("hehehehehe")
        alert('Data saved')
    };

    const addEndpoint = () => {
        endpoints = [...endpoints, currentEndpoint]

        popupAlert()
    }

    const addInput = () => {
        console.log("hehe")
    }

    const addCarrierData = () => {

        console.log("hehe")
        carrierDataFromConfig = {
            carrier_id: '',
            name: carrierName,
            nickname: nickname,
            icon: 'carrierLogo',
            // api_resources: endpoints.map(singleEnpointConfig => {
            //     name: singleEnpointConfig.
            // })
        }

        addEndpoint()
        //MapPostCarrierListRequest(carrierDataFromConfig);
    }

</script>

<div class="mt-8 sm:mx-auto sm:w-full sm:max-w-md">

    <div class="space-y-4 p-2 shadow rounded-lg card">
        <div class="card-header">
            <h1 class="flex justify-center content-center text-3xl">Configure mock</h1>
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

            <label class="label">
                <span>Carrier logo</span>
                <input required
                       class="form-control w-full px-3 py-2 rounded-lg shadow-sm bg-white"
                       type="file" placeholder="Carrier logo" bind:value={carrierLogo}/>
            </label>
        </div>

        <button class="btn variant-filled flex justify-end mt-5" on:click={addCarrierData} type="button">
            Save
        </button>
    </div>
    <Endpoints on:click={addInput}/>



        <div class="card-footer">
            <button type="button" class="btn variant-filled flex justify-end" on:click={popupAlert}>
                <span class="btn variant-filled">Save</span>
            </button>
        </div>
    </div>
