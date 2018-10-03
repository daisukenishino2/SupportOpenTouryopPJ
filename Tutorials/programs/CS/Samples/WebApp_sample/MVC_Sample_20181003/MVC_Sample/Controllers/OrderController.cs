using System;
using System.Linq;
using System.Web.Mvc;

// Business Logics of Touryo framework
using Touryo.Infrastructure.Business.Business;
using Touryo.Infrastructure.Business.Common;
using Touryo.Infrastructure.Business.Dao;
using Touryo.Infrastructure.Business.Exceptions;
using Touryo.Infrastructure.Business.Presentation;
using Touryo.Infrastructure.Business.Util;

// Framework Logics of Touryo framework
using Touryo.Infrastructure.Framework.Business;
using Touryo.Infrastructure.Framework.Common;
using Touryo.Infrastructure.Framework.Dao;
using Touryo.Infrastructure.Framework.Exceptions;
using Touryo.Infrastructure.Framework.Presentation;
using Touryo.Infrastructure.Framework.Util;
using Touryo.Infrastructure.Framework.Transmission;

// Public Logic of Touryo framework
using Touryo.Infrastructure.Public.Db;

// External Business application Logic nameapces
using System.Data;
using MVC_Sample.Logic.Business;
using MVC_Sample.Logic.Common;
using MVC_Sample.Logic.Dao;
using MVC_Sample.Models;

namespace MVC_Sample.Controllers
{
    public class OrderController : MyBaseMVController
    {
        // GET: Order
        public ActionResult Index()
        {
            // Model(業務ロジッククラス) に渡すパラメータを定義
            OrderParameterValue param
                = new OrderParameterValue(
                    this.RouteData.Values["controller"].ToString(),
                    this.RouteData.Values["action"].ToString(),
                    "GetOrders",
                    "SQL", //string.Empty,
                    new MyUserInfo("user01", this.UserInfo.IPAddress));

            // Model(業務ロジッククラス) のメソッドを実行
            OrdersLogic lb = new OrdersLogic();
            OrderReturnValue retValue = (OrderReturnValue)lb.DoBusinessLogic(param);

            if (retValue.ErrorFlag == true)
            {
                // エラーが発生した場合は、エラー内容を Model クラスに格納
                string Message = "ErrorMessageID:" + retValue.ErrorMessageID + ";";
                Message += "ErrorMessage:" + retValue.ErrorMessage + ";";
                Message += "ErrorInfo:" + retValue.ErrorInfo;
                retValue.Message = Message;
            }

            // ビューに戻り値クラスのオブジェクトを渡し、ビューを表示する
            return View(retValue);
        }

        public ActionResult Order(string OrderId)
        {
            // Model(業務ロジッククラス) に渡すパラメータを定義
            OrderParameterValue param
                = new OrderParameterValue(
                    this.RouteData.Values["controller"].ToString(),
                    this.RouteData.Values["action"].ToString(),
                    "GetOrderById",
                    "SQL", //string.Empty,
                    new MyUserInfo("user01", this.UserInfo.IPAddress));
            param.OrderId = OrderId;

            // Model(業務ロジッククラス) のメソッドを実行
            OrdersLogic logic = new OrdersLogic();
            OrderReturnValue retValue = (OrderReturnValue)logic.DoBusinessLogic(param);

            if (retValue.ErrorFlag == true)
            {
                // エラーが発生した場合は、エラー内容を Model クラスに格納
                string Message = "ErrorMessageID:" + retValue.ErrorMessageID + ";";
                Message += "ErrorMessage:" + retValue.ErrorMessage + ";";
                Message += "ErrorInfo:" + retValue.ErrorInfo;
                retValue.Message = Message;
            }
            else
            {
                // 正常に終了した場合は、DB から取得した値を Session に格納
                Session["Orders"] = retValue.Orders;
                Session["OrderDetails"] = retValue.OrderDetails;
            }
            return View(retValue);
        }

        public ActionResult GetOrderDetails(int? id)
        {
            // セッションから、注文サマリ情報、注文詳細情報を取得し、Model クラスに格納
            OrderReturnValue retValue = new OrderReturnValue();
            retValue.OrderDetails = (System.Data.DataTable)Session["OrderDetails"];
            retValue.Orders = (System.Data.DataTable)Session["Orders"];
            retValue.OrderID = Convert.ToInt32(id);
            return View("Order", retValue);
        }

