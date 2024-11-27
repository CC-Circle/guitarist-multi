using UnityEngine;

public class Moveback : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 30f;  // 移動速度の調整用変数

    void Update()
    {
        // 後方に動かす (ローカル座標系を使用)
        Vector3 movement = Vector3.back * moveSpeed * Time.deltaTime;
        transform.Translate(movement, Space.Self);
    }
}
