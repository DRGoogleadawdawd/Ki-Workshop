# Vergleichsgruppe: Legacy-Migration VB.NET

**Aufgabe:** Analysiere den Legacy-Code und implementiere eine moderne, testbare Architektur.

## Materialien

- **Legacy-Code:** Siehe `../LegacyForm.vb` im Root-Verzeichnis
- **Tests:** `Provisionierung.Tests/ProvisionsRechnerTests.vb` (14 Testfälle)
- **UI:** `Provisionierung.WPF/` (bereit, wartet auf funktionierende Logik)

## Vorgehen

1. **Analyse:** Studiere den Legacy-Code (`LegacyForm.vb`) und identifiziere die Geschäftsregeln
2. **Tests:** Verstehe die Testfälle - sie definieren das erwartete Verhalten
3. **Architektur:** Entwerfe das Domänenmodell (verwendbarer Vorschlag vorhanden, aber änderbar)
4. **Implementation:** Implementiere `ProvisionsRechner.Berechne()`
5. **Refactoring:** Verbessere die Struktur nach grünen Tests

## Regeln

- Keine UI-Abhängigkeiten in der Berechnungslogik
- Kein `On Error Resume Next` - verwende Exceptions
- Zeitabhängige Logik über `IProvisionContext` lösen
- Konstanten statt magischer Zahlen

## Build

```bash
dotnet test     # Tests ausführen (rot → grün)
dotnet run --project Provisionierung.WPF/  # GUI starten
```

**Viel Erfolg!**
