using UnityEngine;
using System.Collections;

public class VisibilityChecker : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;  // カメラを指定
    private Renderer objRenderer;               // オブジェクトのRenderer
    private string objectAddress;               // オブジェクトの一意のアドレス
    private bool isInsideCamera = true;

    void Start()
    {
        // オブジェクトのRendererを取得
        objRenderer = GetComponent<Renderer>();

        // 一意のアドレスを取得（仮に名前を使用）
        objectAddress = $"/OscCore/{gameObject.name}";

        // mainCameraが未設定なら自動的に有効なカメラを設定
        if (mainCamera == null)
        {
            AssignActiveCamera();
        }
    }

    void Update()
    {
        //Debug.Log("wwww");
        // オブジェクトの中心位置を取得
        Vector3 objectPosition = transform.position;

        // 座標をビューポート座標に変換
        Vector3 viewportPosition = mainCamera.WorldToViewportPoint(objectPosition);

        // // ビューポート座標がカメラの描画範囲外か判定
        // if ((viewportPosition.x < 0 || viewportPosition.x > 1) ||
        //     (viewportPosition.y < 0 || viewportPosition.y > 1) || 
        //     viewportPosition.z < 0) // zが負の値ならカメラの背後
        // {
        //     // 範囲外なら無効化
        //     gameObject.SetActive(false);
        //     Debug.Log($"Object {objectAddress} disabled because it is out of view.");
        // }
        // else
        // {
        //     // 範囲内なら有効化（必要なら）
        //     if (!gameObject.activeSelf)
        //     {
        //         gameObject.SetActive(true);
        //         Debug.Log($"Object {objectAddress} re-enabled.");
        //     }
        // }
        //Debug.Log("isInsideCamera: "+isInsideCamera);

        // ビューポート座標がカメラの描画範囲外か判定
        if (!isInsideCamera)
        {
            // 範囲外なら無効化
            gameObject.SetActive(false);
            //Debug.Log($"Object {objectAddress} disabled because it is out of view.");
        }
        else if(isInsideCamera)
        {
            // 範囲内なら有効化（必要なら）
            // if (!gameObject.activeSelf)
            // {
                gameObject.SetActive(true);
                //Debug.Log($"Object {objectAddress} re-enabled.");
            // }
        }
    }

    /// <summary>
    /// 有効なカメラを検索し、mainCameraに設定する
    /// </summary>
    private void AssignActiveCamera()
    {
        // 有効なすべてのカメラを取得
        Camera[] cameras = Camera.allCameras;

        // 任意の条件でカメラを選択
        foreach (Camera cam in cameras)
        {
            if (cam.isActiveAndEnabled)
            {
                mainCamera = cam;
                //Debug.Log($"mainCamera set to {mainCamera.name}");
                return;
            }
        }

        Debug.LogWarning("No active camera found!");
    }

    //カメラから外れた
    void OnBecameInvisible() {
        Debug.Log("カメラ範囲外です");
        isInsideCamera = false;
    }
    //カメラ内に入った
    void OnBecameVisible() {
        Debug.Log("カメラ範囲内に入りました");
        isInsideCamera = true;
    }
}
