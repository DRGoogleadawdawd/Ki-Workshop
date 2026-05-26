package provisionierung.domain;

import java.math.BigDecimal;

/**
 * Immutable Input-Objekt für die Provisionsberechnung.
 * Enthält alle Eingabedaten ohne UI-Abhängigkeiten.
 */
public final class ProvisionsEingabe {
    private final BigDecimal umsatz;
    private final Kategorie kategorie;
    private final boolean istVipKunde;
    private final boolean istJubilaeumsAktion;

    public ProvisionsEingabe(BigDecimal umsatz, Kategorie kategorie, 
                            boolean istVipKunde, boolean istJubilaeumsAktion) {
        this.umsatz = umsatz;
        this.kategorie = kategorie;
        this.istVipKunde = istVipKunde;
        this.istJubilaeumsAktion = istJubilaeumsAktion;
    }

    public BigDecimal getUmsatz() {
        return umsatz;
    }

    public Kategorie getKategorie() {
        return kategorie;
    }

    public boolean isIstVipKunde() {
        return istVipKunde;
    }

    public boolean isIstJubilaeumsAktion() {
        return istJubilaeumsAktion;
    }
}
