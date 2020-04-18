using Danish.Covid.Country;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum SpecificFilter
{
    ACTIVE,
    CRITICAL,
    DPMILLION,
    CPMILLION,
    RECOVERED,
    TEST,
    TPMILLION,
    TODAYSCASE,
    TODAYSDEATH
}

public class FilterSortPanel : MonoBehaviour
{
    [SerializeField] CustomToggle sortTotalCase;
    [SerializeField] CustomToggle sortDeath;
    [SerializeField] CustomToggle sortActive;
    [SerializeField] CustomToggle sortCritical;
    [SerializeField] CustomToggle sortDPmillion;
    [SerializeField] CustomToggle sortCPmillion;
    [SerializeField] CustomToggle sortRecoverd;
    [SerializeField] CustomToggle sortTest;
    [SerializeField] CustomToggle sortTPmillion;
    [SerializeField] CustomToggle sortTodaysCase;
    [SerializeField] CustomToggle sortTodaysDeath;

    [SerializeField] CustomToggle[] toggles;

    public void OnClickTotalCaseToggle(CustomToggle _toggle)
    {
        DisableAllToggles(_toggle);

        sortTotalCase.IsOn = true;
        sortTotalCase.RefreshView();
    }
    public void OnClickDeathToggle(CustomToggle _toggle)
    {
        DisableAllToggles(_toggle);

        sortDeath.IsOn = true;
        sortDeath.RefreshView();
    }
    public void OnClickActiveCaseToggle(CustomToggle _toggle)
    {
        DisableAllToggles(_toggle);

        sortActive.IsOn = true;
        sortActive.RefreshView();
    }
    public void OnClickCriticalToggle(CustomToggle _toggle)
    {
        DisableAllToggles(_toggle);

        sortCritical.IsOn = true;
        sortCritical.RefreshView();
    }
    public void OnClickCPmillionToggle(CustomToggle _toggle)
    {
        DisableAllToggles(_toggle);

        sortCPmillion.IsOn = true;
        sortCPmillion.RefreshView();
    }
    public void OnClickDPmillionToggle(CustomToggle _toggle)
    {
        DisableAllToggles(_toggle);

        sortDPmillion.IsOn = true;
        sortDPmillion.RefreshView();
    }
    public void OnClickRecoverdToggle(CustomToggle _toggle)
    {
        DisableAllToggles(_toggle);

        sortRecoverd.IsOn = true;
        sortRecoverd.RefreshView();
    }
    public void OnClickTestToggle(CustomToggle _toggle)
    {
        DisableAllToggles(_toggle);

        sortTest.IsOn = true;
        sortTest.RefreshView();
    }
    public void OnClickTPmillionToggle(CustomToggle _toggle)
    {
        DisableAllToggles(_toggle);

        sortTPmillion.IsOn = true;
        sortTPmillion.RefreshView();
    }
    public void OnClickTodaysCaseToggle(CustomToggle _toggle)
    {
        DisableAllToggles(_toggle);

        sortTodaysCase.IsOn = true;
        sortTodaysCase.RefreshView();
    }
    public void OnClickTodaysDeathToggle(CustomToggle _toggle)
    {
        DisableAllToggles(_toggle);

        sortTodaysDeath.IsOn = true;
        sortTodaysDeath.RefreshView();
    }

    public void Confirm()
    {
        if (sortTotalCase.IsOn)
        {
            CountriesListPanel.instance.SetView(SortbyTotalCases(MainMenuPanel.instance.AllData.countryData.Count, MainMenuPanel.instance.AllData));
        }
        else if (sortDeath.IsOn)
        {
            CountriesListPanel.instance.SetView(SortbyDeaths(MainMenuPanel.instance.AllData.countryData.Count, MainMenuPanel.instance.AllData));

        }
        else if (sortActive.IsOn)
        {
            CountriesListPanel.instance.SetView(SortbyActive(MainMenuPanel.instance.AllData.countryData.Count, MainMenuPanel.instance.AllData), SpecificFilter.ACTIVE);

        }
        else if (sortCritical.IsOn)
        {
            CountriesListPanel.instance.SetView(SortbyCritical(MainMenuPanel.instance.AllData.countryData.Count, MainMenuPanel.instance.AllData), SpecificFilter.CRITICAL);

        }
        else if (sortDPmillion.IsOn)
        {
            CountriesListPanel.instance.SetView(SortbyDeathsPerMillion(MainMenuPanel.instance.AllData.countryData.Count, MainMenuPanel.instance.AllData), SpecificFilter.DPMILLION);

        }
        else if (sortCPmillion.IsOn)
        {
            CountriesListPanel.instance.SetView(SortbyCasesPerMillion(MainMenuPanel.instance.AllData.countryData.Count, MainMenuPanel.instance.AllData), SpecificFilter.CPMILLION);

        }
        else if (sortRecoverd.IsOn)
        {
            CountriesListPanel.instance.SetView(SortbyRecovered(MainMenuPanel.instance.AllData.countryData.Count, MainMenuPanel.instance.AllData), SpecificFilter.RECOVERED);

        }
        else if (sortTest.IsOn)
        {
            CountriesListPanel.instance.SetView(SortbyTest(MainMenuPanel.instance.AllData.countryData.Count, MainMenuPanel.instance.AllData), SpecificFilter.TEST);

        }
        else if (sortTPmillion.IsOn)
        {
            CountriesListPanel.instance.SetView(SortbyTestPerMillion(MainMenuPanel.instance.AllData.countryData.Count, MainMenuPanel.instance.AllData), SpecificFilter.TPMILLION);

        }
        else if (sortTodaysCase.IsOn)
        {
            CountriesListPanel.instance.SetView(SortbyTodayCases(MainMenuPanel.instance.AllData.countryData.Count, MainMenuPanel.instance.AllData), SpecificFilter.TODAYSCASE);

        }
        else if (sortTodaysDeath.IsOn)
        {
            CountriesListPanel.instance.SetView(SortbyTodayDeaths(MainMenuPanel.instance.AllData.countryData.Count, MainMenuPanel.instance.AllData), SpecificFilter.TODAYSDEATH);

        }
    }

