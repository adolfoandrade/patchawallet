import { TestBed } from '@angular/core/testing';

import { StockTransactionsService } from './stock-transactions.service';

describe('StockTransactionsService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: StockTransactionsService = TestBed.get(StockTransactionsService);
    expect(service).toBeTruthy();
  });
});