        [HttpPost]
        public ActionResult UpdateModel_OrderDetails(string txtOrderID, string txtProductID, string txtDiscount, string txtUnitPrice, string txtQuantity)
        {
            // 入力内容をもとに、Model の値を修正
            OrderReturnValue retValue = new OrderReturnValue();
            retValue.OrderDetails = (System.Data.DataTable)Session["OrderDetails"];
            System.Data.DataRow row = retValue.OrderDetails.Select(
                "OrderID='" + txtOrderID + "' AND ProductID ='" + txtProductID + "'").FirstOrDefault();
            row["Discount"] = Convert.ToSingle(txtDiscount);
            row["UnitPrice"] = Convert.ToDecimal(txtUnitPrice);
            row["Quantity"] = Convert.ToInt16(txtQuantity);

            // Session から、Order テーブルのレコードを復元し、Model に格納
            retValue.Orders = (System.Data.DataTable)Session["Orders"];
            retValue.OrderID = Convert.ToInt32(txtOrderID);

            // ビューを表示する
            return View("Order", retValue);
        }

        [HttpPost]
        public ActionResult UpdateModel_OrderSummary(string txtOrderID, string txtCustomerID, string txtEmployeeID, string txtOrderDate,
            string txtRequiredDate, string txtShippedDate, string txtShipVia, string txtFreight, string txtShipName, string txtShipAddress,
            string txtShipCity, string txtShipRegion, string txtShipPostalCode, string txtShipCountry)
        {
            OrderReturnValue retValue = new OrderReturnValue();
            if (txtCustomerID != null)
            {
                // 入力内容をもとに、Model の値を修正
                retValue.Orders = (DataTable)Session["Orders"];
                DataRow rows = retValue.Orders.Select("OrderID='" + txtOrderID + "'").FirstOrDefault();
                rows["OrderDate"] = txtOrderDate;
                rows["RequiredDate"] = txtRequiredDate;
                rows["ShippedDate"] = txtShippedDate;
                rows["ShipVia"] = txtShipVia;
                rows["Freight"] = txtFreight;
                rows["ShipName"] = txtShipName;
                rows["ShipAddress"] = txtShipAddress;
                rows["ShipCity"] = txtShipCity;
                rows["ShipRegion"] = txtShipRegion;
                rows["ShipPostalCode"] = txtShipPostalCode;
                rows["ShipCountry"] = txtShipCountry;

                // Session から、Order Details テーブルのレコードを復元し、Model に格納
                retValue.OrderDetails = (DataTable)Session["OrderDetails"];
                retValue.OrderID = 0;
            }
            else
            {
                // Session から、テーブルのレコードを復元し、Model に格納
                retValue.Orders = (DataTable)Session["Orders"];
                retValue.OrderDetails = (DataTable)Session["OrderDetails"];
            }

            // ビューを表示する
            return View("Order", retValue);
        }

        [HttpPost]
        public ActionResult UpdateDatabase()
        {
            // 修正内容を含む、注文情報を Session から取得
            DataTable orderTable = (DataTable)Session["Orders"];
            DataTable orderDetailTable = (DataTable)Session["OrderDetails"];

            // Model(業務ロジッククラス) に渡すパラメータを定義
            OrderParameterValue param
                = new OrderParameterValue(
                    this.RouteData.Values["controller"].ToString(),
                    this.RouteData.Values["action"].ToString(),
                    "UpdateOrder",
                    "SQL", //string.Empty,
                    new MyUserInfo("user01", this.UserInfo.IPAddress));
            param.Orders = orderTable;
            param.OrderDetails = orderDetailTable;

            // Model(業務ロジッククラス) のメソッドを実行
            OrdersLogic logic = new OrdersLogic();
            OrderReturnValue retValue = (OrderReturnValue)logic.DoBusinessLogic(param);

            // Model の修正内容を確定させる
            orderTable.AcceptChanges();
            orderDetailTable.AcceptChanges();

            // ビューを表示する
            retValue.Orders = orderTable;
            retValue.OrderDetails = orderDetailTable;
            return View("Order", retValue);
        }
    }
}