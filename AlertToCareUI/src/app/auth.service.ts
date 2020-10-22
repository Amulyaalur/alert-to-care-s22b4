import { Inject, Injectable } from '@angular/core';

@Injectable()
export class AuthService{
   
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
    
    loggedIn():boolean{
        return !!localStorage.getItem('role');
    }
}
