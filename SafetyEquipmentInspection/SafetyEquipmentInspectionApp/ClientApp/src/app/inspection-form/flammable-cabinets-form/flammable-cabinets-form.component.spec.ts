import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlammableCabinetsFormComponent } from './flammable-cabinets-form.component';

describe('FlammableCabinetsFormComponent', () => {
  let component: FlammableCabinetsFormComponent;
  let fixture: ComponentFixture<FlammableCabinetsFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlammableCabinetsFormComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlammableCabinetsFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
