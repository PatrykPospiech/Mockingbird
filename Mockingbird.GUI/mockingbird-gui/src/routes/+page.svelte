<script>
    /** @type {import('./$types').PageData} */
    import truck from '$lib/assets/truck.png'
    export let data;
</script>

<div class="flex justify-center content-center mt-2 ">
    <div class="flex-grow-0 justify-center btn-icon btn-icon-xl">
        {#await data.streamed.carriers}
         <span>Loading</span>
        {:then carriers}
            {#each carriers as carrier (carrier.carrier_id)}
                <button type="button" class="btn variant-filled bg-primary-500">
                    <a href="/mocks/{carrier.carrier_id}/responses"> {carrier.name} </a>
                </button>
            {/each}
        {/await}


        <button on:click={() => {console.log("herere")}} > here</button>
    </div>
</div>
