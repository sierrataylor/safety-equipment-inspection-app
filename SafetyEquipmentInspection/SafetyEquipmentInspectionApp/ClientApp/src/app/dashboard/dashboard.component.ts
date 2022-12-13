import { Component, OnInit } from '@angular/core';
import { LogInComponent } from '../log-in/log-in.component';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

  ShowAdministrativeButtons() {
    if (LogInComponent.SignedInEmployee != undefined) {
      return LogInComponent.SignedInEmployee.isAdmin;
    }
    return false;
  }

}
