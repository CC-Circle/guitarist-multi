using UnityEngine;
using OscCore;

public class OSCReciever : MonoBehaviour
{
    // ポート番号を指定するための変数（インスペクターで設定可能）
    [SerializeField] private int port = 7000;

    // OSCServerのインスタンスを格納するプロパティ
    public OscServer Server { get; private set; } 

    // ゲームオブジェクトが初期化される際に呼び出されるメソッド
    private void Awake()
    {
        try
        {
            // 指定したポートでOSC Serverを作成
            Server = new OscServer(port);

            // Serverが正しく作成されなかった場合のエラーハンドリング
            if (Server == null)
            {
                Debug.LogError("OSC Serverインスタンスの作成に失敗しました。");
                return;
            }

            // 正常にOSC Serverが起動した場合のログ
            Debug.Log("OSC Serverがポート " + port + " で起動しました。");
        }
        catch (System.Exception e)
        {
            // サーバー起動中に例外が発生した場合のエラーログ
            Debug.LogError("OSC Serverの起動に失敗しました: " + e);
        }
    }

    // ゲームオブジェクトが破棄される際に呼び出されるメソッド
    private void OnDestroy()
    {
        // OSC Serverが存在する場合、リソースを解放
        if (Server != null)
        {
            Server.Dispose();
            Debug.Log("OSC Serverを破棄しました。");
        }
    }
}

