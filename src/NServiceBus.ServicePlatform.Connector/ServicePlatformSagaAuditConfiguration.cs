namespace NServiceBus
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Contains configuration options for the Saga Audit features of the Particular Service Platform.
    /// </summary>
    public class ServicePlatformSagaAuditConfiguration
    {
        /// <summary>
        /// The transport queue to send Saga Audit messages to.
        /// </summary>
        public string SagaAuditQueue { get; set; }

        /// <summary>
        /// A custom custom strategy for serializing saga state.
        /// </summary>
        public Func<object, Dictionary<string, string>> CustomSagaEntitySerialization { get; set; }

        internal void ApplyTo(EndpointConfiguration endpointConfiguration)
        {
            if (string.IsNullOrWhiteSpace(SagaAuditQueue) == false)
            {
                endpointConfiguration.AuditSagaStateChanges(SagaAuditQueue, CustomSagaEntitySerialization);
            }
        }
    }
}