// Open棟梁
using Touryo.Infrastructure.Business.Dao;
using Touryo.Infrastructure.Public.Dto;
using Touryo.Infrastructure.Public.Db;

// 引数・戻り値クラス
using MVC_Sample.Logic.Common;
using MVC_Sample.Models.ViewModels;

using System.Data;

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

            // DB から注文情報一覧を取得し、戻り値クラスに注文情報一覧を格納し、B 層クラスに返す
            using (IDataReader dr = dao.ExecSelect_DR())
            {
                returnValue.Orders = DataToPoco.DataReaderToList<OrderViweModel>(dr);
            }

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
            returnValue.Orders = DataToPoco.DataTableToList<OrderViweModel>(orderTable);
            returnValue.OrderDetails = DataToPoco.DataTableToList<Order_DetailViweModel>(orderDetailsTable);
            return returnValue;
        }

        public OrderReturnValue UpdateOrder(OrderParameterValue orderParameter)
        {
            // 戻り値クラスを作成する
            OrderReturnValue returnValue = new OrderReturnValue();

            // 自動生成した D 層クラスのインスタンスを生成する
            DaoOrders orderDao = new DaoOrders(this.Dam);
            DaoOrder_Details orderDetailsDao = new DaoOrder_Details(this.Dam);

            // レコードの状態を確認し、修正されていたら DB を更新する
            OrderViweModel ovm = orderParameter.Orders[0];
            if (ovm.Modified == true)
            {
                // 注文情報（サマリ）更新用のパタメータを設定する
                orderDao.PK_OrderID = ovm.OrderID.Value;
                orderDao.Set_OrderDate_forUPD = ovm.OrderDate.Value;
                orderDao.Set_RequiredDate_forUPD = ovm.RequiredDate.Value;
                orderDao.Set_ShippedDate_forUPD = ovm.ShippedDate.Value;
                orderDao.Set_ShipVia_forUPD = ovm.ShipVia.Value;
                orderDao.Set_Freight_forUPD = ovm.Freight.Value;
                orderDao.Set_ShipName_forUPD = ovm.ShipName;
                orderDao.Set_ShipAddress_forUPD = ovm.ShipAddress;
                orderDao.Set_ShipCity_forUPD = ovm.ShipCity;
                orderDao.Set_ShipRegion_forUPD = ovm.ShipRegion;
                orderDao.Set_ShipPostalCode_forUPD = ovm.ShipPostalCode;
                orderDao.Set_ShipCountry_forUPD = ovm.ShipCountry;

                // 注文情報（サマリ）を更新する
                orderDao.D3_Update();
            }

            foreach (Order_DetailViweModel odvm in orderParameter.OrderDetails)
            {
                // レコードの状態を確認し、修正されていたら DB を更新する
                if (odvm.Modified == true)
                {
                    // 注文情報（明細）更新用のパラメータを設定する
                    orderDetailsDao.PK_OrderID = odvm.OrderID.Value;
                    orderDetailsDao.PK_ProductID = odvm.ProductID.Value;
                    orderDetailsDao.Set_UnitPrice_forUPD = odvm.UnitPrice.Value;
                    orderDetailsDao.Set_Quantity_forUPD = odvm.Quantity.Value;
                    orderDetailsDao.Set_Discount_forUPD = odvm.Discount.Value;

                    // 注文情報（明細）を更新する
                    orderDetailsDao.D3_Update();
                }
            }

            // 戻り値クラスをB層クラスに返す（更新処理のため、戻り値はなし）
            return returnValue;
        }
    }
}