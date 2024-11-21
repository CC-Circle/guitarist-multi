using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateHuman : MonoBehaviour
{
    [SerializeField] private GameObject emptyPrefab;  // 空のオブジェクトのプレハブ
    [SerializeField] private float spawnInterval = 1.0f;  // 生成間隔（秒）
    private Transform dynamicObjectParent;  // DynamicObjectのTransform参照
    private Coroutine spawnCoroutine;  // コルーチンを制御するためのフィールド

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

        // 1秒おきに生成するコルーチンを開始
        spawnCoroutine = StartCoroutine(SpawnEmptyObjects());
    }

    private IEnumerator SpawnEmptyObjects()
    {
        while (true)
        {
            // 空のオブジェクトを生成
            Vector3 spawnPosition = transform.position;  // 親オブジェクトの位置を取得
            GameObject newObject = Instantiate(emptyPrefab, spawnPosition, Quaternion.identity); // 生成

            // ワールド座標はそのまま、ヒエラルキー上でDynamicObjectの子に設定
            newObject.transform.SetParent(dynamicObjectParent, true);

            Debug.Log("Empty Object Created at position: " + spawnPosition + ", under DynamicObject in hierarchy");

            // 指定された時間待機
            yield return new WaitForSeconds(spawnInterval);
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
