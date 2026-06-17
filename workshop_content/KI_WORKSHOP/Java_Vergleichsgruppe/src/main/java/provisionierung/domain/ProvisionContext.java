package provisionierung.domain;

import java.time.DayOfWeek;
import java.time.LocalDateTime;

/**
 * Schnittstelle für zeit- und umgebungsabhängige Provisionsaspekte.
 * Ermöglicht Testbarkeit durch Mocking.
 */
public interface ProvisionContext {
    /**
     * Aktuelles Datum/Zeit für zeitabhängige Berechnungen.
     * @return Aktuelles Datum/Zeit
     */
    LocalDateTime getAktuellesDatum();

    /**
     * Prüft ob der Freitag-nach-15h-Bonus gilt.
     * @return true wenn Freitag nach 15 Uhr
     */
    default boolean istFreitagNach15Uhr() {
        LocalDateTime now = getAktuellesDatum();
        return now.getDayOfWeek() == DayOfWeek.FRIDAY && now.getHour() >= 15;
    }
}
