import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './account/login/login.component';
import { AddIcuComponent } from './admin/add-icu/add-icu.component';
import { AdminHomeComponent } from './admin/admin-home/admin-home.component';
import { AllIcuComponent } from './admin/all-icu/all-icu.component';
import { RemoveIcuComponent } from './admin/remove-icu/remove-icu.component';
import { UpdateIcuComponent } from './admin/update-icu/update-icu.component';
import { AppComponent } from './app.component';
import { AuthGuard } from './auth.guard';
import { HomeComponent } from './home/home.component';
import { AdmitPatientComponent } from './patient/admit-patient/admit-patient.component';
import { DischargeComponent } from './patient/discharge/discharge.component';
import { PatientHomeComponent } from './patient/home/home.component';
import { UpdatePatientComponent } from './patient/update-patient/update-patient.component';


const routes: Routes = [
  {
    path: '',
    redirectTo:'/home',pathMatch:'full',
    canActivate:[AuthGuard]
  },
  {
    path:'login', 
    component:LoginComponent
  },
  {
    path: 'home',
    component:HomeComponent,
    canActivate:[AuthGuard]
  },
  {
    path: 'admin',
    component:AdminHomeComponent,
    canActivate:[AuthGuard],
    children:[
      {
        path:'addicu',component:AddIcuComponent
      },
      {
        path:'removeicu',component:RemoveIcuComponent
      },
      {
        path:'showicu',component:AllIcuComponent
      },
      {
        path:'updateicu',component:UpdateIcuComponent
      }
    ]
  },
  {
    path: 'patients',
    component:PatientHomeComponent,
    canActivate:[AuthGuard],
    children:[
      {
        path:'admit',component:AdmitPatientComponent
      },
      {
        path:'discharge/:patientId',component:DischargeComponent
      },
      {
        path:'update/:patientId',component:UpdatePatientComponent
      }
      
    ]
  }
  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
