/** @type {import('./$types').PageLoad} */
export function load({ params }) {

    const carriers = fetch()

    return {
        post: {
            title: `Title for ${params.slug} goes here`,
            content: `Content for ${params.slug} goes here`
        }
    };
}