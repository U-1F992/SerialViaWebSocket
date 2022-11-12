# SerialViaWebSocket

WebSocket経由で、シリアル通信を別端末のCOMポートへ送る

## Usage

ビルド済みファイルを送信側PC、待受側PCに配置します。Uriとシリアルポートの設定は、それぞれ対応するJSONファイルを編集してください。

送信側のPCに[com0com](https://sourceforge.net/projects/com0com/files/com0com/3.0.0.0/)などの仮想COMポートドライバをインストールし、以下の要領で設定します（`COM_x`、`COM_y`、`COM_z`は架空のCOMポート名です）。

```
# 送信側
転送元 --> COM_x --(com0com)--> COM_y --> Client.exe
```

```
# 待受側
(Client.exe) --(WebSocket)--> Server.exe --> COM_z
```

この例では、送信側のJSONでは`COM_y`、待受側では`COM_z`を設定します。

待受側で`Server.exe`を起動したあと、操作を送信側で`Client.exe`を起動します。

## Reference

- [仮想シリアル(COM) ポートドライバ「com0com」によるシリアル通信](https://qiita.com/yaju/items/e5818c99857883a59033)
- [C#でWebSocketサーバを建てる方法](https://kagasu.hatenablog.com/entry/2020/02/15/105453)

## TODO

- ログをちゃんと考える
- 待受側で発生したCOMポートからの出力を送信側に届ける
