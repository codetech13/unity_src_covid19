using UnityEngine;
using UnityEngine.UI;

public class TabsItem : MonoBehaviour
{
    public int myIndex;

    [SerializeField] TMPro.TMP_Text myText;
    [SerializeField] Image bottomLine;

    [SerializeField] Color selectedColor;
    [SerializeField] Color deselectedColor;

    public void OnSelected(bool affectColor, bool affectBottomLine) {
        if (affectColor)
        {
            myText.color = selectedColor;
        }
        else {
            myText.color = deselectedColor;
        }

        if (affectBottomLine) {
            bottomLine.enabled = true;
        }
        else
        {
            bottomLine.enabled = false;
        }
    }

    public void OnDeselected(bool affectColor, bool affectBottomLine) {
        if (affectColor)
        {
            myText.color = deselectedColor;
        }
        else
        {
            myText.color = selectedColor;
        }

        if (affectBottomLine)
        {
            bottomLine.enabled = false;
        }
        else
        {
            bottomLine.enabled = true;
        }
    }
}
