using UnityEngine;
using Danish.Covid.API;
using Danish.Covid.Country;

public class MainMenuPanel : MonoBehaviour
{
    [SerializeField] GameObject dataPanel;
    [SerializeField] GameObject listPanel;

    private CountryList allData;
    public CountryList AllData { get => allData; set => allData = value; }

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
}
