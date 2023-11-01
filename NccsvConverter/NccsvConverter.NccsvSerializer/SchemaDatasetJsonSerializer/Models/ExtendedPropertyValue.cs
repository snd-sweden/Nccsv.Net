using Schema.NET;
using System.Text.Json.Serialization;

namespace NccsvConverter.NccsvSerializer.SchemaDatasetJsonSerializer.Models;

internal class ExtendedPropertyValue : PropertyValue
{
    [JsonPropertyName("additionalProperty")]
    public PropertyValue AdditionalProperty { get; internal set; }
}

