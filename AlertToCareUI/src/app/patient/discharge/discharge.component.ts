import { Route } from '@angular/compiler/src/core';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { observable } from 'rxjs';
import { PatientService } from '../services/patient.service';

@Component({
  selector: 'patient-discharge-comp',
  templateUrl: './discharge.component.html',
  styleUrls: ['./discharge.component.css']
})
export class DischargeComponent implements OnInit {

  patientId:string;
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
  message:string;
  isDisabled:boolean;
  buttonMsg='Cancel';
  constructor(private activatedRoute:ActivatedRoute, private patientService:PatientService,private route:Router) { }

  ngOnInit() {
    this.activatedRoute.paramMap.subscribe((params)=>{
      this.patientId=params.get("patientId");
    });
    
    console.log(this.patientId);
    let observable=this.patientService.getPatientById(this.patientId);
    observable.subscribe((data:any)=>{
      this.patientObj=data;
      console.log(this.patientObj);
    },
    (error:any)=>{
      console.log(error);
      
    },
    ()=>{
      console.log("Patient Details Fetched");
    });
  }
  discharge(){
    console.log("Discharge");
    let observable=this.patientService.deletePatient(this.patientId);
    observable.subscribe((data:any)=>{
      console.log(data);
      this.isDisabled=true;
      this.buttonMsg="Go Back";
      this.message="Patient Discharged Successfully";
    },
    (error:any)=>{
      //console.log(error.error);
      this.message=error.error;
    },
    ()=>{
      console.log("Deleted");
    });
  }
}
