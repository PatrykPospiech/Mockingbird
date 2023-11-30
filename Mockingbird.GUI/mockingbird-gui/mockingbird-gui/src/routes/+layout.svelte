<script>
    import "../app.pcss";
    import {AppShell} from '@skeletonlabs/skeleton';

    export let data;
    let carriers = data.carriers
</script>

<AppShell background="bg-surface-100-800-token">
    <svelte:fragment slot="header">
        <h1 class="h1 flex justify-center bg-surface-400">
            <span class="box-decoration-clone">MockingBird</span>
        </h1>
    </svelte:fragment>
    <svelte:fragment slot="sidebarLeft">
        <div class="bg-surface-400 min-h-screen">
            <div class="btn-group-vertical variant-filled">
                {#each carriers as carrier (carrier.carrierId)}
                    <div>
                        <a class="btn variant-filled" href="/mocks/{carrier.carrierId}/responses"> {carrier.name} </a>
                    </div>
                {/each  }
            </div>
        </div>
    </svelte:fragment>
    <!-- (sidebarRight) -->
    <!-- (pageHeader) -->
    <!-- Router Slot -->
    <slot>

    </slot>
    <svelte:fragment slot="pageFooter">
        <div class="bg-surface-400">
            <button type="button" class="btn variant-filled ">
                <a class="btn variant-filled" href="/mocks/slug/configure">Configure a new mock </a>
            </button>
            <button type="button" class="btn variant-filled jus">
                <a class="btn variant-filled" href="/">Return to main page</a>
            </button>
        </div>
    </svelte:fragment>
</AppShell>
