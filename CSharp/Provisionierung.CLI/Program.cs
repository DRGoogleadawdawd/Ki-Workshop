using Provisionierung;
using Provisionierung.Domain;

var rechner = new ProvisionsRechner();

Console.WriteLine("╔══════════════════════════════════════════════════════╗");
Console.WriteLine("║         Provisionsrechner v1.0 (C# Migration)        ║");
Console.WriteLine("╚══════════════════════════════════════════════════════╝");
Console.WriteLine();

while (true)
{
    Console.Write("Umsatz (EUR, leer = beenden): ");
    var input = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(input)) break;

    if (!decimal.TryParse(input, System.Globalization.NumberStyles.Any,
            System.Globalization.CultureInfo.InvariantCulture, out var umsatz))
    {
        Console.WriteLine("  ✗ Ungültiger Umsatz.\n");
        continue;
    }

    Console.Write("Kategorie (A/B/C): ");
    var katInput = Console.ReadLine()?.Trim().ToUpper();
    if (!Enum.TryParse<Kategorie>(katInput, out var kategorie))
    {
        Console.WriteLine("  ✗ Ungültige Kategorie.\n");
        continue;
    }

    Console.Write("VIP-Kunde? (j/n): ");
    var vip = Console.ReadLine()?.Trim().ToLower() == "j";

    Console.Write("Jubiläumsaktion? (j/n): ");
    var jub = Console.ReadLine()?.Trim().ToLower() == "j";

    Console.WriteLine();

    var eingabe = new ProvisionsEingabe(umsatz, kategorie, vip, jub);
    var ergebnis = rechner.Berechne(eingabe);

    Console.WriteLine($"  Basisprovision : {ergebnis.BasisProvision,12:C}");
    if (ergebnis.AngewandteBoni.Count > 0)
        foreach (var bonus in ergebnis.AngewandteBoni)
            Console.WriteLine($"  Bonus          : {bonus}");
    Console.WriteLine($"  ─────────────────────────────");
    Console.WriteLine($"  Provision      : {ergebnis.EndProvision,12:C}  (Satz: {ergebnis.AngewandterSatz:P0})");
    Console.WriteLine();
}

Console.WriteLine("Auf Wiedersehen!");
