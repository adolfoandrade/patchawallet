import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class StockTransactionService {
  constructor(private _http: HttpClient) {
  }

  // Uses http.get() to load data from a single API endpoint
  getFoods() {
    return this._http.get('/api/food').subscribe((res: Response) => res.json());
  }

  // Uses Observable.forkJoin() to run multiple concurrent http.get() requests.
  // The entire operation will result in an error state if any single request fails.
  getBooksAndMovies() {
    return Observable.forkJoin(
      this._http.get('/app/books.json').subscribe((res: Response) => res.json()),
      this._http.get('/app/movies.json').subscribe((res: Response) => res.json())
    );
  }

  createFood(food) {
    let headers = new Headers({ 'Content-Type': 'application/json' });
    let options = new RequestOptions({ headers: headers });
    let body = JSON.stringify(food);
    return this._http.post('/api/food/', body, options).map((res: Response) => res.json());
  }

  updateFood(food) {
    let headers = new Headers({ 'Content-Type': 'application/json' });
    let options = new RequestOptions({ headers: headers });
    let body = JSON.stringify(food);
    return this.http.put('/api/food/' + food.id, body, options).map((res: Response) => res.json());
  }

  deleteFood(food) {
    return this.http.delete('/api/food/' + food.id);
  }

}
