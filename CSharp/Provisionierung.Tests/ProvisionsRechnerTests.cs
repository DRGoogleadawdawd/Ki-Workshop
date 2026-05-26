using Provisionierung;
using Provisionierung.Domain;
using Xunit;

namespace Provisionierung.Tests;

public class ProvisionsRechnerTests
{
    private readonly ProvisionsRechner _rechner;

    public ProvisionsRechnerTests()
    {
        // Deterministische Tests: kein Freitag-Bonus
        _rechner = new ProvisionsRechner(new FesterProvisionContext(freitagNach15Uhr: false));
    }

    // ── Basislogik ohne Boni ─────────────────────────────────────────────────

    [Fact]
    public void Berechne_1000_KategorieA_OhneBoni_Ergibt100()
    {
        var eingabe = new ProvisionsEingabe(1000m, Kategorie.A, false, false);
        Assert.Equal(100m, _rechner.Berechne(eingabe).EndProvision);
    }

    [Fact]
    public void Berechne_2000_KategorieB_OhneBoni_Ergibt140()
    {
        var eingabe = new ProvisionsEingabe(2000m, Kategorie.B, false, false);
        Assert.Equal(140m, _rechner.Berechne(eingabe).EndProvision);
    }

    [Fact]
    public void Berechne_3000_KategorieC_OhneBoni_Ergibt150()
    {
        var eingabe = new ProvisionsEingabe(3000m, Kategorie.C, false, false);
        Assert.Equal(150m, _rechner.Berechne(eingabe).EndProvision);
    }

    // ── VIP-Bonus (+2% Rate + 50€ Flat) ─────────────────────────────────────

    [Fact]
    public void Berechne_1000_KategorieA_MitVIP_Ergibt170()
    {
        // 1000 * 0.12 = 120 + 50 VIP-Flat = 170
        var eingabe = new ProvisionsEingabe(1000m, Kategorie.A, true, false);
        Assert.Equal(170m, _rechner.Berechne(eingabe).EndProvision);
    }

    [Fact]
    public void Berechne_500_KategorieB_MitVIP_Ergibt95()
    {
        // 500 * 0.09 = 45 + 50 VIP-Flat = 95
        var eingabe = new ProvisionsEingabe(500m, Kategorie.B, true, false);
        Assert.Equal(95m, _rechner.Berechne(eingabe).EndProvision);
    }

    // ── Jubiläums-Bonus (+5% Rate, nur wenn Umsatz > 5000) ──────────────────

    [Fact]
    public void Berechne_6000_KategorieC_MitJubilaeum_Ergibt600()
    {
        // 6000 * 0.10 = 600 (5% + 5% Jubiläum, da 6000 > 5000)
        var eingabe = new ProvisionsEingabe(6000m, Kategorie.C, false, true);
        Assert.Equal(600m, _rechner.Berechne(eingabe).EndProvision);
    }

    [Fact]
    public void Berechne_5000_KategorieA_MitJubilaeum_Grenzwert_Ergibt500()
    {
        // 5000 ist NICHT > 5000, daher kein Jubiläums-Bonus: 5000 * 0.10 = 500
        var eingabe = new ProvisionsEingabe(5000m, Kategorie.A, false, true);
        Assert.Equal(500m, _rechner.Berechne(eingabe).EndProvision);
    }

    // ── Kombination VIP + Jubiläum ───────────────────────────────────────────

    [Fact]
    public void Berechne_10000_KategorieB_MitVIPUndJubilaeum_Ergibt1450()
    {
        // 10000 * 0.14 = 1400 + 50 VIP-Flat = 1450
        var eingabe = new ProvisionsEingabe(10000m, Kategorie.B, true, true);
        Assert.Equal(1450m, _rechner.Berechne(eingabe).EndProvision);
    }

    // ── Großumsatz-Bonus (>50.000: +1000€) ──────────────────────────────────

    [Fact]
    public void Berechne_60000_KategorieA_OhneBoni_Ergibt7000()
    {
        // 60000 * 0.10 = 6000 + 1000 Großumsatz = 7000
        var eingabe = new ProvisionsEingabe(60000m, Kategorie.A, false, false);
        Assert.Equal(7000m, _rechner.Berechne(eingabe).EndProvision);
    }

    [Fact]
    public void Berechne_60000_KategorieA_MitVIP_Ergibt8250()
    {
        // 60000 * 0.12 = 7200 + 1000 Großumsatz + 50 VIP-Flat = 8250
        var eingabe = new ProvisionsEingabe(60000m, Kategorie.A, true, false);
        Assert.Equal(8250m, _rechner.Berechne(eingabe).EndProvision);
    }

    // ── Kleiner Umsatz ───────────────────────────────────────────────────────

    [Fact]
    public void Berechne_100_KategorieC_OhneBoni_Ergibt5()
    {
        // 100 * 0.05 = 5
        var eingabe = new ProvisionsEingabe(100m, Kategorie.C, false, false);
        Assert.Equal(5m, _rechner.Berechne(eingabe).EndProvision);
    }

    // ── Negativer und null Umsatz ────────────────────────────────────────────

    [Fact]
    public void Berechne_NegativerUmsatz_KategorieA_ErgibtNegativProvision()
    {
        // -100 * 0.10 = -10
        var eingabe = new ProvisionsEingabe(-100m, Kategorie.A, false, false);
        Assert.Equal(-10m, _rechner.Berechne(eingabe).EndProvision);
    }

    [Fact]
    public void Berechne_Umsatz0_Ergibt0()
    {
        var eingabe = new ProvisionsEingabe(0m, Kategorie.A, false, false);
        Assert.Equal(0m, _rechner.Berechne(eingabe).EndProvision);
    }

    // ── Großer Umsatz mit allen Boni ─────────────────────────────────────────

    [Fact]
    public void Berechne_999999_KategorieB_MitVIPUndJubilaeum_ErgibtKorrektenWert()
    {
        // 999999 * 0.14 = 139999.86 + 1000 Großumsatz + 50 VIP-Flat = 141049.86
        var eingabe = new ProvisionsEingabe(999999m, Kategorie.B, true, true);
        Assert.Equal(141049.86m, _rechner.Berechne(eingabe).EndProvision);
    }

    // ── Freitag-Bonus ────────────────────────────────────────────────────────

    [Fact]
    public void Berechne_MitFreitagBonus_Fuegt25_50Hinzu()
    {
        var freitagRechner = new ProvisionsRechner(new FesterProvisionContext(freitagNach15Uhr: true));
        var eingabe = new ProvisionsEingabe(1000m, Kategorie.A, false, false);
        var ergebnis = freitagRechner.Berechne(eingabe);

        // 100 + 25.5 Freitag-Bonus = 125.5
        Assert.Equal(125.5m, ergebnis.EndProvision);
        Assert.Contains(ergebnis.AngewandteBoni, b => b.Contains("Freitag"));
    }
}
