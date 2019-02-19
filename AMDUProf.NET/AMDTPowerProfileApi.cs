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
    unsafe class AMDTPowerProfileApi
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="AMDTPwrProfileMode"></param>
        /// <returns></returns>
        [DllImport("AMDPowerProfileAPI.dll")]
        public static extern AMDTDefinitions.AMDTResult AMDTPwrProfileInitialize(AMDTPowerProfileDataTypes.AMDTPwrProfileMode AMDTPwrProfileMode);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="pNumCounters"></param>
        /// <param name="ppCounterDescs"></param>
        /// <returns></returns>
        [DllImport("AMDPowerProfileAPI.dll")]
        public static extern AMDTDefinitions.AMDTResult AMDTPwrGetSupportedCounters(uint* pNumCounters, AMDTPowerProfileDataTypes.AMDTPwrCounterDesc** ppCounterDescs);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="counterId"></param>
        /// <returns></returns>
        [DllImport("AMDPowerProfileAPI.dll")]
        public static extern AMDTDefinitions.AMDTResult AMDTPwrEnableCounter(uint counterId);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="interval"></param>
        /// <returns></returns>
        [DllImport("AMDPowerProfileAPI.dll")]
        public static extern AMDTDefinitions.AMDTResult AMDTPwrSetTimerSamplingPeriod(uint interval);


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [DllImport("AMDPowerProfileAPI.dll")]
        public static extern AMDTDefinitions.AMDTResult AMDTPwrStartProfiling();


        /// <summary>
        /// 
        /// </summary>
        /// <param name="counterId"></param>
        /// <param name="pCounterDesc"></param>
        /// <returns></returns>
        [DllImport("AMDPowerProfileAPI.dll")]
        public static extern AMDTDefinitions.AMDTResult AMDTPwrGetCounterDesc(uint counterId, [In, Out] ref AMDTPowerProfileDataTypes.AMDTPwrCounterDesc pCounterDesc);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="pNumOfSamples"></param>
        /// <param name="ppData"></param>
        /// <returns></returns>
        [DllImport("AMDPowerProfileAPI.dll")]
        public static extern AMDTDefinitions.AMDTResult AMDTPwrReadAllEnabledCounters(uint* pNumOfSamples, AMDTPowerProfileDataTypes.AMDTPwrSample** ppData);


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [DllImport("AMDPowerProfileAPI.dll")]
        public static extern AMDTDefinitions.AMDTResult AMDTPwrStopProfiling();


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [DllImport("AMDPowerProfileAPI.dll")]
        public static extern AMDTDefinitions.AMDTResult AMDTPwrProfileClose();
    }
}
