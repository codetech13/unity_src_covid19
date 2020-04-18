using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingAnimator : MonoBehaviour
{
    public static LoadingAnimator instance;

    [SerializeField] FS_Animation_Panel loaderPanel;

    private void Awake()
    {
        instance = this;
    }

    public void showLoadingAnimation()
    {
        if (loaderPanel != null)
        {
            loaderPanel.ShowPanel(true, 8f, true, 2.5f);
        }
    }

    public void HideLoadingAnimation()
    {
        if (loaderPanel != null)
        {
            loaderPanel.HidePanel(false, 4f, false, 1.5f);
        }
    }
}
