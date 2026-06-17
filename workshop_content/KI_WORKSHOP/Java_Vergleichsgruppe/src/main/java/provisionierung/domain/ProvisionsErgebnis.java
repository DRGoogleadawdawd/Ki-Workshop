package provisionierung.domain;

import java.math.BigDecimal;
import java.util.Collections;
import java.util.List;

/**
 * Immutable Output-Objekt mit detailliertem Provisionsergebnis.
 * Ermöglicht Nachvollziehbarkeit aller Berechnungsschritte.
 */
public final class ProvisionsErgebnis {
    private final BigDecimal endProvision;
    private final BigDecimal basisProvision;
    private final BigDecimal zuschlaege;
    private final double angewandterSatz;
    private final List<String> angewandteBoni;

    public ProvisionsErgebnis(BigDecimal endProvision, BigDecimal basisProvision,
                              BigDecimal zuschlaege, double angewandterSatz,
                              List<String> angewandteBoni) {
        this.endProvision = endProvision;
        this.basisProvision = basisProvision;
        this.zuschlaege = zuschlaege;
        this.angewandterSatz = angewandterSatz;
        this.angewandteBoni = Collections.unmodifiableList(angewandteBoni);
    }

    public BigDecimal getEndProvision() {
        return endProvision;
    }

    public BigDecimal getBasisProvision() {
        return basisProvision;
    }

    public BigDecimal getZuschlaege() {
        return zuschlaege;
    }

    public double getAngewandterSatz() {
        return angewandterSatz;
    }

    public List<String> getAngewandteBoni() {
        return angewandteBoni;
    }

    @Override
    public String toString() {
        return String.format("Provision: %.2f € (Satz: %.1f%%, Boni: %s)",
                endProvision, angewandterSatz * 100, String.join(", ", angewandteBoni));
    }
}
