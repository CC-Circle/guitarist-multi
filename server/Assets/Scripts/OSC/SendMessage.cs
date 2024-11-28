using UnityEngine;

public class SendMessage : MonoBehaviour
{
    private OSCSender oscSender;
    //[SerializeField] private string address = "/test/message";
    //[SerializeField] private string message = "Hello, World!";
    private float sendInterval = 1.0f; // 1 second
    private float timer;

    void Start()
    {
        // Find OscSender in the scene
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

            // sendInterval seconds have passed
            if (timer >= sendInterval)
            {
                //oscSender.SendStringValue(address, message);
                timer = 0;
            }
        }
    }
}
