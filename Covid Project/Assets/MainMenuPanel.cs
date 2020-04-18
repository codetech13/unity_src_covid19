using UnityEngine;
using Danish.Covid.API;
using Danish.Covid.Country;
using System.Linq;
using System.Collections;
using UnityEngine.Networking;

public class MainMenuPanel : MonoBehaviour
{
    [SerializeField] GameObject dataPanel;
    [SerializeField] GameObject listPanel;
    [SerializeField] GameObject indianStateListPanel;

    [SerializeField] UnityEngine.UI.Image flagImage;

    private CountryList allData;
    public CountryList AllData { get => allData; set => allData = value; }

    private IndianStatesLatestData stateLAtestData;
    public IndianStatesLatestData indianStatesLatestData { get => stateLAtestData; set => stateLAtestData = value; }

    public static MainMenuPanel instance;

    private void Awake()
    {
        instance = this;
    }

    public void ShowCountriesPanel()
    {
        dataPanel.SetActive(false);
        listPanel.SetActive(true);
        indianStateListPanel.SetActive(false);
        APIManager.instance.FetchDataFromAPI(APIManager.instance.allCountryAPI, SuccessCountryData, FailureCountryData);
    }

    public void ShowDataPanel()
    {
        dataPanel.SetActive(true);
        listPanel.SetActive(false);
        indianStateListPanel.SetActive(false);
    }

    public void ShowIndianStatesPanel()
    {
        dataPanel.SetActive(false);
        listPanel.SetActive(false);
        indianStateListPanel.SetActive(true);
        APIManager.instance.FetchDataFromAPI(APIManager.instance.latestCountOfStates, OnSuccessIndiaLatestData, OnFailureIndiaLatestData);
    }

    private void SuccessCountryData(UnityEngine.Networking.UnityWebRequest webRequest)
    {
        string data = webRequest.downloadHandler.text;

        string jsonDataMe = data.Replace("long", "longme");

        jsonDataMe = "{ \"countryData\":" + jsonDataMe + "}";
        Debug.Log(jsonDataMe);
        AllData = JsonUtility.FromJson<CountryList>(jsonDataMe);
        CountriesListPanel.instance.SetView(AllData.countryData);
    }

    public void SetFlagOnMainMenu()
    {
        StartCoroutine(DownloadFlagCoroutine(APIManager.instance.GetDataViaCountryName("India").countryInfo.flag));
    }

    private void FailureCountryData(UnityEngine.Networking.UnityWebRequest webRequest)
    {
        string data = webRequest.downloadHandler.text;
        Debug.LogError("FailureTotalCases: " + data);
    }

    private void OnSuccessIndiaLatestData(UnityEngine.Networking.UnityWebRequest webRequest)
    {

        string data = webRequest.downloadHandler.text;
        IndianStatesLatestData indianStatesLatest = Newtonsoft.Json.JsonConvert.DeserializeObject<IndianStatesLatestData>(data);
        this.indianStatesLatestData = indianStatesLatest;
        IndianStatesLatestList.instance.SetView(this.indianStatesLatestData.Data.Regional.ToList());
    }

    private void OnFailureIndiaLatestData(UnityEngine.Networking.UnityWebRequest webRequest)
    {

        string data = webRequest.downloadHandler.text;
        Debug.LogError("OnFailureIndiaLatestData: " + data);
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
                flagImage.sprite = sprite;
            }
        }
    }
}
