import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
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
    this.GetEquipmentList("fire extinguisher");
  }

  GetEquipmentList(equipmentType: any) {
    return this.service.GetEquipmentList(equipmentType).subscribe(data => {
      this.equipments = data;
      console.log(this.equipments);
    });
  }

}
