#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace NServiceBus;

using System;
using System.Text.Json.Serialization;

public partial class ServicePlatformMetricsConfiguration
{

    [ObsoleteEx(
        TreatAsErrorFromVersion = "4.0.0",
        RemoveInVersion = "5.0.0",
        ReplacementTypeOrMember = "TimeToLive")]
    [JsonIgnore]
    public TimeSpan? TimeToBeReceived => throw new NotImplementedException();

}

#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member