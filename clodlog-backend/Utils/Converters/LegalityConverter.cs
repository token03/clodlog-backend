using System.Text.Json;
using clodlog_backend.Models.Entities;

namespace clodlog_backend.Utils.Converters;

public class LegalityConverter : BaseConverter<Legality>
{
    // THIS DOESNT WORK RIGHT NOW
    protected override Dictionary<string, PropertyReader<Legality>> PropertyMap => 
        new Dictionary<string, PropertyReader<Legality>>
        {
            { "standard", (ref Utf8JsonReader reader, Legality legality) => legality.Standard = reader.GetString() },
            { "expanded", (ref Utf8JsonReader reader, Legality legality) => legality.Expanded = reader.GetString() },
            { "unlimited", (ref Utf8JsonReader reader, Legality legality) => legality.Unlimited = reader.GetString() }
        };

    protected override void WriteProperties(Utf8JsonWriter writer, Legality value)
    {
        writer.WriteString("standard", value.Standard);
        writer.WriteString("expanded", value.Expanded);
        writer.WriteString("unlimited", value.Unlimited);
    }
}
