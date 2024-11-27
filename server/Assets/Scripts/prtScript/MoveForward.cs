using UnityEngine;

public class MoveForward : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 30f;  // 移動速度の調整用変数
    private bool hasRotated = false;               // 回転済みフラグ

    void Update()
    {
        // 前方に動かす (ワールド座標系を使用)
        Vector3 movement = transform.forward * moveSpeed * Time.deltaTime;
        transform.Translate(movement, Space.World);

        // Z軸が160を超えた場合にY軸を90度回転（1回のみ）
        if (transform.position.z > 160 && !hasRotated)
        {
            // 現在の回転角を取得
            Vector3 currentRotation = transform.eulerAngles;

            // Y軸を正確に90度回転
            currentRotation.y = Mathf.Round(currentRotation.y + 90) % 360;

            // 回転を適用
            transform.eulerAngles = currentRotation;

            // 回転済みフラグを立てる
            hasRotated = true;
        }
    }
}
