using System.Collections;
using UnityEngine;
using TMPro;

public class SortManager : MonoBehaviour
{
    public GameObject barPrefab;
    public TMP_InputField inputField;
    public float spacing = 1.5f;
    public float sortSpeed = 0.5f;

    private GameObject[] bars;
    private int[] values;

    public void OnSortButtonClick()
    {
        string rawInput = inputField.text;
        if (string.IsNullOrWhiteSpace(rawInput)) return;

        string[] split = rawInput.Split(',');
        values = new int[split.Length];
        bars = new GameObject[split.Length];

        // Clear old bars
        foreach (GameObject oldBar in GameObject.FindGameObjectsWithTag("Bar"))
        {
            Destroy(oldBar);
        }

        // Compute offset for centering
        float totalWidth = (split.Length - 1) * spacing;
        float startX = -totalWidth / 2f;

        // Create new bars
        for (int i = 0; i < split.Length; i++)
        {
            if (int.TryParse(split[i], out int val))
            {
                values[i] = val;
                Vector3 pos = new Vector3(startX + i * spacing, 0, 0);
                GameObject bar = Instantiate(barPrefab, pos, Quaternion.identity);
                bar.tag = "Bar";

                // Label on top
                GameObject labelObj = new GameObject("Label");
                labelObj.transform.SetParent(bar.transform);
                TextMeshPro tmp = labelObj.AddComponent<TextMeshPro>();
                tmp.text = val.ToString();
                tmp.fontSize = 10;
                tmp.alignment = TextAlignmentOptions.Center;
                tmp.color = Color.white;

                labelObj.transform.localPosition = new Vector3(0, 1f, 0);

                bars[i] = bar;
            }
        }

        StartCoroutine(BubbleSort());
    }

    IEnumerator BubbleSort()
    {
        int n = values.Length;
        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - i - 1; j++)
            {
                if (values[j] > values[j + 1])
                {
                    // Swap values
                    int tempVal = values[j];
                    values[j] = values[j + 1];
                    values[j + 1] = tempVal;

                    // Swap bars
                    yield return StartCoroutine(SwapBars(j, j + 1));
                }
            }
        }
    }

    IEnumerator SwapBars(int indexA, int indexB)
    {
        GameObject barA = bars[indexA];
        GameObject barB = bars[indexB];

        Vector3 posA = barA.transform.position;
        Vector3 posB = barB.transform.position;

        float elapsed = 0f;
        while (elapsed < sortSpeed)
        {
            barA.transform.position = Vector3.Lerp(posA, new Vector3(posB.x, posA.y, posA.z), elapsed / sortSpeed);
            barB.transform.position = Vector3.Lerp(posB, new Vector3(posA.x, posB.y, posB.z), elapsed / sortSpeed);
            elapsed += Time.deltaTime;
            yield return null;
        }

        barA.transform.position = new Vector3(posB.x, posA.y, posA.z);
        barB.transform.position = new Vector3(posA.x, posB.y, posB.z);

        // Swap in array
        bars[indexA] = barB;
        bars[indexB] = barA;
    }
}
