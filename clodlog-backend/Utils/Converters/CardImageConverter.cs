using System.Text.Json;
using System.Text.Json.Serialization;
using clodlog_backend.Models.Entities;

namespace clodlog_backend.Utils.Converters;

public class CardImageConverter : BaseConverter<CardImage>
{
    protected override Dictionary<string, PropertyReader<CardImage>> PropertyMap => 
        new Dictionary<string, PropertyReader<CardImage>>
        {
            { "small", (ref Utf8JsonReader reader, CardImage cardImage) => cardImage.Small = reader.GetString() },
            { "large", (ref Utf8JsonReader reader, CardImage cardImage) => cardImage.Large = reader.GetString() }
            // Ignoring Foil and Mask as per instructions
        };

    protected override void WriteProperties(Utf8JsonWriter writer, CardImage value)
    {
        writer.WriteString("small", value.Small);
        writer.WriteString("large", value.Large);
        // Ignoring Foil and Mask as per instructions
    }
}
