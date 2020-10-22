import { Component, OnInit } from '@angular/core';
import { AdminService } from '../services/admin.service';

@Component({
  selector: 'admin-del-icu-comp',
  templateUrl: './remove-icu.component.html',
  styleUrls: ['./remove-icu.component.css']
})
export class RemoveIcuComponent implements OnInit {
  message='';
  icu:string;
  icuList:any;
  adminService:any;
  icuId=[];
  constructor(private adminservice:AdminService) {
    this.adminService=adminservice;
    
   }
   delete(){
     console.log('Delete Called');
     let observable=this.adminService.deleteIcu(this.icu);
     observable.subscribe((data:any)=>{
      
      console.log(data);
      this.message="ICU deleted Successfully";
    },
    (error:any)=>{
      this.message="Error While deleting ICU";
      console.log(error);
    },
    ()=>{
      
      console.log("Completed");
    });
   }
  ngOnInit() {
    
    this.icuId=this.adminService.getIcuList();
    
  }

}
