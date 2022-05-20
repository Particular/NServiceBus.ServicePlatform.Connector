namespace NServiceBus
{
    using System;
    using System.Globalization;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    class JsonTimeSpanConverterFactory : JsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert) =>
            typeToConvert == typeof(TimeSpan)
            || (typeToConvert.IsGenericType && Nullable.GetUnderlyingType(typeToConvert) == typeof(TimeSpan));

#pragma warning disable IDE0004 // Remove Unnecessary Cast - Needed for net472 only
        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options) =>
            typeToConvert.IsGenericType
                ? (JsonConverter)new NullableJsonTimeSpanConverter()
                : new JsonTimeSpanConverter();
#pragma warning restore IDE0004 // Remove Unnecessary Cast

        class JsonTimeSpanConverter : JsonConverter<TimeSpan>
        {
            public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
                TimeSpan.ParseExact(reader.GetString(), "c", CultureInfo.CurrentCulture);

            public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options) =>
                throw new NotImplementedException();
        }

        class NullableJsonTimeSpanConverter : JsonConverter<TimeSpan?>
        {
            public override TimeSpan? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                var s = reader.GetString();
#pragma warning disable IDE0004 // Remove Unnecessary Cast - Needed only for net472
                return s == null ? (TimeSpan?)null : TimeSpan.ParseExact(s, "c", CultureInfo.CurrentCulture);
#pragma warning restore IDE0004 // Remove Unnecessary Cast
            }

            public override void Write(Utf8JsonWriter writer, TimeSpan? value, JsonSerializerOptions options) => throw new NotImplementedException();
        }
    }
}