export interface ICreateOrUpdateStockTransaction {
    id: string;
    stockId: string;
    Commission: number;
    Amount: number;
    Price: number;
    When: Date;
    TradeType: string;
}
