import { localStorageNames } from "../const/localStorageNames";
import{existKey} from '../functions/browserDataInfo'
export function isLogged():boolean{
    
    const exists=existKey(localStorageNames.email);
    if(exists){
        return true;
    }
    return false;
    
}