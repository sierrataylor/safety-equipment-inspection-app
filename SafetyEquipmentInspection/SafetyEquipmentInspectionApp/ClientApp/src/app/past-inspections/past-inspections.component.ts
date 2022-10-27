import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { SharedService } from 'src/app/shared.service';
import { InspectionDto } from '../SharedDTO/inspection.dto';

@Component({
  selector: 'app-past-inspections',
  templateUrl: './past-inspections.component.html',
  styleUrls: ['./past-inspections.component.css']
})
export class PastInspectionsComponent implements OnInit {
  readonly APIUrl = "https://localhost:7023/";
  inspections: InspectionDto[] | any;

  constructor(public service: SharedService) { }

  ngOnInit(): void {
    this.GetInspectionsList();
  }
  GetInspectionsList() {
    return this.service.GetInspectionsList().subscribe(data => {
      this.inspections = data;
    });
  }

}
