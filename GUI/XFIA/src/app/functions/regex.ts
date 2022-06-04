export function checkRegex(regex:RegExp,text:string):boolean{
    const regexExp=new RegExp(regex);
    return regexExp.test(text)
}