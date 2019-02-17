using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Diagnostics;
using System.Threading;
using System.Runtime.InteropServices;


// Developed based on a template to interact with the AMDPowerProfileAPI.dll.


namespace AMDUProf.NET
{
    public enum AMDTPwrProfileMode
    {
        //AMDT_PWR_PROFILE_MODE_ONLINE,
        AMDT_PWR_MODE_TIMELINE_ONLINE,
        AMDT_PWR_MODE_TIMELINE_OFFLINE,
        AMDT_PWR_MODE_APP_ANALYSIS,
        AMDT_PWR_MODE_TRANSLATE,
        AMDT_PWR_MODE_INSTANT_COUNTER,
        AMDT_PWR_MODE_CNT
    }

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


    public enum AMDTPwrAggregation
    {
        AMDT_PWR_VALUE_SINGLE,                  /**< Single instantaneous value */
        AMDT_PWR_VALUE_CUMULATIVE,              /**< Cumulative value */
        AMDT_PWR_VALUE_HISTOGRAM,               /**< Histogram value */
        AMDT_PWR_VALUE_CNT                      /**< Total power value */
    }


    public enum AMDTPwrUnit
    {
        AMDT_PWR_UNIT_TYPE_COUNT,             /**< Count index */
        AMDT_PWR_UNIT_TYPE_NUMBER,            /**< Count index */
        AMDT_PWR_UNIT_TYPE_PERCENT,           /**< Percentage */
        AMDT_PWR_UNIT_TYPE_RATIO,             /**< Ratio */
        AMDT_PWR_UNIT_TYPE_MILLI_SECOND,      /**< Time in milli seconds*/
        AMDT_PWR_UNIT_TYPE_JOULE,             /**< Energy consumption */
        AMDT_PWR_UNIT_TYPE_WATT,              /**< Power consumption */
        AMDT_PWR_UNIT_TYPE_VOLT,              /**< Voltage */
        AMDT_PWR_UNIT_TYPE_MILLI_AMPERE,      /**< Current */
        AMDT_PWR_UNIT_TYPE_MEGA_HERTZ,        /**< Frequency type unit */
        AMDT_PWR_UNIT_TYPE_CENTIGRADE,        /**< Temperature type unit */
        AMDT_PWR_UNIT_TYPE_CNT                /**< Total power unit */
    }


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


    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct AMDTPwrSystemTime
    {
        // Seconds
        public ulong m_second;
        // milliseconds
        public ulong m_microSecond;
    }


    [StructLayout(LayoutKind.Explicit, CharSet=CharSet.Ansi)]
    public unsafe struct AMDTPwrCounterValueUnion
    {
        // counter value
        [FieldOffset(0)]
        public float m_data;

        // pointer to multi value array
        [FieldOffset(0)]
        public float* m_pData;
    }


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


    unsafe class Program
    {
        public const int AMDT_STATUS_OK = 0;

        [DllImport("AMDPowerProfileAPI.dll")]
        public static extern int AMDTPwrProfileInitialize(AMDTPwrProfileMode AMDTPwrProfileMode);

        [DllImport("AMDPowerProfileAPI.dll")]
        public static extern int AMDTPwrGetSupportedCounters(uint* pNumCounters, AMDTPwrCounterDesc** ppCounterDescs);

        [DllImport("AMDPowerProfileAPI.dll")]
        public static extern int AMDTPwrEnableCounter(uint counterId);

        [DllImport("AMDPowerProfileAPI.dll")]
        public static extern int AMDTPwrSetTimerSamplingPeriod(uint interval);

        [DllImport("AMDPowerProfileAPI.dll")]
        public static extern int AMDTPwrStartProfiling();

        [DllImport("AMDPowerProfileAPI.dll")]
        public static extern int AMDTPwrGetCounterDesc(uint counterId, [In, Out] ref AMDTPwrCounterDesc pCounterDesc);

        [DllImport("AMDPowerProfileAPI.dll")]
        public static extern int AMDTPwrReadAllEnabledCounters(uint* pNumOfSamples, AMDTPwrSample** ppData);

        [DllImport("AMDPowerProfileAPI.dll")]
        public static extern int AMDTPwrStopProfiling();

        [DllImport("AMDPowerProfileAPI.dll")]
        public static extern int AMDTPwrProfileClose();


