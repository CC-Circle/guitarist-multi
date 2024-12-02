using UnityEngine;

public class DisplayInfoSender : MonoBehaviour
{
    private OSCSender oscSender;
    //[SerializeField] private string display1WidthAddress = "/display/1/width";
    //[SerializeField] private string display2WidthAddress = "/display/2/width";

    public static class DisplayConstants
    {
        public static int DisplayWidth = 1920; // または実際のディスプレイ幅
    }

    void Start()
    {
        oscSender = FindObjectOfType<OSCSender>();
        if (oscSender == null)
        {
            Debug.LogError("OscSender not found in the scene!");
            return;
        }

        // Get display resolutions and send them immediately
        // oscSender.SendIntValue(display1WidthAddress, DisplayConstants.DisplayWidth);
        // oscSender.SendIntValue(display2WidthAddress, DisplayConstants.DisplayWidth);
    }
}