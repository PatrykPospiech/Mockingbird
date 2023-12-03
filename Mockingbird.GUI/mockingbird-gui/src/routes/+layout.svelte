<script lang="ts">
    import "../app.pcss";
    import {AppShell} from '@skeletonlabs/skeleton';

    export let data;

</script>

<AppShell>
    <svelte:fragment slot="header">
        <div class="bg-primary-50">
        <h1 class="h1 flex justify-center p-2">
            <span class="text-4xl tracking-wider font-light text-surface-500">MockingBird</span>
        </h1>
        </div>
    </svelte:fragment>
    <svelte:fragment slot="sidebarLeft">
        <div class="min-h-full ">
            <div class="flex flex-col p-2 m-1 space-y-2 ">
                {#await data.carriers}
                {:then carriers}
                    {#each carriers as carrier}
                        {#key carrier.carrier_id}
                            <div class="flex justify-center">
                                <a class="btn variant-filled-surface w-36" href="/mocks/{carrier.carrier_id}/responses"> {carrier.name} </a>
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
            <button type="button" class="btn bg-surface-400 m-5">
                <a class="text-white" href="/mocks/create">Configure a new mock </a>
            </button>
            <button type="button" class="btn bg-surface-400 m-5">
                <a class="text-white" href="/">Return to main page</a>
            </button>
        </div>
    </svelte:fragment>
</AppShell>
