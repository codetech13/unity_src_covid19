using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Danish.Covid.Country;
using UnityEngine.Networking;
using System;
using System.Linq;
using Newtonsoft.Json;

namespace Danish.Covid.API
{
    public class APIManager : MonoBehaviour
    {
        [Header("API URL")]
        public string totalCasesAPI;
        public string allCountryAPI;

        [Header("API URL for INDIA ONLY")]
        public string historyOfStates;
        public string latestCountOfStates;

        [Header("Data Objects for INDIA Only")]
        [SerializeField] private IndiaStatesHistoryData indiaStatesHistory;
        [SerializeField] private IndianStatesLatestData indianStatesLatestData;

        [Header("Data Objects")]
        [SerializeField] private TotalCasesObject totalCases;
        [SerializeField] private CountryList allCountryData;
        [SerializeField] private List<AllCountryData> allCountryDatas = new List<AllCountryData>();

        public UnityEngine.Events.UnityAction<TotalCasesObject> TotalCases;

        public UnityEngine.Events.UnityAction<IndianStatesLatestData> IndianStatesLatestCases;

        public static APIManager instance;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            FetchTotalCases();
            FetchCountryData();
            //Invoke("FetchMe", 5.0f);
            //DownloadDataFromWebPage();

            Screen.fullScreen = true;
        }

        private void FetchMe()
        {
            allCountryDatas = SortbyDeaths(10);
        }

        public void FetchTotalCases()
        {
            FetchDataFromAPI(totalCasesAPI, SuccessTotalCases, FailureTotalCases);
        }

        public void FetchCountryData()
        {
            FetchDataFromAPI(allCountryAPI, SuccessCountryData, FailureCountryData);
        }

        private void SuccessTotalCases(UnityWebRequest webRequest)
        {
            string data = webRequest.downloadHandler.text;
            TotalCasesObject casesObject = JsonUtility.FromJson<TotalCasesObject>(data);
            totalCases = casesObject;

            TotalCases(casesObject);

            Debug.Log(data);
        }

        private void FailureTotalCases(UnityWebRequest webRequest)
        {
            string data = webRequest.downloadHandler.text;
            Debug.LogError("FailureTotalCases: " + data);

        }

        private void SuccessCountryData(UnityWebRequest webRequest)
        {
            string data = webRequest.downloadHandler.text;

            string jsonDataMe = data.Replace("long", "longme");

            jsonDataMe = "{ \"countryData\":" + jsonDataMe + "}";
            Debug.Log(jsonDataMe);
            CountryList allData = JsonUtility.FromJson<CountryList>(jsonDataMe);
            this.allCountryData = allData;

            MainMenuPanel.instance.SetFlagOnMainMenu();
        }

        private void FailureCountryData(UnityWebRequest webRequest)
        {
            string data = webRequest.downloadHandler.text;
            Debug.LogError("FailureTotalCases: " + data);

        }

        public void FetchIndiaLatestData()
        {

            FetchDataFromAPI(latestCountOfStates, OnSuccessIndiaLatestData, OnFailureIndiaLatestData);
        }

        private void OnSuccessIndiaLatestData(UnityWebRequest webRequest)
        {

            string data = webRequest.downloadHandler.text;
            IndianStatesLatestData indianStatesLatest = JsonConvert.DeserializeObject<IndianStatesLatestData>(data);
            this.indianStatesLatestData = indianStatesLatest;
            IndianStatesLatestCases(indianStatesLatestData);
        }

        private void OnFailureIndiaLatestData(UnityWebRequest webRequest)
        {

            string data = webRequest.downloadHandler.text;
            Debug.LogError("OnFailureIndiaLatestData: " + data);
        }

        public void FetchIndiaHistoryData()
        {

            FetchDataFromAPI(historyOfStates, OnSuccessIndiaHistoryData, OnFailureIndiaHistoryData);

        }

        private void OnSuccessIndiaHistoryData(UnityWebRequest webRequest)
        {

            string data = webRequest.downloadHandler.text;
            IndiaStatesHistoryData indianStatesHistory = JsonConvert.DeserializeObject<IndiaStatesHistoryData>(data);
            foreach (var item in indianStatesHistory.Data)
            {
                item.timeInString = item.Day.ToString();
            }
            this.indiaStatesHistory = indianStatesHistory;
            Invoke("TestMethod", 2.0f);
        }

