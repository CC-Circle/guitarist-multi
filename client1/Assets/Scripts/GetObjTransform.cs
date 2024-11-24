using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OscCore;

public class GetObjTransform : MonoBehaviour
{
    //OSCメッセージを受信するためのアドレス
    [SerializeField] private string positionAddress = "/test/obj/position"; // 位置情報を受け取るアドレス
    [SerializeField] private string rotationAddress = "/test/obj/rotation"; // 回転情報を受け取るアドレス

    private OSCReciever oscReceiver; // OSCReceiverのインスタンス
    // private DisplayInfoReceiver displayInfoReceiver; // 必要に応じて表示情報を管理するオブジェクト（現在はコメントアウト）
    [SerializeField] private CameraController cameraController; // カメラコントローラーの参照

    private void Start()
    {
        // シーン内に存在するOSCReceiverを検索
        oscReceiver = FindObjectOfType<OSCReciever>();

        // OSCReceiverまたはそのサーバーが見つからない場合はエラーメッセージを表示
        if (oscReceiver == null || oscReceiver.Server == null)
        {
            Debug.LogError("OSCReceiver or OSC Server not found!");
            return;
        }

        // CameraControllerのインスタンスを取得、もし見つからなければエラーメッセージを表示
        if (cameraController == null)
        {
            cameraController = FindObjectOfType<CameraController>();
            if (cameraController == null)
            {
                Debug.LogError("CameraController not found!");
            }
        }

        // OSCServerに受信するメソッドを登録
        // positionAddressとrotationAddressに対応するメソッドを設定
        oscReceiver.Server.TryAddMethod(positionAddress, ReadPositionFromString);
        //oscReceiver.Server.TryAddMethod(rotationAddress, ReadRotation);
    }

   

    // 受信した文字列を処理するメソッド
    private void ReadPositionFromString(OscMessageValues values)
    {
        // 受信したOSCメッセージから文字列を取得
        string receivedString = values.ReadStringElement(0);  // OSCメッセージの最初の要素を取得

        // 受信した文字列をデバッグログに表示
        //Debug.Log("Received string: " + receivedString);

        // 文字列を分解して座標を抽出（例: "x: 1.23, y: 4.56, z: 7.89"）
        // ここでは、"x: ", "y: ", "z: " でそれぞれの値を抽出
        string[] parts = receivedString.Split(',');

        if (parts.Length == 3)
        {
            // 各軸の値を抽出して浮動小数点数に変換
            float x = float.Parse(parts[0].Split(':')[1].Trim());
            float y = float.Parse(parts[1].Split(':')[1].Trim());
            float z = float.Parse(parts[2].Split(':')[1].Trim());

            // Vector3に変換
            Vector3 position = new Vector3(x, y, z);

            // 受け取った位置をUnityのメインスレッドで適用
            UnityMainThreadDispatcher.Enqueue(() =>
            {
                // オブジェクトの位置を更新
                transform.position = position;  // 位置をワールド座標で設定
                Debug.Log("Updated position to: " + position);  // 位置をデバッグログに表示
            });
        }
        else
        {
            Debug.LogError("Received string does not have the expected format.");
        }
    }



    //----------------------------------------------------------------------------------------------------------------------------------------------------------------

    // // 受信した回転情報を処理するメソッド
    // private void ReadRotation(OscMessageValues values)
    // {
    //     // OSCメッセージから回転情報を取得（四元数で受け取る）
    //     // 注意: 受け取る順番に応じて、読み取るインデックスが異なる場合がある
    //     Quaternion localRotation = new Quaternion(values.ReadFloatElement(1), values.ReadFloatElement(2), values.ReadFloatElement(0), values.ReadFloatElement(4));

    //     // 受け取った回転をUnityのメインスレッドで適用
    //     UnityMainThreadDispatcher.Enqueue(() =>
    //     {
    //         // オブジェクトの回転を更新
    //         transform.localRotation = localRotation;
    //         Debug.Log("Received rotation: " + localRotation); // デバッグログに受信した回転を表示
    //     });
    // }

     // // 受信した位置情報を処理するメソッド
    // private void ReadPosition(OscMessageValues values)
    // {
    //     // Vector3 localPosition = new Vector3(
    //     //     values.ReadFloatElement(0), 
    //     //     values.ReadFloatElement(1), 
    //     //     values.ReadFloatElement(2)
    //     // );

    //     float x = values.ReadFloatElement(0); // 1番目の値
    //     float y = values.ReadFloatElement(1); // 2番目の値
    //     float z = values.ReadFloatElement(2); // 3番目の値

    //     Vector3 localPosition = new Vector3(x,y,z);

    //     // 受け取った位置をUnityのメインスレッドで適用
    //     UnityMainThreadDispatcher.Enqueue(() =>
    //     {
    //         // オブジェクトの位置を更新
    //         transform.position = localPosition;
    //         Debug.Log("Received position: " + localPosition); // デバッグログに受信した位置を表示
    //     });
    // }
}
