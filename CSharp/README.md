# Legacy-Migration: Provisionsrechner (VB.Net → C#)

## Ergebnis

15/15 Unit-Tests grün · UI-frei · Domänenmodell · Exception-Handling

---

## Projektstruktur

```
CSharp/
├── Provisionierung/
│   ├── Domain/
│   │   ├── Kategorie.cs          # Enum A/B/C/Sonstige + GetBasisSatz()
│   │   ├── ProvisionsEingabe.cs  # Immutable record (Input)
│   │   ├── ProvisionsErgebnis.cs # Immutable record (Output + Audit-Trail)
│   │   └── IProvisionContext.cs  # Interface für zeitabhängige Logik (testbar)
│   └── ProvisionsRechner.cs      # Reine Fachlogik, keine UI-Abhängigkeiten
└── Provisionierung.Tests/
    └── ProvisionsRechnerTests.cs # 15 xUnit-Tests
```

---

## Starten

```bash
cd CSharp
dotnet test   # → 15/15 grün
```

---

## Extrahierte Geschäftslogik

| Schritt | Regel |
|---------|-------|
| 1 | Basisprovision: A=10%, B=7%, C=5%, Sonstige=1% |
| 2 | VIP-Bonus: +2% Rate **und** +50€ Flat |
| 3 | Jubiläums-Bonus: +5% Rate, **nur wenn** Umsatz **streng >** 5.000€ |
| 4 | `provision = umsatz × gesamtSatz` |
| 5 | Großumsatz: +1.000€ Flat, wenn Umsatz **streng >** 50.000€ |
| 6 | VIP-Flat addieren |
| 7 | Freitag nach 15 Uhr: +25,50€ Flat |

### Testfälle

| Umsatz | Kategorie | VIP | Jubiläum | Erwartet |
|--------|-----------|-----|----------|----------|
| 1.000 | A | ❌ | ❌ | 100,00 € |
| 2.000 | B | ❌ | ❌ | 140,00 € |
| 3.000 | C | ❌ | ❌ | 150,00 € |
| 1.000 | A | ✅ | ❌ | 170,00 € |
| 500 | B | ✅ | ❌ | 95,00 € |
| 6.000 | C | ❌ | ✅ | 600,00 € |
| 5.000 | A | ❌ | ✅ | 500,00 € ← Grenzwert, kein Bonus! |
| 10.000 | B | ✅ | ✅ | 1.450,00 € |
| 60.000 | A | ❌ | ❌ | 7.000,00 € |
| 60.000 | A | ✅ | ❌ | 8.250,00 € |
| 100 | C | ❌ | ❌ | 5,00 € |
| −100 | A | ❌ | ❌ | −10,00 € |
| 0 | A | ❌ | ❌ | 0,00 € |
| 999.999 | B | ✅ | ✅ | 141.049,86 € |
| 1.000 | A | ❌ | ❌ (Freitag) | 125,50 € |

---

## KI-Logbuch

### Code-Archäologie: Stille Sünden

| # | Sünde | Fundstelle |
|---|-------|-----------|
| 1 | `On Error Resume Next` überall – Fehler werden stillschweigend geschluckt | LegacyForm.vb:19, 154 |
| 2 | `GoTo`-Spaghetti mit 8+ Labels | LegacyForm.vb:56–139 |
| 3 | Berechnungslogik direkt in Windows-Forms-Klasse | LegacyForm.vb:4 |
| 4 | `Thread.Sleep` + `Application.DoEvents()` im UI-Thread | LegacyForm.vb:44, 92 |
| 5 | Shared (statische) Mutable State | LegacyForm.vb:7–8 |
| 6 | `Val()` für Parsing – lokalisierungsabhängig, kein Fehler bei Leerstring | LegacyForm.vb:26 |
| 7 | `ByRef`-Parameter werden unbemerkt mutiert | LegacyForm.vb:153, 284 |
| 8 | Kryptische Variablennamen: `flag2`, `resZ`, `valX`, `temp1` | LegacyForm.vb:27–29 |
| 9 | Magische Zahlen ohne Kontext: `0.1`, `50`, `1000`, `25.5` | LegacyForm.vb:55–106 |
| 10 | Freitag-15-Uhr-Bonus: undokumentierte Geschäftsregel | LegacyForm.vb:104 |

### Toter Code (nie erreichbar)

| Label | Bedingung | Warum tot |
|-------|-----------|-----------|
| `AlteLogik` | `DateTime.Now.Year < 2000` | Seit 26 Jahren false |
| `NotfallModus` | `Math.Abs(x) * -6.35 > 5` | Immer false (negatives Produkt ≤ 0, nie > 5) |
| `NotfallBerechnung` | Nur von totem Code aufgerufen | Gegenseitige Rekursion als Ablenkung |
| `BerechneKompensation` | Nur von totem Code aufgerufen | – |

### Prompt-Engineering

**Was nicht funktioniert:** _„Übersetze diesen VB.Net-Code nach C#"_
→ KI produziert 1:1-Übersetzung mit GoTo, On Error Resume Next und Windows.Forms-Imports

**Was funktioniert:**
> „Identifiziere zunächst alle toten Code-Pfade und undokumentierten Geschäftsregeln.
> Trenne dann Fachlogik von UI vollständig.
> Erstelle ein explizites Domänenmodell mit Enums und immutable Records.
> Ersetze On Error Resume Next durch Exceptions.
> Injiziere zeitabhängige Logik über ein Interface für Testbarkeit."

### Antwort 42 – KI-Fehler

Die Workshop-Tabelle zeigt für `(100, C, ❌, ❌) → 30,5` und `(999.999, B, ✅, ✅) → 141.075,36`.
Eine naive KI übernimmt diese Werte direkt – und alle Tests bleiben **rot**.

Ursache: Die Tabelle wurde an einem **Freitag nachmittag** mit aktivem Freitag-Bonus erstellt:
- `5,00 + 25,50 = 30,50` ✓
- `141.049,86 + 25,50 = 141.075,36` ✓

Lösung: `FesterProvisionContext(freitagNach15Uhr: false)` für deterministische Tests.

### Aha-Moment

`NotfallBerechnung` trägt einen einschüchternden Kommentar:
> _„ACHTUNG: KRITISCHER CORE-ALGORITHMUS – Workaround für den Pentium-FDIV-Bug..."_

Die Funktion ist **vollständig toter Code**. Die auslösende Bedingung
`Math.Abs(valX) * -6.35 > 5` kann mathematisch nie wahr sein.
Der Kommentar ist reines Social Engineering – niemand sollte diese Funktion anfassen.

---

## Architektur-Entscheidungen

**`decimal` statt `double`** – Geldbeträge brauchen exakte Dezimalarithmetik.

**`IProvisionContext`-Interface** – Freitag-Bonus ist zeitabhängig und würde Tests nicht-deterministisch machen. Das Interface erlaubt Injektion eines festen Kontexts im Test.

**`record` für Ein-/Ausgabe** – Immutability verhindert unbeabsichtigte Mutation (wie das `ByRef`-Problem im Legacy-Code).

**Konstanten statt Magic Numbers** – Alle Schwellwerte und Sätze sind benannte Konstanten in `ProvisionsRechner.cs`.
