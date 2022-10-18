import { Component, OnInit } from '@angular/core';
import { SharedService } from 'src/app/shared.service';
import { EmployeeDto } from '../SharedDTO/employee.dto';


@Component({
  selector: 'app-log-in',
  templateUrl: './log-in.component.html',
  styleUrls: ['./log-in.component.css']
})
export class LogInComponent implements OnInit {

  public Employee: EmployeeDto | undefined;
  employeeId: string = "";
  employeePassword: string = "";


  constructor(public service: SharedService) { }

  ngOnInit(): void {
  }

  LogInUser() {
    this.GetEmployee(this.employeeId);
    if (this.Employee?.password == this.employeePassword) {
      //log user in and send to dashboard page
    } else {
      //alert log in failed and clear fields
    }
  }

  GetEmployee(employeeId: any) {
    console.log(employeeId);
    return this.service.GetEmployee(employeeId).subscribe(data => {
      this.Employee = data;
      console.log(this.Employee);
      console.log(this.Employee.employeeId);
      console.log(this.Employee.email);
    })
  }

}
