package provisionierung.domain;

import java.time.LocalDateTime;

/**
 * Für Tests: Feste Zeitkonfiguration.
 */
public class FesterProvisionContext implements ProvisionContext {
    private final LocalDateTime festesDatum;
    private final boolean freitagNach15Uhr;

    public FesterProvisionContext(LocalDateTime festesDatum, boolean freitagNach15Uhr) {
        this.festesDatum = festesDatum;
        this.freitagNach15Uhr = freitagNach15Uhr;
    }

    @Override
    public LocalDateTime getAktuellesDatum() {
        return festesDatum;
    }

    @Override
    public boolean istFreitagNach15Uhr() {
        return freitagNach15Uhr;
    }
}
