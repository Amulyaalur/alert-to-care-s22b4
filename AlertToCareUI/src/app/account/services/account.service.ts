import { Inject, Injectable } from '@angular/core';

@Injectable()
export class AccountService{
   
    constructor(){
       
    }
    checkValidUser(username:string,password:string){
        if(username=='admin' && password=='admin'){
            return 'admin';
        }
        else if(username=='staff' && password=='staff'){
            return 'staff';
        }
        else{
            return 'denied';
        }
        
    }
    // signup(user:any){
    //     let observableStream=this.httpClient.post(this.baseUrl+'/accounts/register',user);
    //     return observableStream;
    // }
    // login(data:any){
    //     let observableStream=this.httpClient.post(this.baseUrl+'/accounts/validate',data);
    //     return observableStream;
    // }
    // forgotPassword(data:any){
    //     let observableStream=this.httpClient.get(this.baseUrl+'/accounts/recover/',{headers:new HttpHeaders().set("username",data.username)});
    //     return observableStream;
    // }
}