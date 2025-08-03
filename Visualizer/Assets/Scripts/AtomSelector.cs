using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AtomSelector : MonoBehaviour
{
    public AtomDatabase atomDatabase;
    public TMP_Dropdown atomDropdown;
    public AtomBuilder atomBuilder;
    public Image elementImageUI;

    void Start()
    {
        atomDropdown.ClearOptions();
        foreach (var atom in atomDatabase.atomList)
        {
            atomDropdown.options.Add(new TMP_Dropdown.OptionData(atom.elementName));
        }

        atomDropdown.onValueChanged.AddListener(OnAtomSelected);
        OnAtomSelected(0); // Load first atom initially
    }

    void OnAtomSelected(int index)
    {
        AtomData selected = atomDatabase.atomList[index];
        atomBuilder.atomData = selected;
        atomBuilder.ClearAtom();
        atomBuilder.BuildAtom();

        elementImageUI.sprite = selected.elementImage;
    }
}
