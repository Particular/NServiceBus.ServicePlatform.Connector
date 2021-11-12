namespace NServiceBus
{
    using System;

    /// <summary>
    /// Contains configuration options for the Heartbeat features of the Particular Service Platform.
    /// </summary>
    public class ServicePlatformHeartbeatConfiguration
    {
        /// <summary>
        /// The transport queue to send Heartbeat messages to.
        /// </summary>
        public string HeartbeatQueue { get; set; }

        /// <summary>
        /// The frequency to send Heartbeat messages.
        /// </summary>
        public TimeSpan? Frequency { get; set; }

        /// <summary>
        /// The maximum time to live for Heartbeat messages.
        /// </summary>
        public TimeSpan? TimeToLive { get; set; }

        internal void ApplyTo(EndpointConfiguration endpointConfiguration)
        {
            if (string.IsNullOrWhiteSpace(HeartbeatQueue) == false)
            {
                endpointConfiguration.SendHeartbeatTo(HeartbeatQueue, Frequency, TimeToLive);
            }
        }
    }
}