Namespace Domain

    ''' <summary>
    ''' Schnittstelle für zeit- und umgebungsabhängige Provisionsaspekte.
    ''' Ermöglicht Testbarkeit durch Mocking.
    ''' </summary>
    Public Interface IProvisionContext

        ''' <summary>
        ''' Aktuelles Datum/Zeit für zeitabhängige Berechnungen.
        ''' </summary>
        ReadOnly Property AktuellesDatum As DateTime

        ''' <summary>
        ''' Prüft ob der Freitag-nach-15h-Bonus gilt.
        ''' </summary>
        Function IstFreitagNach15Uhr() As Boolean

    End Interface

    ''' <summary>
    ''' Standard-Implementierung basierend auf Systemzeit.
    ''' </summary>
    Public Class SystemProvisionContext
        Implements IProvisionContext

        Public ReadOnly Property AktuellesDatum As Date Implements IProvisionContext.AktuellesDatum
            Get
                Return DateTime.Now
            End Get
        End Property

        Public Function IstFreitagNach15Uhr() As Boolean Implements IProvisionContext.IstFreitagNach15Uhr
            Dim now = AktuellesDatum
            Return now.DayOfWeek = DayOfWeek.Friday AndAlso now.Hour >= 15
        End Function

    End Class

    ''' <summary>
    ''' Für Tests: Feste Zeitkonfiguration.
    ''' </summary>
    Public Class FesterProvisionContext
        Implements IProvisionContext

        Private ReadOnly _festesDatum As DateTime
        Private ReadOnly _freitagNach15Uhr As Boolean

        Public Sub New(festesDatum As DateTime, Optional freitagNach15Uhr As Boolean = False)
            _festesDatum = festesDatum
            _freitagNach15Uhr = freitagNach15Uhr
        End Sub

        Public ReadOnly Property AktuellesDatum As Date Implements IProvisionContext.AktuellesDatum
            Get
                Return _festesDatum
            End Get
        End Property

        Public Function IstFreitagNach15Uhr() As Boolean Implements IProvisionContext.IstFreitagNach15Uhr
            Return _freitagNach15Uhr
        End Function

    End Class

End Namespace
