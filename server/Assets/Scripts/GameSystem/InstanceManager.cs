using UnityEngine;

public class InstanceManager : MonoBehaviour
{
    // // 3秒ごとに呼び出す
    // [SerializeField]private float interval = 5f;
    // private float timer = 0f;

    // Startメソッドで初期化
    void Start()
    {
  
    }

    // Updateメソッドでタイマーを更新
    void Update()
    {
        
    }

    // 子オブジェクトのCreateHumanメソッドを呼び出す
    // void CreateHumanInChild()
    // {
    //     // 子オブジェクトを取得
    //     foreach (Transform child in transform)
    //     {
    //         // 子オブジェクトにCreateHumanメソッドがあれば実行
    //         var createHumanScript = child.GetComponent<CreateHumans>();
    //         if (createHumanScript != null)
    //         {
    //             createHumanScript.CreateHuman();
    //         }
    //     }
    // }

    // void CreatecarInchild()
    // {
    //     // 子オブジェクトを取得
    //     foreach (Transform child in transform)
    //     {
    //         // 子オブジェクトにCreateHumanメソッドがあれば実行
    //         var createHumanScript = child.GetComponent<CreateCars>();
    //         if (createHumanScript != null)
    //         {
    //             createHumanScript.CreateCar();
    //         }
    //     }
    // }
}
