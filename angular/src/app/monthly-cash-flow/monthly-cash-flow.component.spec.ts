import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MonthlyCashFlowComponent } from './monthly-cash-flow.component';

describe('MonthlyCashFlowComponent', () => {
  let component: MonthlyCashFlowComponent;
  let fixture: ComponentFixture<MonthlyCashFlowComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MonthlyCashFlowComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MonthlyCashFlowComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