        public static string CharPToString(char* convertCharP)
        {
            // Convert the char pointer to Ansi format to make it readable:
            // https://stackoverflow.com/questions/9041094/char-to-a-string-in-c-sharp
            return Marshal.PtrToStringAnsi((IntPtr)convertCharP);
        }


        static void Main(string[] args)
        {
            int hResult = AMDT_STATUS_OK;

            // Initialise online mode.
            hResult = AMDTPwrProfileInitialize(AMDTPwrProfileMode.AMDT_PWR_MODE_TIMELINE_ONLINE);

            // Configure profile run:
            //  1. Get support counters.
            //  2. Enable all counters.
            //  3. Set timer configuration.


            // 1. Get the supported counter details.
            uint nbrCounters = 0;
            AMDTPwrCounterDesc* pCounters = null;

            hResult = AMDTPwrGetSupportedCounters(&nbrCounters, &pCounters);
            Debug.Assert(hResult == AMDT_STATUS_OK);

            AMDTPwrCounterDesc* pCurrCounter = pCounters;
            
            for (uint cnt = 0; cnt < nbrCounters; cnt++, pCurrCounter++)
            {
                if (pCurrCounter != null)
                {
                    // Enable all the counters.
                    hResult = AMDTPwrEnableCounter(pCurrCounter->m_counterID);

                    // Display the description of all the counters.
                    Console.WriteLine(CharPToString(pCurrCounter->m_description));
                    Debug.Assert(hResult == AMDT_STATUS_OK);
                }
            }


            // Set the timer configuration
            // milliseconds
            uint samplingInterval = 100;
            // seconds
            uint profilingDuration = 10;

            hResult = AMDTPwrSetTimerSamplingPeriod(samplingInterval);
            Debug.Assert(hResult == AMDT_STATUS_OK);

            // Start the profile run.
            hResult = AMDTPwrStartProfiling();
            Debug.Assert(hResult == AMDT_STATUS_OK);


            // Collect and report the counter values periodically.
            //  1. Take the snapshot of the counter values.
            //  2. Read the counter values.
            //  3. Report the counter values.

            bool isProfiling = true;
            bool stopProfiling = false;

            uint nbrSamples = 0;

            while (isProfiling)
            {
                // Sleep for refresh duration
                Thread.Sleep((int)samplingInterval);

                // Read all the counter values.
                AMDTPwrSample* pSampleData = null;

                hResult = AMDTPwrReadAllEnabledCounters(&nbrSamples, &pSampleData);

                if (hResult != AMDT_STATUS_OK)
                {
                    continue;
                }

                if (pSampleData != null)
                {
                    // Iterate over all the samples and report the sampled counter values.
                    for (uint idx = 0; idx < nbrSamples; idx++)
                    {
                        // Iterate over the sampled counter values and print.
                        for (uint i = 0; i < pSampleData[idx].m_numOfCounter; i++)
                        {
                            if (pSampleData[idx].m_counterValues != null)
                            {
                                // Get the counter description to print the counter name.
                                // For this you need to have used the "ref" keyword otherwise directly pointing to the structure does not work 
                                // More information and source: https://stackoverflow.com/questions/42201106/c-sharp-pass-pointer-to-struct-containing-non-blittable-types-to-unmanaged-c
                                AMDTPwrCounterDesc counterDesc = new AMDTPwrCounterDesc();
                                AMDTPwrGetCounterDesc(pSampleData[idx].m_counterValues->m_counterID, ref counterDesc);

                                // Print the sample data information.
                                Console.WriteLine(String.Format("{0} : {1}", CharPToString(counterDesc.m_name), pSampleData[idx].m_counterValues->data.m_data));

                                pSampleData[idx].m_counterValues++;
                            }
                        }

                        Console.WriteLine();
                    }


                    // Check if we exceeded the profile duration.
                    if ((profilingDuration > 0) 
                        && (pSampleData->m_elapsedTimeMs >= (profilingDuration * 1000)))
                    {
                        stopProfiling = true;
                    }

                    if (stopProfiling)
                    {
                        // Stop the profiling.
                        hResult = AMDTPwrStopProfiling();
                        Debug.Assert(AMDT_STATUS_OK == hResult);
                        isProfiling = false;
                    }
                }
            }


            // Close the profiler.
            hResult = AMDTPwrProfileClose();
            Debug.Assert(hResult == AMDT_STATUS_OK);

            Console.WriteLine("Profiling Complete.");
            Console.WriteLine(hResult);
            Console.ReadKey();
        }
    }
}