    void DisableAllToggles(CustomToggle _toggle)
    {
        for (int i = 0; i < toggles.Length; i++)
        {
            if (_toggle != toggles[i])
            {
                toggles[i].IsOn = false;
                toggles[i].RefreshView();
            }
            else
            {
            }
        }
    }

    #region Utilities Functions

    private List<AllCountryData> SortbyDeaths(int numberOfCountry, CountryList allCountryData)
    {
        List<AllCountryData> datas = new List<AllCountryData>();
        datas = allCountryData.countryData;
        datas = datas.OrderByDescending(x => x.deaths).ToList();

        int noToShow = GetRange(numberOfCountry, allCountryData);
        datas = datas.GetRange(0, noToShow);
        return datas;
    }

    private List<AllCountryData> SortbyActive(int numberOfCountry, CountryList allCountryData)
    {
        List<AllCountryData> datas = new List<AllCountryData>();
        datas = allCountryData.countryData;
        datas = datas.OrderByDescending(x => x.active).ToList();

        int noToShow = GetRange(numberOfCountry, allCountryData);
        datas = datas.GetRange(0, noToShow);
        return datas;
    }

    private List<AllCountryData> SortbyTotalCases(int numberOfCountry, CountryList allCountryData)
    {
        List<AllCountryData> datas = new List<AllCountryData>();
        datas = allCountryData.countryData;
        datas = datas.OrderByDescending(x => x.cases).ToList();

        int noToShow = GetRange(numberOfCountry, allCountryData);
        datas = datas.GetRange(0, noToShow);
        return datas;
    }

    private List<AllCountryData> SortbyCritical(int numberOfCountry, CountryList allCountryData)
    {
        List<AllCountryData> datas = new List<AllCountryData>();
        datas = allCountryData.countryData;
        datas = datas.OrderByDescending(x => x.critical).ToList();

        int noToShow = GetRange(numberOfCountry, allCountryData);
        datas = datas.GetRange(0, noToShow);
        return datas;
    }

    private List<AllCountryData> SortbyDeathsPerMillion(int numberOfCountry, CountryList allCountryData)
    {
        List<AllCountryData> datas = new List<AllCountryData>();
        datas = allCountryData.countryData;
        datas = datas.OrderByDescending(x => x.deathsPerOneMillion).ToList();

        int noToShow = GetRange(numberOfCountry, allCountryData);
        datas = datas.GetRange(0, noToShow);
        return datas;
    }

    private List<AllCountryData> SortbyCasesPerMillion(int numberOfCountry, CountryList allCountryData)
    {
        List<AllCountryData> datas = new List<AllCountryData>();
        datas = allCountryData.countryData;
        datas = datas.OrderByDescending(x => x.casesPerOneMillion).ToList();

        int noToShow = GetRange(numberOfCountry, allCountryData);
        datas = datas.GetRange(0, noToShow);
        return datas;
    }

    private List<AllCountryData> SortbyRecovered(int numberOfCountry, CountryList allCountryData)
    {
        List<AllCountryData> datas = new List<AllCountryData>();
        datas = allCountryData.countryData;
        datas = datas.OrderByDescending(x => x.recovered).ToList();

        int noToShow = GetRange(numberOfCountry, allCountryData);
        datas = datas.GetRange(0, noToShow);
        return datas;
    }

    private List<AllCountryData> SortbyTest(int numberOfCountry, CountryList allCountryData)
    {
        List<AllCountryData> datas = new List<AllCountryData>();
        datas = allCountryData.countryData;
        datas = datas.OrderByDescending(x => x.tests).ToList();

        int noToShow = GetRange(numberOfCountry, allCountryData);
        datas = datas.GetRange(0, noToShow);
        return datas;
    }

    private List<AllCountryData> SortbyTestPerMillion(int numberOfCountry, CountryList allCountryData)
    {
        List<AllCountryData> datas = new List<AllCountryData>();
        datas = allCountryData.countryData;
        datas = datas.OrderByDescending(x => x.testsPerOneMillion).ToList();

        int noToShow = GetRange(numberOfCountry, allCountryData);
        datas = datas.GetRange(0, noToShow);
        return datas;
    }

    private List<AllCountryData> SortbyTodayDeaths(int numberOfCountry, CountryList allCountryData)
    {
        List<AllCountryData> datas = new List<AllCountryData>();
        datas = allCountryData.countryData;
        datas = datas.OrderByDescending(x => x.todayDeaths).ToList();

        int noToShow = GetRange(numberOfCountry, allCountryData);
        datas = datas.GetRange(0, noToShow);
        return datas;
    }

    private List<AllCountryData> SortbyTodayCases(int numberOfCountry, CountryList allCountryData)
    {
        List<AllCountryData> datas = new List<AllCountryData>();
        datas = allCountryData.countryData;
        datas = datas.OrderByDescending(x => x.todayCases).ToList();

        int noToShow = GetRange(numberOfCountry, allCountryData);
        datas = datas.GetRange(0, noToShow);
        return datas;
    }

    private int GetRange(int numbers, CountryList allCountryData)
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
    #endregion
}
