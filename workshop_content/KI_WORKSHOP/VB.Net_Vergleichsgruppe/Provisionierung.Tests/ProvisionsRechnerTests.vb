Imports Provisionierung
Imports Provisionierung.Domain
Imports Xunit

Namespace Tests

    ''' <summary>
    ''' Unit-Tests für die Provisionsberechnung.
    ''' Alle Testfälle aus der Spezifikation abgedeckt.
    ''' </summary>
    Public Class ProvisionsRechnerTests

        Private ReadOnly _rechner As ProvisionsRechner

        Public Sub New()
            ' Kontext ohne Freitag-Bonus für deterministische Tests
            Dim context = New FesterProvisionContext(New DateTime(2024, 1, 15), freitagNach15Uhr:=False)
            _rechner = New ProvisionsRechner(context)
        End Sub

        ' Test 1: 1000, A, ❌, ❌ → 100€
        <Fact>
        Public Sub Berechne_1000_KategorieA_OhneBoni_Ergibt100()
            Dim eingabe As New ProvisionsEingabe(1000D, Kategorie.A, False, False)
            Dim ergebnis = _rechner.Berechne(eingabe)
            Assert.Equal(100D, ergebnis.EndProvision)
        End Sub

        ' Test 2: 2000, B, ❌, ❌ → 140€
        <Fact>
        Public Sub Berechne_2000_KategorieB_OhneBoni_Ergibt140()
            Dim eingabe As New ProvisionsEingabe(2000D, Kategorie.B, False, False)
            Dim ergebnis = _rechner.Berechne(eingabe)
            Assert.Equal(140D, ergebnis.EndProvision)
        End Sub

        ' Test 3: 3000, C, ❌, ❌ → 150€
        <Fact>
        Public Sub Berechne_3000_KategorieC_OhneBoni_Ergibt150()
            Dim eingabe As New ProvisionsEingabe(3000D, Kategorie.C, False, False)
            Dim ergebnis = _rechner.Berechne(eingabe)
            Assert.Equal(150D, ergebnis.EndProvision)
        End Sub

        ' Test 4: 1000, A, ✔️, ❌ → 170€
        <Fact>
        Public Sub Berechne_1000_KategorieA_MitVIP_Ergibt170()
            Dim eingabe As New ProvisionsEingabe(1000D, Kategorie.A, True, False)
            Dim ergebnis = _rechner.Berechne(eingabe)
            Assert.Equal(170D, ergebnis.EndProvision)
        End Sub

        ' Test 5: 500, B, ✔️, ❌ → 95€
        <Fact>
        Public Sub Berechne_500_KategorieB_MitVIP_Ergibt95()
            Dim eingabe As New ProvisionsEingabe(500D, Kategorie.B, True, False)
            Dim ergebnis = _rechner.Berechne(eingabe)
            Assert.Equal(95D, ergebnis.EndProvision)
        End Sub

        ' Test 6: 6000, C, ❌, ✔️ → 600€
        <Fact>
        Public Sub Berechne_6000_KategorieC_MitJubilaeum_Ergibt600()
            Dim eingabe As New ProvisionsEingabe(6000D, Kategorie.C, False, True)
            Dim ergebnis = _rechner.Berechne(eingabe)
            Assert.Equal(600D, ergebnis.EndProvision)
        End Sub

        ' Test 7: 5000, A, ❌, ✔️ → 500€
        <Fact>
        Public Sub Berechne_5000_KategorieA_MitJubilaeum_Grenze_Ergibt500()
            Dim eingabe As New ProvisionsEingabe(5000D, Kategorie.A, False, True)
            Dim ergebnis = _rechner.Berechne(eingabe)
            Assert.Equal(500D, ergebnis.EndProvision)
        End Sub

        ' Test 8: 10000, B, ✔️, ✔️ → 1.450,00€
        <Fact>
        Public Sub Berechne_10000_KategorieB_MitVIPUndJubilaeum_Ergibt1450()
            Dim eingabe As New ProvisionsEingabe(10000D, Kategorie.B, True, True)
            Dim ergebnis = _rechner.Berechne(eingabe)
            Assert.Equal(1450D, ergebnis.EndProvision)
        End Sub

        ' Test 9: 60000, A, ❌, ❌ → 7.000,00€
        <Fact>
        Public Sub Berechne_60000_KategorieA_Grossumsatz_Ergibt7000()
            Dim eingabe As New ProvisionsEingabe(60000D, Kategorie.A, False, False)
            Dim ergebnis = _rechner.Berechne(eingabe)
            Assert.Equal(7000D, ergebnis.EndProvision)
        End Sub

        ' Test 10: 60000, A, ✔️, ❌ → 8.250,00€
        <Fact>
        Public Sub Berechne_60000_KategorieA_MitVIP_Grossumsatz_Ergibt8250()
            Dim eingabe As New ProvisionsEingabe(60000D, Kategorie.A, True, False)
            Dim ergebnis = _rechner.Berechne(eingabe)
            Assert.Equal(8250D, ergebnis.EndProvision)
        End Sub

        ' Test 11: 100, C, ❌, ❌ → 5€
        <Fact>
        Public Sub Berechne_100_KategorieC_OhneBoni_Ergibt5()
            Dim eingabe As New ProvisionsEingabe(100D, Kategorie.C, False, False)
            Dim ergebnis = _rechner.Berechne(eingabe)
            Assert.Equal(5D, ergebnis.EndProvision)
        End Sub

        ' Test 12: -100, A, ❌, ❌ → -10€
        <Fact>
        Public Sub Berechne_NegativerUmsatz_KategorieA_ErgibtNegativ()
            Dim eingabe As New ProvisionsEingabe(-100D, Kategorie.A, False, False)
            Dim ergebnis = _rechner.Berechne(eingabe)
            Assert.Equal(-10D, ergebnis.EndProvision)
        End Sub

        ' Test 13: 0, A, ❌, ❌ → 0€
        <Fact>
        Public Sub Berechne_0_KategorieA_Ergibt0()
            Dim eingabe As New ProvisionsEingabe(0D, Kategorie.A, False, False)
            Dim ergebnis = _rechner.Berechne(eingabe)
            Assert.Equal(0D, ergebnis.EndProvision)
        End Sub

        ' Test 14: 999999, B, ✔️, ✔️
        <Fact>
        Public Sub Berechne_999999_KategorieB_MitVIPUndJubilaeum_ErgibtKorrektenWert()
            Dim eingabe As New ProvisionsEingabe(999999D, Kategorie.B, True, True)
            Dim ergebnis = _rechner.Berechne(eingabe)
            Assert.Equal(141049.86D, ergebnis.EndProvision)
        End Sub

        ' Zusätzlich: Freitag Bonus Test
        <Fact>
        Public Sub Berechne_MitFreitagBonus_Fuegt25_50Hinzu()
            Dim freitagContext = New FesterProvisionContext(New DateTime(2024, 1, 5, 16, 0, 0), freitagNach15Uhr:=True)
            Dim rechner = New ProvisionsRechner(freitagContext)
            Dim eingabe As New ProvisionsEingabe(1000D, Kategorie.A, False, False)
            Dim ergebnis = rechner.Berechne(eingabe)

            Assert.Equal(125.5D, ergebnis.EndProvision)
            Assert.Contains(ergebnis.AngewandteBoni, Function(b) b.Contains("Freitag"))
        End Sub

    End Class

End Namespace
