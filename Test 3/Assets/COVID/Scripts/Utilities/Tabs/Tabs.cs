using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tabs : MonoBehaviour
{

    [SerializeField] TabsItem[] allButtons;
    [SerializeField] bool affectColor;
    [SerializeField] bool affectBottomLine;
    [SerializeField] int startIndex;
    [SerializeField] bool setViewAtStart = true;
    [SerializeField] bool callAssignedMethodAtStart = true;

    public TabsItem[] AllButtons { get => allButtons; set => allButtons = value; }

    private void Awake()
    {
        for (int i = 0; i < AllButtons.Length; i++)
        {
            AllButtons[i].myIndex = i;
        }
    }

    private void Start()
    {
        if (setViewAtStart && callAssignedMethodAtStart)
        {
            AllButtons[startIndex].GetComponent<Button>().onClick.Invoke();
        }
        else if (setViewAtStart && !callAssignedMethodAtStart)
        {
            OnClickBtn(startIndex);
        }
        else
        {

        }
    }

    public void OnClickBtn(int index) {
        for (int i = 0; i < AllButtons.Length; i++)
        {
            AllButtons[i].OnDeselected(affectColor, affectBottomLine);
        }

        AllButtons[index].OnSelected(affectColor, affectBottomLine);
    }
}
