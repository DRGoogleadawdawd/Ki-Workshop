namespace Provisionierung.Domain;

/// <summary>
/// Schnittstelle für zeit- und umgebungsabhängige Provisionsaspekte.
/// Ermöglicht Testbarkeit durch Mocking.
/// </summary>
public interface IProvisionContext
{
    /// <summary>
    /// Aktuelles Datum/Zeit für zeitabhängige Berechnungen.
    /// </summary>
    DateTime AktuellesDatum { get; }

    /// <summary>
    /// Prüft ob der Freitag-nach-15h-Bonus gilt.
    /// </summary>
    bool IstFreitagNach15Uhr();
}
