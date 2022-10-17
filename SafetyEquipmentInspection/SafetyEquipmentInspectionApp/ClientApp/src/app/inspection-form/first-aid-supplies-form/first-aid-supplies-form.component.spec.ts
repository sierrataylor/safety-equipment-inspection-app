import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FirstAidSuppliesFormComponent } from './first-aid-supplies-form.component';

describe('FirstAidSuppliesFormComponent', () => {
  let component: FirstAidSuppliesFormComponent;
  let fixture: ComponentFixture<FirstAidSuppliesFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FirstAidSuppliesFormComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FirstAidSuppliesFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