        private void OnFailureIndiaHistoryData(UnityWebRequest webRequest)
        {

            string data = webRequest.downloadHandler.text;
            Debug.LogError("OnFailureIndiaHistoryData: " + data);
        }



        #region COROUTINE HANDLER

        public void FetchDataFromAPI(string apiUrl, Action<UnityWebRequest> successAction, Action<UnityWebRequest> failureAction)
        {
            StartCoroutine(FetchDataFromAPIRoutine(apiUrl, successAction, failureAction));
        }


        private IEnumerator FetchDataFromAPIRoutine(string apiUrl, Action<UnityWebRequest> successAction, Action<UnityWebRequest> failureAction)
        {
            UnityWebRequest webRequest = UnityWebRequest.Get(apiUrl);
            yield return webRequest.SendWebRequest();

            if (webRequest.isHttpError || webRequest.isNetworkError)
            {
                Debug.Log(webRequest.error);
                failureAction(webRequest);
            }
            else
            {
                successAction(webRequest);
            }
        }
        #endregion


        #region Filling DATA on respective objects

        private void FillData<D>(D dataObject, string data)
        {

        }
        #endregion

        #region Utilities Functions

        private List<AllCountryData> SortbyTotalCases(int numberOfCountry)
        {
            List<AllCountryData> datas = new List<AllCountryData>();
            datas = allCountryData.countryData;
            datas = datas.OrderByDescending(x => x.todayCases).ToList();

            int noToShow = GetRange(numberOfCountry);
            datas = datas.GetRange(0, noToShow);
            return datas;
        }

        private List<AllCountryData> SortbyDeaths(int numberOfCountry)
        {
            List<AllCountryData> datas = new List<AllCountryData>();
            datas = allCountryData.countryData;
            datas = datas.OrderByDescending(x => x.deaths).ToList();

            int noToShow = GetRange(numberOfCountry);
            datas = datas.GetRange(0, noToShow);
            return datas;
        }

        private List<AllCountryData> SortbyActive(int numberOfCountry)
        {
            List<AllCountryData> datas = new List<AllCountryData>();
            datas = allCountryData.countryData;
            datas = datas.OrderByDescending(x => x.active).ToList();

            int noToShow = GetRange(numberOfCountry);
            datas = datas.GetRange(0, noToShow);
            return datas;
        }

        private List<AllCountryData> SortbyCases(int numberOfCountry)
        {
            List<AllCountryData> datas = new List<AllCountryData>();
            datas = allCountryData.countryData;
            datas = datas.OrderByDescending(x => x.cases).ToList();

            int noToShow = GetRange(numberOfCountry);
            datas = datas.GetRange(0, noToShow);
            return datas;
        }

        private List<AllCountryData> SortbyCritical(int numberOfCountry)
        {
            List<AllCountryData> datas = new List<AllCountryData>();
            datas = allCountryData.countryData;
            datas = datas.OrderByDescending(x => x.critical).ToList();

            int noToShow = GetRange(numberOfCountry);
            datas = datas.GetRange(0, noToShow);
            return datas;
        }

        private List<AllCountryData> SortbyDeathsPerMillion(int numberOfCountry)
        {
            List<AllCountryData> datas = new List<AllCountryData>();
            datas = allCountryData.countryData;
            datas = datas.OrderByDescending(x => x.deathsPerOneMillion).ToList();

            int noToShow = GetRange(numberOfCountry);
            datas = datas.GetRange(0, noToShow);
            return datas;
        }

        private List<AllCountryData> SortbyCasesPerMillion(int numberOfCountry)
        {
            List<AllCountryData> datas = new List<AllCountryData>();
            datas = allCountryData.countryData;
            datas = datas.OrderByDescending(x => x.casesPerOneMillion).ToList();

            int noToShow = GetRange(numberOfCountry);
            datas = datas.GetRange(0, noToShow);
            return datas;
        }

