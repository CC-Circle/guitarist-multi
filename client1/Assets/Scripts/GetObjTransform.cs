using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OscCore;

public class GetObjTransform : MonoBehaviour
{
    [SerializeField] private string positionAddress = "/test/obj/position";
    [SerializeField] private string rotationAddress = "/test/obj/rotation";

    private OSCReciever oscReceiver;
    // private DisplayInfoReceiver displayInfoReceiver;
    [SerializeField] private CameraController cameraController;


    private void Start()
    {
        // Find OSCReceiver instance
        oscReceiver = FindObjectOfType<OSCReciever>();

        if (oscReceiver == null || oscReceiver.Server == null)
        {
            Debug.LogError("OSCReceiver or OSC Server not found!");
            return;
        }

        // displayInfoReceiver = FindObjectOfType<DisplayInfoReceiver>();
        // if (displayInfoReceiver == null)
        // {
        //     Debug.LogError("DisplayInfoReceiver not found!");
        //     return;
        // }

        if (cameraController == null)
        {
            cameraController = FindObjectOfType<CameraController>();
            if (cameraController == null)
            {
                Debug.LogError("CameraController not found!");
            }
        }

        // Add method to OSC Server
        oscReceiver.Server.TryAddMethod(positionAddress, ReadPosition);
        oscReceiver.Server.TryAddMethod(rotationAddress, ReadRotation);
    }

    private void ReadPosition(OscMessageValues values)
    {
        // Vector3 position = new Vector3(values.ReadFloatElement(1), values.ReadFloatElement(2), values.ReadFloatElement(0));
        // Debug.Log("Received position: " + position);

        // UnityMainThreadDispatcher.Enqueue(() =>
        // {
        //     // Adjust position based on display widths from DisplayInfoReceiver
        //     if (position.x > displayInfoReceiver.Display1Width)
        //     {
        //         position.x -= displayInfoReceiver.Display1Width;
        //     }

        //     transform.position = position;
        //     Debug.Log("Received position: " + position);
        // });

        // Isue
        // position.zが使えない
        Vector3 localPosition = new Vector3(values.ReadFloatElement(1), values.ReadFloatElement(2), values.ReadFloatElement(0));

        UnityMainThreadDispatcher.Enqueue(() =>
        {
            transform.localPosition = localPosition;
            Debug.Log("Received position: " + localPosition);
        });
    }

    private void ReadRotation(OscMessageValues values)
    {
        Quaternion localRotation = new Quaternion(values.ReadFloatElement(1), values.ReadFloatElement(2), values.ReadFloatElement(0), values.ReadFloatElement(4));

        UnityMainThreadDispatcher.Enqueue(() =>
        {
            transform.localRotation = localRotation;
            Debug.Log("Received rotation: " + localRotation);
        });
    }
}