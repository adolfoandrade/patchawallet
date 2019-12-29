import { TestBed } from '@angular/core/testing';

import { StockTransactionService } from './stock-transaction.service';

describe('StockTransactionService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: StockTransactionService = TestBed.get(StockTransactionService);
    expect(service).toBeTruthy();
  });
});
