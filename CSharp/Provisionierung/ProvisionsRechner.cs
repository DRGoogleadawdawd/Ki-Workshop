using Provisionierung.Domain;

namespace Provisionierung;

public class ProvisionsRechner
{
    private const decimal VipRateBonus = 0.02m;
    private const decimal VipFlatBonus = 50m;
    private const decimal JubilaeumsRateBonus = 0.05m;
    private const decimal JubilaeumsUmsatzSchwelle = 5000m;
    private const decimal GrossUmsatzSchwelle = 50000m;
    private const decimal GrossUmsatzFlatBonus = 1000m;
    private const decimal FreitagFlatBonus = 25.5m;

    private readonly IProvisionContext _context;

    public ProvisionsRechner(IProvisionContext? context = null)
    {
        _context = context ?? new SystemProvisionContext();
    }

    public ProvisionsErgebnis Berechne(ProvisionsEingabe eingabe)
    {
        var satz = eingabe.Kategorie.GetBasisSatz();
        var flatBoni = 0m;
        var boniNamen = new List<string>();

        if (eingabe.IstVipKunde)
        {
            satz += VipRateBonus;
            flatBoni += VipFlatBonus;
            boniNamen.Add("VIP (+2%, +50€)");
        }

        if (eingabe.IstJubilaeumsAktion && eingabe.Umsatz > JubilaeumsUmsatzSchwelle)
        {
            satz += JubilaeumsRateBonus;
            boniNamen.Add("Jubiläum (+5%)");
        }

        var basisProvision = eingabe.Umsatz * satz;
        var endProvision = basisProvision;

        if (eingabe.Umsatz > GrossUmsatzSchwelle)
        {
            endProvision += GrossUmsatzFlatBonus;
            boniNamen.Add("Großumsatz (+1000€)");
        }

        endProvision += flatBoni;

        if (_context.IstFreitagNach15Uhr())
        {
            endProvision += FreitagFlatBonus;
            boniNamen.Add("Freitag nach 15 Uhr (+25,50€)");
        }

        return new ProvisionsErgebnis(
            EndProvision: endProvision,
            BasisProvision: basisProvision,
            AngewandterSatz: satz,
            AngewandteBoni: boniNamen.AsReadOnly()
        );
    }
}
