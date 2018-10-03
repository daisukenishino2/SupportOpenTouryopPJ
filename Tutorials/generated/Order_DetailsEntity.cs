//**********************************************************************************
//* クラス名        ：Order_DetailsEntity
//* クラス日本語名  ：自動生成Entityクラス
//*
//* 作成日時        ：2018/10/3
//* 作成者          ：棟梁 D層自動生成ツール（墨壺）, 日立 太郎
//* 更新履歴        ：
//*
//*  日時        更新者            内容
//*  ----------  ----------------  -------------------------------------------------
//*  20xx/xx/xx  ＸＸ ＸＸ         ＸＸＸＸ
//**********************************************************************************

// System～
using System;

/// <summary>自動生成Entityクラス</summary>
[Serializable()]
public class Order_DetailsEntity
{
    #region メンバ変数

    /// <summary>メンバ変数：OrderID</summary>
    private System.Int32? _PK_OrderID;

    /// <summary>プロパティ：OrderID</summary>
    public System.Int32? OrderID
    {
        get
        {
            return this._PK_OrderID;
        }
        set
        {
            this._PK_OrderID = value;
        }
    }
    /// <summary>メンバ変数：ProductID</summary>
    private System.Int32? _PK_ProductID;

    /// <summary>プロパティ：ProductID</summary>
    public System.Int32? ProductID
    {
        get
        {
            return this._PK_ProductID;
        }
        set
        {
            this._PK_ProductID = value;
        }
    }

    /// <summary>メンバ変数：UnitPrice</summary>
    private System.Decimal? _UnitPrice;

    /// <summary>プロパティ：UnitPrice</summary>
    public System.Decimal? UnitPrice
    {
        get
        {
            return this._UnitPrice;
        }
        set
        {
            this._UnitPrice = value;
        }
    }
    /// <summary>メンバ変数：Quantity</summary>
    private System.Int16? _Quantity;

    /// <summary>プロパティ：Quantity</summary>
    public System.Int16? Quantity
    {
        get
        {
            return this._Quantity;
        }
        set
        {
            this._Quantity = value;
        }
    }
    /// <summary>メンバ変数：Discount</summary>
    private System.Single? _Discount;

    /// <summary>プロパティ：Discount</summary>
    public System.Single? Discount
    {
        get
        {
            return this._Discount;
        }
        set
        {
            this._Discount = value;
        }
    }

    #endregion
}
