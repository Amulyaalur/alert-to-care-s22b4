import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
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
  constructor(private activatedRoute:ActivatedRoute, private patientService:PatientService) { }

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

}
