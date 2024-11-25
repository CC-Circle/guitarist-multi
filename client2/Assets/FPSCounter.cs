using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class FPSCounter : MonoBehaviour
{
    private Text textComponent;
    private TMP_Text tmpTextComponent;

    private float timer;
    private float fps;
    private List<float> fpsSamples = new List<float>();  // FPSサンプルを保持するリスト
    private float initialAvgFPS;
    private bool isInitialAvgCalculated = false;

    private const float ignoreTime = 5f;   // 最初の無視する時間
    private const float calculationTime = 5f; // 平均計算時間

    private void Start()
    {
        textComponent = GetComponent<Text>();
        tmpTextComponent = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        fps = 1.0f / Time.deltaTime;
        timer += Time.deltaTime;

        // 0.5秒ごとにFPS表示を更新
        if (timer >= 0.5f)
        {
            string fpsDisplay = $"FPS: {fps:F1}";

            // 初期平均の計算
            if (Time.time > ignoreTime && Time.time <= ignoreTime + calculationTime)
            {
                fpsSamples.Add(fps);
            }
            else if (!isInitialAvgCalculated && Time.time > ignoreTime + calculationTime)
            {
                // 初期平均FPSの計算（外れ値を除外）
                float median = CalculateMedian(fpsSamples);
                fpsSamples = fpsSamples.Where(sample => Mathf.Abs(sample - median) <= median * 0.5f).ToList();
                initialAvgFPS = fpsSamples.Average();
                isInitialAvgCalculated = true;
            }

            // 初期平均が計算された後のトレンド表示
            if (isInitialAvgCalculated)
            {
                string trend = fps > initialAvgFPS ? "<color=blue>UP</color>" : "<color=red>DOWN</color>";
                fpsDisplay += $"\nAvg: {initialAvgFPS:F1} ({trend})";
            }

            // テキストに表示
            if (textComponent != null)
            {
                textComponent.text = fpsDisplay;
            }
            else if (tmpTextComponent != null)
            {
                tmpTextComponent.text = fpsDisplay;
            }

            timer = 0f;
        }
    }

    // 中央値を計算するメソッド
    private float CalculateMedian(List<float> values)
    {
        var sortedValues = values.OrderBy(v => v).ToList();
        int count = sortedValues.Count;
        if (count % 2 == 0)
        {
            return (sortedValues[count / 2 - 1] + sortedValues[count / 2]) / 2f;
        }
        else
        {
            return sortedValues[count / 2];
        }
    }
}
