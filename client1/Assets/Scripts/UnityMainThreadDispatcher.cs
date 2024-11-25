using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UnityMainThreadDispatcher : MonoBehaviour
{
    // シングルトンパターンのインスタンス
    public static UnityMainThreadDispatcher Instance { get; private set; }

    // メインスレッドで実行するアクションのキュー（スレッドセーフ用にロックを利用）
    private static readonly Queue<Action> _executionQueue = new Queue<Action>();

    // オブジェクトが初期化される際に呼び出される
    void Awake()
    {
        // シングルトンインスタンスが未設定の場合、現在のオブジェクトを設定
        if (Instance == null)
        {
            Instance = this;

            // このオブジェクトをシーン切り替え時に破棄されないように設定
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // インスタンスが既に存在する場合、重複を防ぐためこのオブジェクトを破棄
            Destroy(gameObject);
        }
    }

    // 毎フレーム呼び出される
    void Update()
    {
        // 実行キューにある全てのアクションを処理
        lock (_executionQueue) // スレッドセーフのためロックを使用
        {
            while (_executionQueue.Count > 0)
            {
                // キューからアクションを取り出して実行
                _executionQueue.Dequeue().Invoke();
            }
        }
    }

    // メインスレッドで実行するアクションをキューに追加
    public static void Enqueue(Action action)
    {
        lock (_executionQueue) // スレッドセーフのためロックを使用
        {
            // アクションをキューに追加
            _executionQueue.Enqueue(action);
        }
    }
}
