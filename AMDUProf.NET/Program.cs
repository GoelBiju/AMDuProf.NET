using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;

using static AMDuProf.NET.AMDTDefinitions;
using static AMDuProf.NET.AMDTPowerProfileApi;
using static AMDuProf.NET.AMDTPowerProfileDataTypes;


// Developed based on a template to interact with the AMDPowerProfileAPI.dll.


namespace AMDuProf.NET
{
    unsafe class Program
    {
        static void Main(string[] args)
        {
            AMDTResult hResult = AMDTResult.AMDT_STATUS_OK;

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
            Debug.Assert(hResult == AMDTResult.AMDT_STATUS_OK);

            AMDTPwrCounterDesc* pCurrCounter = pCounters;
            
            // Loop and find all the enabled counters for this computer.
            for (uint cnt = 0; cnt < nbrCounters; cnt++, pCurrCounter++)
            {
                if (pCurrCounter != null)
                {
                    // Enable all the counters.
                    hResult = AMDTPwrEnableCounter(pCurrCounter->m_counterID);

                    // Display the description of all the counters.
                    Console.WriteLine(Utilities.CharPToString(pCurrCounter->m_description));
                    Debug.Assert(hResult == AMDTResult.AMDT_STATUS_OK);
                }
            }


            // Set the timer configuration
            // milliseconds
            uint samplingInterval = 100;
            // seconds
            uint profilingDuration = 10;

            hResult = AMDTPwrSetTimerSamplingPeriod(samplingInterval);
            Debug.Assert(hResult == AMDTResult.AMDT_STATUS_OK);

            // Start the profile run.
            hResult = AMDTPwrStartProfiling();
            Debug.Assert(hResult == AMDTResult.AMDT_STATUS_OK);


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

                if (hResult != AMDTResult.AMDT_STATUS_OK)
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
                                Console.WriteLine(string.Format("{0} : {1}", Utilities.CharPToString(counterDesc.m_name), pSampleData[idx].m_counterValues->data.m_data));

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
                        Debug.Assert(AMDTResult.AMDT_STATUS_OK == hResult);
                        isProfiling = false;
                    }
                }
            }


            // Close the profiler.
            hResult = AMDTPwrProfileClose();
            Debug.Assert(hResult == AMDTResult.AMDT_STATUS_OK);

            Console.WriteLine("Profiling Complete.");
            Console.WriteLine(hResult);
            Console.ReadKey();
        }
    }
}
