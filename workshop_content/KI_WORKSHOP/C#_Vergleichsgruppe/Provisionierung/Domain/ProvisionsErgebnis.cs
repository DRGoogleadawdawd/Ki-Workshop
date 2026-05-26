namespace Provisionierung.Domain;

/// <summary>
/// Immutable Output-Objekt mit detailliertem Provisionsergebnis.
/// Ermöglicht Nachvollziehbarkeit aller Berechnungsschritte.
/// </summary>
public readonly struct ProvisionsErgebnis
{
    public decimal EndProvision { get; }
    public decimal BasisProvision { get; }
    public decimal Zuschlaege { get; }
    public decimal AngewandterSatz { get; }
    public IReadOnlyList<string> AngewandteBoni { get; }

    public ProvisionsErgebnis(
        decimal endProvision,
        decimal basisProvision,
        decimal zuschlaege,
        decimal angewandterSatz,
        IEnumerable<string> angewandteBoni)
    {
        EndProvision = endProvision;
        BasisProvision = basisProvision;
        Zuschlaege = zuschlaege;
        AngewandterSatz = angewandterSatz;
        AngewandteBoni = angewandteBoni.ToList().AsReadOnly();
    }

    public override string ToString()
    {
        return $"Provision: {EndProvision:C} (Satz: {AngewandterSatz:P1}, Boni: {string.Join(", ", AngewandteBoni)})";
    }
}
