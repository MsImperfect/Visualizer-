using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HanoiVisualizer : MonoBehaviour
{
    public Transform rodA, rodB, rodC;
    public GameObject diskPrefab;
    public TMP_InputField diskInputField;
    public Button startButton;

    public float moveSpeed = 2f;
    public float heightOffset = 2f;

    private int numberOfDisks;
    private Stack<GameObject>[] rods = new Stack<GameObject>[3];
    private bool started = false;

    void Start()
    {
        startButton.onClick.AddListener(OnStartClicked);
    }

    void OnStartClicked()
    {
        if (started) return;

        if (int.TryParse(diskInputField.text, out numberOfDisks) && numberOfDisks > 0)
        {
            started = true;
            for (int i = 0; i < 3; i++)
                rods[i] = new Stack<GameObject>();

            for (int i = numberOfDisks; i >= 1; i--)
            {
                GameObject disk = Instantiate(diskPrefab);
                float scaleFactor = 0.3f + 0.2f * i;
                disk.transform.localScale = new Vector3(scaleFactor, 0.2f, 1);
                disk.transform.position = GetDiskPosition(rodA, rods[0].Count);
                rods[0].Push(disk);
            }

            StartCoroutine(SolveHanoi(numberOfDisks, 0, 2, 1));
        }
        else
        {
            Debug.LogWarning("Invalid input: Enter a positive integer");
        }
    }

    IEnumerator SolveHanoi(int n, int from, int to, int aux)
    {
        if (n > 0)
        {
            yield return StartCoroutine(SolveHanoi(n - 1, from, aux, to));
            yield return StartCoroutine(MoveDisk(from, to));
            yield return StartCoroutine(SolveHanoi(n - 1, aux, to, from));
        }
    }

    IEnumerator MoveDisk(int from, int to)
    {
        GameObject disk = rods[from].Pop();
        Vector3 up = disk.transform.position + Vector3.up * heightOffset;
        Vector3 aboveTarget = GetDiskPosition(GetRod(to), rods[to].Count) + Vector3.up * heightOffset;
        Vector3 down = GetDiskPosition(GetRod(to), rods[to].Count);

        yield return StartCoroutine(MoveTo(disk, up));
        yield return StartCoroutine(MoveTo(disk, aboveTarget));
        yield return StartCoroutine(MoveTo(disk, down));

        rods[to].Push(disk);
    }

    IEnumerator MoveTo(GameObject disk, Vector3 target)
    {
        while (Vector3.Distance(disk.transform.position, target) > 0.01f)
        {
            disk.transform.position = Vector3.MoveTowards(disk.transform.position, target, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }

    Vector3 GetDiskPosition(Transform rod, int diskIndex)
    {
        return rod.position + Vector3.up * (0.2f * diskIndex);
    }

    Transform GetRod(int index)
    {
        if (index == 0) return rodA;
        else if (index == 1) return rodB;
        else return rodC;
    }
}
