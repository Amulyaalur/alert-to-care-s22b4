import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AdminService } from 'src/app/admin/services/admin.service';
import { PatientService } from '../services/patient.service';

@Component({
  selector: 'patient-update-comp',
  templateUrl: './update-patient.component.html',
  styleUrls: ['./update-patient.component.css']
})
export class UpdatePatientComponent implements OnInit {

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
  icu='';
  errorMessage='';
  message='';
  bedList=[];
  patientId='';
  isDefaultIcu=true;
  bedId='';
  oldIcu:string;
  oldBed:string;
  isBedChanged=false;
  constructor(private adminService:AdminService,private patientService:PatientService,private activatedRoute:ActivatedRoute) { }

  ngOnInit() {
    this.icuList=this.adminService.getIcuList();
    this.activatedRoute.paramMap.subscribe((params)=>{
      this.patientId=params.get("patientId");
    });
    
    console.log(this.patientId);
    
    let observable=this.patientService.getPatientById(this.patientId);
    observable.subscribe((data:any)=>{
      this.patientObj=data;
      console.log("Here");
      this.oldIcu=this.patientObj.icuId;
      this.oldBed=this.patientObj.bedId;
      console.log(this.patientObj);
      console.log(this.patientObj.icuId);
      
      this.getAvailableBeds(this.patientObj.icuId);
      
    },
    (error:any)=>{
      console.log(error);
      
    },
    ()=>{
      console.log("Patient Details Fetched");
    });

      

  } 
  getAvailableBeds(icuId:string){
    this.patientObj.icuId=icuId;
    console.log("Test: icuId="+icuId);
    console.log("Test: obje icuId="+this.patientObj.icuId);
    if(icuId==this.oldIcu){
      this.isDefaultIcu=true;
    }
    else{
      this.isDefaultIcu=false;
    }
    let observable=this.patientService.getAvailableIcuBeds(icuId);
    observable.subscribe((data:any)=>{
      console.log(data);
      this.bedList=data;
      if(data.length==0 && icuId!=this.oldIcu){
        this.errorMessage='No beds Available in the ICU';
      }
      else{
        this.errorMessage='';
      }
    },
    (error:any)=>{
      console.log("Unable to fetch bed");
      console.log(error);
    },
    ()=>{
      console.log('Completed getting bed');
    });
  }
  
  onBedChange(){
    this.isBedChanged=true;
  }
  
  onUpdate(){
    
    console.log(this.patientObj);
    if(this.isBedChanged){
      this.patientObj.bedId=this.bedId;
    }
    
    let observable=this.patientService.updatePatient(this.patientObj);
    observable.subscribe((data:any)=>{
      console.log(data);
      this.message="Patient Updated Successfully";
    },
    (error:any)=>{
      console.log(error);
    },
    ()=>{
      console.log("Updation completed");
    });
  }
  onReset(){
    this.message='';
    this.errorMessage='';
    
  }

}
