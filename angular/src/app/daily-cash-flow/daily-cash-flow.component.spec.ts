import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DailyCashFlowComponent } from './daily-cash-flow.component';

describe('DailyCashFlowComponent', () => {
  let component: DailyCashFlowComponent;
  let fixture: ComponentFixture<DailyCashFlowComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DailyCashFlowComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DailyCashFlowComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
