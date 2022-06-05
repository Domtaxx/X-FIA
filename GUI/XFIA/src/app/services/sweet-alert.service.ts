import { Injectable } from '@angular/core';
import Swal, { SweetAlertInput } from 'sweetalert2';
@Injectable({
  providedIn: 'root'
})
export class SweetAlertService {

  constructor() { }

  showError(title: string, text: string) {
    Swal.fire({
      icon: 'error',
      title: title,
      text: text,
    });
  }

  showSuccess(title: string, text: string) {
    Swal.fire({
      icon: 'success',
      title: title,
      text: text,
      confirmButtonColor: '#E10600'
    });
  }

  optionSwal(
    title: string,
    text: string,
    cancelText: string,
    confirmText: string,
    dennyText: string
  ) {
    return Swal.fire({
      title: title,
      text: text,
      showDenyButton: dennyText != '',
      showCancelButton: cancelText != '',
      showConfirmButton: confirmText != '',

      confirmButtonText: confirmText,
      denyButtonText: dennyText,
      cancelButtonText: cancelText,
    });
  }

  htmloptionSwal(
    title: string,
    htmltext: string,
    cancelText: string,
    confirmText: string,
    dennyText: string,
    useInput: SweetAlertInput | null
  ) {
    if (useInput)
      return this.htmloptionSwalAux(
        title,
        htmltext,
        cancelText,
        confirmText,
        dennyText,
        useInput
      );
    else
      return this.htmloptionSwalAuxWithoutInput(
        title,
        htmltext,
        cancelText,
        confirmText,
        dennyText
      );
  }

  private htmloptionSwalAux(
    title: string,
    htmltext: string,
    cancelText: string,
    confirmText: string,
    dennyText: string,
    inputType: SweetAlertInput
  ) {
    return Swal.fire({
      title: title,
      html: htmltext,
      input: inputType,
      showDenyButton: dennyText != '',
      showCancelButton: cancelText != '',
      showConfirmButton: confirmText != '',
      confirmButtonText: confirmText,
      denyButtonText: dennyText,
      cancelButtonText: cancelText,
    });
  }

  private htmloptionSwalAuxWithoutInput(
    title: string,
    htmltext: string,
    cancelText: string,
    confirmText: string,
    dennyText: string
  ) {
    return Swal.fire({
      title: title,
      html: htmltext,
      showDenyButton: dennyText != '',
      showCancelButton: cancelText != '',
      showConfirmButton: confirmText != '',
      confirmButtonText: confirmText,
      denyButtonText: dennyText,
      cancelButtonText: cancelText,
    });
  }

  public inputTextSwal(title:string,confirmButtonText:string,callback:(data:string)=>void){
    Swal.fire({
      title: title,
      input: 'text',
      inputAttributes: {
        autocapitalize: 'off'
      },
      showCancelButton: true,
      confirmButtonText: confirmButtonText,
      showLoaderOnConfirm: true,
      preConfirm: (login) => {
        callback(login);
        return true;
      },
      allowOutsideClick: () => !Swal.isLoading()
    })
    
  }
}
