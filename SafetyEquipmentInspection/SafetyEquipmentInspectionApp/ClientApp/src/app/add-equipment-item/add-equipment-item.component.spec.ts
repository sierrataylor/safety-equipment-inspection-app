import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddEquipmentItemComponent } from './add-equipment-item.component';

describe('AddEquipmentItemComponent', () => {
  let component: AddEquipmentItemComponent;
  let fixture: ComponentFixture<AddEquipmentItemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddEquipmentItemComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddEquipmentItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
