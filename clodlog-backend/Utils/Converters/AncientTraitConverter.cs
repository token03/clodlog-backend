using System.Text.Json;
using clodlog_backend.Models.Entities;
using clodlog_backend.Utils.Converters;

namespace clodlog_backend.Utils.Converters
{
    public class AncientTraitConverter : BaseConverter<AncientTrait>
    {
        protected override Dictionary<string, PropertyReader<AncientTrait>> PropertyMap => 
            new Dictionary<string, PropertyReader<AncientTrait>>
            {
                { "name", (ref Utf8JsonReader reader, AncientTrait ancientTrait) => ancientTrait.Name = reader.GetString() },
                { "text", (ref Utf8JsonReader reader, AncientTrait ancientTrait) => ancientTrait.Text = reader.GetString() },
            };

        protected override void WriteProperties(Utf8JsonWriter writer, AncientTrait value)
        {
            writer.WriteString("name", value.Name);
            writer.WriteString("text", value.Text);
        }
    }
}