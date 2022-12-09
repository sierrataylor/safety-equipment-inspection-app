import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, NgForm, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { SharedService } from 'src/app/shared.service';
import { EmployeeDto } from '../SharedDTO/employee.dto';


@Component({
  selector: 'app-log-in',
  templateUrl: './log-in.component.html',
  styleUrls: ['./log-in.component.css']
})
export class LogInComponent implements OnInit {

  public SignedInEmployee: EmployeeDto | undefined = {
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
  employeePassword: string = "";
  public loginForm!: FormGroup;

  constructor(public service: SharedService, private formBuilder: FormBuilder, private router: Router) { }

  ngOnInit(): void {
    this.loginForm = this.formBuilder.group({
      employeeId: ["", Validators.required],
      employeePassword:["", Validators.required]
    })
  }

  LogInUser(form: NgForm) {
    this.GetEmployee(form.controls.employeeId.value);
    if (this.SignedInEmployee?.employeeId == this.employeeId && this.SignedInEmployee?.password == this.employeePassword) {
      if (this.SignedInEmployee.isAdmin = true) {
        console.log("Signed in, Admin!");
        this.router.navigate(['/admin-dashboard']);
      } else {
        console.log("Signed In!");
        this.router.navigate(['/dashboard']);

      }
      //this.loginForm.reset();
    } else {
      console.log("User EmployeeId and/or Password Not Found");
      this.loginForm.reset();
    }
  }

  GetEmployee(employeeId: any) {
    //console.log(employeeId);
    return this.service.GetEmployee(employeeId).subscribe((data: EmployeeDto) => {
      if (this.SignedInEmployee != undefined) {
        this.SignedInEmployee.employeeId = data.employeeId;
        this.SignedInEmployee.firstName = data.firstName;
        this.SignedInEmployee.lastName = data.lastName;
        this.SignedInEmployee.role = data.role;
        this.SignedInEmployee.email = data.email;
        this.SignedInEmployee.password = data.password;
        this.SignedInEmployee.isAdmin = data.isAdmin;
        this.SignedInEmployee.isSuperAdmin = data.isSuperAdmin;

      }
    });
  }

}
