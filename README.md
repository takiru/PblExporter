# PblExporter
PowerBuilder の PBLからソースのエクスポートを行う。

# 動作要件
 - .NET Framework 4.8以上
 - 必要モジュールに記載されているPowerBuilder環境

# エクスポート可能バージョン
  - 7.0J
  - 8.0J
  - 9.0J
  - 10.0
  - 11.0
  - 11.5
  - 12.0
  - 12.5
  - 12.6
  - 2017

# 必要モジュール
  必要モジュールは、Pluginsディレクトリ内に配置しなければなりません。  
  PowerBuilderがインストールされていれば配置は不要です。
  
  - 7.0J
    - PBVM70J.dll
    - libjcc.dll
    - libjutils.dll

  - 8.0J
    - PBVM80J.dll
    - libjcc.dll
    - libjutils.dll
    - libjsybHeap.dll

  - 9.0J
    - PBVM90J.dll
    - libjcc.dll
    - libjutils.dll

  - 10.0以降  
    ORCAによる操作のため、PowerBuilderのインストールが必要です。

# 利用方法
### PBLの選択
1. エクスポートしたい対象PBLのPBバージョンを選択します。
1. 追加ボタン、またはフォルダ／ファイルのドラッグでPBLを追加します。  
『サブディレクトリまで検索する』がチェックされている時、フォルダのドラッグで  
サブディレクトリに含まれるすべてのPBLを追加します。


### オブジェクト一覧の表示
PBL一覧からPBLファイルをダブルクリックするとオブジェクト一覧を表示できます。

### コードの表示
オブジェクト一覧からオブジェクトをダブルクリックするとオブジェクトのコードを表示できます。

### ファイルヘッダーを付与する
チェックすると、PowerBuilder でインポート可能なヘッダー情報を付与してエクスポートします。

### PBL情報エクスポート
エクスポートしたいPBLを選択（Ctrl, Shiftを組み合わせて複数選択可能）し、  
保存先を指定してエクスポートします。  
PBL内のすべてのオブジェクトについて、コメント、最終変更日時などを、指定した区切り文字で出力します。

### オブジェクト情報エクスポート
エクスポートしたいオブジェクトを選択（Ctrl, Shiftを組み合わせて複数選択可能）し、  
保存先を指定してエクスポートします。  
オブジェクトのコメント、最終変更日時などを、指定した区切り文字で出力します。

### PBLエクスポート
エクスポートしたいPBLを選択（Ctrl, Shiftを組み合わせて複数選択可能）し、  
保存先を指定してエクスポートします。

### オブジェクトエクスポート
エクスポートしたいオブジェクトを選択（Ctrl, Shiftを組み合わせて複数選択可能）し、  
保存先を指定してエクスポートします。

# コマンド実行
--ver  PBバージョン  
--pbl  PBLファイル  
--name オブジェクト名  
--type オブジェクトタイプ  
--out  出力先ディレクトリパス。未指定の場合はPblExporter.exeと同じディレクトリとなる。  
--header ファイルヘッダーを付与する  
--recursive サブディレクトリまで検索する  
--preserve PBLファイルにディレクトリが指定された時、元のディレクトリ構造を保持します。  

例1  
test.pblからファクションf_testをエクスポート  
```
PblExporter.exe --ver "11.5" --pbl "D:\test.pbl" --name "f_test" --type "Function" --out "D:\Exp"
```

例2  
ファイルヘッダーを出力してエクスポート  
```
PblExporter.exe --ver "11.5" --pbl "D:\test.pbl" --name "f_test" --type "Function" --out "D:\Exp" --header
```

例3  
test.pblからすべてのオブジェクトをエクスポート  
```
PblExporter.exe --ver "11.5" --pbl "D:\test.pbl" --out "D:\Exp"
```

例4  
testフォルダからすべてのオブジェクトをエクスポート  
```
PblExporter.exe --ver "11.5" --pbl "D:\test" --out "D:\Exp"
```

例5  
testフォルダ配下からすべてのオブジェクトをディレクトリ構造を保持してエクスポート  
```
PblExporter.exe --ver "11.5" --pbl "D:\test" --out "D:\Exp" --recursive --preserve
```