        private List<AllCountryData> SortbyRecovered(int numberOfCountry)
        {
            List<AllCountryData> datas = new List<AllCountryData>();
            datas = allCountryData.countryData;
            datas = datas.OrderByDescending(x => x.recovered).ToList();

            int noToShow = GetRange(numberOfCountry);
            datas = datas.GetRange(0, noToShow);
            return datas;
        }

        private List<AllCountryData> SortbyTest(int numberOfCountry)
        {
            List<AllCountryData> datas = new List<AllCountryData>();
            datas = allCountryData.countryData;
            datas = datas.OrderByDescending(x => x.tests).ToList();

            int noToShow = GetRange(numberOfCountry);
            datas = datas.GetRange(0, noToShow);
            return datas;
        }

        private List<AllCountryData> SortbyTestPerMillion(int numberOfCountry)
        {
            List<AllCountryData> datas = new List<AllCountryData>();
            datas = allCountryData.countryData;
            datas = datas.OrderByDescending(x => x.testsPerOneMillion).ToList();

            int noToShow = GetRange(numberOfCountry);
            datas = datas.GetRange(0, noToShow);
            return datas;
        }

        private List<AllCountryData> SortbyTodayDeaths(int numberOfCountry)
        {
            List<AllCountryData> datas = new List<AllCountryData>();
            datas = allCountryData.countryData;
            datas = datas.OrderByDescending(x => x.todayDeaths).ToList();

            int noToShow = GetRange(numberOfCountry);
            datas = datas.GetRange(0, noToShow);
            return datas;
        }

        private List<AllCountryData> SortbyTodayCases(int numberOfCountry)
        {
            List<AllCountryData> datas = new List<AllCountryData>();
            datas = allCountryData.countryData;
            datas = datas.OrderByDescending(x => x.todayCases).ToList();

            int noToShow = GetRange(numberOfCountry);
            datas = datas.GetRange(0, noToShow);
            return datas;
        }

        private int GetRange(int numbers)
        {
            int dataItems = 0;

            if (numbers <= allCountryData.countryData.Count)
            {
                dataItems = numbers;
            }
            else
            {
                dataItems = allCountryData.countryData.Count;
            }
            return dataItems;
        }


        public AllCountryData GetDataViaCountryName(string country)
        {
            AllCountryData countryData = null;
            countryData = allCountryData.countryData.Find(x => x.country == country);
            return countryData;
        }
        #endregion

        #region LATEST DATA ONLY FOR INDIA
        private List<IndianStatesRegionalLatest> SortByLatestDeathsINStates()
        {
            List<IndianStatesRegionalLatest> regionalLatests = new List<IndianStatesRegionalLatest>();
            regionalLatests = indianStatesLatestData.Data.Regional.ToList();
            regionalLatests = regionalLatests.OrderByDescending(x => x.Deaths).ToList();

            return regionalLatests;

        }


        private List<IndianStatesRegionalLatest> SortByTotalConfirmedINStates()
        {
            List<IndianStatesRegionalLatest> regionalLatests = new List<IndianStatesRegionalLatest>();
            regionalLatests = indianStatesLatestData.Data.Regional.ToList();
            regionalLatests = regionalLatests.OrderByDescending(x => x.TotalConfirmed).ToList();

            return regionalLatests;

        }


        private List<IndianStatesRegionalLatest> SortByConfirmedIndianINStates()
        {
            List<IndianStatesRegionalLatest> regionalLatests = new List<IndianStatesRegionalLatest>();
            regionalLatests = indianStatesLatestData.Data.Regional.ToList();
            regionalLatests = regionalLatests.OrderByDescending(x => x.ConfirmedCasesIndian).ToList();

            return regionalLatests;

        }


        private List<IndianStatesRegionalLatest> SortByConfirmedForeignINStates()
        {
            List<IndianStatesRegionalLatest> regionalLatests = new List<IndianStatesRegionalLatest>();
            regionalLatests = indianStatesLatestData.Data.Regional.ToList();
            regionalLatests = regionalLatests.OrderByDescending(x => x.ConfirmedCasesForeign).ToList();

            return regionalLatests;

        }


