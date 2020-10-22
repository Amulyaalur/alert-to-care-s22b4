import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login/login.component';
import { FormsModule } from '@angular/forms';
import { AccountService } from './services/account.service';
import { HttpClientModule } from '@angular/common/http';
import { AuthService } from '../auth.service';



@NgModule({
  declarations: [LoginComponent],
  imports: [
    CommonModule,FormsModule,HttpClientModule//,AuthService
  ],
  exports:[LoginComponent],
  providers:[{provide:AuthService,useClass:AuthService},{provide:'apiBaseAddress',useValue:'http://localhost:61575/api'}]
})
export class AccountModule { }
