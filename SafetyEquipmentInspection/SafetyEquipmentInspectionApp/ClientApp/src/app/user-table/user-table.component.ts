import { Component, OnInit } from '@angular/core';
import { SharedService } from '../shared.service';
import { EmployeeDto } from '../SharedDTO/employee.dto';

@Component({
  selector: 'app-user-table',
  templateUrl: './user-table.component.html',
  styleUrls: ['./user-table.component.css']
})
export class UserTableComponent implements OnInit {

  employees: EmployeeDto[] | any;

  constructor(public service: SharedService) { }

  ngOnInit(): void {
    this.GetEmployeeList();
  }

  GetEmployeeList() {
    return this.service.GetEmployeeList().subscribe(data => {
      this.employees = data;
    });
  }

}
