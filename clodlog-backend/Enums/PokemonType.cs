using System.ComponentModel;

namespace clodlog_backend.Enums;

public enum PokemonType
{
    [Description("Colorless")] Colorless,
    [Description("Darkness")] Darkness,
    [Description("Dragon")] Dragon,
    [Description("Fairy")] Fairy,
    [Description("Fighting")] Fighting,
    [Description("Fire")] Fire,
    [Description("Grass")] Grass,
    [Description("Lightning")] Lightning,
    [Description("Metal")] Metal,
    [Description("Psychic")] Psychic,
    [Description("Water")] Water,
}