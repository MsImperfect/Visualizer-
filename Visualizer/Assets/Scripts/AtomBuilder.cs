using System.Collections.Generic;
using UnityEngine;

public class AtomBuilder : MonoBehaviour
{
    [Header("Atom Structure")]
    public List<int> electronsPerShell = new List<int> { 2, 8, 1 }; // Default: Sodium
    public GameObject electronPrefab;
    public GameObject nucleusPrefab;

    [Header("Visual Settings")]
    public float baseRadius = 1f;
    public float radiusStep = 1.5f;

    void Start()
    {
        BuildAtom();
    }

    void BuildAtom()
    {
        // 🔹 Create nucleus if provided
        if (nucleusPrefab)
        {
            Instantiate(nucleusPrefab, transform.position, Quaternion.identity, transform);
        }

        for (int i = 0; i < electronsPerShell.Count; i++)
        {
            int electronCount = electronsPerShell[i];
            float radius = baseRadius + i * radiusStep;

            // 🔹 Create shell GameObject
            GameObject shell = new GameObject($"Shell_{i}");
            shell.transform.parent = this.transform;
            shell.transform.localPosition = Vector3.zero;

            // 🔹 Apply random tilt to the shell
            float tiltX = (i - electronsPerShell.Count / 2f) * 10f;
            float tiltZ = (i - electronsPerShell.Count / 2f) * -7f;
            shell.transform.localRotation = Quaternion.Euler(tiltX, 0f, tiltZ);

            // 🔹 Add orbit visual using LineRenderer
            LineRenderer line = shell.AddComponent<LineRenderer>();
            OrbitRingDrawer ring = shell.AddComponent<OrbitRingDrawer>();
            ring.radius = radius;
            ring.lineWidth = 0.02f;
            ring.segments = 100;

            // 🔹 Add revolution behavior
            ElectronRevolver spinner = shell.AddComponent<ElectronRevolver>();
            spinner.rotationSpeed = Random.Range(20f, 40f);

            // 🔹 Create electrons in local space around the shell
            for (int j = 0; j < electronCount; j++)
            {
                float angle = 360f * j / electronCount;
                Vector3 localPos = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), 0, Mathf.Sin(angle * Mathf.Deg2Rad)) * radius;

                GameObject electron = Instantiate(electronPrefab, Vector3.zero, Quaternion.identity);
                electron.transform.parent = shell.transform;
                electron.transform.localPosition = localPos;
            }
        }
    }
}
