# Open 棟梁テンプレート・ベース (Visual Studio 2017 用)
「Open 棟梁テンプレート・ベース」とは、Open 棟梁を使用したシステムの開発基盤 (プロジェクトテンプレート) を作るための元になるものです。

このファイルの英語版は[こちら](README.md)から。

## サンプルの実行手順
Open 棟梁テンプレート・ベース (Visual Studio 2017 用) に同梱されるサンプルアプリケーションの実行手順は以下のとおりです。

**Optional 表記:**  
以下、optional 表記のある DBMS とデータプロバイダは、Open 棟梁でサポートされていますが、Open 棟梁のプログラムには含まれていません。そのため、optional 表記のある DBMS とデータプロバイダを使う場合、必要に応じてデータプロバイダを手動でダウンロードし、[Open 棟梁のデータアクセス用のプロジェクト (DamXXX.csproj)](https://github.com/OpenTouryoProject/OpenTouryoTemplates/tree/develop/root/programs/CS/Frameworks/Infrastructure/Public/Db) から、当該データプロバイダに参照設定を張り直してください。

### 前提ツールのインストール
あらかじめ、Visual Studio 2017 をインストールしておいてください。  

また、Open 棟梁テンプレート・ベースは、既定で以下の DBMS をサポートしています。必要に応じて使用する DBMS をインストールしておいてください。
- SQL Server  
(SQL Server のバージョンは任意です。また、エディションについては、Express Edition 以外もお使いいただけますが、サンプルアプリケーションに指定する接続文字列を修正する必要がありますので、ご注意ください)
- Oracle Database (Express Edition を含みます)
- IBM DB2 ... optional
- HiRDB ... optional
- MySQL
- PostgreSQL

### Open 棟梁テンプレート・ベースの配置
「root_VS2017」フォルダを、C ドライブ直下にコピーして「root」にリネームしてください。
C ドライブ直下以外にコピーすると、Windows のファイルパスの最大文字長の制限により、ビルドに失敗することがあります。

### データプロバイダの取得と配置
Open 棟梁が現在サポートしているデータベースと、対応するデータプロバイダは以下のとおりです。

- Oracle
  - Oracle.DataAccess.dll ... optional
  - Oracle.ManagedDataAccess.dll
- IBM DB2
  - IBM.Data.DB2.dll ... optional
- HiRDB
  - x86: pddndp40.dll, pddndpcore40.dll ... optional
  - x64: pddndp40x.dll, pddndpcore40x.dll ... optional
- MySQL
  - MySql.Data.dll
- PostgreSQL
  - Npgsql.dll
   
### サンプルデータベースのセットアップ
#### SQL Server  
サンプルアプリケーションの実行には、Northwind データベースが必要です。
以下のマイクロソフトのサイトから、Northwind データベースのセットアップ スクリプトをダウンロードし、インストールしてください。  

- Download: NorthWind and pubs Sample Databases for SQL Server 2000 - Microsoft Download Center  
  http://www.microsoft.com/download/en/details.aspx?displaylang=en&id=23654

インストールが成功すると、C ドライブ直下に "SQL Server 2000 Sample Databases" フォルダが作成されます。  
SQL Server 2012 以降をお使いの場合は、このフォルダにある instnwnd.sql ファイルをエディタなどで開き、以下のコードをコメントアウトしてください。(SQL Server 2012 以降では sp_dboption システム ストアド プロシージャがないため)

```sql
exec sp_dboption 'Northwind','trunc. log on chkpt.','true'
exec sp_dboption 'Northwind','select into/bulkcopy','true'
```

コマンドプロンプトで、下記コマンドを実行してください。  
(以下のコマンドの中で、「SQLCMD.EXE」のフォルダパスは SQL Server のバージョンによって異なります。お使いのバージョンでのフォルダパスをご確認の上、コマンドを実行してください)
```bat
"C:\Program Files\Microsoft SQL Server\100\Tools\Binn\SQLCMD.EXE" -S localhost\SQLExpress -E -i "C:\SQL Server 2000 Sample Databases\instnwnd.sql"
```

#### SQL Server 以外
- 各 DBMS に、空のデータベースを作成してください。
- C:\root\files\resource\Sql\\[DBMS 名]\TestTable.txt を実行し、テスト用のテーブルを作成してください。

### プログラムのビルド
Open 棟梁を使用する場合、**初回のみ、MSBuild を使用したビルドバッチファイルを実行**してプログラムをビルドする必要があります。  
**注意:**  
Open 棟梁のテンプレート・ベースには、2つの部分があります：
- フレームワーク（ベースクラス2およびライブラリ）
- サンプルアプリケーション（サブクラス）

*ビルドによって生成された成果物*、つまり*dllファイル*を既定のフォルダにコピーする必要があります。
このため、これらの一連のビルドプロセスをまとめたバッチファイルを実行します。

ビルドバッチファイルは、以下のフォルダにあります。
- C:\root\programs\CS  
- C:\root\programs\VB

実行するバッチファイルは、以下の表のとおりです。
なお、以下の表で
- ○: 必ず実行する
- △: アプリケーションの形態によっては、実行する必要がある
- 空白: 実行する必要はない

を表します。

<table>
  <thead>
    <tr>
      <th rowspan="2">フォルダ名</th><th rowspan="2">バッチファイル名</th><th rowspan="2">説明</th><th colspan="2">実行するファイル</th>
    </tr>
    <tr>
      <th>C#</th><th>VB</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <td rowspan="18" style="vertical-align: top">C:\root\programs\CS</td><td>1_DeleteDir.bat</td><td>ビルドによってできたフォルダを削除 (クリーン) する。</td><td>○</td><td>○</td>
    </tr>
    <tr>
      <td>2_DeleteFile.bat</td><td>一時ファイルなどを削除 (クリーン) する。</td><td>○</td><td>○</td>
    </tr>
    <tr>
      <td>3_Build_Framework.bat</td><td>.NET Frameworkベースのフレームワーク (ベースクラス２, ライブラリ部分) をビルドする。</td><td>○</td><td>○</td>
    </tr>
    <tr>
      <td>3_Build_NetCore.bat</td><td>.NET Standard, .NET Coreベースのフレームワーク (ベースクラス２, ライブラリ部分) をビルドする。</td><td style="vertical-align: top">△<span style="color: red"><sup>*4</sup></span></td><td></td>
    </tr>
    <tr>
      <td>3_Build_RichClientFramework.bat</td><td>.NET Frameworkベースのリッチクライアント用フレームワークをビルドする。</td><td>△<span style="color: red"><sup>*1</sup></span></td><td>△<span style="color: red"><sup>*2</sup></span></td>
    </tr>
    <tr>
      <td>4_Build_Framework_Tool.bat</td><td>.NET Frameworkベースの付属ツールをビルドする。</td><td>○</td><td>○</td>
    </tr>
    <tr>
      <td>5_Build_2CS_sample.bat</td><td>.NET Frameworkベースのサンプルアプリ (2 層 C/S) をビルドする。</td><td style="vertical-align: top">△<span style="color: red"><sup>*3</sup></span></td><td></td>
    </tr>
    <tr>
      <td>5_Build_Bat_sample.bat</td><td>.NET Frameworkベースのサンプルアプリ (バッチ) をビルドする。</td><td style="vertical-align: top">△<span style="color: red"><sup>*3</sup></span></td><td></td>
    </tr>
    <tr>
      <td>5_Build_BatCore_sample.bat</td><td>.NET Coreベースのサンプルアプリ (バッチ) をビルドする。</td><td style="vertical-align: top">△<span style="color: red"><sup>*3, *4</sup></span></td><td></td>
    </tr>
    <tr>
      <td>6_Build_WSSrv_sample.bat</td><td>.NET Frameworkベースのサンプルアプリ (Web サービス (サーバー側ロジック)) をビルドする。</td><td style="vertical-align: top">△<span style="color: red"><sup>*3</sup></span></td><td></td>
    </tr>
    <tr>
      <td>6_Build_WSSrvCore_sample.bat</td><td>.NET Coreベースのサンプルアプリ (Web サービス (サーバー側ロジック)) をビルドする。</td><td style="vertical-align: top">△<span style="color: red"><sup>*3, *4</sup></span></td><td></td>
    </tr>
    <tr>
      <td>7_Build_Framework_WS.bat</td><td>.NET Frameworkベースのフレームワーク (サービスインタフェース部分) をビルドする。</td><td style="vertical-align: top">△<span style="color: red"><sup>*3</sup></span></td><td></td>
    </tr>
    <tr>
      <td>8_Build_WSClntWin_sample.bat</td><td>.NET Frameworkベースのサンプルアプリ (Web サービスクライアント (Windows フォーム)) をビルドする。</td><td style="vertical-align: top">△<span style="color: red"><sup>*3</sup></span></td><td></td>
    </tr>
    <tr>
      <td>9_Build_WSClntWPF_sample.bat</td><td>.NET Frameworkベースのサンプルアプリ (Web サービスクライアント (WPF)) をビルドする。</td><td style="vertical-align: top">△<span style="color: red"><sup>*3</sup></span></td><td></td>
    </tr>
    <tr>
      <td>10_Build_WebApp_sample.bat</td><td>サンプルWebアプリ (ASP.NET) をビルドする。</td><td style="vertical-align: top">△<span style="color: red"><sup>*3</sup></span></td><td></td>
    </tr>
    <tr>
      <td>10_Build_WebAppCore_sample.bat</td><td>サンプルWebアプリ (ASP.NET Core) をビルドする。</td><td style="vertical-align: top">△<span style="color: red"><sup>*3</sup></span></td><td></td>
    </tr>
    <tr>
      <td>z_Common.bat</td><td>共通設定 (MSBuild 用)</td><td></td><td></td>
    </tr>
    <tr>
      <td>z_Common2.bat</td><td>共通設定 (Visual Studio 用)</td><td></td><td></td>
    </tr>
    <tr>
      <td rowspan="13" style="vertical-align: top">C:\root\programs\VB</td><td>1_DeleteDir.bat</td><td>ビルドによってできたフォルダを削除 (クリーン) する。</td><td></td><td>○</td>
    </tr>
    <tr>
      <td>2_DeleteFile.bat</td><td>一時ファイルなどを削除 (クリーン) する。</td><td></td><td>○</td>
    </tr>
    <tr>
      <td>3_Build_Framework.bat</td><td>.NET Frameworkベースのフレームワーク (ベースクラス２, ライブラリ部分) をビルドする。</td><td></td><td>○</td>
    </tr>
    <tr>
      <td>3_Build_RichClientFramework.bat</td><td>.NET Frameworkベースのリッチクライアント用フレームワークをビルドする。</td><td></td><td>△<span style="color: red"><sup>*1</sup></span></td>
    </tr>
    <tr>
      <td>5_Build_2CS_sample.bat</td><td>.NET Frameworkベースのサンプルアプリ (2 層 C/S) をビルドする。</td><td></td><td rowspan="7" style="vertical-align: top">△<span style="color: red"><sup>*3</sup></span></td>
    </tr>
    <tr>
      <td>5_Build_Bat_sample.bat</td><td>.NET Frameworkベースのサンプルアプリ (バッチ) をビルドする。</td><td></td>
    </tr>
    <tr>
      <td>6_Build_WSSrv_sample.bat</td><td>.NET Frameworkベースのサンプルアプリ (Web サービス (サーバー側ロジック)) をビルドする。</td><td></td>
    </tr>
    <tr>
      <td>7_Build_Framework_WS.bat</td><td>.NET Frameworkベースのフレームワーク (サービスインタフェース部分) をビルドする。</td><td></td>
    </tr>
    <tr>
      <td>8_Build_WSClntWin_sample.bat</td><td>.NET Frameworkベースのサンプルアプリ (Web サービスクライアント (Windows フォーム)) をビルドする。</td><td></td>
    </tr>
    <tr>
      <td>9_Build_WSClntWPF_sample.bat</td><td>.NET Frameworkベースのサンプルアプリ (Web サービスクライアント (WPF)) をビルドする。</td><td></td>
    </tr>
    <tr>
      <td>10_Build_WebApp_sample.bat</td><td>.NET Frameworkベースのサンプルアプリ (ASP.NET) をビルドする。</td><td></td>
    </tr>
    <tr>
      <td>z_Common.bat</td><td>共通設定 (MSBuild 用)</td><td></td><td></td>
    </tr>
    <tr>
      <td>z_Common2.bat</td><td>共通設定 (Visual Studio 用)</td><td></td><td></td>
    </tr>
  </tbody>
</table>
<div style="font-size: small">
  <span style="color: red;">*1</span>　リッチクライアント アプリケーションを作成する場合は必須<br />
  <span style="color: red;">*2</span>　DeployZipPackWithHTTPツールを作成する場合は必須<br />
  <span style="color: red;">*3</span>　実際のアプリケーションの形態に応じて選択してください<br />
  <span style="color: red;">*4</span>　.NET Standard、.NET Core ベースのアプリケーションを開発する場合は必須
</div>

上の表を参考に、ビルドバッチファイルを番号順に実行してプログラムをビルドしてください。  

- 必要であれば、環境に合わせて、z_Common.bat 内の BUILDFILEPATH を書き換えてください。  

- Open 棟梁 Visual Studio 2017 テンプレート・ベースが利用するライブラリは、NuGet 経由でダウンロードします。このため、プロキシ環境では、正常に NuGet ライブラリがダウンロードできないことがあります。プロキシ環境をお使いの場合は、以下のように http_proxy 環境変数を定義してください。
    - C:\root\programs\CS\z_Common.bat および C:\root\programs\VB\z_Common.bat を、テキストエディタで開きます。
    - 既定では、http_proxy 環境変数の定義部分はコメントアウトされていますので、"@rem" を削除して、このコメントを解除します。
    - http_proxy 環境変数に、お使いのプロキシ情報を設定してください。

- ビルド時に以下のエラーが発生した場合は、Windows 8 用の Windows SDK をインストールしてください。([Open 棟梁の issue](https://github.com/OpenTouryoProject/OpenTouryoTemplates/issues/48#issuecomment-241349223) が参考になります。)
```
C:\Windows\Microsoft.NET\Framework\v4.0.30319\Microsoft.Common.targets(2863,5): error MSB3086: タスクは SdkToolsPath "" またはレジストリ キー "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SDKs\Windows\v8.0A\WinSDK-NetFx40Tools-x86" を使用して "AL.exe"を見つけられませんでした。SdkToolsPath が設定されていること、SdkToolsPath の下の適切なプロセッサ固有の場所にツールが存在すること、および Microsoft Windows SDK がインストールされていることを確認してください。 [C:\root\programs\CS\Frameworks\Infrastructure\Public\Public.csproj]
```
  
### ASP.NET 状態サービスの準備
管理者としてコマンドプロンプトを起動し、下記コマンドを実行します。  
```bat
   sc config aspnet_state start= auto
   net start aspnet_state
```

### Web サービスの URL の変更
このテンプレート・ベース内の Web サービスは、以下の環境で実行することを想定しています。
- 開発時 (デバッグ時) : IIS Express
- 運用時 : IIS

上記の環境で実行した場合、以下のように URL が異なります。
```txt
IIS Express 使用時
http://localhost:xxxx/Service.asmx (xxxx: ポート番号)

IIS 使用時
http://localhost/yyyy/Service.asmx (yyyy: IIS のアプリケーション名)
```

この Web サービスの URL は、Open 棟梁の通信制御機能の設定ファイルに定義されます。
既定では、IIS Express 使用時の URL が使用されます。  
このため、Web サービスを IIS にデプロイした場合、以下の変更が必要になります。

- FxXMLTMProtocolDefinition プロパティの値を「TMProtocolDefinition2.xml」から「TMProtocolDefinition.xml」に変更する。  

### サンプルの実行
以下のファイルを開いてください。
- web.config または app.config (.NET Coreの場合は、appsettings.json) を開き、  
実際のデータベース環境に合わせて connectionString セクションの値を修正してください。
- サンプルアプリケーションを実行してください。  
ログイン画面が出た場合は、任意の英数字を入力してください。(既定ではパスワード認証を行っていません)  
   
#### Web の場合：
- ASP.NET Web Forms  
  - C:\root\programs\CS\Samples\WebApp_sample\WebForms_Sample\WebForms_Sample.sln
  - C:\root\programs\VB\Samples\WebApp_sample\WebForms_Sample\WebForms_Sample.sln
- ASP.NET MVC  
  - C:\root\programs\CS\Samples\WebApp_sample\MVC_Sample\MVC_Sample.sln
  - C:\root\programs\VB\Samples\WebApp_sample\MVC_Sample\MVC_Sample.sln
- ASP.NET Core MVC  
  - C:\root\programs\CS\Samples4NetCore\WebApp_sample\MVC_Sample\MVC_Sample.sln
 
#### C/S 2階層の場合：
- Windows Forms  
  - C:\root\programs\CS\Samples\2CS_sample\2CSClientWin_sample\2CSClientWin_sample.sln
  - C:\root\programs\VB\Samples\2CS_sample\2CSClientWin_sample\2CSClientWin_sample.sln
- WPF  
  - C:\root\programs\CS\Samples\2CS_sample\2CSClientWPF_sample\2CSClientWPF_sample.sln
  - C:\root\programs\VB\Samples\2CS_sample\2CSClientWPF_sample\2CSClientWPF_sample.sln

#### C/S 3階層の場合：
- Windows Forms  
  - 通常の Windows フォームアプリケーション  
    - C:\root\programs\CS\Samples\WS_sample\WSClient_sample\WSClientWin_sample\WSClientWin_sample.sln
    - C:\root\programs\VB\Samples\WS_sample\WSClient_sample\WSClientWin_sample\WSClientWin_sample.sln
  - ClickOnce アプリケーション  
C:\root\programs\CS\Samples\WS_sample\WSClient_sample\WSClientWinCone_sample\WSClientWinCone_sample.sln
- WPF  
  - C:\root\programs\CS\Samples\WS_sample\WSClient_sample\WSClientWPF_sample\WSClientWPF_sample.sln
  - C:\root\programs\VB\Samples\WS_sample\WSClient_sample\WSClientWPF_sample\WSClientWPF_sample.sln

## その他 特記事項

### 著作権、ライセンス
[License](https://github.com/OpenTouryoProject/OpenTouryoTemplates/tree/master/license)ディレクトリをご確認ください。

### バグ対応
ご利用いただく中で、バグを発見されましたら、[issue](https://github.com/OpenTouryoProject/OpenTouryoTemplates/issues) としてご連絡ください。  
コミュニティで内容を確認し、適切に対応いたします。

### ライブラリの入手、輸出手続き、使用許諾への添付について
- NuGetまたはnpmなどのパッケージ・マネージャーから取得できるライブラリは、Open 棟梁に同梱されないため、輸出管理する必要はありません。
- これ以外のライブラリ、つまりパッケージ・マネージャーから入手できないライブラリは、必要に応じて自身で入手・同梱して輸出する必要があります。この場合、Open棟梁のライセンスに、使用するライブラリのライセンスを添付する必要があります。

### 参考資料
Open 棟梁をご利用いただくにあたり、OpenTouryoDocument リポジトリのドキュメントをご利用いただけます。
- [紹介資料](https://github.com/OpenTouryoProject/OpenTouryoDocuments/tree/master/documents/0_Introduction)  
Open 棟梁の概要資料 (PowerPoint のスライドなど) をご覧いただけます。
- [利用ガイド](https://github.com/OpenTouryoProject/OpenTouryoDocuments/tree/master/documents/1_User_Guide)  
Open 棟梁の仕組みや、各機能の仕様などをご覧いただけます。
- [チュートリアル](https://github.com/OpenTouryoProject/OpenTouryoDocuments/tree/master/documents/2_Tutorial)  
Open 棟梁のファーストステップガイドです。