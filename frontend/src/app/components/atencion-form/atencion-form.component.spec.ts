import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AtencionFormComponent } from './atencion-form.component';

describe('AtencionForm', () => {
  let component: AtencionFormComponent;
  let fixture: ComponentFixture<AtencionFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AtencionFormComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(AtencionFormComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
