import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { SharedService } from '../shared.service';
import { EmployeeDto } from '../SharedDTO/employee.dto';

@Component({
  selector: 'app-add-user',
  templateUrl: './add-user.component.html',
  styleUrls: ['./add-user.component.css']
})
export class AddUserComponent implements OnInit {

  public addUserForm!: FormGroup;
  public NewEmployee: EmployeeDto | undefined = {
    employeeId: "",
    firstName: "",
    lastName: "",
    email: "",
    role: "",
    password: "",
    isAdmin: false,
    isSuperAdmin: false
  };

  employeeId: string = "";
  firstName: string = "";
  lastName: string = "";
  email: string = "";
  role: string = "";
  employeePassword: string = "";
  isAdmin: boolean = false;
  isSuperAdmin: boolean = false;

  constructor(public service: SharedService, private formBuilder: FormBuilder, private router: Router) { }

  ngOnInit(): void {
  }

  async AddUser(form: NgForm) {
    await this.service.AddEmployee(this.employeeId, this.firstName, this.lastName, this.email, this.role, this.employeePassword, this.isAdmin, this.isSuperAdmin);
  }

}
