using UnityEngine;
using OscCore;

public class OSCReciever : MonoBehaviour
{
    [SerializeField] private int port = 7000;
    public OscServer Server { get; private set; } // OSCServer instance

    private void Start()
    {
        try
        {
            // Create OSC Server instance
            Server = new OscServer(port);

            if (Server == null)
            {
                Debug.LogError("Failed to create OSC Server instance.");
                return;
            }
            Debug.Log("OSC Server started on port " + port);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Failed to start OSC Server: " + e);
        }
    }

    private void OnDestroy()
    {
        if (Server != null)
        {
            Server.Dispose();
            Debug.Log("OSC Server disposed.");
        }
    }
}
