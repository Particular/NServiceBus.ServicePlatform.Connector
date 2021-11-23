namespace NServiceBus
{
    using System;

    /// <summary>
    /// Contains configuration options for the Audit features of the Particular ServicePlatform.
    /// </summary>
    public class ServicePlatformMessageAuditConfiguration
    {
        /// <summary>
        /// If true, the endpoint will send a copy of each message processed to the Particular Service Platform.
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// The transport queue to send Audit message to.
        /// </summary>
        public string AuditQueue { get; set; }

        internal void ApplyTo(EndpointConfiguration endpointConfiguration)
        {
            if (!Enabled)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(AuditQueue))
            {
                throw new Exception(
                    @"Message auditing is enabled but no audit queue has been configured.
Configure an audit queue or disable message auditing");
            }

            endpointConfiguration.AuditProcessedMessagesTo(AuditQueue);
        }
    }
}