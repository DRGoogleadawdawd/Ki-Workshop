namespace Provisionierung.Domain;

/// <summary>
/// Für Tests: Feste Zeitkonfiguration.
/// </summary>
public class FesterProvisionContext : IProvisionContext
{
    private readonly DateTime _festesDatum;
    private readonly bool _freitagNach15Uhr;

    public FesterProvisionContext(DateTime festesDatum, bool freitagNach15Uhr = false)
    {
        _festesDatum = festesDatum;
        _freitagNach15Uhr = freitagNach15Uhr;
    }

    public DateTime AktuellesDatum => _festesDatum;

    public bool IstFreitagNach15Uhr() => _freitagNach15Uhr;
}
