using System.Collections.Generic;
using UnityEngine;
using OscCore;

public class InstanceManager : MonoBehaviour
{
    private string nameAddress = "/OscCore/instancemanager"; // OSCアドレス
    private OSCReciever oscReceiver; // OSCReceiverのインスタンス

    [SerializeField] private List<GameObject> prefabs; // プレハブを登録するリスト
    private Dictionary<string, GameObject> prefabDictionary; // プレハブ名とプレハブの対応付け辞書

    private void Start()
    {
        // プレハブ辞書を初期化
        prefabDictionary = new Dictionary<string, GameObject>();
        foreach (var prefab in prefabs)
        {
            if (prefab != null)
            {
                prefabDictionary[prefab.name] = prefab;
            }
        }

        // OSCReceiverを取得
        oscReceiver = FindObjectOfType<OSCReciever>();
        if (oscReceiver == null || oscReceiver.Server == null)
        {
            Debug.LogError("OSCReceiverまたはOSC Serverが見つかりません！");
            return;
        }

        // OSCアドレスにコールバックを登録
        bool methodAdded = oscReceiver.Server.TryAddMethod(nameAddress, HandlePositionAddress);
        if (!methodAdded)
        {
            Debug.LogWarning($"アドレス{nameAddress}へのメソッド登録に失敗しました。すでに登録されている可能性があります。");
        }
    }

    // OSCメッセージの処理
    private void HandlePositionAddress(OscMessageValues values)
    {
        // メッセージ内容を取得
        string message = values.ReadStringElement(0);  
        //Debug.Log($"受信メッセージ: {message}");

        // メッセージを解析してオブジェクト名を取得
        string[] messageParts = message.Split('/');
        if (messageParts.Length >= 2)
        {
            string objectName = messageParts[2];
            //Debug.Log($"解析結果: {objectName}");

            UnityMainThreadDispatcher.Enqueue(() =>
            {
                if (prefabDictionary.TryGetValue(objectName, out GameObject prefab))
                {
                    // InstanceManager の Transform を取得
                    Transform instanceManagerTransform = this.transform;

                    // プレハブをインスタンス化し、InstanceManager の子として配置
                    Vector3 spawnPosition = instanceManagerTransform.position; // InstanceManager の位置
                    GameObject obj = Instantiate(prefab, spawnPosition, Quaternion.identity, instanceManagerTransform);

                    obj.name = message; // オブジェクト名を設定
                    //Debug.Log($"{objectName} を {spawnPosition} に生成し、InstanceManager の子にしました。");
                }
                else
                {
                    Debug.LogError($"プレハブ {objectName} が見つかりません！");
                }
            });
        }
    }
}
