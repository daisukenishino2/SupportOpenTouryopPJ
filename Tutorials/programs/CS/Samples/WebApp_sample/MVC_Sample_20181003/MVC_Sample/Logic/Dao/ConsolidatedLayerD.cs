//    Open棟梁
using Touryo.Infrastructure.Business.Dao;
using Touryo.Infrastructure.Public.Db;

// 引数・戻り値クラス
using MVC_Sample.Logic.Common;

namespace MVC_Sample.Logic.Dao
{
    public class ConsolidatedLayerD : BaseConsolidateDao
    {
        public ConsolidatedLayerD(BaseDam dam) : base(dam) { }

        public OrderReturnValue GetOrders(OrderParameterValue orderParameter)
        {
            // 戻り値クラスを作成する
            OrderReturnValue returnValue = new OrderReturnValue();

            // 共通 DAO を作成する (SQL ファイルとして、4.5.2 項で作成したファイルを使用する)
            CmnDao dao = new CmnDao(this.Dam);
            dao.SQLFileName = "SelectOrders.sql";

            // 結果格納用の DataTable
            System.Data.DataTable table = new System.Data.DataTable();

            // DB から注文情報一覧を取得し、DataTable に格納する
            dao.ExecSelectFill_DT(table);

            // 戻り値クラスに注文情報一覧を格納し、B 層クラスに返す
            returnValue.Orders = table;
            return returnValue;
        }

        public OrderReturnValue GetOrderById(OrderParameterValue orderParameter)
        {
            // 戻り値クラスを作成する
            OrderReturnValue returnValue = new OrderReturnValue();

            // 自動生成した D 層クラスのインスタンスを生成する
            DaoOrders orderDao = new DaoOrders(this.Dam);
            DaoOrder_Details orderDetailsDao = new DaoOrder_Details(this.Dam);

            // 注文情報、注文詳細情報を格納するための DataTable
            System.Data.DataTable orderTable = new System.Data.DataTable();
            System.Data.DataTable orderDetailsTable = new System.Data.DataTable();

            // パラメータを設定する
            orderDao.PK_OrderID = orderParameter.OrderId;
            orderDetailsDao.PK_OrderID = orderParameter.OrderId;

            // 注文 ID をもとに注文情報を検索する
            orderDao.D2_Select(orderTable);
            orderDetailsDao.D2_Select(orderDetailsTable);

            // 戻り値クラスに結果セットを格納し、B 層クラスに返す
            returnValue.Orders = orderTable;
            returnValue.OrderDetails = orderDetailsTable;
            return returnValue;
        }

        public OrderReturnValue UpdateOrder(OrderParameterValue orderParameter)
        {
            // 戻り値クラスを作成する
            OrderReturnValue returnValue = new OrderReturnValue();

            // 自動生成した D 層クラスのインスタンスを生成する
            DaoOrders orderDao = new DaoOrders(this.Dam);
            DaoOrder_Details orderDetailsDao = new DaoOrder_Details(this.Dam);

            // 注文情報、注文詳細情報を格納するための DataTable
            System.Data.DataTable orderTable = orderParameter.Orders;
            System.Data.DataTable orderDetailsTable = orderParameter.OrderDetails;

            // レコードの状態を確認し、修正されていたら DB を更新する
            if (orderTable.Rows[0].RowState == System.Data.DataRowState.Modified)
            {
                // 注文情報（サマリ）更新用のパタメータを設定する
                orderDao.PK_OrderID = orderTable.Rows[0]["OrderId"];
                orderDao.Set_OrderDate_forUPD = orderTable.Rows[0]["OrderDate"];
                orderDao.Set_RequiredDate_forUPD = orderTable.Rows[0]["RequiredDate"];
                orderDao.Set_ShippedDate_forUPD = orderTable.Rows[0]["ShippedDate"];
                orderDao.Set_ShipVia_forUPD = orderTable.Rows[0]["ShipVia"];
                orderDao.Set_Freight_forUPD = orderTable.Rows[0]["Freight"];
                orderDao.Set_ShipName_forUPD = orderTable.Rows[0]["ShipName"];
                orderDao.Set_ShipAddress_forUPD = orderTable.Rows[0]["ShipAddress"];
                orderDao.Set_ShipCity_forUPD = orderTable.Rows[0]["ShipCity"];
                orderDao.Set_ShipRegion_forUPD = orderTable.Rows[0]["ShipRegion"];
                orderDao.Set_ShipPostalCode_forUPD = orderTable.Rows[0]["ShipPostalCode"];
                orderDao.Set_ShipCountry_forUPD = orderTable.Rows[0]["ShipCountry"];

                // 注文情報（サマリ）を更新する
                orderDao.D3_Update();
            }

            foreach (System.Data.DataRow row in orderDetailsTable.Rows)
            {
                // レコードの状態を確認し、修正されていたら DB を更新する
                if (row.RowState == System.Data.DataRowState.Modified)
                {
                    // 注文情報（明細）更新用のパラメータを設定する
                    orderDetailsDao.PK_OrderID = row["OrderId"];
                    orderDetailsDao.PK_ProductID = row["ProductId"];
                    orderDetailsDao.Set_UnitPrice_forUPD = row["UnitPrice"];
                    orderDetailsDao.Set_Quantity_forUPD = row["Quantity"];
                    orderDetailsDao.Set_Discount_forUPD = row["Discount"];

                    // 注文情報（明細）を更新する
                    orderDetailsDao.D3_Update();
                }
            }

            // 戻り値クラスをB層クラスに返す（更新処理のため、戻り値はなし）
            return returnValue;
        }
    }
}