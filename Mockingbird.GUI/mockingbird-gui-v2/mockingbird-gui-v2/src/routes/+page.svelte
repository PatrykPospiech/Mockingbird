<script>
    /** @type {import('./$types').PageData} */
    import truck from '$lib/assets/truck.png'
    export let data;

    let carriers = data.carriers
</script>

<div class="flex justify-center content-center mt-2 ">
    <div class="flex-grow-0 justify-center btn-icon btn-icon-xl">
        {#each carriers as carrier (carrier.carrierId)}
            <div>
                <a class="btn variant-filled" href="/mocks/{carrier.carrierId}/responses"> {carrier.name} </a>
            </div>
        {/each}
    </div>
</div>
