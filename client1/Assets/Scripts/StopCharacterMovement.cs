using UnityEngine;

public class StopCharacterMovement : MonoBehaviour
{
    private Vector3 originalPosition;

    void Start()
    {
        // 初期位置を保存
        originalPosition = transform.position;
    }

    void Update()
    {
        // アニメーション再生中は位置を元に戻す
        transform.position = originalPosition;
    }
}

