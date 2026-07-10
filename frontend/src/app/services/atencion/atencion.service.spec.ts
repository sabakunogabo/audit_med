import { TestBed } from '@angular/core/testing';
import { AtencionService } from './atencion.service';

describe('Atencion', () => {
  let service: AtencionService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AtencionService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
