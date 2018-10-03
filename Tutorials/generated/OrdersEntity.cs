//**********************************************************************************
//* クラス名        ：OrdersEntity
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
public class OrdersEntity
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

    /// <summary>メンバ変数：CustomerID</summary>
    private System.String _CustomerID;

    /// <summary>プロパティ：CustomerID</summary>
    public System.String CustomerID
    {
        get
        {
            return this._CustomerID;
        }
        set
        {
            this._CustomerID = value;
        }
    }
    /// <summary>メンバ変数：EmployeeID</summary>
    private System.Int32? _EmployeeID;

    /// <summary>プロパティ：EmployeeID</summary>
    public System.Int32? EmployeeID
    {
        get
        {
            return this._EmployeeID;
        }
        set
        {
            this._EmployeeID = value;
        }
    }
    /// <summary>メンバ変数：OrderDate</summary>
    private System.DateTime? _OrderDate;

    /// <summary>プロパティ：OrderDate</summary>
    public System.DateTime? OrderDate
    {
        get
        {
            return this._OrderDate;
        }
        set
        {
            this._OrderDate = value;
        }
    }
    /// <summary>メンバ変数：RequiredDate</summary>
    private System.DateTime? _RequiredDate;

    /// <summary>プロパティ：RequiredDate</summary>
    public System.DateTime? RequiredDate
    {
        get
        {
            return this._RequiredDate;
        }
        set
        {
            this._RequiredDate = value;
        }
    }
    /// <summary>メンバ変数：ShippedDate</summary>
    private System.DateTime? _ShippedDate;

    /// <summary>プロパティ：ShippedDate</summary>
    public System.DateTime? ShippedDate
    {
        get
        {
            return this._ShippedDate;
        }
        set
        {
            this._ShippedDate = value;
        }
    }
    /// <summary>メンバ変数：ShipVia</summary>
    private System.Int32? _ShipVia;

    /// <summary>プロパティ：ShipVia</summary>
    public System.Int32? ShipVia
    {
        get
        {
            return this._ShipVia;
        }
        set
        {
            this._ShipVia = value;
        }
    }
    /// <summary>メンバ変数：Freight</summary>
    private System.Decimal? _Freight;

    /// <summary>プロパティ：Freight</summary>
    public System.Decimal? Freight
    {
        get
        {
            return this._Freight;
        }
        set
        {
            this._Freight = value;
        }
    }
    /// <summary>メンバ変数：ShipName</summary>
    private System.String _ShipName;

    /// <summary>プロパティ：ShipName</summary>
    public System.String ShipName
    {
        get
        {
            return this._ShipName;
        }
        set
        {
            this._ShipName = value;
        }
    }
    /// <summary>メンバ変数：ShipAddress</summary>
    private System.String _ShipAddress;

    /// <summary>プロパティ：ShipAddress</summary>
    public System.String ShipAddress
    {
        get
        {
            return this._ShipAddress;
        }
        set
        {
            this._ShipAddress = value;
        }
    }
    /// <summary>メンバ変数：ShipCity</summary>
    private System.String _ShipCity;

    /// <summary>プロパティ：ShipCity</summary>
    public System.String ShipCity
    {
        get
        {
            return this._ShipCity;
        }
        set
        {
            this._ShipCity = value;
        }
    }
    /// <summary>メンバ変数：ShipRegion</summary>
    private System.String _ShipRegion;

    /// <summary>プロパティ：ShipRegion</summary>
    public System.String ShipRegion
    {
        get
        {
            return this._ShipRegion;
        }
        set
        {
            this._ShipRegion = value;
        }
    }
    /// <summary>メンバ変数：ShipPostalCode</summary>
    private System.String _ShipPostalCode;

    /// <summary>プロパティ：ShipPostalCode</summary>
    public System.String ShipPostalCode
    {
        get
        {
            return this._ShipPostalCode;
        }
        set
        {
            this._ShipPostalCode = value;
        }
    }
    /// <summary>メンバ変数：ShipCountry</summary>
    private System.String _ShipCountry;

    /// <summary>プロパティ：ShipCountry</summary>
    public System.String ShipCountry
    {
        get
        {
            return this._ShipCountry;
        }
        set
        {
            this._ShipCountry = value;
        }
    }

    #endregion
}
