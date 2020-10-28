import { Component, OnInit } from '@angular/core';
import { CommonServices } from 'src/app/services/common.services';
import { AdminService } from '../services/admin.service';

@Component({
  selector: 'admin-add-icu-comp',
  templateUrl: './add-icu.component.html',
  styleUrls: ['./add-icu.component.css']
})
export class AddIcuComponent implements OnInit {

  icuId:string;
  bedCount:number;
  layoutList=[];
  layouts:string;
  isSubmitted=false;
  imageSrc:string;
  message:string;
  constructor(private adminService:AdminService,private commonServices:CommonServices) {
    this.getAllLayouts();
   
   }

   getAllLayouts(){
    let observable=this.commonServices.getAllLayouts();
    
    observable.subscribe((data:any)=>{
      this.layoutList=data;
      
      console.log(this.layoutList);
    },
    (error:any)=>{
      console.log(error);
    },
    ()=>{
      console.log("Layouts Fetched");
    });
   }

   dropdown(){
     console.log(this.layouts);
   }

   getLayout(){
     this.isSubmitted=true;
     this.imageSrc='../../../assets/images/'+this.layouts+'.jpg';
   }

   addFunction(){
     //Add Layout ID for loop here
     let layoutId:string;
     for(let i=0;i<this.layoutList.length;i++)
     {
       console.log(this.layoutList[i].layoutName+" : "+this.layouts);
        if(this.layoutList[i].layoutName==this.layouts)
        {
          layoutId=this.layoutList[i].layoutId;
        }
     }
     console.log(layoutId);
     let data=
     {
      icuId:this.icuId,
      layoutId:layoutId,
      bedsCount:this.bedCount
     };
     let observable=this.adminService.addIcu(data);
   
     console.log(this.icuId);
     console.log(this.layouts);
     console.log(this.bedCount);
     observable.subscribe((data:any)=>{
      
      console.log(data);
      this.message="ICU Added Successfully";
    },
    (error:any)=>{
      this.message="Error While adding ICU Duplicate ICU id";
      console.log(error);
    },
    ()=>{
      
      console.log("Completed adding");
    });

   }
   onReset(){
     this.message='';
     this.isSubmitted=false;
     this.icuId=undefined;
     this.bedCount=undefined;
     this.layouts=undefined;
   }
  ngOnInit() {
  }

}
