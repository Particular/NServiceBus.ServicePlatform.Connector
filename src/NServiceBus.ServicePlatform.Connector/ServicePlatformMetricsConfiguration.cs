namespace NServiceBus
{
    using System;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Contains configuration options for the Metrics features of the Particular Service Platform.
    /// </summary>
    public class ServicePlatformMetricsConfiguration
    {
        /// <summary>
        /// If true, the endpoint will send metric data to the Particular Service Platform.
        /// </summary>
        public bool Enabled { get; set; }

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
        [ObsoleteEx(
            TreatAsErrorFromVersion = "4.0.0",
            RemoveInVersion = "5.0.0",
            ReplacementTypeOrMember = "TimeToLive")]
        [JsonIgnore]
        public TimeSpan? TimeToBeReceived
        {
            get => TimeToLive;
            set => TimeToLive = value;
        }

        /// <summary>
        /// The maximum time to live for Metrics messages.
        /// </summary>
        public TimeSpan? TimeToLive { get; set; }

        internal void ApplyTo(EndpointConfiguration endpointConfiguration)
        {
            if (!Enabled)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(MetricsQueue))
            {
                throw new Exception(
                    @"Sending metric data is enabled but no metrics queue has been configured.
Configure a metrics queue or disable sending metric data to the Particular Service Platform");
            }

            var metrics = endpointConfiguration.EnableMetrics();
            metrics.SendMetricDataToServiceControl(MetricsQueue, Interval, InstanceId);

            if (TimeToLive.HasValue)
            {
                metrics.SetServiceControlMetricsMessageTTBR(TimeToLive.Value);
            }
        }
    }
}
