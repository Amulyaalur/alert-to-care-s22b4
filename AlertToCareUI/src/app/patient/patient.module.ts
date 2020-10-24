import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdmitPatientComponent } from './admit-patient/admit-patient.component';
import { RouterModule } from '@angular/router';
import { LayoutModule } from '../layout/layout.module';
import { FormsModule } from '@angular/forms';
import { MatTableModule } from '@angular/material';
import { PatientHomeComponent } from './home/home.component';
import { PatientService } from './services/patient.service';
import { DischargeComponent } from './discharge/discharge.component';
import { UpdatePatientComponent } from './update-patient/update-patient.component';



@NgModule({
  declarations: [AdmitPatientComponent, PatientHomeComponent, DischargeComponent, UpdatePatientComponent],
  imports: [
    CommonModule,RouterModule,LayoutModule,FormsModule,MatTableModule
  ],
  exports:[AdmitPatientComponent,PatientHomeComponent,DischargeComponent,UpdatePatientComponent],
  providers:[{provide:PatientService,useClass:PatientService}]
})
export class PatientModule { }
