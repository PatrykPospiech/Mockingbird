<script lang="ts">
    let endpointBaseUrl = "";

    let paths:string[] = ["", ""]

    function addInput() {
        console.log("add paths" + paths.length)
        paths = [...paths, ""]
    }

</script>

<div class="my-2 shadow rounded-lg card">
    <div class="card-header">
        <h1 class="flex justify-center content-center text-3xl">Configure endpoints</h1>
    </div>
    <div class="m-2 px-2">

        <label class="label">
            <span>Endpoint</span>
            <input class="form-control w-full px-3 py-2 rounded-lg shadow-sm"
                   type="text" placeholder="Endpoint" required bind:value={endpointBaseUrl}/>
        </label>
    </div>

    {#each paths as path, index}
        <label class="label">
            <span>Path number: {index + 1} </span>
            <input required
                   class="input w-full border px-3 py-2 rounded-lg shadow-sm"
                   type="text" bind:value={path}/>
        </label>
     {/each}

    <button class="btn variant-filled flex justify-end" on:click={addInput}>
        <span>Add path</span>
    </button>
</div>
