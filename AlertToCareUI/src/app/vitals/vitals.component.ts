import { Component, OnInit } from '@angular/core';
import { VitalInfo, VitalService } from '../services/vitals.service';
import { timer} from 'rxjs';
import {takeWhile} from 'rxjs/operators';

@Component({
  selector: 'vital-comp',
  templateUrl: './vitals.component.html',
  styleUrls: ['./vitals.component.css']
})
export class VitalsComponent implements OnInit {
  //isFirst=true;
  vitalInfo:VitalInfo[];

  constructor(private vitalService:VitalService){

    // timer(1,50).pipe(takeWhile(value => value < 2)).
    // subscribe(
    //   (value:number) => {
       
    //    this.alertInfo=this.alertService.getAllVitals();
       
    //    if(this.alertInfo.length==0){
    //      this.isEmptyList=true;
       
    //    }
    //    else{
    //      this.isEmptyList=false;
         
    //    }
       
    //     console.log(this.alertInfo);
    //   }
    // );

    timer(1,50).pipe(takeWhile(value => value < 2)).subscribe(
      (value:number) => {
       this.vitalInfo=this.vitalService.getAllVitals();
      
        console.log(this.vitalInfo);
      }
    );

    timer(1,3000).pipe().subscribe(
      () => {
       this.vitalInfo=this.vitalService.getAllVitals();
      
        console.log(this.vitalInfo);
      }
    );
  }
  
  ngOnInit() {
 
  }
  
}
