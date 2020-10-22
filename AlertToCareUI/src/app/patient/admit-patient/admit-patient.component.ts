import { Component,Input,Output, OnInit, EventEmitter } from '@angular/core';
// import { EventEmitter } from 'events';
import { observable } from 'rxjs';
import { AdminService } from 'src/app/admin/services/admin.service';
import { PatientService } from '../services/patient.service';

@Component({
  selector: 'admit-patient-comp',
  templateUrl: './admit-patient.component.html',
  styleUrls: ['./admit-patient.component.css']
})
export class AdmitPatientComponent implements OnInit {
  patientObj={
    address: "",
    age: undefined,
    bedId: "",
    contactNumber: "",
    email: "",
    icuId: "",
    patientId: "",
    patientName: ""
  };
  icuList=[];
  icu:string;
  errorMessage='';
  bedList=[];
  @Output('show') show=new EventEmitter<boolean>();
  constructor(private adminService:AdminService,private patientService:PatientService) { }
 
  ngOnInit()
   {
     this.show.emit(false);
    this.icuList=this.adminService.getIcuList();
  }
 
  getAvailableBeds(){
    console.log(this.icu);
    let observable=this.patientService.getAvailableIcuBeds(this.icu);
    observable.subscribe((data:any)=>{
      console.log(data);
      this.bedList=data;
      if(data.length==0){
        this.errorMessage='No beds Available in the ICU';
      }
      else{
        this.errorMessage='';
      }
    },
    (error:any)=>{
      console.log(error);
    },
    ()=>{
      console.log('Completed getting bed');
    });
  }
  admitPatient(){
    this.patientObj.icuId=this.icu;
    console.log(this.patientObj);
    let observable=this.patientService.admitPatient(this.patientObj);
    observable.subscribe((data:any)=>{
      console.log(data);
    },
    (error:any)=>{
      console.log(error);
    },
    ()=>{
      console.log("Addition completed");
    });
  }
}
