using Provisionierung.Domain;

namespace Provisionierung;

/// <summary>
/// Aufgabe: Implementiere die Provisionsberechnung basierend auf den Tests und dem Legacy-Code.
/// 
/// Analyse:
/// - Die Testfälle in ProvisionsRechnerTests.cs definieren das erwartete Verhalten
/// - Der Legacy-Code (LegacyForm.vb) enthält die originale Berechnungslogik
/// 
/// Anforderungen:
/// - Keine UI-Abhängigkeiten (kein MessageBox, kein DoEvents)
/// - Nutze das IProvisionContext-Interface für zeitabhängige Logik (Freitag-Bonus)
/// - Alle magischen Zahlen als Konstanten definieren
/// - Modernes Error-Handling statt On Error Resume Next
/// 
/// Hinweis: Du darfst das Domänenmodell anpassen wenn nötig.
/// </summary>
public class ProvisionsRechner
{
    // Definiere hier deine Konstanten

    private readonly IProvisionContext _context;

    public ProvisionsRechner(IProvisionContext? context = null)
    {
        _context = context ?? new SystemProvisionContext();
    }

    public ProvisionsErgebnis Berechne(ProvisionsEingabe eingabe)
    {
        throw new NotImplementedException("Implementiere die Berechnungslogik basierend auf den Tests.");
    }
}
