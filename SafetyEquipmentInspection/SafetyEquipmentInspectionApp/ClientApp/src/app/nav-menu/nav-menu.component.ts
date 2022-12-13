import { Component } from '@angular/core';
import { LogInComponent } from '../log-in/log-in.component';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  isExpanded = false;

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  ShowAdministrativeSettings() {
    if (LogInComponent.SignedInEmployee != undefined) {
      return LogInComponent.SignedInEmployee.isAdmin;
    }
    return false;
  }

  LogOutUser() {
    if (LogInComponent.SignedInEmployee != undefined) {
      LogInComponent.SignedInEmployee.employeeId = "";
      LogInComponent.SignedInEmployee.firstName = "";
      LogInComponent.SignedInEmployee.lastName = "";
      LogInComponent.SignedInEmployee.email = "";
      LogInComponent.SignedInEmployee.role = "";
      LogInComponent.SignedInEmployee.password = "";
      LogInComponent.SignedInEmployee.isAdmin = false;
      LogInComponent.SignedInEmployee.isSuperAdmin = false;
    }
  }
}
