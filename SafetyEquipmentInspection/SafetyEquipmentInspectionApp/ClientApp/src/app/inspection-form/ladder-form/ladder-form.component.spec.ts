import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LadderFormComponent } from './ladder-form.component';

describe('LadderFormComponent', () => {
  let component: LadderFormComponent;
  let fixture: ComponentFixture<LadderFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LadderFormComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LadderFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
