using UnityEngine;

public class MoveUpwards : MonoBehaviour
{
    // 移動速度（単位: メートル/秒）
    public float moveSpeed = 1f;

    // Startはシーンが読み込まれたときに一度だけ呼ばれる
    void Start()
    {
        // 初期化処理、必要ならここにログなども書けます
        Debug.Log("シーンが読み込まれました。オブジェクトが上方向に移動を始めます。");
    }

    // Updateは毎フレーム呼ばれる
    void Update()
    {
        // オブジェクトを上方向に移動させる
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
    }
}

