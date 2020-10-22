import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdminHomeComponent } from './admin-home/admin-home.component';
import { LayoutModule } from '../layout/layout.module';
import { AddIcuComponent } from './add-icu/add-icu.component';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { RemoveIcuComponent } from './remove-icu/remove-icu.component';
import { AllIcuComponent } from './all-icu/all-icu.component';
import { UpdateIcuComponent } from './update-icu/update-icu.component';
import { AdminService } from './services/admin.service';
import { MatTableModule } from '@angular/material';



@NgModule({
  declarations: [AdminHomeComponent, AddIcuComponent, RemoveIcuComponent, AllIcuComponent, UpdateIcuComponent],
  imports: [
    CommonModule,LayoutModule,RouterModule,FormsModule,MatTableModule
  ],
  providers:[{provide:AdminService,useClass:AdminService}]
})
export class AdminModule { }
