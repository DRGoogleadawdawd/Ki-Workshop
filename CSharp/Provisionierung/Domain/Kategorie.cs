namespace Provisionierung.Domain;

public enum Kategorie
{
    A,       // 10% Basisprovision
    B,       // 7%  Basisprovision
    C,       // 5%  Basisprovision
    Sonstige // 1%  Basisprovision (Fallback)
}

public static class KategorieExtensions
{
    public static decimal GetBasisSatz(this Kategorie kategorie) => kategorie switch
    {
        Kategorie.A => 0.10m,
        Kategorie.B => 0.07m,
        Kategorie.C => 0.05m,
        Kategorie.Sonstige => 0.01m,
        _ => throw new ArgumentOutOfRangeException(nameof(kategorie), $"Unbekannte Kategorie: {kategorie}")
    };
}
