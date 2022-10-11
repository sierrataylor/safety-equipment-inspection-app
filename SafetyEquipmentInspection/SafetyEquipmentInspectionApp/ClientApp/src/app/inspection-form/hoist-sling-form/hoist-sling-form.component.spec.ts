import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HoistSlingFormComponent } from './hoist-sling-form.component';

describe('HoistSlingFormComponent', () => {
  let component: HoistSlingFormComponent;
  let fixture: ComponentFixture<HoistSlingFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ HoistSlingFormComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(HoistSlingFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
