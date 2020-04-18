using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Danish.Covid.Country;
using System;

public class ShowAllCountryData : MonoBehaviour
{
    [Header("URL")]
    [SerializeField]private string allCountryUrl;


    [Header("DATA")]
    [SerializeField] private CountryList allCountryData;

    [Header("PREFABS")]
    [SerializeField] private GameObject countryPrefabData;
    [SerializeField] private Transform parentTransform;


    DateTime localDateTime, univDateTime;
    private void Start()
    {
        Debug.Log(univDateTime);
        FetchCountryData();
        DateTime dateTime = new DateTime();
         dateTime = Danish.Covid.Utility.Utility.FromUnixTime(1586597726324);
        Debug.Log(localDateTime.ToLocalTime());

        //Debug.Log(dateTime.Date.ToLocalTime().ToString());
    }

    public void FetchCountryData()
    {
        StartCoroutine(FetchCountryDataRoutine(allCountryUrl));
    }

    private IEnumerator FetchCountryDataRoutine(string url)
    {
        UnityWebRequest webRequest = UnityWebRequest.Get(url);
        yield return webRequest.SendWebRequest();

        if (webRequest.isHttpError || webRequest.isNetworkError)
        {
            Debug.Log(webRequest.error);
        }
        else
        {
            string jsonData = webRequest.downloadHandler.text;
            string jsonDataMe = jsonData.Replace("long", "longme");
            Debug.Log(jsonData);
            Debug.Log(jsonDataMe);
            jsonDataMe = "{ \"countryData\":" + jsonDataMe + "}";
            Debug.Log(jsonDataMe);
            CountryList allData = JsonUtility.FromJson<CountryList>(jsonDataMe);
            this.allCountryData = allData;
            Invoke("InitData", 1.0f);
        }

    }

    private void InitData()
    {
        for (int i = 0; i < allCountryData.countryData.Count; i++)
        {
            GameObject countryObj = Instantiate(countryPrefabData, parentTransform);
            CountryPrefabData prefabData = countryObj.GetComponent<CountryPrefabData>();
            //prefabData.SetCountryData(allCountryData.countryData[i].country, allCountryData.countryData[i].countryInfo.flag);
        }
    }
}
