import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AedFormComponent } from './aed-form.component';

describe('AedFormComponent', () => {
  let component: AedFormComponent;
  let fixture: ComponentFixture<AedFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AedFormComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AedFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
