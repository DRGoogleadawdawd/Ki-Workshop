namespace Provisionierung.Domain;

public record ProvisionsErgebnis(
    decimal EndProvision,
    decimal BasisProvision,
    decimal AngewandterSatz,
    IReadOnlyList<string> AngewandteBoni
)
{
    public override string ToString() =>
        $"Provision: {EndProvision:C} (Satz: {AngewandterSatz:P1}, Boni: {string.Join(", ", AngewandteBoni)})";
}
