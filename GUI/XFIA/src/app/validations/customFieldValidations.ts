import { AbstractControl, ValidationErrors } from '@angular/forms'
import { totalBudget } from '../interface/interfaces';
import { repitedElement } from '../functions/arrayFunctions';
export function matchPassword(fieldName1:string,fieldName2:string){
    return (control:AbstractControl):ValidationErrors|null =>{
        if(control.get(fieldName1)?.value!=control.get(fieldName2)?.value){
            return {noMatch:true}
        }
        else return null;
    }
}
export function pilotsDoesntMatch(numberOfPilots:number){
    return (control:AbstractControl)=>{
        const pilotArray:string[]=[];
        for(var i=1;i<=numberOfPilots;i++){
            pilotArray.push(String.bind(null,control.get('pilot'+i)?.value)())
        }
        if(repitedElement(pilotArray)){
            return{repitedPilot:true}
        }
        return null;
    }

}

export function overBudget(budget:totalBudget){
    return (control:AbstractControl)=>{
        if(budget.leftBudget<0){
            return {overBudget:true}
        }
        else return null;
    }

}