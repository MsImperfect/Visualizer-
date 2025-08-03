using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AtomData", menuName = "Chemistry/AtomData", order = 1)]
public class AtomData : ScriptableObject
{
    public string elementName;
    public Sprite elementImage; // 2D diagram for UI
    public List<int> electronsPerShell = new List<int>();
}
