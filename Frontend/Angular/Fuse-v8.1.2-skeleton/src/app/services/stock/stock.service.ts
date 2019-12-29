import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from 'environments/environment';
import { catchError, tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class StockService {

  _url: string = environment.bff;

  constructor(
    private _http: HttpClient
  ) { }

  getStocks (): Observable<Stock[]> {
    return this._http.get<Stock[]>(this._url + "/api/Stocks")
      .pipe(
        tap(stocks => console.log('read the stocks')),
        catchError(this.handleError('getStocks', []))
      );
  }

  private handleError<T> (operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {

      console.error(error);

      return of(result as T);
    };
  }

}

export class Stock {
    id: string;
    name: string;
    symbol: string;
    type: string;
    region: string;
    marketOpen: Date;
    marketClose: Date;
    timeZone: Date;
    currency: string;
    quote: Date;
}

export class Quote {
    id: string;
}