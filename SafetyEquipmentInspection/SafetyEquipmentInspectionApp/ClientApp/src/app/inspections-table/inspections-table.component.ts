import { Component, OnInit } from '@angular/core';
import { SharedService } from 'src/app/shared.service';
import { InspectionDto } from '../SharedDTO/inspection.dto';

@Component({
  selector: 'app-inspections-table',
  templateUrl: './inspections-table.component.html',
  styleUrls: ['./inspections-table.component.css']
})
export class InspectionsTableComponent implements OnInit {

  readonly APIUrl = "https://localhost:7023/";
  inspections: InspectionDto[] | undefined;

  constructor(public service: SharedService) { }
  

  ngOnInit(): void {
    this.GetPastInspections();
  }

  GetPastInspections(equipmentId: any = "") {
    return this.service.GetInspectionsList(equipmentId).subscribe(data => {
      this.inspections = data;
    });
  }

}
