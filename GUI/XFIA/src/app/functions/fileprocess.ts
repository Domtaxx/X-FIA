/*
input: 
    *a file to get
    * a funcion to set a url to the given file
output:  void
description: takes a file a funcion that recieves a string, once has gotten the file url
calls the funcion with the url string
*/

export function fileProcessFuncion(file:File,setUrl:(url:string)=>void){
    let reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload =()=>
    {
        if(typeof(reader.result)=='string'){
            setUrl(reader.result);//call the function with the url param 
        }
        
    }
    
}
export async function getFileFromUrl(url:string,fileName:string,type:string):Promise<File>{
    let file:File = await fetch(url).then(r => r.blob()).then(blobFile => new File([blobFile], fileName, { type: type }));
    return file;
}

export function getExtension(url:string){
    const partArray=url.split('.');
    return partArray[partArray.length-1]
}