namespace Provisionierung.Domain;

/// <summary>
/// Immutable Input-Objekt für die Provisionsberechnung.
/// Enthält alle Eingabedaten ohne UI-Abhängigkeiten.
/// </summary>
public readonly struct ProvisionsEingabe
{
    public decimal Umsatz { get; }
    public Kategorie Kategorie { get; }
    public bool IstVipKunde { get; }
    public bool IstJubilaeumsAktion { get; }

    public ProvisionsEingabe(decimal umsatz, Kategorie kategorie, bool istVipKunde, bool istJubilaeumsAktion)
    {
        Umsatz = umsatz;
        Kategorie = kategorie;
        IstVipKunde = istVipKunde;
        IstJubilaeumsAktion = istJubilaeumsAktion;
    }
}
