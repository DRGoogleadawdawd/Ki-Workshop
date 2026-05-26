namespace Provisionierung.Domain;

/// <summary>
/// Kundenkategorien mit ihren Basis-Provisionssätzen.
/// </summary>
public enum Kategorie
{
    A = 0,  // 10% Basisprovision
    B = 1,  // 7% Basisprovision
    C = 2,  // 5% Basisprovision
    Sonstige = 3  // 1% Basisprovision (Fallback)
}

public static class KategorieExtensions
{
    /// <summary>
    /// Gibt den Basis-Provisionssatz für eine Kategorie zurück.
    /// </summary>
    public static decimal GetBasisSatz(this Kategorie kategorie) => kategorie switch
    {
        Kategorie.A => 0.10m,
        Kategorie.B => 0.07m,
        Kategorie.C => 0.05m,
        Kategorie.Sonstige => 0.01m,
        _ => throw new ArgumentOutOfRangeException(nameof(kategorie), $"Unbekannte Kategorie: {kategorie}")
    };
}
