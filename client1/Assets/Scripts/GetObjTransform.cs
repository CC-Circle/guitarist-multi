using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OscCore;

public class GetObjTransform : MonoBehaviour
{
    // OSCメッセージを受信するためのアドレス
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

        // displayInfoReceiverが必要な場合（現在は使用していないが、コメントアウトされている）
        // displayInfoReceiver = FindObjectOfType<DisplayInfoReceiver>();
        // if (displayInfoReceiver == null)
        // {
        //     Debug.LogError("DisplayInfoReceiver not found!");
        //     return;
        // }

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
        oscReceiver.Server.TryAddMethod(positionAddress, ReadPosition);
        oscReceiver.Server.TryAddMethod(rotationAddress, ReadRotation);
    }

    // 受信した位置情報を処理するメソッド
    private void ReadPosition(OscMessageValues values)
    {
        // OSCメッセージから位置情報を取得（x, y, zの順番で読み取り）
        // 注意: 値の順番が通常のVector3の順番（x, y, z）とは逆になっている場合があるので注意
        // ここではx = z, y = y, z = x の順番で受け取っている想定
        Vector3 localPosition = new Vector3(values.ReadFloatElement(1), values.ReadFloatElement(2), values.ReadFloatElement(0));

        // 受け取った位置をUnityのメインスレッドで適用
        UnityMainThreadDispatcher.Enqueue(() =>
        {
            // オブジェクトの位置を更新
            transform.localPosition = localPosition;
            Debug.Log("Received position: " + localPosition); // デバッグログに受信した位置を表示
        });
    }

    // 受信した回転情報を処理するメソッド
    private void ReadRotation(OscMessageValues values)
    {
        // OSCメッセージから回転情報を取得（四元数で受け取る）
        // 注意: 受け取る順番に応じて、読み取るインデックスが異なる場合がある
        Quaternion localRotation = new Quaternion(values.ReadFloatElement(1), values.ReadFloatElement(2), values.ReadFloatElement(0), values.ReadFloatElement(4));

        // 受け取った回転をUnityのメインスレッドで適用
        UnityMainThreadDispatcher.Enqueue(() =>
        {
            // オブジェクトの回転を更新
            transform.localRotation = localRotation;
            Debug.Log("Received rotation: " + localRotation); // デバッグログに受信した回転を表示
        });
    }
}
