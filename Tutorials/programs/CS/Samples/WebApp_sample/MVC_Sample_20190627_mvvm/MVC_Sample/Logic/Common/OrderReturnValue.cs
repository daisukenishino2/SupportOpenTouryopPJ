using System.Collections.Generic;

using Touryo.Infrastructure.Business.Common;

using MVC_Sample.Models.ViewModels;

namespace MVC_Sample.Logic.Common
{
    public class OrderReturnValue : MyReturnValue
    {
        /// <summary>メッセージ</summary>
        public string Message;

        /// <summary>注文 ID</summary>
        public int OrderID;

        /// <summary>注文情報（サマリ）</summary>
        public List<OrderViweModel> Orders;

        /// <summary>注文情報（明細）</summary>
        public List<Order_DetailViweModel>  OrderDetails;
    }
}
    
