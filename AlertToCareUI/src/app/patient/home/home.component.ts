import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { PatientService } from '../services/patient.service';
import { ActivatedRoute } from '@angular/router'; 

@Component({
  selector: 'patient-home-comp',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class PatientHomeComponent implements OnInit {
  patientsArr=[];
  patientService:any;
  show:boolean;
  patientId='';
  
  constructor(patientService:PatientService,private route:Router,private activatedRoute:ActivatedRoute) { 
    this.show=true;
    this.patientService=patientService;
  }

  ngOnInit() {
    // this.show=;
   // this.show=false;
    let observable=this.patientService.getAllPatient();
    observable.subscribe( (data:any)=>{
      console.log(data);
      for(let i=0;i<data.length;i++){
        this.patientsArr.push(data[i]);
      }
     // console.log("patientId:"+this.patientsArr[0].patientId+"patientName:"+this.patientsArr[0].patientName);
    },
    (error:any)=>{
      console.log(error);
    },
    ()=>{
      console.log("completed");
    });
  }
  toggle(){
    this.show=!this.show;
    console.log(this.show);
  }
  discharge(patientid:string){
    this.show=!this.show;
    console.log(patientid)
    this.patientId=patientid;
    this.route.navigate(['discharge',this.patientId],{relativeTo:this.activatedRoute});
  }
  update(patientid:string){
    this.show=!this.show;
    console.log(patientid)
    this.patientId=patientid;
    this.route.navigate(['update',this.patientId],{relativeTo:this.activatedRoute});
  }

}
