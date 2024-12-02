using System.Collections.Generic;
using UnityEngine;

public class AddressSystem : MonoBehaviour
{
    public static HashSet<string> usedAddresses = new HashSet<string>(); // 使用済みアドレスのセット
    private static HashSet<string> unusedAddresses = new HashSet<string>(); // 未使用アドレスのセット

    // アドレスを生成し、一意なものを返す
    public static string CreateAddress(GameObject obj)
    {
        // 未使用アドレスがある場合は再利用
        if (unusedAddresses.Count > 0)
        {
            foreach (var unusedAddress in unusedAddresses)
            {
                unusedAddresses.Remove(unusedAddress);
                usedAddresses.Add(unusedAddress);
                //Debug.Log($"Reusing unused address: {unusedAddress}");
                return unusedAddress;
            }
        }

        // 未使用アドレスがない場合は新しいアドレスを生成
        string baseAddress = $"/OscCore/{obj.name}";
        string address = baseAddress;
        int counter = 1;
        address = $"{baseAddress}/{counter}";

        // 一意のアドレスになるまで確認
        while (usedAddresses.Contains(address) || unusedAddresses.Contains(address))
        {
            //Debug.Log($"Checking for uniqueness: {address}");
            address = $"{baseAddress}/{counter}";
            counter++;
        }

        //使用済みアドレスとして登録
        usedAddresses.Add(address);
        //Debug.Log($"New address created: {address}");
        return address;
    }

    // 使用済みアドレスを未使用に戻す
    public static void MarkAsUnused(string address)
    {
        if (usedAddresses.Contains(address))
        {
            usedAddresses.Remove(address);
            unusedAddresses.Add(address);
            //Debug.Log($"Address {address} marked as unused.");
        }
        else
        {
            Debug.LogWarning($"Address {address} is not in the used list.");
        }
    }

    // 未使用アドレスを取得して使用済みにする（再利用用）
    public static string GetUnusedAddress()
    {
        if (unusedAddresses.Count > 0)
        {
            foreach (var address in unusedAddresses)
            {
                unusedAddresses.Remove(address);
                usedAddresses.Add(address);
                Debug.Log($"Reusing address: {address}");
                return address;
            }
        }
        Debug.LogWarning("No unused addresses available.");
        return null;
    }

    // 使用済み・未使用リストにアドレスを登録（手動で追加する場合用）
    public static void RegisterAddress(string address, bool isUsed = false)
    {
        if (!usedAddresses.Contains(address) && !unusedAddresses.Contains(address))
        {
            if (isUsed)
            {
                usedAddresses.Add(address);
                Debug.Log($"Address {address} registered as used.");
            }
            else
            {
                unusedAddresses.Add(address);
                Debug.Log($"Address {address} registered as unused.");
            }
        }
        else
        {
            Debug.LogWarning($"Address {address} is already registered.");
        }
    }
}
