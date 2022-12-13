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

  public static CurrentUserEmployee: EmployeeDto | undefined = {
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
  oldPassword: string = "";

  constructor(public service: SharedService, private formBuilder: FormBuilder, private router: Router) { }

  ngOnInit(): void {
    this.forgotPasswordForm = this.formBuilder.group({
      employeeId: ["", Validators.required],
      firstName: ["", Validators.required],
      lastName: ["", Validators.required],
      newPassword: ["", Validators.required],
      oldPassword: ["", Validators.required]
    })
  }

  async ResetEmployeePassword(form: NgForm) {
    await this.GetEmployee(form.controls.employeeId.value);

    if (ForgotPasswordComponent.CurrentUserEmployee && ForgotPasswordComponent.CurrentUserEmployee.password === this.oldPassword) {
      await this.service.ResetEmployeePassword(this.employeeId, this.newPassword).toPromise();
      alert("Password successfully changed!");
      this.router.navigate(['/']);
    } else {
      alert("Hmmm... we seemd to have encountered an error. Please ensure that your EmployeeID and old password were entered correctly.");
    }
  }

  async GetEmployee(employeeId: any) {
    let data = await this.service.GetEmployee(employeeId).toPromise();
    if (ForgotPasswordComponent.CurrentUserEmployee && data) {
      ForgotPasswordComponent.CurrentUserEmployee.employeeId = data.employeeId;
      ForgotPasswordComponent.CurrentUserEmployee.firstName = data.firstName;
      ForgotPasswordComponent.CurrentUserEmployee.lastName = data.lastName;
      ForgotPasswordComponent.CurrentUserEmployee.role = data.role;
      ForgotPasswordComponent.CurrentUserEmployee.email = data.email;
      ForgotPasswordComponent.CurrentUserEmployee.password = data.password;
      ForgotPasswordComponent.CurrentUserEmployee.isAdmin = data.isAdmin;
      ForgotPasswordComponent.CurrentUserEmployee.isSuperAdmin = data.isSuperAdmin;
    }
  }

}
