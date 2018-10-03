using Touryo.Infrastructure.Business.Util;
using Touryo.Infrastructure.Business.Common;

namespace MVC_Sample.Logic.Common
{
    public class OrderParameterValue : MyParameterValue
    {
        /// <summary>注文 ID</summary>
        public string OrderId;

        /// <summary>注文情報（サマリ）</summary>
        public System.Data.DataTable Orders;

        /// <summary>注文情報（明細）</summary>
        public System.Data.DataTable OrderDetails;

        public OrderParameterValue(string screenId, string controlId, string methodName, string actionType, MyUserInfo user) : base(screenId, controlId, methodName, actionType, user)
        {
        }
    }
}
    
