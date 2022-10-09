import { Component, OnInit } from '@angular/core';
import { SharedService } from 'src/app/shared.service';
import { EquipmentDto } from '../SharedDTO/equipment.dto';

@Component({
  selector: 'app-inspection-form',
  templateUrl: './inspection-form.component.html',
  styleUrls: ['./inspection-form.component.css']
})
export class InspectionFormComponent implements OnInit {

  constructor(public service: SharedService) { }

  EquipmentItem: EquipmentDto | undefined;

  ngOnInit(): void {
    this.GetEquipmentItem("328e66b6-0e86-4b07-89db-da2cc345c5e6");
  }

  GetEquipmentItem(equipmentId: any) {
    return this.service.GetEquipmentItem(equipmentId).subscribe(data => {
      this.EquipmentItem = data;
      console.log(this.EquipmentItem);
    })
  }
}
