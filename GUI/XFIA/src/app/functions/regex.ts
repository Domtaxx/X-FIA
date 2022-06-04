export function checkRegex(regex:string,text:string):boolean{
    const regexExp=new RegExp(regex);
    return regexExp.test(text)
}