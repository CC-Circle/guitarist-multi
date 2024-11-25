using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicSpawner : MonoBehaviour
{
    public GameObject magicPrefab;
    public float spawnInterval = 0.01f;
    public int maxMagicCount = 50; // 生成上限数

    private int currentMagicCount = 0; // 現在の生成数

    private void Start()
    {
        StartCoroutine(SpawnMagic());
    }

    private IEnumerator SpawnMagic()
    {
        while (true)
        {
            // 現在の生成数が上限に達しているか確認
            if (currentMagicCount < maxMagicCount)
            {
                Instantiate(magicPrefab, transform.position, Quaternion.identity);
                currentMagicCount++;
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
