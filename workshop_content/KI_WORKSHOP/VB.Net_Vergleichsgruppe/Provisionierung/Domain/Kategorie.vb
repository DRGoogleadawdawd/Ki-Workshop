Namespace Domain

    ''' <summary>
    ''' Kundenkategorien mit ihren Basis-Provisionssätzen.
    ''' </summary>
    Public Enum Kategorie
        A = 0   ' 10% Basisprovision
        B = 1   ' 7% Basisprovision
        C = 2   ' 5% Basisprovision
        Sonstige = 3   ' 1% Basisprovision (Fallback)
    End Enum

    ''' <summary>
    ''' Extension Methods für Kategorie
    ''' </summary>
    Public Module KategorieExtensions

        ''' <summary>
        ''' Gibt den Basis-Provisionssatz für eine Kategorie zurück.
        ''' </summary>
        <System.Runtime.CompilerServices.Extension>
        Public Function GetBasisSatz(kategorie As Kategorie) As Decimal
            Select Case kategorie
                Case Kategorie.A
                    Return 0.10D
                Case Kategorie.B
                    Return 0.07D
                Case Kategorie.C
                    Return 0.05D
                Case Kategorie.Sonstige
                    Return 0.01D
                Case Else
                    Throw New ArgumentOutOfRangeException(NameOf(kategorie), $"Unbekannte Kategorie: {kategorie}")
            End Select
        End Function

    End Module

End Namespace
