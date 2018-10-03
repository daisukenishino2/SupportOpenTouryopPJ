// 業務フレームワーク
using Touryo.Infrastructure.Business.Business;
using Touryo.Infrastructure.Business.Common;
using Touryo.Infrastructure.Business.Dao;
using Touryo.Infrastructure.Business.Exceptions;
using Touryo.Infrastructure.Business.Presentation;
using Touryo.Infrastructure.Business.Util;

// フレームワーク
using Touryo.Infrastructure.Framework.Business;
using Touryo.Infrastructure.Framework.Common;
using Touryo.Infrastructure.Framework.Dao;
using Touryo.Infrastructure.Framework.Exceptions;
using Touryo.Infrastructure.Framework.Presentation;
using Touryo.Infrastructure.Framework.Util;
using Touryo.Infrastructure.Framework.Transmission;

// 部品
using Touryo.Infrastructure.Public.Db;
using Touryo.Infrastructure.Public.IO;
using Touryo.Infrastructure.Public.Log;
using Touryo.Infrastructure.Public.Str;
using Touryo.Infrastructure.Public.Util;

using MVC_Sample.Logic.Common;
using MVC_Sample.Logic.Dao;

namespace MVC_Sample.Logic.Business
{
    public class OrdersLogic : MyFcBaseLogic
    {
        private void UOC_GetOrders(OrderParameterValue orderParameter)
        {
            // DAO 集約クラスを生成する
            ConsolidatedLayerD facade = new ConsolidatedLayerD(this.GetDam());

            // 注文情報一覧を取得する
            OrderReturnValue returnValue = facade.GetOrders(orderParameter);

            // 戻り値クラスを返す
            this.ReturnValue = returnValue;
        }

        private void UOC_GetOrderById(OrderParameterValue orderParameter)
        {
            // DAO 集約クラスを生成する
            ConsolidatedLayerD facade = new ConsolidatedLayerD(this.GetDam());

            // 注文情報の詳細を取得する
            OrderReturnValue returnValue = facade.GetOrderById(orderParameter);

            // 戻り値クラスを返す
            this.ReturnValue = returnValue;
        }

        private void UOC_UpdateOrder(OrderParameterValue orderParameter)
        {
            // DAO 集約クラスを生成する
            ConsolidatedLayerD facade = new ConsolidatedLayerD(this.GetDam());

            // 注文情報をDBに登録する
            OrderReturnValue returnValue = facade.UpdateOrder(orderParameter);

            // 戻り値クラスを返す
            this.ReturnValue = returnValue;
        }
    }
}