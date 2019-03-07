# Andoroid遡り録画ツール
## ダウンロード
[Releaseページ](https://github.com/aktsk/AndroidRecorder/releases/)から
## もくじ
* どういうソフト？
* 動作条件(PC)
* 動作条件(Android)
* 使用方法
* 設定項目
### どういうソフト？
* 本ツールで録画状態の間、Androidの画面を録画し続け、任意のタイミングで一定時間遡って動画を保存するツールです
    * NVIDIAのShadowPlayやNintendoSwitchの録画が機能としては近いです 
### 動作条件(PC)
* MacOSX
* adbコマンドが利用できる
    * AndroidStudioが入っていればほとんどの場合OKです
* Android端末とUSBケーブルで接続できる
### 動作条件(Android)
* screenrecordコマンドが利用できる
* 録画に使用するPCを信頼している
### 使用方法
* 録画をしたいAndroid端末をMacに接続する
* 本アプリケーションを起動する
* Recordボタンを押す
* 遡って保存したいタイミングでSaveボタンを押す
### 設定項目
#### adb Path
* ADBコマンドが存在するパスを指定します。
* AndroidStudioのデフォルト以外の場所にADBコマンドがある方は指定してください
#### Cache dir
* 録画中の動画をMac側に一時保存するディレクトリです。
* 自分が書き込み権限のある場所を指定してください
* 指定したディレクトリが存在しなければ自動で作成します。
#### Save dir
* Saveボタンを押した際動画を出力するディレクトリです。
* 自分が書き込み権限のある場所を指定してください
* 指定したディレクトリが存在しなければ自動で作成します。
#### One recording time (sec)
* 動画1ファイルあたりの時間です。
* 1~180秒で設定することができます。
* 遡って保存できる動画は10ファイルまでで、遡れる時間は1ファイルあたりの時間*10になります。
#### Pull wait time (msec)
* 保存した動画ファイルが壊れていた際に利用します。
* 数字を増やすと待ち時間が長くなりますがファイルが壊れにくくなります。
