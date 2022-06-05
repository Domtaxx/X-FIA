
export function getData(key:string):string|null{
    return localStorage.getItem(key);

}
export function saveData(key:string,data:string):void{
    localStorage.setItem(key,data);
}