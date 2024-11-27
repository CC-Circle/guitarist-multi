using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateHumans : MonoBehaviour
{
    [SerializeField] private GameObject emptyPrefab;  // 空のオブジェクトのプレハブ
    //[SerializeField] private float spawnInterval = 3.0f;  // 生成間隔（秒）
    private Transform dynamicObjectParent;  // DynamicObjectのTransform参照
    private Coroutine spawnCoroutine;  // コルーチンを制御するためのフィールド
    private OSCSender oscSender;

    protected string instanceAdress = "/OscCore/instancemanager";

    // 3秒ごとに呼び出す
    [SerializeField]private float interval = 5f;
    private float timer = 0f;

    void Start()
    {
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
    }

    void Update()
    {
        timer += Time.deltaTime;
        
        if (timer >= interval)
        {
            timer = 0f;
            CreateHuman();
        }
    }
    public void CreateHuman()
    {
        // オブジェクトを生成
        GameObject newObject = CreateEmptyObject();
        //Debug.Log("生成しました");

        // 一意のアドレスを取得
        string positionAddress = GenerateAddress(newObject);
        //Debug.Log("アドレス取得しました");

        // オブジェクトの名前にアドレスを設定
        newObject.name = positionAddress;
        //Debug.Log("名前を変更しました");

        // OSCで送信
        SendOSC(positionAddress);
        //Debug.Log("送信しました");
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
            oscSender.SendAddress(instanceAdress, positionAddress);  // 生成されたアドレスを送信
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
