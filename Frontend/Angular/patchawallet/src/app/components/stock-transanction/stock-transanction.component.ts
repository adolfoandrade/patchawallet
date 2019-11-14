import { Component, OnInit } from '@angular/core';
import { Store, select } from '@ngrx/store';
import { IAppState } from 'src/app/store/state/app.state';
import { selectPostStockTransaction } from 'src/app/store/selectors/stocktransaction.selectors';

@Component({
  selector: 'app-stock-transanction',
  templateUrl: './stock-transanction.component.html',
  styleUrls: ['./stock-transanction.component.scss']
})
export class StockTransanctionComponent implements OnInit {

  stockTransaction$ = this._store.select(selectPostStockTransaction);

  constructor(private _store: Store<IAppState>) { }

  ngOnInit() {
  }

}
