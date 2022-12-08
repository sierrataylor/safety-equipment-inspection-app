import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, NgForm, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { SharedService } from '../shared.service';
import { EmployeeDto } from '../SharedDTO/employee.dto';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.css']
})
export class ForgotPasswordComponent implements OnInit {

  public CurrentUserEmployee: EmployeeDto | undefined = {
    employeeId: "",
    firstName: "",
    lastName: "",
    email: "",
    role: "",
    password: "",
    isAdmin: false,
    isSuperAdmin: false
  };

  public forgotPasswordForm!: FormGroup;
  employeeId: string = "";
  firstName: string = "";
  lastName: string = "";
  newPassword: string = "";

  constructor(public service: SharedService, private formBuilder: FormBuilder, private router: Router) { }

  ngOnInit(): void {
    this.forgotPasswordForm = this.formBuilder.group({
      employeeId: ["", Validators.required],
      firstName: ["", Validators.required],
      lastName: ["", Validators.required],
      newPassword: ["", Validators.required]
    })
  }

  ResetEmployeePassword(form: NgForm) {
    this.GetEmployee(form.controls.employeeId.value);
    console.log(this.employeeId);
    console.log(form.controls.employeeId.value);
    //console.log(this.CurrentUserEmployee);
    //console.log(this.CurrentUserEmployee.employeeId);
    if (this.CurrentUserEmployee) {
      this.service.ResetEmployeePassword(this.employeeId, this.firstName, this.lastName, this.newPassword).subscribe(response => {
        console.log(response);
      });
      alert("Password successfully changed!");
      this.router.navigate(['/']);
    } else {
      alert("Something went wrong with the passord reset process. Please ensure that your EmployeeID and name were entered correctly.");
      this.router.navigate(['/']);
    }
  }

  GetEmployee(employeeId: any) {
    //console.log(employeeId);
    return this.service.GetEmployee(employeeId).subscribe((data: EmployeeDto) => {
      if (this.CurrentUserEmployee != undefined) {
        this.CurrentUserEmployee.employeeId = data.employeeId;
        this.CurrentUserEmployee.firstName = data.firstName;
        this.CurrentUserEmployee.lastName = data.lastName;
        this.CurrentUserEmployee.role = data.role;
        this.CurrentUserEmployee.email = data.email;
        this.CurrentUserEmployee.password = data.password;
        this.CurrentUserEmployee.isAdmin = data.isAdmin;
        this.CurrentUserEmployee.isSuperAdmin = data.isSuperAdmin;

      }
    });
  }

}
