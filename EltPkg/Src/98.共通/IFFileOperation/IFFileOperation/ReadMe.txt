[IFFileLoad.cs]のXMLは以下のイメージ

<?xml version="1.0" encoding="UTF-8"?>
<IFLoadXMLData>
 <RecordKBN KBN="1">
  <Item Name="レコード区分" Attr="C" Size="1" StartPos="1" />
  <Item Name="ファイルID" Attr="C" Size="5" StartPos="2" />
  <Item Name="ファイル識別区分" Attr="C" Size="3" StartPos="7" />
  <Item Name="銀行コード" Attr="C" Size="4" StartPos="10" />
  <Item Name="作成日" Attr="N" Size="8" StartPos="14" />
  <Item Name="送信一連番号" Attr="C" Size="8" StartPos="22" />
 </RecordKBN>
 <RecordKBN KBN="2">
  <Item Name="レコード区分" Attr="C" Size="1" StartPos="1" />
  <Item Name="証券イメージファイル名" Attr="C" Size="64" StartPos="2" />
  <Item Name="表証券イメージファイル名" Attr="C" Size="4" StartPos="64" />
  <Item Name="表・裏等の別" Attr="C" Size="2" StartPos="126" />
 </RecordKBN>

～～～～

</IFLoadXMLData>

