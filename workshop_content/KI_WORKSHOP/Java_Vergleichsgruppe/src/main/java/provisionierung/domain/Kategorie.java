package provisionierung.domain;

/**
 * Kundenkategorien mit ihren Basis-Provisionssätzen.
 */
public enum Kategorie {
    A(0.10),    // 10% Basisprovision
    B(0.07),    // 7% Basisprovision
    C(0.05),    // 5% Basisprovision
    SONSTIGE(0.01);  // 1% Basisprovision (Fallback)

    private final double basisSatz;

    Kategorie(double basisSatz) {
        this.basisSatz = basisSatz;
    }

    /**
     * Gibt den Basis-Provisionssatz für die Kategorie zurück.
     * @return Provisionsatz als Dezimalwert (z.B. 0.10 für 10%)
     */
    public double getBasisSatz() {
        return basisSatz;
    }
}
