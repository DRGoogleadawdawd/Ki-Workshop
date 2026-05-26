namespace Provisionierung.Domain;

public record ProvisionsEingabe(
    decimal Umsatz,
    Kategorie Kategorie,
    bool IstVipKunde,
    bool IstJubilaeumsAktion
);
