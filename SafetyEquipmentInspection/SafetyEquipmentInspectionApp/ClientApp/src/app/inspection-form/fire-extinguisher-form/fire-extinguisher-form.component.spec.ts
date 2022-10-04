import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FireExtinguisherFormComponent } from './fire-extinguisher-form.component';

describe('FireExtinguisherFormComponent', () => {
  let component: FireExtinguisherFormComponent;
  let fixture: ComponentFixture<FireExtinguisherFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FireExtinguisherFormComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FireExtinguisherFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
