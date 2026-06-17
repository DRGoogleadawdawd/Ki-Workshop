namespace Provisionierung.Domain;

public interface IProvisionContext
{
    bool IstFreitagNach15Uhr();
}

public class SystemProvisionContext : IProvisionContext
{
    public bool IstFreitagNach15Uhr()
    {
        var now = DateTime.Now;
        return now.DayOfWeek == DayOfWeek.Friday && now.Hour >= 15;
    }
}

public class FesterProvisionContext : IProvisionContext
{
    private readonly bool _freitagNach15Uhr;

    public FesterProvisionContext(bool freitagNach15Uhr = false)
    {
        _freitagNach15Uhr = freitagNach15Uhr;
    }

    public bool IstFreitagNach15Uhr() => _freitagNach15Uhr;
}
