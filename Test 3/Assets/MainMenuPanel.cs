using UnityEngine;
using Danish.Covid.API;
using Danish.Covid.Country;
using System.Linq;

public class MainMenuPanel : MonoBehaviour
{
    [SerializeField] GameObject dataPanel;
    [SerializeField] GameObject listPanel;

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
        APIManager.instance.FetchDataFromAPI(APIManager.instance.allCountryAPI, SuccessCountryData, FailureCountryData);
    }

    public void ShowDataPanel()
    {
        dataPanel.SetActive(true);
        listPanel.SetActive(false);
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


    private void FailureCountryData(UnityEngine.Networking.UnityWebRequest webRequest)
    {
        string data = webRequest.downloadHandler.text;
        Debug.LogError("FailureTotalCases: " + data);
    }

    public void ShowIndianStatesPanel()
    {
        dataPanel.SetActive(false);
        listPanel.SetActive(true);
        APIManager.instance.FetchDataFromAPI(APIManager.instance.allCountryAPI, OnSuccessIndiaLatestData, OnFailureIndiaLatestData);
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
}
