<script lang="ts">
    import type {PopupSettings} from "@skeletonlabs/skeleton";

    let endpoints: string[] = []

    let currentEndpoint: string = ""

    const popupState: PopupSettings = {
        target: "",
        event:"click",
        state: (e: Record<string, boolean>) => console.log(e)
    };
    const addEndpoint = () => {
       endpoints = [...endpoints, currentEndpoint]
        alert('Data saved')
    }


</script>

<div class="mt-8 sm:mx-auto sm:w-full sm:max-w-md">

    <h1 class="flex justify-center content-center mt-2 text-3xl px-10 py-5">Configure mock</h1>
    <div class="py-8 px-6 shadow rounded-lg sm:px-10">
        <label class="label">
            <span>Endpoint</span>
            <input required
                   class="input w-full border border-gray-900 px-3 py-2 rounded-lg shadow-sm focus:outline-purple-700 focus:border-indigo-900 focus:ring-50 focus:ring-indigo-950"
                   type="text" placeholder="Endpoint" bind:value={currentEndpoint}/>
        </label>

        <label class="label">
            <span>Carrier name</span>
            <input id="carrier name" required
                   class="input w-full border border-gray-900 px-3 py-2 rounded-lg shadow-sm focus:outline-purple-700 focus:border-indigo-900 focus:ring-50 focus:ring-indigo-950"
                   type="text" placeholder="Carrier name"/>
        </label>

        <label class="label">
            <span>Nickname</span>
            <input required
                   class="input w-full border border-gray-900 px-3 py-2 rounded-lg shadow-sm focus:outline-purple-700 focus:border-indigo-900 focus:ring-50 focus:ring-indigo-950"
                   type="text" placeholder="Nickname"/>
        </label>


        <label class="Carrier logo">
            <span>Carrier logo</span>
            <input required
                   class="input w-full border border-gray-900 px-3 py-2 rounded-lg shadow-sm focus:outline-purple-700 focus:border-indigo-900 focus:ring-50 focus:ring-indigo-950"
                   type="file" placeholder="Carrier logo"/>
        </label>

        <button class="btn variant-filled flex justify-end mt-5" on:click={addEndpoint} type="button">
            Save
        </button>

    </div>

    <h1 class="flex justify-center content-center mt-2 text-3xl px-10 py-5">Configure paths</h1>

    <div class="py-8 px-6 shadow rounded-lg sm:px-10">

        <label class="label">
            <span>Choose endpoint for given path</span>
            <select class="select">
                {#each endpoints as endpoint, index}
                <option value={index}>{endpoint}</option>
                {/each}
            </select>
        </label>


        <label class="label">
            <span>Path</span>
            <input required
                   class="input w-full border border-gray-900 px-3 py-2 rounded-lg shadow-sm focus:outline-purple-700 focus:border-indigo-900 focus:ring-50 focus:ring-indigo-950"
                   type="text" placeholder="Endpoit path e.g. 'Auth'"/>
            </label>

        <button type="button" class="btn variant-filled flex justify-end mt-5">
            <span class="btn variant-filled">Save</span>
        </button>

    </div>
</div>