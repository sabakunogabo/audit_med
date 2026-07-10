import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AtencionTableComponent } from './atencion-table.component';

describe('AtencionTable', () => {
  let component: AtencionTableComponent;
  let fixture: ComponentFixture<AtencionTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AtencionTableComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(AtencionTableComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
