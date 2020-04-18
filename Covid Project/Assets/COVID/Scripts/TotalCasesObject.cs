using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Danish.Covid.Country
{
    [System.Serializable]

    public class TotalCasesObject
    {
        public long updated ;
        public int cases ;
        public int todayCases ;
        public int deaths ;
        public int todayDeaths ;
        public int recovered ;
        public int active ;
        public int critical ;
        public int casesPerOneMillion ;
        public int deathsPerOneMillion ;
        public int tests ;
        public double testsPerOneMillion ;
        public int affectedCountries ;
    }
}
