import { TestBed } from '@angular/core/testing';

import { DataTransmitterServiceService } from './data-transmitter-service.service';

describe('DataTransmitterServiceService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: DataTransmitterServiceService = TestBed.get(DataTransmitterServiceService);
    expect(service).toBeTruthy();
  });
});
