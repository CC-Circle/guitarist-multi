using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OscCore;

public class InstanceManager : MonoBehaviour
{
    //OSCメッセージを受信するためのアドレス
    private string nameAddress = "/OscCore/instancemanager"; // 位置情報を受け取るアドレス
    private OSCReciever oscReceiver; // OSCReceiverのインスタンス
    [SerializeField] private CameraController cameraController; // カメラコントローラーの参照


    // Start is called before the first frame update
    private void Start()
    {
        // OSCReceiverのインスタンスを取得
        oscReceiver = FindObjectOfType<OSCReciever>();
        if (oscReceiver == null || oscReceiver.Server == null)
        {
            Debug.LogError("OSCReceiver or OSC Server not found!");
            return;
        }

        // CameraControllerのインスタンスを取得
        // if (cameraController == null)
        // {
        //     Debug.LogError("CameraController not found!");
        //     return;
        // }

        // すでに登録されているか確認する方法が無いため、TryAddMethodが失敗してもエラーログを出す
        bool methodAdded = oscReceiver.Server.TryAddMethod(nameAddress, HandlePositionAddress);
        if (!methodAdded)
        {
            Debug.LogWarning($"Method for address {nameAddress} is already registered.");
        }
    }

    // OSCメッセージを受信したときに呼び出されるコールバック関数
    private void HandlePositionAddress(OscMessageValues values)
    {
        // 受信したOSCメッセージから文字列を取得
        string message = values.ReadStringElement(0);  // OSCメッセージの最初の要素を取得

        // データから "human" を取得する
        string[] messageParts = message.Split('/');  // '/' で分割
        if (messageParts.Length >= 2)
        {
            string objectName = messageParts[1];  // "human"

            // "human" というプレハブをリソースから探す
            if (objectName == "human")
            {
                // Resources/Prefabs フォルダからプレハブをロード
                GameObject human = Resources.Load<GameObject>("Prefabs/human");
                if (human != null)
                {
                    // プレハブをインスタンス化（シーンに生成）
                    Vector3 spawnPosition = new Vector3(0, 0, 0);  // 任意の位置に生成（位置は適宜調整）
                    GameObject humanObject = Instantiate(human, spawnPosition, Quaternion.identity);

                    humanObject.name = message;

                    // 生成したオブジェクトを無効化
                    humanObject.SetActive(false);

                    // 追加の処理があればここで行う
                    Debug.Log($"Created {objectName} at position {spawnPosition}に生成しました");
                }
                else
                {
                    Debug.LogError($"Prefab for {objectName} not found in Resources/Prefabs");
                }
            }
        }

    }
}
