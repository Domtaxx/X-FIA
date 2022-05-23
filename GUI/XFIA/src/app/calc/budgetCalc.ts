import { carInterface,pilotInterface } from "../interface/interfaces";
export class budgedCalc{
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