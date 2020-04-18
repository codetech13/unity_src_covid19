using System;

namespace Danish.Covid.Country
{
    [Serializable]
    public class IndiaStatesHistoryData
    {
        public bool Success ;
        public IndianStatesHistoryObject[] Data ;
        public DateTimeOffset LastRefreshed ;
        public DateTimeOffset LastOriginUpdate ;
    }
}
