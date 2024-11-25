using UnityEngine;

public class VisibilityController : MonoBehaviour
{
    private Renderer objRenderer;
    private Camera targetCamera;

    void Start()
    {
        objRenderer = GetComponent<Renderer>();
        // "Main Camera2" のカメラを指定
        targetCamera = GameObject.Find("Main Camera1").GetComponent<Camera>();
    }

    void Update()
    {
        // "Main Camera2" の視野内かどうかを確認
        Vector3 screenPoint = targetCamera.WorldToViewportPoint(transform.position);
        bool onScreen = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;

        // カメラの範囲内にいる場合、描画を有効化
        if (onScreen)
        {
            if (!objRenderer.enabled)
            {
                objRenderer.enabled = true; // 描画を有効にする
            }
        }
        else
        {
            if (objRenderer.enabled)
            {
                objRenderer.enabled = false; // 描画を無効にする
            }
        }
    }
}