        private List<IndianStatesRegionalLatest> SortByDischargedINStates()
        {
            List<IndianStatesRegionalLatest> regionalLatests = new List<IndianStatesRegionalLatest>();
            regionalLatests = indianStatesLatestData.Data.Regional.ToList();
            regionalLatests = regionalLatests.OrderByDescending(x => x.Discharged).ToList();

            return regionalLatests;

        }


        private IndianStatesRegionalLatest LatestDataByNameOfINStates(string statesName)
        {
            IndianStatesRegionalLatest regionalLatests = new IndianStatesRegionalLatest();
            regionalLatests = indianStatesLatestData.Data.Regional.First(x => x.Loc == statesName);
            return regionalLatests;

        }

        private List<IndianStatesRegional> SortByDeathsStatesHistory(DateTimeOffset offset)
        {
            List<IndianStatesRegional> regionalHistoryByDeath = new List<IndianStatesRegional>();
            regionalHistoryByDeath = indiaStatesHistory.Data.FirstOrDefault(x => x.Day.ToString() == offset.ToString()).Regional.ToList();
            regionalHistoryByDeath = regionalHistoryByDeath.OrderByDescending(x => x.Deaths).ToList();

            return regionalHistoryByDeath;

        }

        private List<IndianStatesRegional> SortByTotalConfirmStatesHistory(DateTimeOffset offset)
        {
            List<IndianStatesRegional> regionaTotConfirmByDeath = new List<IndianStatesRegional>();
            regionaTotConfirmByDeath = indiaStatesHistory.Data.FirstOrDefault(x => x.Day.ToString() == offset.ToString()).Regional.ToList();
            regionaTotConfirmByDeath = regionaTotConfirmByDeath.OrderByDescending(x => x.TotalConfirmed).ToList();

            return regionaTotConfirmByDeath;

        }

        private List<IndianStatesRegional> SortByDischargedStatesHistory(DateTimeOffset offset)
        {
            List<IndianStatesRegional> regionalHistoryByDischarged = new List<IndianStatesRegional>();
            regionalHistoryByDischarged = indiaStatesHistory.Data.FirstOrDefault(x => x.Day.ToString() == offset.ToString()).Regional.ToList();
            regionalHistoryByDischarged = regionalHistoryByDischarged.OrderByDescending(x => x.Discharged).ToList();

            return regionalHistoryByDischarged;

        }

        private List<IndianStatesRegional> GetUniqueStateHistoryData(string stateName)
        {
            List<IndianStatesRegional> uniqueStateHistoryData = new List<IndianStatesRegional>();
            for (int i = 0; i < indiaStatesHistory.Data.Length; i++)
            {
                for (int j = 0; j < indiaStatesHistory.Data[i].Regional.Length; j++)
                {
                    if (indiaStatesHistory.Data[i].Regional[j].Loc == stateName)
                    {
                        uniqueStateHistoryData.Add(indiaStatesHistory.Data[i].Regional[j]);
                    }
                }
            }
            return uniqueStateHistoryData;

        }

        private List<IndianStatesSummary> GetStatesSummaries()
        {
            List<IndianStatesSummary> uniqueStateHistoryData = new List<IndianStatesSummary>();
            for (int i = 0; i < indiaStatesHistory.Data.Length; i++)
            {
                uniqueStateHistoryData.Add(indiaStatesHistory.Data[i].Summary);
            }
            return uniqueStateHistoryData;

        }

        private IndianStatesSummary GetSummary(DateTimeOffset dateTimeOffset)
        {
            IndianStatesSummary uniqueStateHistoryData = new IndianStatesSummary();
            for (int i = 0; i < indiaStatesHistory.Data.Length; i++)
            {
                if (indiaStatesHistory.Data[i].Day == dateTimeOffset)
                {
                    uniqueStateHistoryData = indiaStatesHistory.Data[i].Summary;
                }
            }
            return uniqueStateHistoryData;

        }

        private IndianStatesLatestSummary GetLatestSummary(DateTimeOffset dateTimeOffset)
        {
            IndianStatesLatestSummary latestSummary = new IndianStatesLatestSummary();
            latestSummary = indianStatesLatestData.Data.Summary;
            return latestSummary;

        }

        #endregion
    }
}
