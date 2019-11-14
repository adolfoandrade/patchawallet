import { RouterReducerState } from '@ngrx/router-store';
import { IStockTransactionState, initialStockTransactionState } from './stocktransaction.state';

export interface IAppState {
    stockTransaction: IStockTransactionState;
    router: RouterReducerState;
}

export const initialAppState: IAppState = {
    router: null,
    stockTransaction: initialStockTransactionState
}

export function getInitialState(): IAppState {
    return initialAppState;
}
