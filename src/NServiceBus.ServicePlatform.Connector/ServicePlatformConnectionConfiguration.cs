namespace NServiceBus
{
    using System.Text.Json;

    /// <summary>
    /// Contains configuration options for connection to the Particular Service Platform.
    /// </summary>
    public class ServicePlatformConnectionConfiguration
    {
        /// <summary>
        /// The transport queue to send audit messages to.
        /// </summary>
        public string AuditQueue { get; set; }

        /// <summary>
        /// The transport queue to send failed messages to.
        /// </summary>
        public string ErrorQueue { get; set; }

        /// <summary>
        /// Configuration options for the Heartbeats feature.
        /// </summary>
        public ServicePlatformHeartbeatConfiguration Heartbeats { get; set; }

        /// <summary>
        /// Configuration options for the Custom Checks feature.
        /// </summary>
        public ServicePlatformCustomChecksConfiguration CustomChecks { get; set; }

        /// <summary>
        /// Configuration options for the Saga Audit feature.
        /// </summary>
        public ServicePlatformSagaAuditConfiguration SagaAudit { get; set; }

        /// <summary>
        /// Configuration options for the Metrics feature.
        /// </summary>
        public ServicePlatformMetricsConfiguration Metrics { get; set; }

        internal void ApplyTo(EndpointConfiguration endpointConfiguration)
        {
            if (string.IsNullOrWhiteSpace(AuditQueue) == false)
            {
                endpointConfiguration.AuditProcessedMessagesTo(AuditQueue);
            }

            if (string.IsNullOrWhiteSpace(ErrorQueue) == false)
            {
                endpointConfiguration.SendFailedMessagesTo(ErrorQueue);
            }

            Heartbeats?.ApplyTo(endpointConfiguration);
            CustomChecks?.ApplyTo(endpointConfiguration);
            SagaAudit?.ApplyTo(endpointConfiguration);
            Metrics?.ApplyTo(endpointConfiguration);
        }

        /// <summary>
        /// Creates a new <see cref="ServicePlatformConnectionConfiguration"/> from json produced by the Particular Service Platform.
        /// </summary>
        public static ServicePlatformConnectionConfiguration Parse(string jsonConfiguration)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            options.Converters.Add(new JsonTimeSpanConverterFactory());
            return System.Text.Json.JsonSerializer.Deserialize<ServicePlatformConnectionConfiguration>(jsonConfiguration, options);
        }
    }
}