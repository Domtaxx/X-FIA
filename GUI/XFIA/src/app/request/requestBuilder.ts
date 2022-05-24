import { FormGroup } from "@angular/forms";
import { userRegisterInterface } from "../interface/interfaces";
import { RegisterTeamComponent } from "../modules/auth/register-team/register-team.component";
import { UserRegisterComponent } from "../modules/auth/user-register/user-register.component";
import { sha256, sha224 } from 'js-sha256';
export function userRegisterRequest(user:UserRegisterComponent,team1:RegisterTeamComponent,team2:RegisterTeamComponent):userRegisterInterface{
    const userForm:FormGroup=user.userRegisterForm;
    const team1Form:FormGroup=team1.teamForm;
    const team2Form:FormGroup=team2.teamForm;
    var registerRequest:userRegisterInterface={
        Username:userForm.controls['userName'].value,
        Password: sha256(userForm.controls['password'].value),
        Email:userForm.controls['email'].value,
        TeamsName:userForm.controls['teamName'].value,
        TeamsLogo:user.imageFile,
        CountryName:userForm.controls['countryName'].value,
        NameSubteam1:team1Form.controls['teamName'].value,
        Car1:team1Form.controls['car'].value,
        pilot1Subteam1:team1Form.controls['pilot1'].value,
        pilot2Subteam1:team1Form.controls['pilot2'].value,
        pilot3Subteam1:team1Form.controls['pilot3'].value,
        pilot4Subteam1:team1Form.controls['pilot4'].value,
        pilot5Subteam1:team1Form.controls['pilot5'].value,
        NameSubteam2:team2Form.controls['teamName'].value,
        Car2:team2Form.controls['car'].value,
        pilot1Subteam2:team2Form.controls['pilot1'].value,
        pilot2Subteam2:team2Form.controls['pilot2'].value,
        pilot3Subteam2:team2Form.controls['pilot3'].value,
        pilot4Subteam2:team2Form.controls['pilot4'].value,
        pilot5Subteam2:team2Form.controls['pilot5'].value,

    }
    console.log(registerRequest)
    return registerRequest;

}