<script lang="ts">
    import { page } from "$app/stores"

    export let data;
</script>


<nav class="list-nav">

{#await data.carrierResources}
    <span>Loading..</span>
    {:then carrierResources}
    {#if data && !data.ok}
        <span>carrier returned no configured responses due to error: {data.reason}</span>
        {:else}
        <div class="grid-rows-auto items-stretch mx-auto p-2">
            <ul>
                <li>
            {#each carrierResources as resource (resource.api_resource_id)}
                <a class="anchor" href="/mocks/{$page.params.slug}/responses/{resource.api_resource_id}">{resource.url} </a>
            {/each}
                </li>
            </ul>
        </div>
    {/if}
{/await}
</nav>