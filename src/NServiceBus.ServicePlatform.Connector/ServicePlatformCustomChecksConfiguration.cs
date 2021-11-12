namespace NServiceBus
{
    using System;

    /// <summary>
    /// Contains configuration options for the Custom Checks features of the Particular Service Platform.
    /// </summary>
    public class ServicePlatformCustomChecksConfiguration
    {
        /// <summary>
        /// The transport queue to send Custom Checks messages to.
        /// </summary>
        public string CustomCheckQueue { get; set; }

        /// <summary>
        /// The maximum time to live for Custom Checks messages.
        /// </summary>
        public TimeSpan? TimeToLive { get; set; }

        internal void ApplyTo(EndpointConfiguration endpointConfiguration)
        {
            if (string.IsNullOrWhiteSpace(CustomCheckQueue) == false)
            {
                endpointConfiguration.ReportCustomChecksTo(CustomCheckQueue, TimeToLive);
            }
        }
    }
}