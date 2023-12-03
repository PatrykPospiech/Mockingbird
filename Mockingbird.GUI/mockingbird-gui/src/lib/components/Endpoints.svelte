<script lang="ts">

    import type {ApiResource} from "../../helpers/model/api_resources";
    import { fly } from 'svelte/transition'

    export let endpoint: ApiResource;

    function addInput() {
        endpoint.methods = [...endpoint.methods || [], { name: "", method_id:null, method_type:"", responses: []}]
    }

</script>

{#if endpoint}
<div class="my-2 shadow rounded-lg card" transition:fly={{ x: 300, duration: 1000}}>
    <div class="card-header">
        <h1 class="flex justify-center content-center text-3xl">Configure endpoints</h1>
    </div>
    <div class="m-2 px-2">

        <label class="label">
            <span>Endpoint</span>
            <input class="form-control w-full px-3 py-2 rounded-lg shadow-sm"
                   type="text" placeholder="Endpoint" required bind:value={endpoint.url}/>
        </label>
    </div>

    {#each endpoint.methods || [] as path, index}
        <label class="label" transition:fly={{x:300, duration:500}}>
            <span>Path number: {index + 1} </span>
            <input required
                   class="input w-full border px-3 py-2 rounded-lg shadow-sm"
                   type="text" bind:value={path.name}/>
        </label>
     {/each}

    <button class="btn variant-filled flex justify-end" on:click={addInput}>
        <span>Add path</span>
    </button>
</div>
{/if}