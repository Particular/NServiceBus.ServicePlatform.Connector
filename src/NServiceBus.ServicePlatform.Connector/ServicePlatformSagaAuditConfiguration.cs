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
        /// If true, the endpoint will audit saga invocations to the Particular Service Platform.
        /// </summary>
        public bool Enabled { get; set; }

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
            if (!Enabled)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(SagaAuditQueue))
            {
                throw new Exception(
                    @"Sending saga audit information is enabled but no saga audit queue has been configured.
Configure a saga audit queue or disable sending saga audit information to the Particular Service Platform");

            }

            endpointConfiguration.AuditSagaStateChanges(SagaAuditQueue, CustomSagaEntitySerialization);
        }
    }
}