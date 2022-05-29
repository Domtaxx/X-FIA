import { carInterface,pilotInterface } from "../interface/interfaces";
export class budgedCalc{
    /*
    input: a map with pilotInterface(interface representing the pilot object in the format send by the server)
    , car interface( a interface representing a car in the format sent by the server)
    output: the price of the full team, using the pilots and car prices
    description: Takes the member of the team and adds their prices
    */
    public static calculateTeamCost(pilots:Map<number,pilotInterface>,car?:carInterface):number{
        var totalCost=0;
        const pilotIterator=pilots.values();
        var done=false;
        var current;
        while(!done){
            current=pilotIterator.next(); 
            
            if(current.done){
                done=true;
            }
            else{
                totalCost+=current.value.Price
            }
            

        }
        if(car!=undefined){
            totalCost+=car.Price;
        }
        return totalCost

    }
}