import { AbstractControl, ValidationErrors } from '@angular/forms'
export function matchPassword(fieldName1:string,fieldName2:string){
    return (control:AbstractControl):ValidationErrors|null =>{
        if(control.get(fieldName1)?.value!=control.get(fieldName2)?.value){
            return {noMatch:control}
        }
        else return null;
    }
}
export function pilotsDoesntMatch(numberOfPilots:number){
    return (control:AbstractControl)=>{
        const pilotArray:number[]=[];
        for(var i=1;i<=numberOfPilots;i++){
            pilotArray.push(Number.bind(null,control.get('pilot'+i)?.value)())
        }
        pilotArray.sort(
            (a,b)=>a-b
        );
        if(matchInOrderedArray(pilotArray)){
            return{noMatch:control}
        }
        return null;
    }

}

function matchInOrderedArray(array:any[]):boolean{
    for(var i=0;i<array.length-1;i++){
        if(array[i]==array[i+1])return true;
    }
    return false;

}