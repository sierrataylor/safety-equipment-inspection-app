import { Injectable } from '@angular/core';
import { HttpClient, } from '@angular/common/http';
import { HttpClientModule, HttpResponse } from '@angular/common/http';

import { Observable } from 'rxjs';
import { EquipmentDto } from './SharedDTO/equipment.dto';
import { EmployeeDto } from './SharedDTO/employee.dto';
import { InspectionDto } from './SharedDTO/inspection.dto';

@Injectable({
  providedIn: 'root'
})

export class SharedService {
  readonly APIUrl = "https://localhost:7023/";

  constructor(private http: HttpClient) { }


  //Equipment API Methods

  GetEquipmentItem(equipmentId: string): Observable<EquipmentDto> {
    return this.http.get<EquipmentDto>(this.APIUrl + "equipment/item/" + equipmentId);
  }

  GetEquipmentList(equipmentType: string = ""): Observable<EquipmentDto[]>{
    return this.http.get<EquipmentDto[]>(this.APIUrl + "equipment/items/" + equipmentType);
  }

  AddEquipmentItem(equipmentType: string, building: string, floor: number, location: string) {
    let EquipmentItem :any = {
          equipmentType,
          building,
          floor,
          location,
     };

    return this.http.post(this.APIUrl + "AddEquipmentPiece", EquipmentItem);
  }

  UpdateEquipmentItem(equipmentId: string, equipmentType: string, building: string, floor: number, location: string) {
    let EquipmentItem: any = {
          equipmentId,
          equipmentType,
          building,
          floor,
          location,
     };

    return this.http.put(this.APIUrl + "equipment/updateItem/" + equipmentId, EquipmentItem);
  }

  async DeleteEquipmentItem(equipmentId: string) {
    console.log("Deleting equipment item.");
    await this.http.delete(this.APIUrl + "equipment/deleteItem/" + equipmentId, {responseType: "text"}).toPromise();
  }

  AddInspection(equipmentId: string, inspectionDate: string, result: string, reviewer: string) {
    let InspectionData: any= {
      equipmentId,
      inspectionDate,
      result,
      reviewer,
    }

    return this.http.post(this.APIUrl + "/inspection/", InspectionData);
  }
  GetInspectionsList(equipmentId: any = "") {
    return this.http.get<InspectionDto[]>(this.APIUrl + "inspections/past/" + equipmentId)
  }
  GetToDoList(equipmentId: any = "") {
    return this.http.get<InspectionDto[]>(this.APIUrl + "inspections/todo/" + equipmentId)
  }
  //Employee API Methods

  GetEmployee(employeeId: string): Observable<EmployeeDto>{
    return this.http.get<EmployeeDto>(this.APIUrl + "employees/employee/" + employeeId);
  }

  AddEmployee(employeeId: string, firstName: string, lastname: string, email: string, role: string, password: string, isAdmin: boolean = false, isSuperAdmin: boolean = false) {
    let Employee: any = {
      employeeId,
      firstName,
      lastname,
      email,
      role,
      password,
      isAdmin,
      isSuperAdmin
    }

    return this.http.post(this.APIUrl + "employees/" + employeeId, Employee);
  }

  UpdateEmployee(employeeId: string, firstName?: string, lastname?: string, email?: string, role?: string, password? : string, isAdmin?: boolean, isSuperAdmin?: boolean) {
    let Employee: any = {
      employeeId,
      firstName,
      lastname,
      role,
      email,
      password,
      isAdmin,
      isSuperAdmin
    }
    return this.http.put(this.APIUrl + "employees/edit/" + employeeId, Employee);
  }

  DeleteEmployee(employeeId: string) {
    return this.http.delete(this.APIUrl + "employees/delete/" + employeeId).subscribe;
  }

  GetEmployeeList() {
    return this.http.get(this.APIUrl + "employees/");
  }

  //Password Reset Method
  ResetEmployeePassword(employeeId: string, firstName: string, lastName: string, newPassword: string) {
    return this.http.put(this.APIUrl + "employee/resetPassword/" + employeeId + "/"  + firstName + "/"+ lastName, newPassword);
  }

  //Question API Methods

  GetQuestionsList(equipmentType: string) {
    return this.http.get(this.APIUrl + "inspection/past/" + equipmentType);
  }

  AddQuestion(equipmentType: string, field: string, questionNum: number) {
    let QuestionData: any = {
      equipmentType,
      field,
      questionNum,
    }

    return this.http.post(this.APIUrl + "admin/quesitons/", QuestionData);
  }

  UpdateQuestion(questionId: string, equipmentType: string, questionNum: number, field: string) {
    let QuestionData: any = {
      questionId,
      equipmentType,
      field,
      questionNum,
    }

    return this.http.put(this.APIUrl + "admin/questions/editQuestion/" + questionId, QuestionData);
  }

  DeleteQuestion(questionId: string, equipmentId: string) {
    return this.http.delete(this.APIUrl + "admin/questions/deleteQuestion/" + questionId);
  }

  //Answer API Methods

  InputAnswer(equipmentId: string, questionNum: number, response: string) {
    let AnswerData: any = {
      equipmentId,
      questionNum,
      response,
    }

    return this.http.post(this.APIUrl + "inspections/answers/" + equipmentId + "/" + questionNum, AnswerData);
  }

}
