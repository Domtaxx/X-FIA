export class fileValidations{
    static allowedImgExtension:string[]=['png','jpeg'];
    static allowedDataExtension:string[]=['csv']

    public static checkImage(file:any):boolean{
        for(var i in this.allowedImgExtension){
            console.log(file.type)
            if(file.type=="image/"+this.allowedImgExtension[i]){
                return true;
            }
        }
        
        return false;
    }
    public static checkData(file:any):boolean{
        for(var i in this.allowedDataExtension){
            
            if(file.type=="text/"+this.allowedDataExtension[i]){
                return true;
            }
        }
        
        return false;
    }
}