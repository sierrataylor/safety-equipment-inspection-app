import { Component, OnInit } from '@angular/core';
import { SharedService } from 'src/app/shared.service';

@Component({
  selector: 'app-inspections-table',
  templateUrl: './inspections-table.component.html',
  styleUrls: ['./inspections-table.component.css']
})
export class InspectionsTableComponent implements OnInit {

  constructor(public service: SharedService) { }
  

  ngOnInit(): void {
  }

}
