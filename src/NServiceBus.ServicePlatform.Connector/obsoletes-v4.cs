#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace NServiceBus;

using System;
using System.Text.Json.Serialization;
using Particular.Obsoletes;

public partial class ServicePlatformMetricsConfiguration
{

    [ObsoleteMetadata(
        TreatAsErrorFromVersion = "4.0.0",
        RemoveInVersion = "5.0.0",
        ReplacementTypeOrMember = "TimeToLive")]
    [JsonIgnore]
    [Obsolete("Use 'TimeToLive' instead. Will be removed in version 5.0.0.", true)]
    public TimeSpan? TimeToBeReceived => throw new NotImplementedException();

}

#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member