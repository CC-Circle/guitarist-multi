using UnityEngine;
using OscCore;

public class DisplayInfoReceiver : MonoBehaviour
{
    private OSCReciever oscReceiver;
    [SerializeField] private string display1WidthAddress = "/display/1/width";
    [SerializeField] private string display2WidthAddress = "/display/2/width";

    public int Display1Width { get; private set; }
    public int Display2Width { get; private set; }

    void Start()
    {
        oscReceiver = FindObjectOfType<OSCReciever>();
        if (oscReceiver == null || oscReceiver.Server == null)
        {
            Debug.LogError("OSCReceiver or OSC Server not found!");
            return;
        }

        oscReceiver.Server.TryAddMethod(display1WidthAddress, ReadDisplay1Width);
        oscReceiver.Server.TryAddMethod(display2WidthAddress, ReadDisplay2Width);
    }

    private void ReadDisplay1Width(OscMessageValues values)
    {
        Display1Width = values.ReadIntElement(0);
    }

    private void ReadDisplay2Width(OscMessageValues values)
    {
        Display2Width = values.ReadIntElement(0);
    }
}