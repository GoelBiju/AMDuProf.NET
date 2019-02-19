using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.InteropServices;


namespace AMDuProf.NET
{
    /// <summary>
    /// 
    /// </summary>
    class AMDTPowerProfileDataTypes
    {
        /// <summary>
        /// 
        /// </summary>
        public enum AMDTPwrProfileMode
        {
            AMDT_PWR_MODE_TIMELINE_ONLINE,
            AMDT_PWR_MODE_TIMELINE_OFFLINE,
            AMDT_PWR_MODE_APP_ANALYSIS,
            AMDT_PWR_MODE_TRANSLATE,
            AMDT_PWR_MODE_INSTANT_COUNTER,
            AMDT_PWR_MODE_CNT
        }


        /// <summary>
        /// 
        /// </summary>
        public enum AMDTCounter
        {
            AMD_PWR_SOCKET_POWER = 1,
            AMD_PWR_SOCKET_TEMPERATURE,
            AMD_PWR_SOCKET_STAPM_LIMIT,
            AMD_PWR_SOCKET_PPT_FAST_LIMIT,
            AMD_PWR_SOCKET_PPT_SLOW_LIMIT,
            AMD_PWR_SOCKET_PPT_LIMIT,
            AMD_PWR_SOCKET_COUNTER_CNT = AMD_PWR_SOCKET_PPT_LIMIT,
        }


        /// <summary>
        /// 
        /// </summary>
        public enum AMDTDeviceType
        {
            AMDT_PWR_DEVICE_SYSTEM,
            AMDT_PWR_DEVICE_PACKAGE,
            AMDT_PWR_DEVICE_CPU_COMPUTE_UNIT,
            AMDT_PWR_DEVICE_CPU_CORE,
            AMDT_PWR_DEVICE_DIE,
            AMDT_PWR_DEVICE_PHYSICAL_CORE,
            AMDT_PWR_DEVICE_THREAD,
            AMDT_PWR_DEVICE_INTERNAL_GPU,
            AMDT_PWR_DEVICE_EXTERNAL_GPU,
            AMDT_PWR_DEVICE_SVNI2,
            AMDT_PWR_DEVICE_CNT
        }


        /// <summary>
        /// 
        /// </summary>
        public enum AMDTPwrCategory
        {
            AMDT_PWR_CATEGORY_POWER,
            AMDT_PWR_CATEGORY_FREQUENCY,
            AMDT_PWR_CATEGORY_TEMPERATURE,
            AMDT_PWR_CATEGORY_VOLTAGE,
            AMDT_PWR_CATEGORY_CURRENT,
            AMDT_PWR_CATEGORY_PSTATE,
            AMDT_PWR_CATEGORY_CSTATES_RESIDENCY,
            AMDT_PWR_CATEGORY_TIME,
            AMDT_PWR_CATEGORY_ENERGY,
            AMDT_PWR_CATEGORY_CORRELATED_POWER,
            AMDT_PWR_CATEGORY_CAC,
            AMDT_PWR_CATEGORY_CONTROLLER,
            AMDT_PWR_CATEGORY_DPM,
            AMDT_PWR_CATEGORY_CNT,
        }


        /// <summary>
        /// 
        /// </summary>
        public enum AMDTPwrAggregation
        {
            AMDT_PWR_VALUE_SINGLE,                  
            AMDT_PWR_VALUE_CUMULATIVE,              
            AMDT_PWR_VALUE_HISTOGRAM,               
            AMDT_PWR_VALUE_CNT                      
        }


        /// <summary>
        /// 
        /// </summary>
        public enum AMDTPwrUnit
        {
            AMDT_PWR_UNIT_TYPE_COUNT,             
            AMDT_PWR_UNIT_TYPE_NUMBER,            
            AMDT_PWR_UNIT_TYPE_PERCENT,           
            AMDT_PWR_UNIT_TYPE_RATIO,             
            AMDT_PWR_UNIT_TYPE_MILLI_SECOND,      
            AMDT_PWR_UNIT_TYPE_JOULE,             
            AMDT_PWR_UNIT_TYPE_WATT,              
            AMDT_PWR_UNIT_TYPE_VOLT,              
            AMDT_PWR_UNIT_TYPE_MILLI_AMPERE,      
            AMDT_PWR_UNIT_TYPE_MEGA_HERTZ,        
            AMDT_PWR_UNIT_TYPE_CENTIGRADE,        
            AMDT_PWR_UNIT_TYPE_CNT                
        }


        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct AMDTPwrCounterDesc
        {
            // counter index
            public uint m_counterID;
            // device id
            public uint m_deviceId;
            // device type
            public AMDTDeviceType m_devType;
            // instance id
            public uint m_devInstanceId;
            // name
            public char* m_name;
            // description
            public char* m_description;
            // power/frequency/temperature
            public AMDTPwrCategory m_category;
            // single/histogram/cumulative
            public AMDTPwrAggregation m_aggregation;
            // minimum counter value
            public double m_minValue;
            // maximum counter value
            public double m_maxValue;
            // Units: Seconds/Mhz/Joules/Watts/Volt/Ampere
            public AMDTPwrUnit m_units;
            // counter has child counters
            public bool m_isParentCounter;
        }


        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct AMDTPwrSystemTime
        {
            // Seconds
            public ulong m_second;
            // milliseconds
            public ulong m_microSecond;
        }


        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Ansi)]
        public unsafe struct AMDTPwrCounterValueUnion
        {
            // counter value
            [FieldOffset(0)]
            public float m_data;

            // pointer to multi value array
            [FieldOffset(0)]
            public float* m_pData;
        }


        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct AMDTPwrCounterValue
        {
            // counter index
            public uint m_counterID;
            // number of value for this counter
            public uint m_valueCnt;
            // relate to the union in this struct
            public AMDTPwrCounterValueUnion data;
        }


        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct AMDTPwrSample
        {
            // start time of profiling
            public AMDTPwrSystemTime m_systemTime;
            // elapsed time in milliseconds - relative to start time of profile
            public ulong m_elapsedTimeMs;
            // record id
            public ulong m_recordId;
            // number of counter values available
            public uint m_numOfCounter;
            // list of counter values
            public AMDTPwrCounterValue* m_counterValues;
        }
    }
}
