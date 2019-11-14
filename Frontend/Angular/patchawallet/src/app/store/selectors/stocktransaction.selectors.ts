import { createSelector } from '@ngrx/store';
import { IAppState } from '../state/app.state';
import { IStockTransactionState } from '../state/stocktransaction.state';

const selectIStockTransactionState = (state: IAppState) => state.stockTransaction;

export const selectPostStockTransaction = createSelector(
    selectIStockTransactionState,
    (state: IStockTransactionState) => state.createOrUpdateStockTransaction
);

