Namespace Domain

    ''' <summary>
    ''' Immutable Input-Objekt für die Provisionsberechnung.
    ''' Enthält alle Eingabedaten ohne UI-Abhängigkeiten.
    ''' </summary>
    Public Structure ProvisionsEingabe

        Public ReadOnly Property Umsatz As Decimal
        Public ReadOnly Property Kategorie As Kategorie
        Public ReadOnly Property IstVipKunde As Boolean
        Public ReadOnly Property IstJubilaeumsAktion As Boolean

        Public Sub New(umsatz As Decimal, kategorie As Kategorie, istVipKunde As Boolean, istJubilaeumsAktion As Boolean)
            Me.Umsatz = umsatz
            Me.Kategorie = kategorie
            Me.IstVipKunde = istVipKunde
            Me.IstJubilaeumsAktion = istJubilaeumsAktion
        End Sub

    End Structure

End Namespace
