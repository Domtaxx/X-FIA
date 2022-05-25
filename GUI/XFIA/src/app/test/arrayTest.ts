import { orderNumberArray } from "../functions/arrayFunctions";
import { repitedElement } from "../functions/arrayFunctions";

function orderedArrayTest(){
    var array:number[];
    var orderedArray:number[];
    

    //proves
    array=[5,4,3,2,1]
    orderedArray=[1,2,3,4,5];
    test('sucession 1,2,3,4,5',
    ()=>{
        expect(orderNumberArray(array)).toBe(orderedArray)
    })
}