import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AccountModule } from './account/account.module';
import { AuthGuard } from './auth.guard';
import { AuthService } from './auth.service';
import { HomeComponent } from './home/home.component';
import { LayoutModule } from './layout/layout.module';
import { NavComponent } from './layout/nav/nav.component';
import { AdminModule } from './admin/admin.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { PatientModule } from './patient/patient.module';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    AccountModule,
    LayoutModule,
    AdminModule,
    BrowserAnimationsModule,
    PatientModule
  ],
  providers: [{provide:'apiBaseAddress',useValue:'http://localhost:61575/api/'}, AuthGuard,AuthService],
  bootstrap: [AppComponent]
})
export class AppModule { }
