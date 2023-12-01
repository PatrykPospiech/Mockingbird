<script lang="ts">
    import "../app.pcss";
    import {AppShell} from '@skeletonlabs/skeleton';
    import type {CarrierData} from "../helpers/model/carrierData";

    export let data;

</script>

<AppShell class="bg-surface-400">
    <svelte:fragment slot="header">
        <h1 class="h1 flex justify-center bg-surface-800 p-2">
            <span class="text-4xl tracking-wider font-light text-white">MockingBird</span>
        </h1>
    </svelte:fragment>
    <svelte:fragment slot="sidebarLeft">
        <div class="bg-surface-800 min-h-full">
            <div class="btn-group-vertical variant-filled p-2 m-1 bg-primary-500">
                {#await data.streamed.carriers}
                    {:then carriers}
                {#each carriers as carrier (carrier.carrier_id)}
                    <div>
                        <a href="/mocks/{carrier.carrier_id}/responses"> {carrier.name} </a>
                    </div>
                {/each  }
                {/await}
            </div>
        </div>
    </svelte:fragment>
    <!-- (sidebarRight) -->
    <!-- (pageHeader) -->
    <!-- Router Slot -->
    <slot>

    </slot>
    <svelte:fragment slot="pageFooter">
        <div class="bg-surface-400 mt-10">
            <button type="button" class="btn variant-filled bg-primary-500 m-5">
                <a href="/mocks/create">Configure a new mock </a>
            </button>
            <button type="button" class="btn variant-filled bg-primary-500 m-5">
                <a href="/">Return to main page</a>
            </button>
        </div>
    </svelte:fragment>
</AppShell>
