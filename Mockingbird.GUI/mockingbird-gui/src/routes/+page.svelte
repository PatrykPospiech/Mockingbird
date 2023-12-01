<script>
    /** @type {import('./$types').PageData} */
    import truck from '$lib/assets/truck.png'
    export let data;

    let carriers = data.carriers
</script>

<div class="flex justify-center content-center mt-2 ">
    <div class="flex-grow-0 justify-center btn-icon btn-icon-xl">
        {#each carriers as carrier (carrier.carrier_id)}
            <button type="button" class="btn variant-filled bg-primary-500">
                <a href="/mocks/{carrier.carrier_id}/responses"> {carrier.name} </a>
            </button>
        {/each}
    </div>
</div>
