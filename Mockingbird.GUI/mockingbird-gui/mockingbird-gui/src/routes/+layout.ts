
export async function load() {

    //fetch localhost:5173/carriers
    //const carriers = fetch())
/*
    console.log("fetching data from api");
        GetCarrierList().then((carriers) =>{
            console.log("fetched carriers", carriers);
            console.log("loading data")   ;
            return {
                carriers: [
                    {
                        carrierId: "1",
                        name: "DPD Polska",
                        nickname: "DPD"
                    },
                    {
                        carrierId: "2",
                        name: "FedEx1",
                        nickname: "string"
                    },
                    {
                        carrierId: "3",
                        name: "UPS",
                        nickname: "string"
                    }]
            };
        }
    );*/

    //from database


/*    console.log("fetching data from api");
    const carriers = await GetCarrierList();
    console.log("fetched carriers", carriers);
    console.log("loading data")*/
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
                name: "FedEx1",
                nickname: "string"
            },
            {
                carrierId: "3",
                name: "UPS",
                nickname: "string"
            }]
    };
}