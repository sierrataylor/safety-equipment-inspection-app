import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SpillKitFormComponent } from './spill-kit-form.component';

describe('SpillKitFormComponent', () => {
  let component: SpillKitFormComponent;
  let fixture: ComponentFixture<SpillKitFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SpillKitFormComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SpillKitFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
