using UnityEngine;

public class MoveForward : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;  // 移動速度の調整用変数

    void Update()
    {
        // 前方に動かす
        Vector3 movement = transform.forward * moveSpeed * Time.deltaTime;

        // オブジェクトを前方に移動
        transform.Translate(movement);
    }
}

