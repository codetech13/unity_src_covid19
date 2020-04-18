using System;

namespace Danish.Covid.Country
{
    [Serializable]
    public class IndianStatesHistoryObject
    {
        public DateTimeOffset Day ;
        public string timeInString;
        public IndianStatesSummary Summary ;
        public IndianStatesRegional[] Regional ;
    }
}

