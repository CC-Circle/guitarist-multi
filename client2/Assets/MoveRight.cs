using UnityEngine;

public class MoveForward : MonoBehaviour
{
    // オブジェクトの移動速度
    public float speed = 5f;

    // Z座標が60を超えたかどうかのフラグ
    private bool isMovingRight = false;

    void Update()
    {
        // zが60を超えたら右方向に変更
        if (transform.position.z > 60f)
        {
            isMovingRight = true;
        }

        // フラグに応じて移動方向を変更
        if (isMovingRight)
        {
            // 右方向（x軸）に移動
            transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);
        }
        else
        {
            // 正面方向（z軸）に移動
            transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.World);
        }
    }
}

