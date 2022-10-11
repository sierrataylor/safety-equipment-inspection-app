import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EyewashShowerFormComponent } from './eyewash-shower-form.component';

describe('EyewashShowerFormComponent', () => {
  let component: EyewashShowerFormComponent;
  let fixture: ComponentFixture<EyewashShowerFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EyewashShowerFormComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EyewashShowerFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
