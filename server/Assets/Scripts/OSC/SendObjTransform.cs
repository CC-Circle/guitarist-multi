using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendObjTransform : MonoBehaviour
{
    public string positionAddress; // アドレスを保持するフィールド
    private OSCSender oscSender;
    //[SerializeField] private string positionAddress = "/OscCore/float";
    
    //[SerializeField] private string rotationAddress = "/test/obj/rotation";
    private float sendInterval = 0.01f; // 1 second
    private float timer;
    [SerializeField] private Camera mainCamera;


    void Start()
    {
        // オブジェクトの名前をそのままアドレスとして使用
        positionAddress = gameObject.name;  // オブジェクトの名前をアドレスに設定

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

                // オブジェクトの回転情報をオイラー角形式で送信
                Vector3 eulerAngles = transform.eulerAngles;

                oscSender.SendStringValue(positionAddress, worldPosition, eulerAngles);

                timer = 0;
            }
        }
    }
}