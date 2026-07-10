import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AtencionMainViewComponent } from './atencion-main-view.component';

describe('AtencionMainViewComponent', () => {
  let component: AtencionMainViewComponent;
  let fixture: ComponentFixture<AtencionMainViewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AtencionMainViewComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(AtencionMainViewComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
