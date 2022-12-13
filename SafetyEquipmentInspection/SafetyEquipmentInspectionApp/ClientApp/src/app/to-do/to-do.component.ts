import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { SharedService } from 'src/app/shared.service';
import { InspectionDto } from '../SharedDTO/inspection.dto';

@Component({
  selector: 'app-scanner',
  templateUrl: './to-do.component.html',
  styleUrls: ['./to-do.component.css']
})
export class ToDoComponent implements OnInit {

  readonly APIUrl = "https://localhost:7023/";
  inspections: InspectionDto[] | any;
  constructor(public service: SharedService) {}

  ngOnInit(): void {
    this.GetToDoList();
  }
  GetToDoList(equipmentId: any = "") {
    return this.service.GetToDoList(equipmentId).subscribe(data => {
      this.inspections = data;
    });
  }
}