using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    [SerializeField] private float destroyAfterSeconds = 5.0f; // 破壊されるまでの時間
    [SerializeField] private string positionAddress; // オブジェクトのアドレス

    void Start()
    {
        // 指定された秒数後に自身を破壊
        Invoke(nameof(DestroySelf), destroyAfterSeconds);
    }

    private void DestroySelf()
    {
        // AddressSystemにアドレスを返す
        GameObject addressSystemObject = GameObject.Find("GameSystem"); // AddressSystem がアタッチされている親オブジェクト名
        if (addressSystemObject != null)
        {
            AddressSystem.MarkAsUnused(positionAddress); // AddressSystemクラス名で呼び出し
            //Debug.Log("Address returned to AddressSystem: " + positionAddress);
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
