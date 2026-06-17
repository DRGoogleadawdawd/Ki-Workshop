Namespace Domain

    ''' <summary>
    ''' Immutable Output-Objekt mit detailliertem Provisionsergebnis.
    ''' Ermöglicht Nachvollziehbarkeit aller Berechnungsschritte.
    ''' </summary>
    Public Structure ProvisionsErgebnis

        Public ReadOnly Property EndProvision As Decimal
        Public ReadOnly Property BasisProvision As Decimal
        Public ReadOnly Property Zuschlaege As Decimal
        Public ReadOnly Property AngewandterSatz As Decimal
        Public ReadOnly Property AngewandteBoni As IReadOnlyList(Of String)

        Public Sub New(endProvision As Decimal, basisProvision As Decimal, zuschlaege As Decimal, angewandterSatz As Decimal, angewandteBoni As IEnumerable(Of String))
            Me.EndProvision = endProvision
            Me.BasisProvision = basisProvision
            Me.Zuschlaege = zuschlaege
            Me.AngewandterSatz = angewandterSatz
            Me.AngewandteBoni = angewandteBoni.ToList().AsReadOnly()
        End Sub

        Public Overrides Function ToString() As String
            Return $"Provision: {EndProvision:C} (Satz: {AngewandterSatz:P1}, Boni: {String.Join(", ", AngewandteBoni)})"
        End Function

    End Structure

End Namespace
