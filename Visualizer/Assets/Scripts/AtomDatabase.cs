using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AtomDatabase", menuName = "Chemistry/AtomDatabase", order = 2)]
public class AtomDatabase : ScriptableObject
{
    public List<AtomData> atomList = new List<AtomData>();
}
