export class fileValidations{
    static allowedImgExtension:string[]=['png','jpeg'];
    static allowedDataExtension:string[]=[]

    public static checkImage(file:any):boolean{
        for(var i in this.allowedImgExtension){
            console.log(file.type)
            if(file.type=="image/"+this.allowedImgExtension[i]){
                return true;
            }
        }
        
        return false;
    }
}