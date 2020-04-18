using Danish.Covid.Country;
using System.Collections.Generic;
using UnityEngine;

public class CountriesListPanel : MonoBehaviour
{
    [SerializeField] GameObject FilterPanel;

    [Header("Pregenrated GroupView Items")]
    [SerializeField] List<CountryPrefabData> listItemsPool;

    [SerializeField] GameObject counrtyPrefab;
    [SerializeField] GameObject rootForNewItems;

    public static CountriesListPanel instance;

    private void Awake()
    {
        instance = this;
    }

    List<GameObject> allClildGO = new List<GameObject>();

    public void SetView(List<AllCountryData> allCountryDatas)
    {
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
            listItemsPool[i].SetCountryData(allCountryDatas[i].country, allCountryDatas[i].countryInfo.flag, allCountryDatas[i].cases, allCountryDatas[i].deaths); //user the view item as omaha item
            allClildGO.Add(listItemsPool[i].gameObject);
        }
    }

    public void ShowFilterPanel()
    {
        FilterPanel.SetActive(true);
    }

    public void ShowHidePanel()
    {
        FilterPanel.SetActive(false);
    }
}
