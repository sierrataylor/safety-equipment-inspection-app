import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EyewashFormComponent } from './eyewash-form.component';

describe('EyewashFormComponent', () => {
  let component: EyewashFormComponent;
  let fixture: ComponentFixture<EyewashFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EyewashFormComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EyewashFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
