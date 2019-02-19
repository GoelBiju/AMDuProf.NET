using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMDuProf.NET
{
    class AMDTDefinitions
    {
        /// <summary>
        /// 
        /// </summary>
        public enum AMDTResult : uint
        {
            /// <summary>
            /// 
            /// </summary>
            AMDT_STATUS_OK = 0,

            /// <summary>
            /// 
            /// </summary>
            AMDT_ERROR_FAIL = 0x80004005,

            /// <summary>
            /// 
            /// </summary>
            AMDT_ERROR_INVALIDARG = 0x80070057,

            /// <summary>
            /// 
            /// </summary>
            AMDT_ERROR_OUTOFMEMORY = 0x8007000E,


            /// <summary>
            /// 
            /// </summary>
            AMDT_ERROR_UNEXPECTED = 0x8000FFFF,


            /// <summary>
            /// 
            /// </summary>
            AMDT_ERROR_ACCESSDENIED = 0x80070005,


            /// <summary>
            /// 
            /// </summary>
            AMDT_ERROR_HANDLE = 0x80070006,


            /// <summary>
            /// 
            /// </summary>
            AMDT_ERROR_ABORT = 0x80004004,


            /// <summary>
            /// 
            /// </summary>
            AMDT_ERROR_NOTIMPL = 0x80004001,


            /// <summary>
            /// 
            /// </summary>
            AMDT_ERROR_NOFILE = 0x80070002,


            /// <summary>
            /// 
            /// </summary>
            AMDT_ERROR_INVALIDPATH = 0x80070003,


            /// <summary>
            /// 
            /// </summary>
            AMDT_ERROR_INVALIDDATA = 0x8007000D,


            /// <summary>
            /// 
            /// </summary>
            AMDT_ERROR_NOTAVAILABLE = 0x80075006,


            /// <summary>
            /// 
            /// </summary>
            AMDT_ERROR_NODATA = 0x800700E8,


            /// <summary>
            /// 
            /// </summary>
            AMDT_ERROR_LOCKED = 0x80070021,


            /// <summary>
            /// 
            /// </summary>
            AMDT_ERROR_TIMEOUT = 0x800705B4,


            /// <summary>
            /// 
            /// </summary>
            AMDT_STATUS_PENDING = 0x8000000A,


            /// <summary>
            /// 
            /// </summary>
            AMDT_ERROR_NOTSUPPORTED = 0x8000FFFE,


            /// <summary>
            /// 
            /// </summary>
            AMDT_ERROR_DRIVER_ALREADY_INITIALIZED = 0x80080001,


            /// <summary>
            /// 
            /// </summary>
            AMDT_ERROR_DRIVER_UNAVAILABLE = 0x80080002,


            /// <summary>
            /// 
            /// </summary>
            AMDT_WARN_SMU_DISABLED = 0x80080003,


            /// <summary>
            /// 
            /// </summary>
            AMDT_WARN_IGPU_DISABLED = 0x80080004,


            /// <summary>
            /// 
            /// </summary>
            AMDT_ERROR_DRIVER_UNINITIALIZED = 0x80080005,


            /// <summary>
            /// 
            /// </summary>
            AMDT_ERROR_INVALID_DEVICEID = 0x80080006,


            /// <summary>
            /// 
            /// </summary>
            AMDT_ERROR_INVALID_COUNTERID = 0x80080007,


            /// <summary>
            /// 
            /// </summary>
            AMDT_ERROR_COUNTER_ALREADY_ENABLED = 0x80080008,

            
            /// <summary>
            /// 
            /// </summary>
            AMDT_ERROR_NO_WRITE_PERMISSION = 0x80080009,


            /// <summary>
            /// 
            /// </summary>
            AMDT_ERROR_COUNTER_NOT_ENABLED = 0x8008000A,


            /// <summary>
            /// 
            /// </summary>
            AMDT_ERROR_TIMER_NOT_SET = 0x8008000B,



            /// <summary>
            /// 
            /// </summary>
            AMDT_ERROR_PROFILE_DATAFILE_NOT_SET = 0x8008000C,


            /// <summary>
            /// 
            /// </summary>
            AMDT_ERROR_PROFILE_ALREADY_STARTED = 0x8008000D,


            /// <summary>
            /// 
            /// </summary>
            AMDT_ERROR_PROFILE_NOT_STARTED = 0x8008000E,


            /// <summary>
            /// 
            /// </summary>
            AMDT_ERROR_PROFILE_NOT_PAUSED = 0x8008000F,


            /// <summary>
            /// 
            /// </summary>
            AMDT_ERROR_PROFILE_DATA_NOT_AVAILABLE = 0x80080010,


            /// <summary>
            /// 
            /// </summary>
            AMDT_ERROR_PLATFORM_NOT_SUPPORTED = 0x80080011,


            /// <summary>
            /// 
            /// </summary>
            AMDT_ERROR_INTERNAL = 0x80080012,


            /// <summary>
            /// 
            /// </summary>
            AMDT_DRIVER_VERSION_MISMATCH = 0x80080013,


            /// <summary>
            /// 
            /// </summary>
            AMDT_ERROR_BIOS_VERSION_NOT_SUPPORTED = 0x80080014,


            /// <summary>
            /// 
            /// </summary>
            AMDT_ERROR_PROFILE_ALREADY_CONFIGURED = 0x80080015,


            /// <summary>
            /// 
            /// </summary>
            AMDT_ERROR_PROFILE_NOT_CONFIGURED = 0x80080016,


            /// <summary>
            /// 
            /// </summary>
            AMDT_ERROR_PROFILE_SESSION_EXISTS = 0x80080017,


            /// <summary>
            /// 
            /// </summary>
            AMDT_ERROR_SMU_ACCESS_FAILED = 0x80080018,


            /// <summary>
            /// 
            /// </summary>
            AMDT_ERROR_COUNTERS_NOT_ENABLED = 0x80080019,
        

            /// <summary>
            /// 
            /// </summary>
            AMDT_ERROR_PREVIOUS_SESSION_NOT_CLOSED = 0x80080020,


            /// <summary>
            /// 
            /// </summary>
            AMDT_ERROR_COUNTER_NOHIERARCHY = 0x80080021,


            /// <summary>
            /// 
            /// </summary>
            AMDT_ERROR_COUNTER_NOT_ACCESSIBLE = 0x80080022,



            /// <summary>
            /// 
            /// </summary>
            AMDT_ERROR_HYPERVISOR_NOT_SUPPORTED = 0x80080023,


            /// <summary>
            /// 
            /// </summary>
            AMDT_WARN_PROCESS_PROFILE_NOT_SUPPORTED = 0x80080024,


            /// <summary>
            /// 
            /// </summary>
            AMDT_ERROR_MARKER_NOT_SET = 0x80080025,
        

            /// <summary>
            /// 
            /// </summary>
            AMDT_WARNING_THREAD_PROFILE_NO_SET = 0x80080026
        }
    }
}
