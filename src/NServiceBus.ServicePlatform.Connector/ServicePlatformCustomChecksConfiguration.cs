namespace NServiceBus
{
    using System;

    /// <summary>
    /// Contains configuration options for the Custom Checks features of the Particular Service Platform.
    /// </summary>
    public class ServicePlatformCustomChecksConfiguration
    {
        /// <summary>
        /// If true, the endpoint will send custom check results to the Particular Service Platform.
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// The transport queue to send Custom Checks messages to.
        /// </summary>
        public string CustomChecksQueue { get; set; }

        /// <summary>
        /// The maximum time to live for Custom Checks messages.
        /// </summary>
        public TimeSpan? TimeToLive { get; set; }

        internal void ApplyTo(EndpointConfiguration endpointConfiguration)
        {
            if (!Enabled)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(CustomChecksQueue))
            {
                throw new Exception("Sending custom checks results is enabled but no custom check queue has been configured. Configure a custom check queue or disable sending custom checks to the Particular Service Platform.");
            }

            endpointConfiguration.ReportCustomChecksTo(CustomChecksQueue, TimeToLive);
        }
    }
}