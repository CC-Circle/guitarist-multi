using OscCore;
using UnityEngine;
using System.Collections.Generic;

public class OSCSender : MonoBehaviour
{
    private OscClient client;
    [SerializeField] private string ip = "127.0.0.1"; // localhost
    // [SerializeField] private int port = 7000;
    [SerializeField] private List<OscClient> clients = new List<OscClient>();
    [SerializeField] private List<int> ports = new List<int>(); // 受信側のポート番号のリスト

    void Start()
    {
        // Create a new client
        // client = new OscClient(ip, port);
        // Log the client creation
        // Debug.Log("Created OSC client for " + ip + ":" + port);

        foreach (int port in ports)
        {
            clients.Add(new OscClient(ip, port));
            Debug.Log("Created OSC client for port " + port);
        }
    }

    public void SendFloatValue(string address, float value)
    {
        foreach (var client in clients)
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
    }

    public void SendVector3Value(string address, Vector3 value)
    {
        foreach (var client in clients)
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
    }

    public void SendStringValue(string address, string value)
    {
        foreach (var client in clients)
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

    public void SendIntValue(string address, int value)
    {
        foreach (var client in clients)
        {
            if (client == null) return;

            try
            {
                client.Send(address, value);
                Debug.Log("Sent " + address + " " + value);
            }
            catch (System.Exception e)
            {
                Debug.Log("Error sending int: " + address + " " + value + " " + e);
            }
        }
    }

    public void SendQuaternionValue(string address, Quaternion value)
    {
        foreach (var client in clients)
        {
            if (client == null) return;

            try
            {
                // Issue
                // OscCore.OscClient.Send() メソッドが、float[] (または object[] で float を渡した場合でも内部的には float[] として扱われる) を引数として受け取るオーバーロードを実装していない

                client.Send(address, new int[] { (int)value.x, (int)value.y, (int)value.z, (int)value.w }); // int[] 配列に格納
                Debug.Log("Sent " + address + " " + value);
            }
            catch (System.Exception e)
            {
                Debug.Log("Error sending Quaternion: " + address + " " + value + " " + e);
            }
        }
    }

}
