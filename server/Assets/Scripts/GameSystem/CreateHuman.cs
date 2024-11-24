using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateHuman : MonoBehaviour
{
    [SerializeField] private GameObject emptyPrefab;  // 空のオブジェクトのプレハブ
    [SerializeField] private float spawnInterval = 3.0f;  // 生成間隔（秒）
    private Transform dynamicObjectParent;  // DynamicObjectのTransform参照
    private Coroutine spawnCoroutine;  // コルーチンを制御するためのフィールド
    private OSCSender oscSender;

    void Start()
    {
        // プレハブが設定されていない場合にエラーメッセージを表示
        if (emptyPrefab == null)
        {
            Debug.LogError("Empty prefab is not assigned!");
            return;
        }

        // DynamicObjectをシーンから探す
        GameObject dynamicObject = GameObject.Find("DynamicObject");
        if (dynamicObject == null)
        {
            Debug.LogError("DynamicObject not found in the scene!");
            return;
        }

        dynamicObjectParent = dynamicObject.transform;

        // OSC Senderを取得
        oscSender = FindObjectOfType<OSCSender>();
        if (oscSender == null)
        {
            Debug.LogError("OscSender not found in the scene!");
        }

        // コルーチンがすでに開始されているか確認
        if (spawnCoroutine == null)
        {
            spawnCoroutine = StartCoroutine(SpawnEmptyObjects());
        }
    }

    private IEnumerator SpawnEmptyObjects()
    {
        while (true)
        {
            // オブジェクトを生成
            GameObject newObject = CreateEmptyObject();

            // 一意のアドレスを取得
            string positionAddress = GenerateAddress(newObject);

            // オブジェクトの名前にアドレスを設定
            newObject.name = positionAddress;

            // OSCで送信
            SendOSC(positionAddress);

            // 指定された時間待機
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    // 空のオブジェクトを生成するメソッド
    private GameObject CreateEmptyObject()
    {
        Vector3 spawnPosition = transform.position;  // 親オブジェクトの位置を取得
        GameObject newObject = Instantiate(emptyPrefab, spawnPosition, Quaternion.identity); // 生成

        // ワールド座標はそのまま、ヒエラルキー上でDynamicObjectの子に設定
        newObject.transform.SetParent(dynamicObjectParent, true);

        return newObject;
    }

    // オブジェクトに一意のアドレスを生成するメソッド
    private string GenerateAddress(GameObject newObject)
    {
        return AddressSystem.CreateAddress(newObject);  // 新しいオブジェクトに対してアドレスを生成
    }

    // OSCで生成されたアドレスを送信するメソッド
    private void SendOSC(string positionAddress)
    {
        if (oscSender != null)
        {
            oscSender.SendAddress("/OscCore/instancemanager", positionAddress);  // 生成されたアドレスを送信
            //Debug.Log($"Sent address: {positionAddress} to /OscCore/instancemanager");
        }
    }

    void OnDestroy()
    {
        // スクリプトが破棄されたときにコルーチンを停止
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
        }
    }
}
