<div class="mt-8 sm:mx-auto sm:w-full sm:max-w-md">
    <h1 class="flex justify-center content-center mt-2 text-3xl px-10 py-5">Add responses to mock</h1>

    <div class="py-8 px-6 shadow rounded-lg sm:px-10 mt-2 ">
        <div class="flex items-stretch">
            <label class="label">
                <span>Path</span>
                <select class="select">
                    <option value="1">Auth</option>
                    <option value="2">Label</option>
                    <option value="3">Manifest</option>
                </select>
            </label>

            <label class="label">
                <span>Status</span>
                <select class="select">
                    <option value="1">200</option>
                    <option value="2">400</option>
                    <option value="3">401</option>
                    <option value="3">403</option>
                    <option value="3">500</option>
                </select>
            </label>


            <label class="label">
                <span>Path</span>
                <textarea required
                          class="textarea w-full border border-gray-900 px-3 py-2 rounded-lg shadow-sm focus:outline-purple-700 focus:border-indigo-900 focus:ring-50 focus:ring-indigo-950"
                          rows="20" placeholder="Endpoit path e.g. 'Auth'"/>
            </label>

        </div>

        <button type="button" class="btn variant-filled flex justify-end mt-5">
            <span class="btn variant-filled ">Save</span>
        </button>

    </div>


    <h1 class="flex justify-center content-center mt-2 text-3xl px-10 py-5">Current response configuration</h1>

    <div class="space-y-2">
        <label class="flex items-center space-x-2">
            <input class="checkbox" type="checkbox" checked/>
            <p>Generate validation set</p>
        </label>
        <label class="flex items-center space-x-2">
            <input class="checkbox" type="checkbox"/>
            <p>Call TPS</p>
        </label>
        <label class="flex items-center space-x-2">
            <input class="checkbox" type="checkbox"/>
            <p>Regresion tests</p>
        </label>
    </div>


    <div class="py-8 px-6 shadow rounded-lg sm:px-10">

    </div>
</div>
