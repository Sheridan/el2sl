
namespace Bng.EL2SL.Base
{
    public class SyslogFacilityPriorityPair
    {
        private int _facilityLevel;
        private int _priorityLevel;
        private int _convertedToInt;

        public static int ConvertPairToInt(int facilityLevel, int priorityLevel)
        {
            return (facilityLevel * 8) + priorityLevel;
        }

        public static SyslogFacilityPriorityPair ConvertIntToPair(int i)
        {
            return new SyslogFacilityPriorityPair(i);
        }

        public SyslogFacilityPriorityPair(int facilityLevel, int priorityLevel)
        {
            _facilityLevel  = facilityLevel;
            _priorityLevel  = priorityLevel;
            _convertedToInt = ConvertPairToInt(_facilityLevel, _priorityLevel);
        }

        public SyslogFacilityPriorityPair(int i)
        {
            _convertedToInt = i;
            _facilityLevel  = (int)(_convertedToInt >> 3   );
            _priorityLevel  = (int)(_convertedToInt &  0x07);
        }

        public int Facility()
        {
            return _facilityLevel; 
        }
        
        public int Priority()
        {
            return _priorityLevel;
        }

        public int AsInt()
        {
            return _convertedToInt;
        }

        public string AsString()
        {
            return System.String.Format("{0}:{1}", _facilityLevel.ToString("D2"), _priorityLevel.ToString("D1"));
        }

        public string AsSyslogHead()
        {
            return System.String.Format("<{0}>", _convertedToInt);
        }
    }

}
