<div class="mt-8 sm:mx-auto sm:w-full sm:max-w-md">

    <h1 class="flex justify-center content-center mt-2 text-3xl px-10 py-5">Configure mock</h1>
    <div class="py-8 px-6 shadow rounded-lg sm:px-10">
        <label class="label">
            <span>Endpoint</span>
            <input required
                   class="input w-full border border-gray-900 px-3 py-2 rounded-lg shadow-sm focus:outline-purple-700 focus:border-indigo-900 focus:ring-50 focus:ring-indigo-950"
                   type="text" placeholder="Endpoint"/>
        </label>

        <label class="label">
            <span>Carrier name</span>
            <input id="carrier name" required
                   class="input w-full border border-gray-900 px-3 py-2 rounded-lg shadow-sm focus:outline-purple-700 focus:border-indigo-900 focus:ring-50 focus:ring-indigo-950"
                   type="text" placeholder="Carrier name"/>
        </label>

        <label class="label">
            <span>Nickname</span>
            <input required
                   class="input w-full border border-gray-900 px-3 py-2 rounded-lg shadow-sm focus:outline-purple-700 focus:border-indigo-900 focus:ring-50 focus:ring-indigo-950"
                   type="text" placeholder="Nickname"/>
        </label>


        <label class="Carrier logo">
            <span>Carrier logo</span>
            <input required
                   class="input w-full border border-gray-900 px-3 py-2 rounded-lg shadow-sm focus:outline-purple-700 focus:border-indigo-900 focus:ring-50 focus:ring-indigo-950"
                   type="file" placeholder="Carrier logo"/>
        </label>

    </div>

    <h1 class="flex justify-center content-center mt-2 text-3xl px-10 py-5">Configure paths</h1>

    <div class="py-8 px-6 shadow rounded-lg sm:px-10">

        <label class="label">
            <span>Choose endpoint for given path</span>
            <select class="select">
                <option value="1">Endpoint 1</option>
                <option value="2">Endpoint 2</option>
                <option value="3">Endpoint 3</option>
            </select>
        </label>


        <label class="label">
            <span>Path</span>
            <input required
                   class="input w-full border border-gray-900 px-3 py-2 rounded-lg shadow-sm focus:outline-purple-700 focus:border-indigo-900 focus:ring-50 focus:ring-indigo-950"
                   type="text" placeholder="Endpoit path e.g. 'Auth'"/>
            </label>

        <button type="button" class="btn variant-filled flex justify-end mt-5">
            <span class="btn variant-filled">Save</span>
        </button>

    </div>
</div>