import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';

@Injectable()
export class PatientService{
    httpClient:HttpClient
    baseUrl:string;
    constructor(httpClient:HttpClient){
        this.httpClient=httpClient;
       this.baseUrl="http://localhost:61575/api/";
    }
    getAllPatient(){
        let observableStream=this.httpClient.get(this.baseUrl+'IcuOccupancy/Patients');
        return observableStream;
    }
    getAvailableIcuBeds(icuId:string){
        let observableStream=this.httpClient.get(this.baseUrl+'IcuOccupancy/Beds/'+icuId);
        return observableStream;
    }
    admitPatient(patientObj:any){
        let observableStream=this.httpClient.post(this.baseUrl+'IcuOccupancy/Patient',patientObj);
        return observableStream;
    }
    getPatientById(patientId:string){
        let observableStream=this.httpClient.get(this.baseUrl+'IcuOccupancy/Patient/'+patientId);
        return observableStream;
    }
    // addIcu(data:any){
    //     console.log("Admin Add ICU method");
    //     console.log(data);
    //     let observableStream=this.httpClient.post(this.baseUrl+'IcuConfiguration/Icu',data);
    //     console.log(observableStream);
    //     return observableStream;
    // }
    // deleteIcu(icuId:string){
    //     console.log("Admin Delete ICU method "+icuId);
    //     let observableStream=this.httpClient.delete(this.baseUrl+"IcuConfiguration/Icu/"+icuId);
    //     return observableStream;
    // }
    // // getIcu(){

    // // }

    // getIcuById(icuId:string){
    //     let observableStream=this.httpClient.get(this.baseUrl+'IcuConfiguration/Icu/'+icuId);
    //     return observableStream;
    // }

    // update(data:any){
    //     let observableStream=this.httpClient.put(this.baseUrl+'IcuConfiguration/Icu/'+data.icuId,data);
    //     console.log(this.baseUrl+'IcuConfiguration/Icu/'+data.icuId);
    //     console.log(data);
    //     return observableStream;
    // }

    // getIcuList(){
    //     let icuId=[];
    //     let observable=this.getAllIcu();
    //     observable.subscribe((data:any)=>
    //     {
    //         for(let i=0;i<data.length;i++)
    //         {   
    //             icuId.push(data[i].icuId); 
    //         }
    //     },
    //     (error:any)=>{
    //     //   this.message="Error While adding ICU Duplicate ICU id";
    //        console.log(error);
    //     },
    //     ()=>{
    //         console.log("ICU list populated");
    //     });
    // return icuId;
    // }
   
}
