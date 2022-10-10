import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FpLanyardFormComponent } from './fp-lanyard-form.component';

describe('FpLanyardFormComponent', () => {
  let component: FpLanyardFormComponent;
  let fixture: ComponentFixture<FpLanyardFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FpLanyardFormComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FpLanyardFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
