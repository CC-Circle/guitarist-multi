using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendObjTransform : MonoBehaviour
{
    private OSCSender oscSender;
    [SerializeField] private string positionAddress = "/test/obj/position";
    [SerializeField] private string rotationAddress = "/test/obj/rotation";
    private float sendInterval = 0.3f; // 1 second
    private float timer;
    [SerializeField] private Camera mainCamera;


    void Start()
    {
        oscSender = FindObjectOfType<OSCSender>();

        if (oscSender == null)
        {
            Debug.LogError("OscSender not found in the scene!");
        }
    }

    void Update()
    {
        if (oscSender != null)
        {
            timer += Time.deltaTime;

            if (timer >= sendInterval)
            {
                // オブジェクトのローカル座標を送信
                Vector3 localPosition = transform.localPosition;
                oscSender.SendVector3Value(positionAddress, localPosition);

                // ローカル回転を送信
                Quaternion localRotation = transform.localRotation;
                oscSender.SendQuaternionValue(rotationAddress, localRotation);

                timer = 0;
            }
        }
    }
}