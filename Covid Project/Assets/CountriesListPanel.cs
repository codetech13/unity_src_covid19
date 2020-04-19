using Danish.Covid.API;
using Danish.Covid.Country;
using System.Collections.Generic;
using UnityEngine;

public class CountriesListPanel : MonoBehaviour
{
    [SerializeField] GameObject FilterPanel;
    [SerializeField] FS_Animation_Panel detailsPopUp;

    [Header("Pregenrated GroupView Items")]
    [SerializeField] List<CountryPrefabData> listItemsPool;

    [SerializeField] GameObject counrtyPrefab;
    [SerializeField] GameObject rootForNewItems;

    public static CountriesListPanel instance;

    List<AllCountryData> _allCountryDatas;

    private void Awake()
    {
        instance = this;
    }

    List<GameObject> allClildGO = new List<GameObject>();

    public void SetView(List<AllCountryData> allCountryDatas)
    {
        _allCountryDatas = allCountryDatas;
        if (allCountryDatas.Count > listItemsPool.Count)
        {
            int temp = (allCountryDatas.Count - listItemsPool.Count) + 5;

            for (int i = 0; i < temp; i++)
            {
                GameObject go = Instantiate(counrtyPrefab, rootForNewItems.transform, false);
                listItemsPool.Add(go.GetComponent<CountryPrefabData>());
            }
        }

        for (int i = 0; i < listItemsPool.Count; i++)
        {
            listItemsPool[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < allClildGO.Count; i++)
        {
            allClildGO[i].transform.SetParent(rootForNewItems.transform, false);
        }

        for (int i = 0; i < allCountryDatas.Count; i++)
        {
            listItemsPool[i].gameObject.SetActive(true);
            listItemsPool[i].transform.SetParent(rootForNewItems.transform, false);
            listItemsPool[i].SetCountryData(allCountryDatas[i].country, allCountryDatas[i].countryInfo.flag, allCountryDatas[i].cases, allCountryDatas[i].deaths);
            allClildGO.Add(listItemsPool[i].gameObject);
        }

        LoadingAnimator.instance.HideLoadingAnimation();
    }

    public void SetView(List<AllCountryData> allCountryDatas, SpecificFilter specificFilter)
    {
        _allCountryDatas = allCountryDatas;
        if (allCountryDatas.Count > listItemsPool.Count)
        {
            int temp = (allCountryDatas.Count - listItemsPool.Count) + 5;

            for (int i = 0; i < temp; i++)
            {
                GameObject go = Instantiate(counrtyPrefab, rootForNewItems.transform, false);
                listItemsPool.Add(go.GetComponent<CountryPrefabData>());
            }
        }

        for (int i = 0; i < listItemsPool.Count; i++)
        {
            listItemsPool[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < allClildGO.Count; i++)
        {
            allClildGO[i].transform.SetParent(rootForNewItems.transform, false);
        }

        for (int i = 0; i < allCountryDatas.Count; i++)
        {
            listItemsPool[i].gameObject.SetActive(true);
            listItemsPool[i].transform.SetParent(rootForNewItems.transform, false);

            switch (specificFilter)
            {
                case SpecificFilter.ACTIVE:
                    listItemsPool[i].SetSpecificData(allCountryDatas[i].country, allCountryDatas[i].countryInfo.flag, SpecificFilter.ACTIVE, allCountryDatas[i].active);
                    break;
                case SpecificFilter.CRITICAL:
                    listItemsPool[i].SetSpecificData(allCountryDatas[i].country, allCountryDatas[i].countryInfo.flag, SpecificFilter.CRITICAL, allCountryDatas[i].critical);
                    break;
                case SpecificFilter.DPMILLION:
                    listItemsPool[i].SetSpecificData(allCountryDatas[i].country, allCountryDatas[i].countryInfo.flag, SpecificFilter.DPMILLION, (float)allCountryDatas[i].deathsPerOneMillion);
                    break;
                case SpecificFilter.CPMILLION:
                    listItemsPool[i].SetSpecificData(allCountryDatas[i].country, allCountryDatas[i].countryInfo.flag, SpecificFilter.CPMILLION, (float)allCountryDatas[i].casesPerOneMillion);
                    break;
                case SpecificFilter.RECOVERED:
                    listItemsPool[i].SetSpecificData(allCountryDatas[i].country, allCountryDatas[i].countryInfo.flag, SpecificFilter.RECOVERED, allCountryDatas[i].recovered);
                    break;
                case SpecificFilter.TEST:
                    listItemsPool[i].SetSpecificData(allCountryDatas[i].country, allCountryDatas[i].countryInfo.flag, SpecificFilter.TEST, allCountryDatas[i].tests);
                    break;
                case SpecificFilter.TPMILLION:
                    listItemsPool[i].SetSpecificData(allCountryDatas[i].country, allCountryDatas[i].countryInfo.flag, SpecificFilter.TPMILLION, allCountryDatas[i].testsPerOneMillion);
                    break;
                case SpecificFilter.TODAYSCASE:
                    listItemsPool[i].SetSpecificData(allCountryDatas[i].country, allCountryDatas[i].countryInfo.flag, SpecificFilter.TODAYSCASE, allCountryDatas[i].todayCases);
                    break;
                case SpecificFilter.TODAYSDEATH:
                    listItemsPool[i].SetSpecificData(allCountryDatas[i].country, allCountryDatas[i].countryInfo.flag, SpecificFilter.TODAYSDEATH, allCountryDatas[i].todayDeaths);
                    break;

                default:
                    listItemsPool[i].SetCountryData(allCountryDatas[i].country, allCountryDatas[i].countryInfo.flag, allCountryDatas[i].cases, allCountryDatas[i].deaths);
                    break;
            }

            allClildGO.Add(listItemsPool[i].gameObject);
        }
        LoadingAnimator.instance.HideLoadingAnimation();
    }

    public void ShowCountryDetails(CountryPrefabData countryPrefabData)
    {

        if (detailsPopUp != null)
        {
            detailsPopUp.ShowPanel(true, 8f, true, 2.5f);
        }
        detailsPopUp.GetComponent<CountryDetailsPopUp>().SetView(GetDataViaCountryName(countryPrefabData.CountryName, APIManager.instance.allCountryData));
    }

    public void HideCountryDetailsPopup()
    {
        if (detailsPopUp != null)
        {
            detailsPopUp.HidePanel(false, 4f, false, 1.5f);
        }
    }


    private AllCountryData GetDataViaCountryName(string country, CountryList allCountryData)
    {
        AllCountryData countryData = null;
        countryData = allCountryData.countryData.Find(x => x.country == country);
        return countryData;
    }

    public void ShowFilterPanel()
    {
        FilterPanel.SetActive(true);
    }

    public void HideFilterPanel()
    {
        FilterPanel.SetActive(false);
    }
}
