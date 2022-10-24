import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { SharedService } from 'src/app/shared.service';
import { EmployeeDto } from '../SharedDTO/employee.dto';


@Component({
  selector: 'app-log-in',
  templateUrl: './log-in.component.html',
  styleUrls: ['./log-in.component.css']
})
export class LogInComponent implements OnInit {

  public SignedInEmployee: EmployeeDto | undefined;
  employeeId: string = "";
  employeePassword: string = "";
  public loginForm!: FormGroup;

  constructor(public service: SharedService, private formBuilder : FormBuilder) { }

  ngOnInit(): void {
    this.loginForm = this.formBuilder.group({
      employeeId: ["", Validators.required],
      employeePassword:["", Validators.required]
    })
  }

  LogInUser() {
    this.GetEmployee(this.loginForm.value.employeeId);
    console.log(this.SignedInEmployee);


    if (this.SignedInEmployee?.password == this.loginForm.value.employeePassword) {
      console.log("Signed In!");
      this.loginForm.reset();
    } else {
      console.log("User EmployeeId and/or Password Not Found");
      this.loginForm.reset();
    }
  }

  GetEmployee(employeeId: any) {
    console.log(employeeId);
    return this.service.GetEmployee(employeeId).subscribe(data => {
      this.SignedInEmployee = data;
      console.log("finish querying employee");
    });
  }

}
