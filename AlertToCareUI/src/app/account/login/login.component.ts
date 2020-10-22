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
    
//     let data={username:this.username,password:this.password};
//     let observableStream=this.accountService.login(data);
//     observableStream.subscribe((data:any)=>{

//       this.message="Valid User";
//       this.route.navigate(['dashboard',this.username]);
      
//     },
// (error:any)=>{
    
//       this.message="Invalid User";
//     },
//     ()=>{
//       console.log("Completed");
//     }
//     );

let role=this.accountService.checkValidUser(this.username,this.password);
    if(role=='denied' || role==undefined){
      this.message='Enter Valid Credentials';
    }
    else if(role=='admin'){
      this.message='Transfer on Admin page';
      localStorage.setItem('role','admin');
      this.route.navigate(['/admin']);
    }
    else{
      this.message='Transfer on Nurse page';
      localStorage.setItem('role','nurse');
      this.route.navigate(['/home']);
    }
  }
  onReset(){
    this.username='';
   this.password='';
   this.message='';
  }
}
