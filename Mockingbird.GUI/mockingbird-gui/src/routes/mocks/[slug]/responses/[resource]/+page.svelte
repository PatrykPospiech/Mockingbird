<script lang="ts">

    export let data;

</script>
{#await data.resource}
    <span>...Loading</span>
{:then resource}
<div class="mt-8 sm:mx-auto sm:w-full sm:px-10">
    <div class="py-8 px-6 shadow rounded-lg sm:px-10 mt-2 card">
        <h1 class="flex justify-center content-center mt-2 text-3xl px-10 py-5 text-surface-500">Add responses to path: {resource.url} </h1>
        <div class="flex flex-row">
            <label class="label basis-1/6">
                <span>Path</span>
                <select class="select">
                    {#each resource.methods as method}
                    <option value={method.method_id}>{method.name}</option>
                    {/each}
                </select>

                <span>Status</span>
                <select class="select">
                    <option value="1">200</option>
                    <option value="2">400</option>
                    <option value="3">401</option>
                    <option value="4">403</option>
                    <option value="5">500</option>
                </select>
            </label>

            <div>
                <label class="label basis-1/2">
                    <span>Response</span>
                    <textarea required
                              class="textarea w-full border border-gray-900 px-3 py-2 rounded-lg basis-1 shadow-sm focus:outline-purple-700 focus:border-indigo-900 focus:ring-50 focus:ring-indigo-950"
                              rows="20" placeholder="Carrier response to be returned"/>
                </label>
            </div>

        </div>

        <div class="flex justify-end">
            <button type="button" class="btn variant-filled justify-self-end mt-5">
                <span class="btn variant-filled ">Save</span>
            </button>
        </div>

    </div>


    <div class="mt-10">
        <h1 class="flex justify-center content-center text-3xl text-surface-500">Current response configuration</h1>
        <div class="flex flex-row justify-start space-y-2 card">
            <label class="flex items-center space-x-2 p-10">
                <input class="checkbox" type="checkbox" checked/>
                <p>Generate validation set</p>
            </label>
            <label class="flex items-center space-x-2 p-10">
                <input class="checkbox" type="checkbox"/>
                <p>Call TPS</p>
            </label>
            <label class="flex items-center space-x-2 p-10">
                <input class="checkbox" type="checkbox"/>
                <p>Regression tests</p>
            </label>
        </div>
    </div>

    <div class="py-8 px-6 shadow rounded-lg sm:px-10 mt-5 flex items-stretch p-5 card">
        <label class="flex items-center space-x-2">
            <input class="checkbox" type="checkbox"/>
            <p>Is active</p>
        </label>

        <p class="m-2 p-5">200</p>
        <p class="m-2 p-5">Label</p>
    </div>


</div>
{/await}

