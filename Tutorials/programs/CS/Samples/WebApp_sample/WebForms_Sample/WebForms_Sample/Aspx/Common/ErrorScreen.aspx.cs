﻿//**********************************************************************************
//* Copyright (C) 2007,2016 Hitachi Solutions,Ltd.
//**********************************************************************************

#region Apache License
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License. 
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion

//**********************************************************************************
//* クラス名        ：ErrorScreen
//* クラス日本語名  ：例外発生時に表示される画面（開発用 ※ 本稼動時には、本番用に差し替える）
//*
//* 作成日時        ：－
//* 作成者          ：生技
//* 更新履歴        ：
//*
//*  日時        更新者            内容
//*  ----------  ----------------  -------------------------------------------------
//*  20xx/xx/xx  ＸＸ ＸＸ         ＸＸＸＸ
//**********************************************************************************

using System;
using System.Collections;
using System.Web;

using Touryo.Infrastructure.Framework.Util;
using Touryo.Infrastructure.Public.Str;

namespace WebForms_Sample.Aspx.Common
{
    /// <summary>例外発生時に表示されるページ</summary>
    public partial class ErrorScreen : System.Web.UI.Page
    {
        // リピータ用

        /// <summary>Form情報：リピータ処理用</summary>
        private ArrayList al_form = new ArrayList();

        /// <summary>Session情報：リピータ処理用</summary>
        private ArrayList al_session = new ArrayList();

        #region Event Handler

        /// <summary>
        /// 画面起動時に実行されるEvent Handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            //画面にError Message・エラー情報を表示する-----------------------------

            //Error MessageをＨＴＴＰコンテキストから取得
            string err_msg =
                (string)HttpContext.Current.Items[FxHttpContextIndex.SYSTEM_EXCEPTION_MESSAGE];

            //エラー情報をＨＴＴＰコンテキストから取得
            string err_info =
                (string)HttpContext.Current.Items[FxHttpContextIndex.SYSTEM_EXCEPTION_INFORMATION];

            //画面にError Messageを表示する
            this.Label1.Text = CustomEncode.HtmlEncode(err_msg);

            //画面にエラー情報を表示する
            this.Label2.Text = CustomEncode.HtmlEncode(err_info);

            // ------------------------------------------------------------------------

            //画面にフォーム情報を表示する---------------------------------------------

            //ＨＴＴＰリクエスト フォーム情報
            HttpRequest req = HttpContext.Current.Request;

            //コレクション
            System.Collections.Specialized.NameValueCollection froms = req.Form;

            if (froms != null)
            {
                //foreach
                foreach (string strKey in froms)
                {
                    if (froms[strKey] == null)
                    {
                        al_form.Add(new PositionData(strKey, "null"));
                    }
                    else
                    {
                        al_form.Add(new PositionData(strKey, CustomEncode.HtmlEncode(froms[strKey].ToString())));
                    }
                }

                //データバインド
                this.Repeater1.DataSource = al_form;
                this.Repeater1.DataBind();
            }

            // ------------------------------------------------------------------------

            // 画面にセッション情報を表示する------------------------------------------

            //ＨＴＴＰセッション情報
            System.Web.SessionState.HttpSessionState sess = HttpContext.Current.Session;

            if (sess != null)
            {
                //foreach
                foreach (string strKey in sess)
                {
                    if (sess[strKey] == null)
                    {
                        al_session.Add(new PositionData(strKey, "null"));
                    }
                    else
                    {
                        al_session.Add(new PositionData(strKey, CustomEncode.HtmlEncode(sess[strKey].ToString())));
                    }
                }

                //データバインド
                this.Repeater2.DataSource = al_session;
                this.Repeater2.DataBind();
            }

            // ------------------------------------------------------------------------

            // セッション情報を削除する------------------------------------------------

            if ((bool)HttpContext.Current.Items[FxHttpContextIndex.SESSION_ABANDON_FLAG])
            {
                // 2009/09/18-start

                // セッション タイムアウト検出用Cookieを消去
                // ※ Removeが正常に動作しないため、値を空文字に設定 ＝ 消去とする

                // Set-Cookie HTTPヘッダをレスポンス
                Response.Cookies.Set(FxCmnFunction.DeleteCookieForSessionTimeoutDetection());

                // 2009/09/18-end

                try
                {
                    // セッションを消去
                    Session.Abandon();
                }
                catch (Exception ex2)
                {
                    // エラー発生時

                    // このカバレージを通過する場合、
                    // おそらく起動した画面のパスが間違っている。
                    Console.WriteLine("このカバレージを通過する場合、おそらく起動した画面のパスが間違っている。");
                    Console.WriteLine(ex2.Message);
                }
            }
        }

        #endregion

    }

    # region リピータ処理用クラス

    /// <summary>リピータ処理用クラス</summary>
    public class PositionData
    {
        /// <summary>キー</summary>
        private string _key;

        /// <summary>値</summary>
        private string _value;

        /// <summary>コンストラクタ</summary>
        public PositionData(string key, string value)
        {
            this._key = key;
            this._value = value;
        }

        /// <summary>キー</summary>
        public string key
        {
            get
            {
                return _key;
            }
        }

        /// <summary>値</summary>
        public string value
        {
            get
            {
                return _value;
            }
        }
    }

    # endregion 
}
