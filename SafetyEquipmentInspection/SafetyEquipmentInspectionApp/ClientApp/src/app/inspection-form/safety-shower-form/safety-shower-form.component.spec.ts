import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SafetyShowerFormComponent } from './safety-shower-form.component';

describe('SafetyShowerFormComponent', () => {
  let component: SafetyShowerFormComponent;
  let fixture: ComponentFixture<SafetyShowerFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SafetyShowerFormComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SafetyShowerFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
