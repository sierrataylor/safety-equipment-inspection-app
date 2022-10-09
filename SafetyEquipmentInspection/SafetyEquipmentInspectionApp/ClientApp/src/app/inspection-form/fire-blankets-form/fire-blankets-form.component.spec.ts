import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FireBlanketsFormComponent } from './fire-blankets-form.component';

describe('FireBlanketsFormComponent', () => {
  let component: FireBlanketsFormComponent;
  let fixture: ComponentFixture<FireBlanketsFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FireBlanketsFormComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FireBlanketsFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
