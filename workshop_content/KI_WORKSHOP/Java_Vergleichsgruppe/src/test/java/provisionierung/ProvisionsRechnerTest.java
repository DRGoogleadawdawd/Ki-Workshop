package provisionierung;

import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import provisionierung.domain.*;

import java.math.BigDecimal;
import java.time.LocalDateTime;

import static org.junit.jupiter.api.Assertions.*;

/**
 * Unit-Tests für die Provisionsberechnung.
 * Alle Testfälle aus der Spezifikation abgedeckt.
 */
class ProvisionsRechnerTest {

    private ProvisionsRechner rechner;

    @BeforeEach
    void setUp() {
        // Kontext ohne Freitag-Bonus für deterministische Tests
        ProvisionContext context = new FesterProvisionContext(
                LocalDateTime.of(2024, 1, 15, 10, 0), false);
        rechner = new ProvisionsRechner(context);
    }

    // Test 1: 1000, A, ❌, ❌ → 100€
    @Test
    void berechne_1000_KategorieA_OhneBoni_Ergibt100() {
        ProvisionsEingabe eingabe = new ProvisionsEingabe(
                new BigDecimal("1000"), Kategorie.A, false, false);
        ProvisionsErgebnis ergebnis = rechner.berechne(eingabe);
        assertEquals(new BigDecimal("100.00"), ergebnis.getEndProvision());
    }

    // Test 2: 2000, B, ❌, ❌ → 140€
    @Test
    void berechne_2000_KategorieB_OhneBoni_Ergibt140() {
        ProvisionsEingabe eingabe = new ProvisionsEingabe(
                new BigDecimal("2000"), Kategorie.B, false, false);
        ProvisionsErgebnis ergebnis = rechner.berechne(eingabe);
        assertEquals(new BigDecimal("140.00"), ergebnis.getEndProvision());
    }

    // Test 3: 3000, C, ❌, ❌ → 150€
    @Test
    void berechne_3000_KategorieC_OhneBoni_Ergibt150() {
        ProvisionsEingabe eingabe = new ProvisionsEingabe(
                new BigDecimal("3000"), Kategorie.C, false, false);
        ProvisionsErgebnis ergebnis = rechner.berechne(eingabe);
        assertEquals(new BigDecimal("150.00"), ergebnis.getEndProvision());
    }

    // Test 4: 1000, A, ✔️, ❌ → 170€
    // 1000 × (10% + 2%) + 50 VIP-Pauschale = 120 + 50 = 170
    @Test
    void berechne_1000_KategorieA_MitVIP_Ergibt170() {
        ProvisionsEingabe eingabe = new ProvisionsEingabe(
                new BigDecimal("1000"), Kategorie.A, true, false);
        ProvisionsErgebnis ergebnis = rechner.berechne(eingabe);
        assertEquals(new BigDecimal("170.00"), ergebnis.getEndProvision());
    }

    // Test 5: 500, B, ✔️, ❌ → 95€
    // 500 × (7% + 2%) + 50 VIP-Pauschale = 45 + 50 = 95
    @Test
    void berechne_500_KategorieB_MitVIP_Ergibt95() {
        ProvisionsEingabe eingabe = new ProvisionsEingabe(
                new BigDecimal("500"), Kategorie.B, true, false);
        ProvisionsErgebnis ergebnis = rechner.berechne(eingabe);
        assertEquals(new BigDecimal("95.00"), ergebnis.getEndProvision());
    }

    // Test 6: 6000, C, ❌, ✔️ → 600€
    // 6000 × (5% + 5%) = 600 (Jubiläum gilt, da >5000)
    @Test
    void berechne_6000_KategorieC_MitJubilaeum_Ergibt600() {
        ProvisionsEingabe eingabe = new ProvisionsEingabe(
                new BigDecimal("6000"), Kategorie.C, false, true);
        ProvisionsErgebnis ergebnis = rechner.berechne(eingabe);
        assertEquals(new BigDecimal("600.00"), ergebnis.getEndProvision());
    }

    // Test 7: 5000, A, ❌, ✔️ → 500€
    // 5000 × 10% = 500 (Jubiläum gilt NICHT, da <=5000)
    @Test
    void berechne_5000_KategorieA_MitJubilaeum_Grenze_Ergibt500() {
        ProvisionsEingabe eingabe = new ProvisionsEingabe(
                new BigDecimal("5000"), Kategorie.A, false, true);
        ProvisionsErgebnis ergebnis = rechner.berechne(eingabe);
        assertEquals(new BigDecimal("500.00"), ergebnis.getEndProvision());
    }

    // Test 8: 10000, B, ✔️, ✔️ → 1.450,00€
    // 10000 × (7% + 2% + 5%) + 50 = 1400 + 50 = 1450
    @Test
    void berechne_10000_KategorieB_MitVIPUndJubilaeum_Ergibt1450() {
        ProvisionsEingabe eingabe = new ProvisionsEingabe(
                new BigDecimal("10000"), Kategorie.B, true, true);
        ProvisionsErgebnis ergebnis = rechner.berechne(eingabe);
        assertEquals(new BigDecimal("1450.00"), ergebnis.getEndProvision());
    }

