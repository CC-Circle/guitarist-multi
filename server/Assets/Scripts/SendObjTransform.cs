using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendObjTransform : MonoBehaviour
{
    private OSCSender oscSender;
    [SerializeField] private string positionAddress = "/test/obj/position";
    private float sendInterval = 1.0f; // 1 second
    private float timer;

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
                oscSender.SendVector3Value(positionAddress, transform.position);

                timer = 0;
            }
        }
    }
}