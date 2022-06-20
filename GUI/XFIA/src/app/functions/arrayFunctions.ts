export function orderNumberArray(array:number[]):number[]{
    array.sort(
        (a,b)=>a-b
    );
    return array
}
export function repitedElement(array:any[]):boolean{
    const sortedArray=orderNumberArray(array);
    for(var i=0;i<sortedArray.length-1;i++){
        if(sortedArray[i]==sortedArray[i+1])return true;
    }
    return false;
}