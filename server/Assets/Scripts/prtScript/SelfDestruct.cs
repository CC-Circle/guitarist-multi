using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    [SerializeField] private float destroyAfterSeconds = 5.0f; // 破壊されるまでの時間
    private string positionAddress; // オブジェクトのアドレス

    private SendObjTransform sendObjtransform;
    private OSCSender oscSender;

    protected string instanceAdress = "/OscCore/instancemanager";
    void Start()
    {
        oscSender = FindObjectOfType<OSCSender>();

        // 同じオブジェクトにアタッチされているPositionManagerを取得
        SendObjTransform sendObjtransform = GetComponent<SendObjTransform>();

        if (sendObjtransform != null)
        {
            // positionAddressを取得
            positionAddress = sendObjtransform.positionAddress;
            //Debug.Log("Position Address: " + address);
        }
        else
        {
            //Debug.LogError("PositionManagerが見つかりません！");
        }
        // 指定された秒数後に自身を破壊
        Invoke(nameof(DestroySelf), destroyAfterSeconds);
    }

    private void DestroySelf()
    {
        oscSender.SenddesAddress(instanceAdress,positionAddress);

        // AddressSystemにアドレスを返す
        GameObject addressSystemObject = GameObject.Find("GameSystem"); // AddressSystem がアタッチされている親オブジェクト名
        if (addressSystemObject != null)
        {
            AddressSystem.MarkAsUnused(positionAddress); // AddressSystemクラス名で呼び出し
            //Debug.Log(positionAddress);
        }
        else
        {
            Debug.LogError("GameSystem object not found in the scene.");
        }

        // 自身を破壊
        Destroy(gameObject);
    }

    // positionAddressを設定するためのメソッド
    public void SetPositionAddress(string address)
    {
        positionAddress = address;
    }
}
