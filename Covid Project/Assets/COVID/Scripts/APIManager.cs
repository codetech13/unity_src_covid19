using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Danish.Covid.Country;
using UnityEngine.Networking;
using System;
using System.Linq;

namespace Danish.Covid.API
{
    public class APIManager : MonoBehaviour
    {
        [Header("API URL")]
        public string totalCasesAPI;
        public string allCountryAPI;


        [Header("Data Objects")]
        [SerializeField] private TotalCasesObject totalCases;
        [SerializeField] private CountryList allCountryData;
        [SerializeField] private List<AllCountryData> allCountryDatas = new List<AllCountryData>();

        public UnityEngine.Events.UnityAction<TotalCasesObject> TotalCases;

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
        }

        private void FailureCountryData(UnityWebRequest webRequest)
        {
            string data = webRequest.downloadHandler.text;
            Debug.LogError("FailureTotalCases: " + data);

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


        private AllCountryData GetDataViaCountryName(string country)
        {
            AllCountryData countryData = null;
            countryData = allCountryData.countryData.Find(x => x.country == country);
            return countryData;
        }
        #endregion
    }
}
