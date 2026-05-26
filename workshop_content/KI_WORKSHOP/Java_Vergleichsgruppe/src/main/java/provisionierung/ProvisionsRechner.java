package provisionierung;

import provisionierung.domain.*;

/**
 * Aufgabe: Implementiere die Provisionsberechnung basierend auf den Tests und dem Legacy-Code.
 *
 * Analyse:
 * - Die Testfälle definieren das erwartete Verhalten
 * - Der Legacy-Code (siehe Projekt-Root) enthält die originale Berechnungslogik
 *
 * Anforderungen:
 * - Keine UI-Abhängigkeiten
 * - Nutze das ProvisionContext-Interface für zeitabhängige Logik
 * - Alle magischen Zahlen als Konstanten definieren
 * - Modernes Error-Handling
 *
 * Hinweis: Du darfst das Domänenmodell anpassen wenn nötig.
 */
public class ProvisionsRechner {

    // Definiere hier deine Konstanten

    private final ProvisionContext context;

    public ProvisionsRechner() {
        this(new SystemProvisionContext());
    }

    public ProvisionsRechner(ProvisionContext context) {
        this.context = context;
    }

    public ProvisionsErgebnis berechne(ProvisionsEingabe eingabe) {
        throw new UnsupportedOperationException("Implementiere die Berechnungslogik basierend auf den Tests.");
    }
}
