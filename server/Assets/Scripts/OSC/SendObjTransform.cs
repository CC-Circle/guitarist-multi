using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendObjTransform : MonoBehaviour
{
    public string positionAddress; // アドレスを保持するフィールド
    private OSCSender oscSender;
    //[SerializeField] private string positionAddress = "/OscCore/float";
    
    //[SerializeField] private string rotationAddress = "/test/obj/rotation";
    private float sendInterval = 0.1f; // 1 second
    private float timer;
    [SerializeField] private Camera mainCamera;


    void Start()
    {
        // AddressSystemから一意のアドレスを取得
        positionAddress = AddressSystem.CreateAddress(gameObject);
        Debug.Log($"Assigned Address: {positionAddress}");

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
                // オブジェクトのワールド座標を送信
                Vector3 worldPosition = transform.position;
                //oscSender.SendVector3Value(positionAddress, worldPosition);
                oscSender.SendStringValue(positionAddress, worldPosition);

                // ローカル回転を送信
                // Quaternion localRotation = transform.localRotation;
                // oscSender.SendQuaternionValue(rotationAddress, localRotation);

                timer = 0;
            }
        }
    }
}