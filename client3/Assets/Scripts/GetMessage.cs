using OscCore;
using UnityEngine;

public class GetMessage : MonoBehaviour
{
    [SerializeField] private string address = "/test/message";
    private OSCReciever oscReceiver;
    private string message;

    private void Start()
    {
        // Find OSCReceiver instance
        oscReceiver = FindObjectOfType<OSCReciever>();

        if (oscReceiver == null || oscReceiver.Server == null)
        {
            Debug.LogError("OSCReceiver or OSC Server not found!");
            return;
        }

        // Add method to OSC Server
        oscReceiver.Server.TryAddMethod(address, ReadValue);
    }

    private void ReadValue(OscMessageValues values)
    {
        message = values.ReadStringElement(0);
        Debug.Log("Received message: " + message);
    }
}
