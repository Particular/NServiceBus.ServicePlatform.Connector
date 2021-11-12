namespace NServiceBus
{
    using System;

    /// <summary>
    /// Contains configuration options for the Metrics features of the Particular Service Platform.
    /// </summary>
    public class ServicePlatformMetricsConfiguration
    {
        /// <summary>
        /// The transport queue to send Metrics messages to.
        /// </summary>
        public string MetricsQueue { get; set; }

        /// <summary>
        /// The longest interval allowed between Metrics messages.
        /// </summary>
        public TimeSpan Interval { get; set; }

        /// <summary>
        /// Unique, human-readable, stable between restarts, identifier for running endpoint instance.
        /// </summary>
        public string InstanceId { get; set; }

        /// <summary>
        /// The maximum time to live for Metrics messages.
        /// </summary>
        public TimeSpan? TimeToBeReceived { get; set; }

        internal void ApplyTo(EndpointConfiguration endpointConfiguration)
        {
            if (string.IsNullOrWhiteSpace(MetricsQueue) == false)
            {
                var metrics = endpointConfiguration.EnableMetrics();
                metrics.SendMetricDataToServiceControl(MetricsQueue, Interval, InstanceId);
                if (TimeToBeReceived.HasValue)
                {
                    metrics.SetServiceControlMetricsMessageTTBR(TimeToBeReceived.Value);
                }
            }
        }
    }
}
