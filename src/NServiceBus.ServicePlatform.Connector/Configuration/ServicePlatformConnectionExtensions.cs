namespace NServiceBus
{
    using System;

    /// <summary>
    /// Provides extension methods to configure an NServiceBus endpoint connection to the Particular Service Platform.
    /// </summary>
    public static class ServicePlatformConnectionExtensions
    {
        /// <summary>
        /// Configures an NServiceBus endpoint to send data to the Particular Service Platform.
        /// </summary>
        public static void ConnectToServicePlatform(this EndpointConfiguration endpointConfiguration, ServicePlatformConnectionConfiguration servicePlatformConnectionConfiguration)
        {
            ArgumentNullException.ThrowIfNull(endpointConfiguration);
            ArgumentNullException.ThrowIfNull(servicePlatformConnectionConfiguration);

            servicePlatformConnectionConfiguration.ApplyTo(endpointConfiguration);
        }
    }
}