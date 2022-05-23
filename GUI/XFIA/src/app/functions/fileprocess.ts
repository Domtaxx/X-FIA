export function fileProcessFuncion(file:File,setUrl:(url:string)=>void){
    let reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload =()=>
    {
        if(typeof(reader.result)=='string'){
            setUrl(reader.result);
        }
        
    }
    
}