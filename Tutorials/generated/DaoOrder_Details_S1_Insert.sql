-- DaoOrder_Details_S1_Insert
-- 2018/10/3 日立 太郎
INSERT INTO 
  [Order Details]
    (
      [OrderID],
      [ProductID],
      [UnitPrice],
      [Quantity],
      [Discount]
    )
VALUES
    (
      @OrderID,
      @ProductID,
      @UnitPrice,
      @Quantity,
      @Discount
    )
