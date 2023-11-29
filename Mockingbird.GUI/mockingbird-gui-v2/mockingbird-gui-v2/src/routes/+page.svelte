<script>
    /** @type {import('./$types').PageData} */
    export let data;

    let carriers = data.carriers
</script>

<div class="flex justify-center content-center mt-2">
    <div class="flex-grow-0 justify-center">
        {#each carriers as carrier (carrier.carrierId)}

            <button class="btn btn-xl variant-filled grid grid-cols-2 md:grid-cols-3 gap-4 ">
                <span class="justify-center">{carrier.name}</span>
            </button>

        {/each}
    </div>
</div>
