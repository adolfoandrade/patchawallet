import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { StockTransanctionComponent } from './stock-transanction.component';

describe('StockTransanctionComponent', () => {
  let component: StockTransanctionComponent;
  let fixture: ComponentFixture<StockTransanctionComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ StockTransanctionComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StockTransanctionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
