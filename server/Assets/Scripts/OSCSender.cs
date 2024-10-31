using OscCore;
using UnityEngine;

public class OSCSender : MonoBehaviour
{
    private OscClient client;
    [SerializeField] private string ip = "127.0.0.1"; // localhost
    [SerializeField] private int port = 7000;

    void Start()
    {
        // Create a new client
        client = new OscClient(ip, port);
        // Log the client creation
        Debug.Log("Created OSC client for " + ip + ":" + port);
    }

    public void SendFloatValue(string address, float value)
    {
        if (client == null) return;

        try
        {
            client.Send(address, value);
            Debug.Log("Sent " + address + " " + value);
        }
        catch (System.Exception e)
        {
            Debug.Log("Error sending float: " + address + " " + value + " " + e);
        }
    }

    public void SendVector3Value(string address, Vector3 value)
    {
        if (client == null) return;

        try
        {
            client.Send(address, value);
            Debug.Log("Sent " + address + " " + value);
        }
        catch (System.Exception e)
        {
            Debug.Log("Error sending Vector3: " + address + " " + value + " " + e);
        }
    }

    public void SendStringValue(string address, string value)
    {
        if (client == null) return;

        try
        {
            client.Send(address, value);
            Debug.Log("Sent " + address + " " + value);
        }
        catch (System.Exception e)
        {
            Debug.Log("Error sending string: " + address + " " + value + " " + e);
        }
    }
}