    // Test 9: 60000, A, ❌, ❌ → 7.000,00€
    // 60000 × 10% + 1000 Großumsatz = 6000 + 1000 = 7000
    @Test
    void berechne_60000_KategorieA_Grossumsatz_Ergibt7000() {
        ProvisionsEingabe eingabe = new ProvisionsEingabe(
                new BigDecimal("60000"), Kategorie.A, false, false);
        ProvisionsErgebnis ergebnis = rechner.berechne(eingabe);
        assertEquals(new BigDecimal("7000.00"), ergebnis.getEndProvision());
    }

    // Test 10: 60000, A, ✔️, ❌ → 8.250,00€
    // 60000 × (10% + 2%) + 1000 + 50 = 7200 + 1050 = 8250
    @Test
    void berechne_60000_KategorieA_MitVIP_Grossumsatz_Ergibt8250() {
        ProvisionsEingabe eingabe = new ProvisionsEingabe(
                new BigDecimal("60000"), Kategorie.A, true, false);
        ProvisionsErgebnis ergebnis = rechner.berechne(eingabe);
        assertEquals(new BigDecimal("8250.00"), ergebnis.getEndProvision());
    }

    // Test 11: 100, C, ❌, ❌ → 30,5€ (Legacy-Verhalten)
    @Test
    void berechne_100_KategorieC_OhneBoni_Ergibt5() {
        ProvisionsEingabe eingabe = new ProvisionsEingabe(
                new BigDecimal("100"), Kategorie.C, false, false);
        ProvisionsErgebnis ergebnis = rechner.berechne(eingabe);
        assertEquals(new BigDecimal("5.00"), ergebnis.getEndProvision());
    }

    // Test 12: -100, A, ❌, ❌ → -10€
    @Test
    void berechne_NegativerUmsatz_KategorieA_ErgibtNegativ() {
        ProvisionsEingabe eingabe = new ProvisionsEingabe(
                new BigDecimal("-100"), Kategorie.A, false, false);
        ProvisionsErgebnis ergebnis = rechner.berechne(eingabe);
        assertEquals(new BigDecimal("-10.00"), ergebnis.getEndProvision());
    }

    // Test 13: 0, A, ❌, ❌ → 0€
    @Test
    void berechne_0_KategorieA_Ergibt0() {
        ProvisionsEingabe eingabe = new ProvisionsEingabe(
                new BigDecimal("0"), Kategorie.A, false, false);
        ProvisionsErgebnis ergebnis = rechner.berechne(eingabe);
        assertEquals(new BigDecimal("0.00"), ergebnis.getEndProvision());
    }

    // Test 14: 999999, B, ✔️, ✔️ → 141.075,36€
    @Test
    void berechne_999999_KategorieB_MitVIPUndJubilaeum_ErgibtKorrektenWert() {
        ProvisionsEingabe eingabe = new ProvisionsEingabe(
                new BigDecimal("999999"), Kategorie.B, true, true);
        ProvisionsErgebnis ergebnis = rechner.berechne(eingabe);
        assertEquals(new BigDecimal("141049.86"), ergebnis.getEndProvision());
    }

    @Test
    void berechne_MitFreitagBonus_Fuegt25_50Hinzu() {
        ProvisionContext freitagContext = new FesterProvisionContext(
                LocalDateTime.of(2024, 1, 5, 16, 0), true);
        ProvisionsRechner freitagRechner = new ProvisionsRechner(freitagContext);

        ProvisionsEingabe eingabe = new ProvisionsEingabe(
                new BigDecimal("1000"), Kategorie.A, false, false);
        ProvisionsErgebnis ergebnis = freitagRechner.berechne(eingabe);

        assertEquals(new BigDecimal("125.50"), ergebnis.getEndProvision());
        assertTrue(ergebnis.getAngewandteBoni().get(0).contains("Freitag"));
    }

    @Test
    void berechne_ZeigtAlleAngewandteBoni() {
        ProvisionsEingabe eingabe = new ProvisionsEingabe(
                new BigDecimal("60000"), Kategorie.B, true, true);
        ProvisionsErgebnis ergebnis = rechner.berechne(eingabe);

        assertEquals(4, ergebnis.getAngewandteBoni().size());
        assertTrue(ergebnis.getAngewandteBoni().stream().anyMatch(b -> b.contains("VIP")));
        assertTrue(ergebnis.getAngewandteBoni().stream().anyMatch(b -> b.contains("Jubiläum")));
        assertTrue(ergebnis.getAngewandteBoni().stream().anyMatch(b -> b.contains("Großumsatz")));
        assertTrue(ergebnis.getAngewandteBoni().stream().anyMatch(b -> b.contains("VIP-Pauschale")));
    }
}
