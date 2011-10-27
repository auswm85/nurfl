using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nurfl.Core
{
    public class Device
    {
        public string Id { get; set; }
        public Dictionary<string, object> Capabilities { get; set; }
        public string UserAgent { get; set; }

        /// <summary>
        /// Get device capability
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>object</returns>
        public object GetCapability(string key)
        {
            if (Capabilities[key] == null)
                throw new ArgumentOutOfRangeException("key");

            return Capabilities[key];
        }

        /// <summary>
        /// Determines if device is mobile device
        /// </summary>
        /// <returns>boolean</returns>
        public bool IsMobileDevice()
        {
            return ((bool)Capabilities["is_wireless_device"] == true);
        }

        /// <summary>
        /// Determines if device is a tablet
        /// </summary>
        /// <returns>boolean</returns>
        public bool IsTablet()
        {
            return ((bool)Capabilities["is_tablet"] == true);
        }
    }
}
