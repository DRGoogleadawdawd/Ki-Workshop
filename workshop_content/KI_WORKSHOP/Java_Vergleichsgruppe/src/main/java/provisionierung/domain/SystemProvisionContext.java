package provisionierung.domain;

import java.time.LocalDateTime;

/**
 * Standard-Implementierung basierend auf Systemzeit.
 */
public class SystemProvisionContext implements ProvisionContext {
    @Override
    public LocalDateTime getAktuellesDatum() {
        return LocalDateTime.now();
    }
}
