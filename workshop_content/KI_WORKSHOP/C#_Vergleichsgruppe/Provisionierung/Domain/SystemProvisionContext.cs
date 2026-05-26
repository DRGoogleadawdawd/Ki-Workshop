namespace Provisionierung.Domain;

/// <summary>
/// Standard-Implementierung basierend auf Systemzeit.
/// </summary>
public class SystemProvisionContext : IProvisionContext
{
    public DateTime AktuellesDatum => DateTime.Now;

    public bool IstFreitagNach15Uhr()
    {
        var now = AktuellesDatum;
        return now.DayOfWeek == DayOfWeek.Friday && now.Hour >= 15;
    }
}
