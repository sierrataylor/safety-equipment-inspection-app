import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PastInspectionsComponent } from './past-inspections.component';

describe('PastInspectionsComponent', () => {
  let component: PastInspectionsComponent;
  let fixture: ComponentFixture<PastInspectionsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PastInspectionsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PastInspectionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
