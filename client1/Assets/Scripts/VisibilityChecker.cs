using UnityEngine;

public class VisibilityChecker : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;  // カメラを指定
    private Renderer objRenderer;               // オブジェクトのRenderer
    private string objectAddress;               // オブジェクトの一意のアドレス

    void Start()
    {
        // オブジェクトのRendererを取得
        objRenderer = GetComponent<Renderer>();

        // 一意のアドレスを取得（仮に名前を使用）
        objectAddress = $"/OscCore/{gameObject.name}";
    }

    void Update()
    {
        // オブジェクトの中心位置を取得
        Vector3 objectPosition = transform.position;

        // 座標をビューポート座標に変換
        Vector3 viewportPosition = mainCamera.WorldToViewportPoint(objectPosition);

        // ビューポート座標がカメラの描画範囲外か判定
        if (viewportPosition.x < 0 || viewportPosition.x > 1 || 
            viewportPosition.y < 0 || viewportPosition.y > 1 || 
            viewportPosition.z < 0) // zが負の値ならカメラの背後
        {
            // 範囲外なら無効化
            gameObject.SetActive(false);
            Debug.Log($"Object {objectAddress} disabled because it is out of view.");
        }
        else
        {
            // 範囲内なら有効化（必要なら）
            if (!gameObject.activeSelf)
            {
                gameObject.SetActive(true);
                Debug.Log($"Object {objectAddress} re-enabled.");
            }
        }
    }
}
