import { formattedError } from '@angular/compiler';
import { Component, OnInit } from '@angular/core';
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

  layoutId=[
    {
      id:'LID01',name:'LShape'
    },
    {
      id:'LID02',name:'Parallel'
    },
    {
      id:'LID03',name:'SingleRow'
    },
    {id:'LID04',name:'UShape'}
  ]
  adminService:any;
  
  constructor(private adminservice:AdminService) {
    this.adminService=adminservice;
  
   }
   getIcuDetails(){
      this.isChanged=true;
    
      this.isSubmitted=true;
      
      let observable=this.icuObj=this.adminService.getIcuById(this.icu);
      
      observable.subscribe((data:any)=>{
        this.icuObj=data;
        console.log(this.icuObj);
        this.bedCount=this.icuObj.bedsCount;
        for(let i=0;i<this.layoutId.length;i++)
        {
          if(this.layoutId[i].id==this.icuObj.layoutId)
          {
            this.layoutName=this.layoutId[i].name;
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
    for(let i=0;i<this.layoutId.length;i++)
        {
          if(this.layoutId[i].name==this.layouts)
          {
            this.newLayoutId=this.layoutId[i].id;
            this.layoutName=this.layoutId[i].name;
            
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
