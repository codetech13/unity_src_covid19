using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Danish.Covid.Utility;

namespace Danish.Covid.Country
{
    public class MenuDataPanel : MonoBehaviour
    {
        [SerializeField] TMPro.TMP_Text cases;
        [SerializeField] TMPro.TMP_Text todayCases;
        [SerializeField] TMPro.TMP_Text death;
        [SerializeField] TMPro.TMP_Text todayDeath;
        [SerializeField] TMPro.TMP_Text recovered;
        [SerializeField] TMPro.TMP_Text active;
        [SerializeField] TMPro.TMP_Text critical;
        [SerializeField] TMPro.TMP_Text cpMillion;
        [SerializeField] TMPro.TMP_Text dpMillion;
        [SerializeField] TMPro.TMP_Text tests;
        [SerializeField] TMPro.TMP_Text tpMillion;
        [SerializeField] TMPro.TMP_Text affetecCountried;
        [SerializeField] TMPro.TMP_Text lastUpdatedTxt;

        private void Start()
        {
            API.APIManager.instance.TotalCases += SetData;
            API.APIManager.instance.IndianStatesLatestCases += LatestStatesData;
            LoadingAnimator.instance.showLoadingAnimation();
        }

        void SetData(TotalCasesObject _data)
        {
            LoadingAnimator.instance.HideLoadingAnimation();
            cases.text = _data.cases.ToString();
            todayCases.text = _data.todayCases.ToString();
            death.text = _data.deaths.ToString();
            todayDeath.text = _data.todayDeaths.ToString();
            recovered.text = _data.recovered.ToString();
            active.text = _data.active.ToString();
            critical.text = _data.critical.ToString();
            cpMillion.text = _data.casesPerOneMillion.ToString();
            dpMillion.text = _data.deathsPerOneMillion.ToString();
            tests.text = _data.tests.ToString();
            tpMillion.text = _data.testsPerOneMillion.ToString();
            affetecCountried.text = _data.affectedCountries.ToString();

            lastUpdatedTxt.text = "Last updated at : " + Utility.Utility.FromUnixTime(_data.updated).ToLongDateString();
            
        }


        public void IndianStatesLatestData()
        {
            API.APIManager.instance.FetchIndiaLatestData();
        }

        void LatestStatesData(IndianStatesLatestData latestData)
        {

        }
    }
}
