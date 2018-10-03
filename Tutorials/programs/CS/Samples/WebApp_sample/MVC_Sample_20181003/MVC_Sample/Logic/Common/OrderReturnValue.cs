using Touryo.Infrastructure.Business.Common;

namespace MVC_Sample.Logic.Common
{
    public class OrderReturnValue : MyReturnValue
    {
        /// <summary>メッセージ</summary>
        public string Message;

        /// <summary>注文 ID</summary>
        public int OrderID;

        /// <summary>注文情報（サマリ）</summary>
        public System.Data.DataTable Orders;

        /// <summary>注文情報（明細）</summary>
        public System.Data.DataTable OrderDetails;
    }
}
    
