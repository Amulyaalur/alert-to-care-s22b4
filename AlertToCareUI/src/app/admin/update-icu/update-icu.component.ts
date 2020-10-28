import { formattedError } from '@angular/compiler';
import { Component, OnInit } from '@angular/core';
import { CommonServices } from 'src/app/services/common.services';
import { AdminService } from '../services/admin.service';

@Component({
  selector: 'admin-update-icu-comp',
  templateUrl: './update-icu.component.html',
  styleUrls: ['./update-icu.component.css']
})
export class UpdateIcuComponent implements OnInit {

  message='';
  icu:string;
  icuList:any;
  bedCount:number;
  layouts:string;
  layoutName:string
  isChanged=false;
  icuObj:any;
  isSubmitted=false;
  imageSrc:string;
  newLayoutId:string;
  layoutList=[]
  // layoutId=[
  //   {
  //     id:'LID01',name:'LShape'
  //   },
  //   {
  //     id:'LID02',name:'Parallel'
  //   },
  //   {
  //     id:'LID03',name:'SingleRow'
  //   },
  //   {id:'LID04',name:'UShape'}
  // ]
 // adminService:any;
  
  constructor(private adminService:AdminService,private commonServices:CommonServices) {
    //this.adminService=adminservice;
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

   getIcuDetails(){
      this.isChanged=true;
    
      this.isSubmitted=true;
      
      let observable=this.icuObj=this.adminService.getIcuById(this.icu);
      
      observable.subscribe((data:any)=>{
        this.icuObj=data;
        console.log(this.icuObj);
        this.bedCount=this.icuObj.bedsCount;
        for(let i=0;i<this.layoutList.length;i++)
        {
          if(this.layoutList[i].layoutId==this.icuObj.layoutId)
          {
            this.layoutName=this.layoutList[i].layoutName;
            this.imageSrc='../../../assets/images/'+this.layoutName+'.jpg';
          }
        }
        
      },
      (error:any)=>{
        console.log("Error");
      },
      ()=>{
        console.log("Completed");
      }); 
   }

   getLayoutImage(){
    //To get Layout ID
    for(let i=0;i<this.layoutList.length;i++)
        {
          if(this.layoutList[i].layoutName==this.layouts)
          {
            this.newLayoutId=this.layoutList[i].layoutId;
            this.layoutName=this.layoutList[i].layoutName;
            
          }
        }
    //To get Layout Image
     this.layoutName=this.layouts;
    this.imageSrc='../../../assets/images/'+this.layoutName+'.jpg';
   }

   onReset(){
     this.isChanged=false;
     this.icu=undefined;
     this.bedCount=undefined;
     this.layouts=undefined;
     this.message='';
   }

   onUpdate(){
   // console.log(this.icuObj);
     let data={
      icuId:this.icu,
      layoutId:this.newLayoutId,
      bedsCount:this.bedCount
     };
    console.log(data);
    let observable=this.adminService.update(data);
    
    observable.subscribe((data:any)=>{
      this.message="Updated successfully";
    },
    (error:any)=>{
     this.message='Error Updating';
    },
    ()=>{
      console.log("Completed");
    }); 
   }

  ngOnInit() {
   this.icuList=this.adminService.getIcuList();
  }

}
