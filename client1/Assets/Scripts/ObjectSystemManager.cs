using System.Collections.Generic;
using UnityEngine;
using OscCore;

public class ObjectSystemManager : MonoBehaviour
{
    [SerializeField] private string positionAddress = "/test/obj/position"; // 位置情報を受け取るOSCアドレス
    [SerializeField] private Transform spawnParent; // 生成されたオブジェクトを格納する親オブジェクト

    private Dictionary<string, GameObject> spawnedObjects = new Dictionary<string, GameObject>(); // 生成されたオブジェクトの辞書
    private OSCReciever oscReceiver; // OSC受信クラス

    private void Start()
    {
        // OSCReceiverを探す
        oscReceiver = FindObjectOfType<OSCReciever>();
        if (oscReceiver == null || oscReceiver.Server == null)
        {
            Debug.LogError("OSCReceiver or OSC Server not found!");
            return;
        }

        // OSCアドレスに対応するメソッドを登録
        oscReceiver.Server.TryAddMethod(positionAddress, HandleOSCMessage);
    }

    private void HandleOSCMessage(OscMessageValues values)
    {
        // OSCメッセージの最初の要素を取得（例: "Cube:1.0,2.0,3.0"）
        string receivedString = values.ReadStringElement(0);
        Debug.Log("Received message: " + receivedString);

        // 文字列を ":" で分割してオブジェクト名と座標情報を取得
        string[] parts = receivedString.Split(':');
        if (parts.Length != 2)
        {
            Debug.LogError("Invalid OSC message format.");
            return;
        }

        string objectName = parts[0].Trim(); // オブジェクト名（例: "Cube"）
        string positionString = parts[1].Trim(); // 座標文字列（例: "1.0,2.0,3.0"）

        // 座標をカンマで分割
        string[] positionParts = positionString.Split(',');
        if (positionParts.Length != 3)
        {
            Debug.LogError("Invalid position format in OSC message.");
            return;
        }

        // 座標の値を浮動小数点に変換
        if (!float.TryParse(positionParts[0], out float x) ||
            !float.TryParse(positionParts[1], out float y) ||
            !float.TryParse(positionParts[2], out float z))
        {
            Debug.LogError("Failed to parse position values.");
            return;
        }

        Vector3 position = new Vector3(x, y, z);

        // オブジェクトを生成または更新
        if (spawnedObjects.ContainsKey(receivedString))
        {
            // 既存オブジェクトの位置を更新
            GameObject existingObject = spawnedObjects[receivedString];
            existingObject.transform.position = position;
            Debug.Log($"Updated position of {objectName} to {position}");
        }
        else
        {
            // 新規オブジェクトを生成
            GameObject newObject = new GameObject(objectName);  // 新しい空のGameObjectを作成
            newObject.transform.position = position;  // 座標を設定
            newObject.transform.SetParent(spawnParent);  // 親オブジェクトに設定

            spawnedObjects[receivedString] = newObject; // アドレスで管理
            Debug.Log($"Spawned new {objectName} at {position}");
        }
    }
}
