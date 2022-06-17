
export function getData(key:string):string|null{
    return localStorage.getItem(key);

}
export function saveData(key:string,data:string):void{
    localStorage.setItem(key,data);
}
export function existKey(key:string):boolean{
    if(key in localStorage){
        return true
    }
    return false;
}
export function deleteKey(key:string){
    localStorage.removeItem(key)
}