# RemoteParamControl
## Overview
RemoteParamControlは、ビルド済みのスタンドアロンアプリケーションに対し、Unity Editorからパラメータ制御を行うことを可能にします。  

![RPC_test](https://github.com/user-attachments/assets/df0fab89-efcb-4d23-b220-b9b254749b3a)

## Library Dependency
[OSCJack](https://github.com/keijiro/OscJack)

## Installation
Package ManagerのAdd package from git URLオプションから以下の順でパッケージを追加してください
1. ```https://github.com/keijiro/OscJack.git?path=Packages/jp.keijiro.osc-jack#2.0.0```
2. ```https://github.com/molimolily/RemoteParamControlForUnity.git?path=/Packages/com.molimolily.RemoteParamControl```

## Usage
1. RPCSetterを継承したセッタークラスを作成  
   ```RPCSetter```を継承したセッタークラスを作成し、制御したい変数に```RemoteControllable```アトリビュートを追加します。  
   ``` C#
    const string intValAdr = "value/i"; // Address
   [RemoteControllable(intValAdr)] public int intVal = 0;
   ```
   データ受信時のコールバックを追加します。    
   ```C#
   AddCallback(intValAdr, value => rpcSample.intVal = (int)value);
   ```
   Unity APIを使用する場合、メインスレッドで実行されるようメソッドを設定します。  
   ```C#
   AddCallback(posAddr, value => { EnqueueMainThreadAction(() => cubeTransform.position = (Vector3)value); });
   ```
   セッタークラスには、Create > ScriptableObjects > OSCJack > Connectionで作成したScriptableObjectをアタッチします。  
2. RPC Dataの生成  
   Create > ScriptableObjects > RPC > RPCDataでRPC Dataを作成します。Window > RPC > RPC GeneratorにあるRPC Generatorウィンドウを開き、対象のセッタークラスとRPC Dataをアタッチした後、ボタンを押して生成します。  
   
   ![image](https://github.com/user-attachments/assets/032bbebb-1bc7-4179-b633-56a1876ce50e)


3. 通信  
   Window > RPC > RPC WindowにあるRPC ウィンドウを開き、生成したRPC DataとConnectionをアタッチします。  

   ![image](https://github.com/user-attachments/assets/5f0bb1a4-b76e-4fd3-abae-0cb390bd9af5)

   セッタークラスを含むアプリケーションをビルドし、RPC Windowのスタートボタンを押すと通信が開始します。    
   (※ビルド時にはRun In Backgroundを```true```に設定してください。)  

### Supported Layouts
```C#
    public enum RPCLayoutType
    {
        FloatField,
        IntField,
        Slider,
        IntSlider,
        Vector2Field,
        Vector2IntField,
        Vector3Field,
        Vector3IntField,
        Vector4Field,
        ColorField,
        RectField,
        RectIntField,
        Toggle,
        TextField
    }
```

#### Layout Sample
![image](https://github.com/user-attachments/assets/aa62c9bf-e3f5-4e30-beed-0ab9b6292483)  
