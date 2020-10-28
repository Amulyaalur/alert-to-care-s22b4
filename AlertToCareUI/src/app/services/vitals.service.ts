
import { HttpClient } from '@angular/common/http';
import {  Injectable } from '@angular/core';
import { Observable, Subject} from 'rxjs';
// import { switchMap, tap, share, retry, takeUntil, takeWhile} from 'rxjs/operators';

export interface VitalInfo {
    name:string ;
    PatientId :string;
    Bpm : number;
    Spo2 :number;
    RespRate: number;
}

@Injectable()
export class VitalService{
    
    baseUrl:string;
    stopPolling=new Subject();
    allVitals:Observable<VitalInfo[]>;
    patientVital=[];
    constructor(private httpClient:HttpClient){

    }
    
    getAllVitals():VitalInfo[]{
       let observable=this.httpClient.get<VitalInfo[]>('http://localhost:61575/api/PatientMonitoring/Vitals');
       observable.subscribe((data:any)=>{
            this.patientVital=data;
        },
       (error:any)=>{
            return error;
       },
       ()=>{
            return null;
       }
       );

        return this.patientVital;
       }
       
    
}
