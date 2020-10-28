import { Component, OnInit } from '@angular/core';
import { AlertInfo, AlertService } from '../services/alert.services';
import {timer} from 'rxjs';
import {takeWhile} from 'rxjs/operators';
import { CommonServices } from '../services/common.services';

@Component({
  selector: 'home-comp',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  alertInfo:AlertInfo[];
  isEmptyList=true;
  layoutList=[];
  constructor(private alertService:AlertService, private commonService:CommonServices){
    this.getIcuLayouts();
    timer(1,50).pipe(takeWhile(value => value < 2)).
    subscribe(
      (value:number) => {
       
       this.alertInfo=this.alertService.getAllVitals();
       
       if(this.alertInfo.length==0){
         this.isEmptyList=true;
       
       }
       else{
         this.isEmptyList=false;
         
       }
       
        console.log(this.alertInfo);
      }
    );
   
      timer(1,1000).pipe().subscribe(
        () => {
         this.alertInfo=this.alertService.getAllVitals();
         if(this.alertInfo.length==0){
           this.isEmptyList=true;
         }
         else{
           this.isEmptyList=false;
         }
        
          console.log(this.alertInfo);
        }
      );
    
  }

  stopAlert(alertId:string){
    
    let observable= this.alertService.stopAlert(alertId);
    observable.subscribe((data:any)=>{
      console.log(data)
    },
    (error:any)=>{
      console.log(error)
    },
    ()=>{
      console.log("Stopping Alert....");
    });

  }
  restartAlert(alertId:string){
    let observable= this.alertService.stopAlert(alertId);
    observable.subscribe((data:any)=>{
      console.log(data)
    },
    (error:any)=>{
      console.log(error)
    },
    ()=>{
      console.log("Alert Restarted....");
    });
  }
  removeAlert(alertId:string){
    let observable= this.alertService.removeAlert(alertId);
    observable.subscribe((data:any)=>{
      console.log(data)
    },
    (error:any)=>{
      console.log(error)
    },
    ()=>{
      console.log("Alert removed....");
    });
  }
  getIcuLayouts(){
    let observable=this.commonService.getAllLayouts();
    
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
  getLayoutImage(layoutId:string){
    
    let layoutName:string;
    this.layoutList.forEach(element => {
      if(element.layoutId==layoutId){
        layoutName=element.layoutName;
      }
    });
    return "../../assets/images/"+layoutName+".jpg";
  }
  ngOnInit() {
  }

}
