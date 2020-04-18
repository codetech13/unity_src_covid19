using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Danish.Covid.Country
{
    [System.Serializable]
    public class AllCountryData
    {
        public string country ;
        public CountryInfo countryInfo ;
        public object updated ;
        public int cases ;
        public int todayCases ;
        public int deaths ;
        public int todayDeaths ;
        public int recovered ;
        public int active ;
        public int critical ;
        public double casesPerOneMillion ;
        public double deathsPerOneMillion ;
        public int tests ;
        public int testsPerOneMillion ;
    }
}
