using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OscCore;

public class GetObjTransform : MonoBehaviour
{
    [SerializeField] private string positionAddress = "/test/obj/position";

    private OSCReciever oscReceiver;
    private DisplayInfoReceiver displayInfoReceiver;

    private void Start()
    {
        // Find OSCReceiver instance
        oscReceiver = FindObjectOfType<OSCReciever>();

        if (oscReceiver == null || oscReceiver.Server == null)
        {
            Debug.LogError("OSCReceiver or OSC Server not found!");
            return;
        }

        displayInfoReceiver = FindObjectOfType<DisplayInfoReceiver>();
        if (displayInfoReceiver == null)
        {
            Debug.LogError("DisplayInfoReceiver not found!");
            return;
        }

        // Add method to OSC Server
        oscReceiver.Server.TryAddMethod(positionAddress, ReadPosition);
    }

    private void ReadPosition(OscMessageValues values)
    {
        Vector3 position = new Vector3(values.ReadFloatElement(1), values.ReadFloatElement(2), values.ReadFloatElement(0));
        Debug.Log("Received position: " + position);

        UnityMainThreadDispatcher.Enqueue(() =>
        {
            // Adjust position based on display widths from DisplayInfoReceiver
            if (position.x > displayInfoReceiver.Display1Width)
            {
                position.x -= displayInfoReceiver.Display1Width;
            }

            transform.position = position;
            Debug.Log("Received position: " + position);
        });
    }
}