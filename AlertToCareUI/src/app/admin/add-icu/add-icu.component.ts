import { Component, OnInit } from '@angular/core';
import { AdminService } from '../services/admin.service';

@Component({
  selector: 'admin-add-icu-comp',
  templateUrl: './add-icu.component.html',
  styleUrls: ['./add-icu.component.css']
})
export class AddIcuComponent implements OnInit {

  icuId:string;
  bedCount:number;
  layoutId:any;
  layouts:string;
  isSubmitted=false;
  imageSrc:string;
  adminService:any;
  message:string;
  constructor(private adminservice:AdminService) {
    this.adminService=adminservice;
    // let a=
    this.layoutId=[
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
                  ];
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
     for(let i=0;i<this.layoutId.length;i++)
     {
       console.log(this.layoutId[i].name+" : "+this.layouts);
        if(this.layoutId[i].name==this.layouts)
        {
          layoutId=this.layoutId[i].id;
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
