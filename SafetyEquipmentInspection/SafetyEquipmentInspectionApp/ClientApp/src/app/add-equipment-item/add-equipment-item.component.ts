import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { SharedService } from '../shared.service';

@Component({
  selector: 'app-add-equipment-item',
  templateUrl: './add-equipment-item.component.html',
  styleUrls: ['./add-equipment-item.component.css']
})
export class AddEquipmentItemComponent implements OnInit {

  public addItemForm!: FormGroup;
  equipmentType: string = ""
  building: string = ""
  floor: string = ""
  location: string = ""

  constructor(public service: SharedService, public router: Router, public formBuilder: FormBuilder) { }

  ngOnInit(): void {
  }

  async AddItem(form: NgForm) {
    await this.service.AddEquipmentItem(this.equipmentType, this.building, parseInt(this.floor), this.location)
  }

}
