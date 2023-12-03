<script lang="ts">
    import "../app.pcss";
    import {AppShell} from '@skeletonlabs/skeleton';

    export let data;

</script>

<AppShell>
    <svelte:fragment slot="header">
        <div class="bg-primary-50">
        <h1 class="h1 flex justify-center p-2">
            <span class="text-4xl tracking-wider font-light text-white">MockingBird</span>
        </h1>
        </div>
    </svelte:fragment>
    <svelte:fragment slot="sidebarLeft">
        <div class="min-h-full bg-primary-50 ">
            <div class="btn-group-vertical btn bg-gradient-to-br variant-gradient-tertiary-secondary p-2 m-1 ">
                {#await data.carriers}
                {:then carriers}
                    {#each carriers as carrier}
                        {#key carrier.carrier_id}
                            <div>
                                <a href="/mocks/{carrier.carrier_id}/responses"> {carrier.name} </a>
                            </div>
                        {/key}
                    {/each}
                {/await}
            </div>
        </div>
    </svelte:fragment>
    <slot>

    </slot>
    <svelte:fragment slot="pageFooter">
        <div class="mt-10">
            <button type="button" class="btn bg-gradient-to-br variant-gradient-tertiary-secondary m-5">
                <a href="/mocks/create">Configure a new mock </a>
            </button>
            <button type="button" class="btn bg-gradient-to-br variant-gradient-tertiary-secondary m-5">
                <a href="/">Return to main page</a>
            </button>
        </div>
    </svelte:fragment>
</AppShell>
