import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material';
import { AdminService } from '../services/admin.service';


@Component({
  selector: 'admin-all-icu-comp',
  templateUrl: './all-icu.component.html',
  styleUrls: ['./all-icu.component.css']
})
export class AllIcuComponent implements OnInit {
 // data:any;
  observable:any;
  dataSource=new MatTableDataSource<Icu>();
  displayedColumns=['icuId','layoutId','bedsCount'];
  adminService:any;
  constructor(private adminservice:AdminService) { 
    this.adminService=adminservice;
  }

  ngOnInit() {
    this.observable= this.adminService.getAllIcu();
    this.observable.subscribe((data:any)=>{
      //this.data=data;
      this.dataSource.data=data;
      console.log(data[0]);
    },
    (error:any)=>{
      console.log("Error");
    },
    ()=>{
      console.log("Completed");
    });
  }

}
class Icu{
  icuId:string;
  layoutId:string;
  bedsCount:number;
}
