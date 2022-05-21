export class fileValidations{
    static allowedImgExtension:string[]=['png','jpg'];
    static allowedDataExtension:string[]=[]

    public static checkImage(file:any):boolean{
        for(var i in this.allowedImgExtension){
            console.log(file.type)
            if(file.type=="image/"+i){
                return true;
            }
        }
        
        return false;
    }
}