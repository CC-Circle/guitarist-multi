using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateHuman : MonoBehaviour
{
    [SerializeField] private GameObject emptyPrefab;  // 空のオブジェクトのプレハブ（空のオブジェクトを指定）

    void Start()
    {
        // プレハブが設定されていない場合にエラーメッセージを表示
        if (emptyPrefab == null)
        {
            Debug.LogError("Empty prefab is not assigned!");
            return;
        }

        // 親オブジェクトの位置で空のオブジェクトを生成
        Vector3 spawnPosition = transform.position;  // 親オブジェクトの位置を取得
        Instantiate(emptyPrefab, spawnPosition, Quaternion.identity); // 親の位置に生成
        Debug.Log("Empty Object Created at Parent's position: " + spawnPosition);
    }
}
