<script>
    import Headline from "./components/Headline.svelte";
    import ButtonComponent from "./components/Button.svelte"
</script>

import <Headline></Headline>


<div class="button-container">
    <ButtonComponent symbol="DPD" bgColor={"#2c777a"}></ButtonComponent>
    <ButtonComponent symbol="Add new mock" bgColor={"#c53d46"}></ButtonComponent>
</div>


<style>
    main {
        height: 100vh;
        min-width: 100%;
        position: relative;
        display: flex;
        flex-direction: column;
        align-items: center;
        justify-content: center;
        font-family: sans-serif;
        background-color: rgb(130, 141, 110);
    }

    h1 {
        color: #ff3e00;
        text-transform: uppercase;
        font-size: clamp(2.5rem,5vw,4rem);
        font-weight: 100;
    }

    p {
        font-size: 2rem;
        color: #222;
        margin: 2rem 0 1rem;
    }

    .button-container {
        display: flex;
        align-items: center;
        justify-content: space-between;
        width: 240px;
    }
</style>