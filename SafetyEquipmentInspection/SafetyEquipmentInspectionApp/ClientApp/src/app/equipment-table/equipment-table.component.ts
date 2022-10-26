import { Component, OnInit } from '@angular/core';
import { SharedService } from 'src/app/shared.service';
import { EquipmentDto } from '../SharedDTO/equipment.dto';

@Component({
  selector: 'app-equipment-table',
  templateUrl: './equipment-table.component.html',
  styleUrls: ['./equipment-table.component.css']
})
export class EquipmentTableComponent implements OnInit {

  readonly APIUrl = "https://localhost:7023/";
  equipments: EquipmentDto[] | undefined;


  constructor(public service: SharedService) { }


  ngOnInit(): void {
    this.GetEquipmentList("");
  }

  GetEquipmentList(equipmentType?: any) {
    return this.service.GetEquipmentList(equipmentType).subscribe(data => {
      this.equipments = data;
    });
  }

  async DeleteEquipmentItem(equipmentId: any) {
    await this.service.DeleteEquipmentItem(equipmentId);
    this.GetEquipmentList("fire extinguisher");
  }

}
