import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HarnessFormComponent } from './harness-form.component';

describe('HarnessFormComponent', () => {
  let component: HarnessFormComponent;
  let fixture: ComponentFixture<HarnessFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ HarnessFormComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(HarnessFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
