import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'nav-comp',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  role=localStorage.getItem('role');
  isAdmin=(this.role==='admin');
  constructor(private route:Router) { }

  ngOnInit() {
  }
  logout(){
    localStorage.clear();
    this.route.navigate(['/login']);
  }
}
