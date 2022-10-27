export class dateValidations{
    public static inThePast(date:string):boolean{
        var today = new Date();
        var dd = String(today.getDate()).padStart(2, '0');
        var mm = String(today.getMonth() + 1).padStart(2, '0'); 
        var yyyy = today.getFullYear();
        var currentDate = yyyy + '-' + mm + '-' + dd;
        return date<currentDate;
    }
    public static continousDate(initialDate:string,finalDate:string){
        return finalDate>initialDate;
    }
    public static continousTime(initialDate:string,finalDate:string,initialTime:string,finalTime:string){
        if(finalDate>initialDate){
            return true
        }
        if(finalDate==initialDate){
            return finalTime>initialTime;
        }
        return false
    }
}