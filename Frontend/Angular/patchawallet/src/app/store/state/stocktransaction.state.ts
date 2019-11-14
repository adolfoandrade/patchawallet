import { IError } from 'src/app/models/error.interface';
import { ICreateOrUpdateStockTransaction } from 'src/app/models/stocktransaction.interface';

export interface IStockTransactionState {
    createOrUpdateStockTransaction: ICreateOrUpdateStockTransaction;
    errors: IError;
}

export const initialStockTransactionState: IStockTransactionState = {
    createOrUpdateStockTransaction: null,
    errors: null
};

