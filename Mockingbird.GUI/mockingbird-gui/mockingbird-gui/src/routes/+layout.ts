export function load() {

    //fetch localhost:5173/carriers
    //const carriers = fetch())

    console.log("loading data")

    //from database
    return {
        carriers: [
            {
                carrierId: "1",
                name: "DPD Polska",
                nickname: "DPD"
            },
            {
                carrierId: "2",
                name: "FedEx",
                nickname: "string"
            },
            {
                carrierId: "3",
                name: "UPS",
                nickname: "string"
            }]
    };
}