using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class CountryPrefabData : MonoBehaviour
{
    [SerializeField] UnityEngine.UI.Image flag;
    [SerializeField] TMP_Text countryNameTxt;
    [SerializeField] TMP_Text totalCasePlaceholder;
    [SerializeField] TMP_Text totalDeathPlaceholder;
    [SerializeField] TMP_Text totalCaseTxt;
    [SerializeField] TMP_Text totalDeathTxt;

    [SerializeField] TMP_Text specificDataPlaceholder;
    [SerializeField] TMP_Text specificDataTxt;

    private string countryName;
    public string CountryName { get => countryName; set => countryName = value; }

    public void SetCountryData(string _countryName, string flagUrl, float totalCases, float totalDeaths)
    {
        CountryName = _countryName;
        countryNameTxt.text = _countryName;
        totalCasePlaceholder.enabled = true;
        totalDeathPlaceholder.enabled = true;
        totalCaseTxt.enabled = true;
        totalDeathTxt.enabled = true;
        totalCaseTxt.text = totalCases.ToString();
        totalDeathTxt.text = totalDeaths.ToString();
        StartCoroutine(DownloadFlagCoroutine(flagUrl));

        specificDataPlaceholder.enabled = false;
        specificDataTxt.enabled = false;
    }

    public void SetSpecificData(string _countryName, string flagUrl, SpecificFilter specificFilter, float value)
    {
        CountryName = _countryName;
        totalCasePlaceholder.enabled = false;
        totalDeathPlaceholder.enabled = false;
        totalCaseTxt.enabled = false;
        totalDeathTxt.enabled = false;
        specificDataPlaceholder.enabled = true;
        specificDataTxt.enabled = true;

        countryNameTxt.text = _countryName;
        StartCoroutine(DownloadFlagCoroutine(flagUrl));

        switch(specificFilter)
        {
            case SpecificFilter.ACTIVE:
                specificDataPlaceholder.text = "Active Cases";
                break;
            case SpecificFilter.CRITICAL:
                specificDataPlaceholder.text = "Critical Cases";
                break;
            case SpecificFilter.DPMILLION:
                specificDataPlaceholder.text = "Deaths / Million";
                break;
            case SpecificFilter.CPMILLION:
                specificDataPlaceholder.text = "Cases / Milliom";
                break;
            case SpecificFilter.RECOVERED:
                specificDataPlaceholder.text = "Recovered";
                break;
            case SpecificFilter.TEST:
                specificDataPlaceholder.text = "Total Tests";
                break;
            case SpecificFilter.TPMILLION:
                specificDataPlaceholder.text = "Tests / Million";
                break;
            case SpecificFilter.TODAYSCASE:
                specificDataPlaceholder.text = "Today's Cases";
                break;
            case SpecificFilter.TODAYSDEATH:
                specificDataPlaceholder.text = "Today's Deaths";
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
