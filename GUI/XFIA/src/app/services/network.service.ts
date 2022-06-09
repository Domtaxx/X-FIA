import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { HttpParams } from '@angular/common/http';
import { HttpHeaders } from '@angular/common/http';
@Injectable({
  providedIn: 'root'
})
export class NetworkService {

  constructor(private http:HttpClient) { }
  serverIp: string = 'https://localhost:7282/';

  get_request(url: string, Params: any): Observable<any> {
    var http_params = new HttpParams({ fromObject: Params });
    return this.http.get(this.serverIp + url, { params: http_params });
  }
  post_request(url: string, Params: object) {
    return this.http.post(this.serverIp + url, Params);
  }
  put_request(url: string, Params: object) {
    return this.http.put(this.serverIp + url, Params);
  }
  delete_request(url: string, Params: any) {
    var http_params = new HttpParams({ fromObject: Params });
    return this.http.delete(this.serverIp + url, { params: http_params });
  }
  post_request_multipart(url:string,Params:any){
    var form_data = new FormData();
    const HttpUploadOptions = {
      headers: new HttpHeaders({ "Content-Type": "multipart/form-data" })
    }
    for ( var key in Params ) {
      console.log(key);
      console.log(Params[key])
      form_data.append(key, Params[key]);
    }
    console.log("form");
    for (var pair of form_data.entries()) {
      console.log(pair[0]+ ', ' + pair[1]); 
  }


    return this.http.post(this.serverIp + url, form_data);
  }
}
