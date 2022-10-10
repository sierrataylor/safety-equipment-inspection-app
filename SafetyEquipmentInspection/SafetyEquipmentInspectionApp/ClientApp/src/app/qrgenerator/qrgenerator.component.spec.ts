import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ScannerComponent } from './qrgenerator.component';

describe('qrgeneratorComponent', () => {
  let component: qrgeneratorComponent;
  let fixture: ComponentFixture<ScannerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [qrgeneratorComponent]
    })
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(qrgeneratorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

