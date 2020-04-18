using System;
namespace Danish.Covid.Country
{
    [Serializable]

    public class IndianStatesLatestData
    {
        public bool Success ;
        public IndianStatesLatestObject Data ;
        public DateTimeOffset LastRefreshed ;
        public DateTimeOffset LastOriginUpdate ;
    }
}
