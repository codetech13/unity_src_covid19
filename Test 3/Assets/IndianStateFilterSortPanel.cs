using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Danish.Covid.Country;
using UnityEngine;

public class IndianStateFilterSortPanel : MonoBehaviour
{

    [SerializeField] CustomToggle sortTotalCase;
    [SerializeField] CustomToggle sortDeath;
    [SerializeField] CustomToggle sortRecoverd;
    [SerializeField] CustomToggle confirmForeign;
    [SerializeField] CustomToggle confirmIndian;

    [SerializeField] CustomToggle[] toggles;

    public void OnClickTotalCaseToggle(CustomToggle _toggle)
    {
        Debug.Log("OnClickTotalCaseToggle");
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
    public void OnClickForeignCaseToggle(CustomToggle _toggle)
    {
        DisableAllToggles(_toggle);

        confirmForeign.IsOn = true;
        confirmForeign.RefreshView();
    }
    public void OnClickIndianToggle(CustomToggle _toggle)
    {
        DisableAllToggles(_toggle);

        confirmIndian.IsOn = true;
        confirmIndian.RefreshView();
    }
    public void OnClickRecoverdToggle(CustomToggle _toggle)
    {
        DisableAllToggles(_toggle);

        sortRecoverd.IsOn = true;
        sortRecoverd.RefreshView();
    }



    void DisableAllToggles(CustomToggle _toggle)
    {
        Debug.Log("DisableAllToggles");
        for (int i = 0; i < toggles.Length; i++)
        {
            if (_toggle != toggles[i])
            {
                Debug.Log("if");
                toggles[i].IsOn = false;
                toggles[i].RefreshView();
            }
            else
            {
                Debug.Log("else");
            }
        }
    }



    public void Confirm()
    {
        if (sortTotalCase.IsOn)
        {
            IndianStatesLatestList.instance.SetView(SortByTotalConfirmedINStates());
        }
        else if (sortDeath.IsOn)
        {
            IndianStatesLatestList.instance.SetView(SortByLatestDeathsINStates());

        }
        else if (confirmForeign.IsOn)
        {
            IndianStatesLatestList.instance.SetView(SortByConfirmedForeignINStates(), IndianStateSpeciifcFilter.CONFIRM_FOREIGN);

        }
        else if (confirmIndian.IsOn)
        {
            IndianStatesLatestList.instance.SetView(SortByConfirmedIndianINStates(), IndianStateSpeciifcFilter.CONFIRM_INDIA);

        }
        else if (sortRecoverd.IsOn)
        {
            IndianStatesLatestList.instance.SetView(SortbyRecoveredINStates(), IndianStateSpeciifcFilter.RECOVERED);

        }
    }


    private List<IndianStatesRegionalLatest> SortByLatestDeathsINStates()
    {
        List<IndianStatesRegionalLatest> regionalLatests = new List<IndianStatesRegionalLatest>();
        regionalLatests = MainMenuPanel.instance.indianStatesLatestData.Data.Regional.ToList();
        regionalLatests = regionalLatests.OrderByDescending(x => x.Deaths).ToList();

        return regionalLatests;

    }


    private List<IndianStatesRegionalLatest> SortByTotalConfirmedINStates()
    {
        List<IndianStatesRegionalLatest> regionalLatests = new List<IndianStatesRegionalLatest>();
        regionalLatests = MainMenuPanel.instance.indianStatesLatestData.Data.Regional.ToList();
        regionalLatests = regionalLatests.OrderByDescending(x => x.TotalConfirmed).ToList();

        return regionalLatests;

    }


    private List<IndianStatesRegionalLatest> SortByConfirmedIndianINStates()
    {
        List<IndianStatesRegionalLatest> regionalLatests = new List<IndianStatesRegionalLatest>();
        regionalLatests = MainMenuPanel.instance.indianStatesLatestData.Data.Regional.ToList();
        regionalLatests = regionalLatests.OrderByDescending(x => x.ConfirmedCasesIndian).ToList();

        return regionalLatests;

    }


    private List<IndianStatesRegionalLatest> SortByConfirmedForeignINStates()
    {
        List<IndianStatesRegionalLatest> regionalLatests = new List<IndianStatesRegionalLatest>();
        regionalLatests = MainMenuPanel.instance.indianStatesLatestData.Data.Regional.ToList();
        regionalLatests = regionalLatests.OrderByDescending(x => x.ConfirmedCasesForeign).ToList();

        return regionalLatests;

    }


    private List<IndianStatesRegionalLatest> SortbyRecoveredINStates()
    {
        List<IndianStatesRegionalLatest> regionalLatests = new List<IndianStatesRegionalLatest>();
        regionalLatests = MainMenuPanel.instance.indianStatesLatestData.Data.Regional.ToList();
        regionalLatests = regionalLatests.OrderByDescending(x => x.Discharged).ToList();

        return regionalLatests;

    }
}
