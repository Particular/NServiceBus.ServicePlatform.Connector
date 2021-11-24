namespace NServiceBus
{
    using System;

    /// <summary>
    /// Contains configuration options for the Heartbeat features of the Particular Service Platform.
    /// </summary>
    public class ServicePlatformHeartbeatConfiguration
    {
        /// <summary>
        /// If true, the endpoint will send heartbeats to the Particular Service Platform.
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// The transport queue to send Heartbeat messages to.
        /// </summary>
        public string HeartbeatsQueue { get; set; }

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
            if (!Enabled)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(HeartbeatsQueue))
            {
                throw new Exception(
                    @"Sending heartbeats is enabled but no heartbeat queue has been configured.
Configure a heartbeat queue or disable sending heartbeats to the Particular Service Platform");
            }

            endpointConfiguration.SendHeartbeatTo(HeartbeatsQueue, Frequency, TimeToLive);
        }
    }
}