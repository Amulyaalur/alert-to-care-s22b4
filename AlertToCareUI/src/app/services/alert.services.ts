
import { HttpClient } from '@angular/common/http';
import {  Injectable, OnDestroy } from '@angular/core';
import { interval, Observable, Subject, Subscription, timer} from 'rxjs';
// import { switchMap, tap, share, retry, takeUntil, takeWhile} from 'rxjs/operators';

export interface AlertInfo {
     alertId:string;
     layoutId :string;
    icuId :string;
    bedId :string;
    patientId :string;
    patientName :string;
    bpm :number;
    spo2 :number;
    respRate :number;
    alertStatus:boolean;
}

@Injectable()
export class AlertService{
    
    baseUrl:string;
    stopPolling=new Subject();
    allVitals:Observable<AlertInfo[]>;
    patientAlerts=[];
    constructor(private httpClient:HttpClient){

    }
    
    getAllVitals():AlertInfo[]{
       let observable=this.httpClient.get<AlertInfo[]>('http://localhost:61575/api/PatientMonitoring/Alerts');
       observable.subscribe((data:any)=>{
            this.patientAlerts=data;
        },
       (error:any)=>{
            return error;
       },
       ()=>{
            return null;
       }
       );

        return this.patientAlerts;
       }

       stopAlert(alertId:string){
          let observable=this.httpClient.put('http://localhost:61575/api/PatientMonitoring/Alert/'+alertId,null);
          return observable;
       }
       removeAlert(alertId:string){
          let observable=this.httpClient.delete('http://localhost:61575/api/PatientMonitoring/Alert/'+alertId);
          return observable;
       }
       
       
    
}
