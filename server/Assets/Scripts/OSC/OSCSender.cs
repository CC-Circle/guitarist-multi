using OscCore;
using UnityEngine;
using System.Collections.Generic;

public class OSCSender : MonoBehaviour
{
    private OscClient client; // OSCクライアントインスタンス（現在未使用）
    [SerializeField] private string ip = "127.0.0.1"; // OSCメッセージを送信する先のIPアドレス（デフォルトはlocalhost）
    [SerializeField] private List<OscClient> clients = new List<OscClient>(); // OSCクライアントのリスト（複数の受信先に対応）
    [SerializeField] private List<int> ports = new List<int>(); // OSCメッセージを送信するポート番号のリスト

    void Start()
    {
        // 各ポートに対応するOSCクライアントを作成し、リストに追加
        foreach (int port in ports)
        {
            clients.Add(new OscClient(ip, port)); // IPアドレスとポートを指定してOSCクライアントを作成
            //Debug.Log("Created OSC client for port " + port); // 作成したクライアントの情報をログ出力
        }
    }

    //指定したアドレスに対してstring型の値を送信するメソッド
    public void SendStringValue(string address, Vector3 Pvalue, Vector3 Rvalue)
    {
        foreach (var client in clients)
        {
            if (client == null) return;

            try
            {
                // Vector3の値を文字列に変換
                string valueString = $"x: {Pvalue.x}, y: {Pvalue.y}, z: {Pvalue.z},x: {Rvalue.x}, y: {Rvalue.y}, z: {Rvalue.z}";

                // 文字列としてOSCメッセージを送信
                client.Send(address, valueString);

                //Debug.Log("Sent "+ " " + valueString);
            }
            catch (System.Exception e)
            {
                Debug.Log("Error sending string: " + address + " " + Pvalue + " " + e);
            }
        }
    }

    // 一意のアドレスとそのデータを送信するメソッド
    public void SendAddress(string address, string data)
    {
        foreach (var client in clients)
        {
            if (client == null) continue;

            try
            {
                // メッセージを送信
                client.Send(address, data);

                // デバッグログ（送信された内容を確認）
                //Debug.Log($"Sent OSC message to {address}: {data}");
            }
            catch (System.Exception e)
            {
                //Debug.Log("Error sending string: " + address + " " + data + " " + e);
            }
        }
    }
    // public void SenddesAddress(string address, string data)
    // {
    //     foreach (var client in clients)
    //     {
    //         string Desdata = data+"-1";
    //         if (client == null) continue;

    //         try
    //         {
    //             // メッセージを送信
    //             client.Send(address, Desdata);
    //             Debug.Log("送信しました");

    //         }
    //         catch (System.Exception e)
    //         {
    //             //Debug.Log("Error sending string: " + address + " " + data + " " + e);
    //         }
    //     }
    // }








    //----------------------------------------------------------------------------------------------------------------------------------------------------------------

    // 指定したアドレスに対してint型の値を送信するメソッド
    // public void SendIntValue(string address, int value)
    // {
    //     foreach (var client in clients)
    //     {
    //         if (client == null) return;

    //         try
    //         {
    //             client.Send(address, value); // 整数値をOSCメッセージとして送信
    //             Debug.Log("Sent " + address + " " + value);
    //         }
    //         catch (System.Exception e)
    //         {
    //             Debug.Log("Error sending int: " + address + " " + value + " " + e);
    //         }
    //     }
    // }

    // 指定したアドレスに対してQuaternion型の値を送信するメソッド
    // public void SendQuaternionValue(string address, Quaternion value)
    // {
    //     foreach (var client in clients)
    //     {
    //         if (client == null) return;

    //         try
    //         {
    //             // Quaternionを4つのfloat値として送信（x, y, z, wの順）
    //             client.Send("/OscCore/float", new float[] { (float)value.x, (float)value.y, (float)value.z, (float)value.w });
    //             Debug.Log("Sent " + address + " " + value); // 送信内容をログ出力
    //         }
    //         catch (System.Exception e)
    //         {
    //             Debug.Log("Error sending Quaternion: " + address + " " + value + " " + e); // 送信エラーをログ出力
    //         }
    //     }
    // }

    //   指定したアドレスに対してfloat型の値を送信するメソッド
    // public void SendFloatValue(string address, float value)
    // {
    //     foreach (var client in clients) // 全てのOSCクライアントに対して送信
    //     {
    //         if (client == null) return; // クライアントが存在しない場合は処理を終了

    //         try
    //         {
    //             client.Send(address, value); // float値をOSCメッセージとして送信
    //             Debug.Log("Sent " + address + " " + value); // 送信内容をログ出力
    //         }
    //         catch (System.Exception e)
    //         {
    //             Debug.Log("Error sending float: " + address + " " + value + " " + e); // 送信エラーをログ出力
    //         }
    //     }
    // }

    // 指定したアドレスに対してVector3型の値を送信するメソッド
    // public void SendVector3Value(string address, Vector3 value)
    // {
    //     foreach (var client in clients)
    //     {
    //         if (client == null) return;

    //         try
    //         {
    //             // 送信する前にデバッグで確認
    //             Debug.Log($"Sending {address} with values: {value.x}, {value.y}, {value.z}");

    //             // Vector3をfloat[]に変換して送信
    //             client.Send(address, new float[] { value.x, value.y, value.z });

    //             // デバッグ出力
    //             Debug.Log("Sent " + address + " " + value);
    //         }
    //         catch (System.Exception e)
    //         {
    //             Debug.Log("Error sending Vector3: " + address + " " + value + " " + e);
    //         }
    //     }
    // }


}
