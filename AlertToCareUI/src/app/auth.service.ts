import { Inject, Injectable } from '@angular/core';

@Injectable()
export class AuthService{
   
    constructor(){
       
    }
    checkValidUser(username:string,password:string){

        return this.checkAdmin(username,password);
        
        
    }
    checkAdmin(username:string,password:string){
        let role:string;
        if( username=="admin" && password=='admin'){
            role= "admin"
        }
        else{
            role=this.checkStaff(username,password);
        }
        return role;
    }
    checkStaff(username:string,password:string){
        if( username=="staff" && password=='staff'){
            return "staff"
        }
        else{
            return "denied";
        }
    }
    
    loggedIn():boolean{
        return !!localStorage.getItem('role');
    }
}
