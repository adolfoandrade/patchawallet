import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { StocktransactionComponent } from './stocktransaction.component';

describe('StocktransactionComponent', () => {
  let component: StocktransactionComponent;
  let fixture: ComponentFixture<StocktransactionComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ StocktransactionComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StocktransactionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
