
import { HttpClient } from '@angular/common/http';
import {  Injectable } from '@angular/core';
// import { interval, Observable, Subject, Subscription, timer} from 'rxjs';


@Injectable()
export class CommonServices{
    
    
    constructor(private httpClient:HttpClient){

    }
    
    getAllLayouts(){
        let observable=this.httpClient.get('http://localhost:61575/api/IcuConfiguration/Layouts/');
          return observable;
    }
       
       
    
}
