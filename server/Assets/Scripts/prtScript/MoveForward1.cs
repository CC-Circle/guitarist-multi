using UnityEngine;

public class MoveForward1 : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 50f;  // 移動速度の調整用変数

    void Update()
    {
        // 前方に動かす (ワールド座標系を使用)
        Vector3 movement = transform.forward * moveSpeed * Time.deltaTime;
        transform.Translate(movement, Space.World);
    }
}
