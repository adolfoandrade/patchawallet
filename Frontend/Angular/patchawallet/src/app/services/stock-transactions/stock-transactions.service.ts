import { Injectable } from '@angular/core';
import { Store } from '@ngrx/store';
import { HttpClient } from '@angular/common/http';

import { environment } from 'src/environments/environment';
import { IAppState } from 'src/app/store/state/app.state';
import { ICreateOrUpdateStockTransaction } from 'src/app/models/stocktransaction.interface';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class StockTransactionsService {

  _url: string =  environment.services.patchawallet;

  constructor(private _http: HttpClient, private _store: Store<IAppState>) { }
  
  public postStockTransaction(createOrUpdateStockTransaction: ICreateOrUpdateStockTransaction): Observable<any> {
    //this._store.dispatch(new ShowLoader());
    return this._http.post(
      `${this._url}/api/StockTransactions`,
      createOrUpdateStockTransaction,
      { observe: 'response' }
    );
  }

}
