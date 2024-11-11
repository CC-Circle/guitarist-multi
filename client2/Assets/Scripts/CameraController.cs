using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera controlledCamera;

    private void Start()
    {
        if (controlledCamera == null)
        {
            controlledCamera = GetComponent<Camera>();
            if (controlledCamera == null)
            {
                Debug.LogError("Camera component not found!");
            }
        }
    }

    public void SetCameraPosition(Vector3 position)
    {
        controlledCamera.transform.position = position;
    }

    public void SetCameraRotation(Quaternion rotation)
    {
        controlledCamera.transform.rotation = rotation;
    }
}