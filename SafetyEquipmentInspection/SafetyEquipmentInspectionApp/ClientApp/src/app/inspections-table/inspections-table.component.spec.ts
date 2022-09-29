import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InspectionsTableComponent } from './inspections-table.component';

describe('InspectionsTableComponent', () => {
  let component: InspectionsTableComponent;
  let fixture: ComponentFixture<InspectionsTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ InspectionsTableComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(InspectionsTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
