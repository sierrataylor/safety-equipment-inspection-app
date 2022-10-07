import { Component, OnInit } from '@angular/core';
import { SharedService } from 'src/app/shared.service';

@Component({
  selector: 'app-inspection-form',
  templateUrl: './inspection-form.component.html',
  styleUrls: ['./inspection-form.component.css']
})
export class InspectionFormComponent implements OnInit {

  constructor(public service: SharedService) { }

  EquipmentItem: any;

  ngOnInit(): void {
  }

  GetEquipmentItem(equipmentId: any) {
    this.service.GetEquipmentItem(equipmentId).subscribe(data => {
      this.EquipmentItem = data;
    })
  }

}
