using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (CanvasGroup))]
public class FS_Animation_Panel : MonoBehaviour
{
    private CanvasGroup myCanvasGroup;
    RectTransform myRectTransform;

    private bool InShowTransition;
    private bool InHideTransition;

    private bool isVisible;
    public bool IsVisible { get => isVisible; private set => isVisible = value; }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space)) {
        //    ShowPanel(true, 1f, true, 1f);
        //}
        //else if (Input.GetKeyDown(KeyCode.L))
        //{
        //    HidePanel(true, 4, false, 1.5f);
        //}
    }

    private void Awake()
    {
        myCanvasGroup = GetComponent<CanvasGroup>();
        myRectTransform = myCanvasGroup.GetComponent<RectTransform>();
        if (myCanvasGroup.alpha >= 0.1f)
        {
            isVisible = true;
        }
        else {
            isVisible = false;
        }
        //HidePanel(false, 1, false, 1);
        //Debug.Log("Hiding the panel in awake : FS_Animation_Panel");
    }

    public void ShowPanel(bool fadeOut = false, float fadeOutSpeed = 1, bool scaleOutEffect = false, float scaleOutSpeed = 1) {
        if (!InShowTransition)
        {
            InShowTransition = true;
            if (fadeOutSpeed == 0) {
                fadeOutSpeed = 1;
            }

            if (scaleOutSpeed == 0) {
                scaleOutSpeed = 1;
            }
            if (fadeOut)
            {
                fadeOutRoutine = StartCoroutine(fadeOutNow(fadeOutSpeed));
            }
            else {
                ShowCanvas();
            }

            if (scaleOutEffect)
            {
                scaleOutRoutine = StartCoroutine(scaleOutNow(scaleOutSpeed));
            }
            else {
                setScaleToDefault();
            }
        }
    }

    public void HidePanel(bool fadeIn = false, float fadeInSpeed = 1, bool scaleInEffect = false, float scaleInSpeed = 1) {
        if (!InHideTransition)
        {
            InHideTransition = true;
            if (fadeInSpeed == 0) {
                fadeInSpeed = 1;
            }

            if (scaleInSpeed == 0)
            {
                scaleInSpeed = 1;
            }
            if (fadeIn)
            {
                fadeInRoutine = StartCoroutine(fadeInNow(fadeInSpeed));
            }
            else {
                HideCanvas();
            }

            if (scaleInEffect)
            {
                scaleInRoutine = StartCoroutine(scaleInNow(scaleInSpeed));
            }
            else
            {
                setScaleToDefault();
            }
        }
    }

    public void Hide() {
        HidePanel(false, 4f, false, 1.5f);
    }

    #region CO-Routines
    Coroutine fadeInRoutine;
    Coroutine fadeOutRoutine;
    Coroutine scaleInRoutine;
    Coroutine scaleOutRoutine;


    IEnumerator fadeOutNow(float fadeOutSpeed)
    {
        //check and delete this if not behaving accordingly
        if (fadeInRoutine != null) {
            StopCoroutine(fadeInRoutine);
        }
        //

        myCanvasGroup.alpha = 0;
        while (myCanvasGroup.alpha < 0.95f) {
            myCanvasGroup.alpha += Time.deltaTime * fadeOutSpeed;
            yield return null;
        }
        ShowCanvas();
        yield return null;
        StopCoroutine(fadeOutRoutine);
    }

    IEnumerator fadeInNow(float fadeInSpeed)
    {
        if (fadeOutRoutine != null) {
            StopCoroutine(fadeOutRoutine);
        }

        myCanvasGroup.alpha = 1;
        while (myCanvasGroup.alpha > 0.05f)
        {
            myCanvasGroup.alpha -= Time.deltaTime * fadeInSpeed;
            yield return null;
        }
        HideCanvas();
        yield return null;
        StopCoroutine(fadeInRoutine);
    }

    IEnumerator scaleOutNow(float scaleOutSpeed)
    {
        if (scaleInRoutine != null) {
            StopCoroutine(scaleInRoutine);
        }

        float downScaleValue = 0.85f;
        Vector3 tempScale = new Vector3(downScaleValue, downScaleValue, 1);
        
        myRectTransform.localScale = tempScale;
        while (downScaleValue < 0.99f) {
            downScaleValue += Time.deltaTime * scaleOutSpeed;
            tempScale.x = downScaleValue;
            tempScale.y = downScaleValue;
            myRectTransform.localScale = tempScale;
            yield return null;
        }
        setScaleToDefault();
        yield return null;
        StopCoroutine(scaleOutRoutine);
    }

    IEnumerator scaleInNow(float scaleInSpeed)
    {
        if (scaleOutRoutine != null)
        {
            StopCoroutine(scaleOutRoutine);
        }
        float upScaleValue = 1f;
        Vector3 tempScale = new Vector3(upScaleValue, upScaleValue, 1);

        myRectTransform.localScale = tempScale;
        while (upScaleValue > 0.85f)
        {
            upScaleValue -= Time.deltaTime * scaleInSpeed;
            tempScale.x = upScaleValue;
            tempScale.y = upScaleValue;
            myRectTransform.localScale = tempScale;
            yield return null;
        }
        yield return new WaitForSeconds(1);
        setScaleToDefault();
        yield return null;
        StopCoroutine(scaleInRoutine);
    }
    #endregion

    void ShowCanvas() {
        IsVisible = true;
        InShowTransition = false;
        myCanvasGroup.alpha = 1;
        myCanvasGroup.blocksRaycasts = true;
        //myCanvasGroup.interactable = true;
    }

    void HideCanvas() {
        IsVisible = false;
        InHideTransition = false;
        myCanvasGroup.alpha = 0;
        myCanvasGroup.blocksRaycasts = false;
        //myCanvasGroup.interactable = false;
    }

    void setScaleToDefault() {
        myRectTransform.localScale = Vector3.one;
    }
}
