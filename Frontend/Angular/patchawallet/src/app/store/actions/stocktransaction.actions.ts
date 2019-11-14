import { Action } from '@ngrx/store';

export enum EStockTransactionActions {
    GetStockTransaction = '[StockTransaction] Get StockTransaction Fast Search',
    PostStockTransaction = '[StockTransaction] Post StockTransaction Checkout',
    PostStockTransactionSuccess = '[StockTransaction] Post StockTransaction Checkout Success',
    SetError = '[StockTransaction] Set Error'
}

export class GetStockTransaction implements Action {
    public readonly type = EStockTransactionActions.GetStockTransaction;
    constructor(public payload: object[]) { }
}

export class PostStockTransaction implements Action {
    public readonly type = EStockTransactionActions.PostStockTransaction;
    constructor(public payload: object) { }
}

export class PostStockTransactionSuccess implements Action {
    public readonly type = EStockTransactionActions.PostStockTransactionSuccess;
    constructor(public payload: object[]) { }
}

export type StockTransactionActions =
    GetStockTransaction |
    PostStockTransaction |
    PostStockTransactionSuccess;
