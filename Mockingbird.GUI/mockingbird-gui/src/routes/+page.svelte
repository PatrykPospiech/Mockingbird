<script>
    /** @type {import('./$types').PageData} */
    export let data;
</script>

<div class="flex justify-center content-center mt-2 ">
    <div class="flex-grow-0 justify-center btn-icon btn-icon-xl">
        {#await data.carriers}
         <span>Loading</span>
        {:then carriers}
            {#each carriers as carrier}
                <button type="button" class="btn bg-gradient-to-br variant-gradient-tertiary-secondary">
                    <a href="/mocks/{carrier.carrier_id}/responses"> {carrier.name} </a>
                </button>
            {/each}
        {/await}
    </div>
</div>
