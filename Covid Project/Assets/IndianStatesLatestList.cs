using System.Collections;
using System.Collections.Generic;
using Danish.Covid.API;
using Danish.Covid.Country;
using UnityEngine;

public class IndianStatesLatestList : MonoBehaviour
{
    [SerializeField] GameObject FilterPanel;
    [SerializeField] FS_Animation_Panel detailsPopUp;
    [SerializeField] CountryPrefabData indiaData;

    [Header("Pregenrated GroupView Items")]
    [SerializeField] List<IndianStatesLatestPrefabData> listItemsPool;

    [SerializeField] GameObject stateLatestPrefab;
    [SerializeField] GameObject rootForNewItems;

    public static IndianStatesLatestList instance;

    List<IndianStatesRegionalLatest> _allStatesLevel;

    private void Awake()
    {
        instance = this;
    }

    List<GameObject> allClildGO = new List<GameObject>();

    public void SetView(List<IndianStatesRegionalLatest> statesLatestData)
    {
        _allStatesLevel = statesLatestData;
        AllCountryData _indiaData = APIManager.instance.GetDataViaCountryName("India");
        indiaData.SetCountryData(_indiaData.country, _indiaData.countryInfo.flag, _indiaData.todayCases, _indiaData.todayDeaths);

        if (statesLatestData.Count > listItemsPool.Count)
        {
            int temp = (statesLatestData.Count - listItemsPool.Count) + 5;

            for (int i = 0; i < temp; i++)
            {
                GameObject go = Instantiate(stateLatestPrefab, rootForNewItems.transform, false);
                listItemsPool.Add(go.GetComponent<IndianStatesLatestPrefabData>());
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

        for (int i = 0; i < statesLatestData.Count; i++)
        {
            listItemsPool[i].gameObject.SetActive(true);
            listItemsPool[i].transform.SetParent(rootForNewItems.transform, false);
            listItemsPool[i].SetStatesData(statesLatestData[i].Loc, "", statesLatestData[i].TotalConfirmed, statesLatestData[i].Deaths);
            allClildGO.Add(listItemsPool[i].gameObject);
        }
        LoadingAnimator.instance.HideLoadingAnimation();
    }

    public void SetView(List<IndianStatesRegionalLatest> allStatesLatest, IndianStateSpeciifcFilter specificFilter)
    {
        _allStatesLevel = allStatesLatest;
        AllCountryData _indiaData = APIManager.instance.GetDataViaCountryName("india");
        indiaData.SetCountryData(_indiaData.country, _indiaData.countryInfo.flag, _indiaData.todayCases, _indiaData.todayDeaths);

        if (allStatesLatest.Count > listItemsPool.Count)
        {
            int temp = (allStatesLatest.Count - listItemsPool.Count) + 5;

            for (int i = 0; i < temp; i++)
            {
                GameObject go = Instantiate(stateLatestPrefab, rootForNewItems.transform, false);
                listItemsPool.Add(go.GetComponent<IndianStatesLatestPrefabData>());
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

        for (int i = 0; i < allStatesLatest.Count; i++)
        {
            listItemsPool[i].gameObject.SetActive(true);
            listItemsPool[i].transform.SetParent(rootForNewItems.transform, false);

            switch (specificFilter)
            {
                //case IndianStateSpeciifcFilter.ACTIVE:
                //    listItemsPool[i].SetSpecificData(allStatesLatest[i].Loc, "", IndianStateSpeciifcFilter.ACTIVE, allStatesLatest[i].TotalConfirmed);
                //    break;
                case IndianStateSpeciifcFilter.CONFIRM_FOREIGN:
                    listItemsPool[i].SetSpecificData(allStatesLatest[i].Loc, "", IndianStateSpeciifcFilter.CONFIRM_FOREIGN, allStatesLatest[i].ConfirmedCasesForeign);
                    break;
                case IndianStateSpeciifcFilter.CONFIRM_INDIA:
                    listItemsPool[i].SetSpecificData(allStatesLatest[i].Loc, "", IndianStateSpeciifcFilter.CONFIRM_INDIA, allStatesLatest[i].ConfirmedCasesIndian);
                    break;
                //case IndianStateSpeciifcFilter.DEATH:
                //    listItemsPool[i].SetSpecificData(allStatesLatest[i].Loc, "", IndianStateSpeciifcFilter.DEATH, allStatesLatest[i].Deaths);
                //    break;
                case IndianStateSpeciifcFilter.RECOVERED:
                    listItemsPool[i].SetSpecificData(allStatesLatest[i].Loc, "", IndianStateSpeciifcFilter.RECOVERED, allStatesLatest[i].Discharged);
                    break;

                default:
                    listItemsPool[i].SetStatesData(allStatesLatest[i].Loc, "", allStatesLatest[i].TotalConfirmed, allStatesLatest[i].Deaths);

                    break;
            }

            allClildGO.Add(listItemsPool[i].gameObject);
        }
    }

    public void ShowLatestStateDetails(IndianStatesLatestPrefabData statesPrefabData)
    {
        if (detailsPopUp != null)
        {
            detailsPopUp.ShowPanel(true, 8f, true, 2.5f);
        }
        detailsPopUp.GetComponent<IndianStatesDetailsPopUp>().SetView(GetDataViaStateName(statesPrefabData.StateName, MainMenuPanel.instance.indianStatesLatestData));
    }

    public void HideStateLatestPopUp()
    {
        if (detailsPopUp != null)
        {
            detailsPopUp.HidePanel(false, 4f, false, 1.5f);
        }
    }


    private IndianStatesRegionalLatest GetDataViaStateName(string StateName, IndianStatesLatestData allStateData)
    {
        for (int i = 0; i < allStateData.Data.Regional.Length; i++)
        {
            if( allStateData.Data.Regional[i].Loc == StateName)
            {
                return allStateData.Data.Regional[i];
            }
        }

        return null;
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
