import { Component, OnInit,Inject } from '@angular/core';
import { Router } from '@angular/router';
//import { AccountService } from '../services/account.service';
import { AuthService } from '../../auth.service';

@Component({
  selector: 'login-comp',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  username='';
  password='';
 message='';
  //accountService:any;
  constructor(private accountService:AuthService,private route:Router,) {
    this.accountService=accountService;
   }

  ngOnInit() {
  }
  
  loginFunction(){

    let role=this.accountService.checkValidUser(this.username,this.password);
   
     if(role=='admin'){
     
      localStorage.setItem('role','admin');
      this.route.navigate(['/admin']);
    }
   
    else if(role=='nurse'){
     
      localStorage.setItem('role','nurse');
      this.route.navigate(['/home']);
    }
    else{
      this.route.navigate(['/login'])
    }
  }
  // if(role=='denied' || role==undefined){
  onReset(){
    this.username='';
   this.password='';
   this.message='';
  }
}
