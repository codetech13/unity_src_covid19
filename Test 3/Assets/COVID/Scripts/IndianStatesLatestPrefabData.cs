using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class IndianStatesLatestPrefabData : MonoBehaviour
{

    [SerializeField] UnityEngine.UI.Image flag;
    [SerializeField] TMP_Text stateNameTxt;
    [SerializeField] TMP_Text totalCasePlaceholder;
    [SerializeField] TMP_Text totalDeathPlaceholder;
    [SerializeField] TMP_Text totalDeathTxt;
    [SerializeField] TMP_Text totalCaseTxt;

    [SerializeField] TMP_Text specificDataPlaceholder;
    [SerializeField] TMP_Text specificDataTxt;

    private string stateName;

    public string StateName { get => stateName; set => stateName = value; }

    public void SetStatesData(string _stateNames, string flagUrl, float totalCases, float totalDeaths)
    {
        StateName = _stateNames;
        stateNameTxt.text = _stateNames;
        totalCasePlaceholder.enabled = true;
        totalDeathPlaceholder.enabled = true;
        totalCaseTxt.enabled = true;
        totalDeathTxt.enabled = true;
        totalCaseTxt.text = totalCases.ToString();
        totalDeathTxt.text = totalDeaths.ToString();
        //StartCoroutine(DownloadFlagCoroutine(flagUrl));

        //specificDataPlaceholder.enabled = false;
        //specificDataTxt.enabled = false;
    }

    public void SetSpecificData(string _stateName, string flagUrl, IndianStateSpeciifcFilter specificFilter, float value)
    {
        StateName = _stateName;
        totalCasePlaceholder.enabled = false;
        totalDeathPlaceholder.enabled = false;
        totalCaseTxt.enabled = false;
        totalDeathTxt.enabled = false;
        specificDataPlaceholder.enabled = true;
        specificDataTxt.enabled = true;

        stateNameTxt.text = _stateName;
        //StartCoroutine(DownloadFlagCoroutine(flagUrl)); 

        switch (specificFilter)
        {
            case IndianStateSpeciifcFilter.ACTIVE:
                specificDataPlaceholder.text = "Active Cases";
                break;
            case IndianStateSpeciifcFilter.CONFIRM_FOREIGN:
                specificDataPlaceholder.text = "Foreign Cases";
                break;
            case IndianStateSpeciifcFilter.CONFIRM_INDIA:
                specificDataPlaceholder.text = "Indian Cases";
                break;
            case IndianStateSpeciifcFilter.RECOVERED:
                specificDataPlaceholder.text = "Recovered";
                break;
            case IndianStateSpeciifcFilter.DEATH:
                specificDataPlaceholder.text = "Deaths";
                break;

            default:
                specificDataPlaceholder.enabled = false;
                specificDataTxt.enabled = false;
                break;
        }

        specificDataTxt.text = value.ToString();

    }
    private IEnumerator DownloadFlagCoroutine(string url)
    {
        UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(url);
        yield return webRequest.SendWebRequest();


        if (webRequest.isHttpError || webRequest.isNetworkError)
        {
            Debug.Log(webRequest.error);
        }
        else
        {
            Texture2D texture2d = DownloadHandlerTexture.GetContent(webRequest);

            Sprite sprite = null;
            sprite = Sprite.Create(texture2d, new Rect(0, 0, texture2d.width, texture2d.height), Vector2.zero);

            if (sprite != null)
            {
                flag.sprite = sprite;
            }
        }
    }
}
