# Vergleichsgruppe: Legacy-Migration Java

**Aufgabe:** Analysiere den Legacy-Code und implementiere eine moderne, testbare Architektur.

## Materialien

- **Legacy-Code:** Siehe `../LegacyForm.vb` im Root-Verzeichnis
- **Tests:** `src/test/java/provisionierung/ProvisionsRechnerTest.java` (14 Testfälle)
- **UI:** `src/main/java/provisionierung/ui/JavaFXApp.java` (bereit, wartet auf funktionierende Logik)

## Vorgehen

1. **Analyse:** Studiere den Legacy-Code (`LegacyForm.vb`) und identifiziere die Geschäftsregeln
2. **Tests:** Verstehe die Testfälle - sie definieren das erwartete Verhalten
3. **Architektur:** Entwerfe das Domänenmodell (verwendbarer Vorschlag vorhanden, aber änderbar)
4. **Implementation:** Implementiere `ProvisionsRechner.berechne()`
5. **Refactoring:** Verbessere die Struktur nach grünen Tests

## Regeln

- Keine UI-Abhängigkeiten in der Berechnungslogik
- Zeitabhängige Logik über `ProvisionContext` lösen
- Konstanten statt magischer Zahlen
- `BigDecimal` für Geld-Berechnungen (nicht `double`!)

## Build

```bash
mvn clean test      # Tests ausführen (rot → grün)
mvn javafx:run      # GUI starten
```

**Viel Erfolg!**
