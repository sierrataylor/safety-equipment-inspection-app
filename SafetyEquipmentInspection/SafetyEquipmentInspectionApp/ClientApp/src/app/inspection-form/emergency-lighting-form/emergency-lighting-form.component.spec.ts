import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmergencyLightingFormComponent } from './emergency-lighting-form.component';

describe('EmergencyLightingFormComponent', () => {
  let component: EmergencyLightingFormComponent;
  let fixture: ComponentFixture<EmergencyLightingFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EmergencyLightingFormComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EmergencyLightingFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
