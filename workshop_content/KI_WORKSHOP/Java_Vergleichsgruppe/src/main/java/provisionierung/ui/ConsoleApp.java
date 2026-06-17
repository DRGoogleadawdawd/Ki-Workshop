package provisionierung.ui;

import provisionierung.ProvisionsRechner;
import provisionierung.domain.*;

import java.math.BigDecimal;
import java.time.LocalDateTime;
import java.util.Scanner;

/**
 * Konsolen-Frontend für die Provisionsberechnung.
 * Einfache CLI ohne externe Abhängigkeiten.
 */
public class ConsoleApp {

    public static void main(String[] args) {
        System.out.println("╔════════════════════════════════════════════════╗");
        System.out.println("║     Provisionsrechner - Legacy Migration       ║");
        System.out.println("║           Workshop Version 2024                ║");
        System.out.println("╚════════════════════════════════════════════════╝");
        System.out.println();

        Scanner scanner = new Scanner(System.in);
        ProvisionContext context = new SystemProvisionContext();
        ProvisionsRechner rechner = new ProvisionsRechner(context);

        while (true) {
            System.out.println("\n--- Neue Berechnung ---");

            // Umsatz eingeben
            System.out.print("Umsatz (€): ");
            String umsatzInput = scanner.nextLine().trim();
            if (umsatzInput.isEmpty()) {
                System.out.println("Programm beendet.");
                break;
            }

            BigDecimal umsatz;
            try {
                umsatz = new BigDecimal(umsatzInput.replace(",", "."));
            } catch (NumberFormatException e) {
                System.out.println("❌ Ungültiger Umsatz! Bitte Zahl eingeben.");
                continue;
            }

            // Kategorie wählen
            System.out.println("Kategorie: [A] A-Kunde (10%)  [B] B-Kunde (7%)  [C] C-Kunde (5%)");
            System.out.print("Wahl: ");
            String katInput = scanner.nextLine().trim().toUpperCase();

            Kategorie kategorie;
            switch (katInput) {
                case "A":
                    kategorie = Kategorie.A;
                    break;
                case "B":
                    kategorie = Kategorie.B;
                    break;
                case "C":
                    kategorie = Kategorie.C;
                    break;
                default:
                    System.out.println("❌ Ungültige Kategorie! Standard: A");
                    kategorie = Kategorie.A;
            }

            // VIP
            System.out.print("VIP-Kunde? (j/n): ");
            boolean isVip = scanner.nextLine().trim().toLowerCase().startsWith("j");

            // Jubiläum
            System.out.print("Jubiläumsaktion? (j/n): ");
            boolean isJubilaeum = scanner.nextLine().trim().toLowerCase().startsWith("j");

            // Berechnung
            ProvisionsEingabe eingabe = new ProvisionsEingabe(umsatz, kategorie, isVip, isJubilaeum);

            try {
                ProvisionsErgebnis ergebnis = rechner.berechne(eingabe);

                // Ausgabe
                System.out.println("\n" + "═".repeat(50));
                System.out.println("           B E R E C H N U N G");
                System.out.println("═".repeat(50));
                System.out.printf("Umsatz:           %12s €%n", umsatz);
                System.out.printf("Kategorie:        %12s (%s)%n", kategorie, kategorie.getBasisSatz() * 100 + "%");
                System.out.printf("Angewandter Satz: %12s%n", (ergebnis.getAngewandterSatz() * 100) + "%");
                System.out.println("-".repeat(50));
                System.out.printf("Basisprovision:   %12s €%n", ergebnis.getBasisProvision());
                System.out.printf("Zuschläge:        %12s €%n", ergebnis.getZuschlaege());
                System.out.println("-".repeat(50));
                System.out.printf("ENDPROVISION:     %12s €%n", ergebnis.getEndProvision());
                System.out.println("═".repeat(50));

                if (!ergebnis.getAngewandteBoni().isEmpty()) {
                    System.out.println("\nAngewandte Boni:");
                    for (String bonus : ergebnis.getAngewandteBoni()) {
                        System.out.println("  ✓ " + bonus);
                    }
                }

            } catch (IllegalArgumentException e) {
                System.out.println("❌ Fehler: " + e.getMessage());
            }

            System.out.println("\n[Enter] für neue Berechnung, oder 'ende' eingeben:");
            if (scanner.nextLine().trim().equalsIgnoreCase("ende")) {
                break;
            }
        }

        System.out.println("\nAuf Wiedersehen! 👋");
    }
}
