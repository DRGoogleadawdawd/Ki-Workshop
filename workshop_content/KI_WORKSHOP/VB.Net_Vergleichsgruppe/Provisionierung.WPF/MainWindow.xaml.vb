Imports Provisionierung
Imports Provisionierung.Domain

Class MainWindow

    Private _rechner As ProvisionsRechner

    Public Sub New()
        InitializeComponent()
        _rechner = New ProvisionsRechner()
    End Sub

    Private Sub OnBerechnenClick(sender As Object, e As RoutedEventArgs)
        Try
            lblStatus.Text = "Berechne..."
            btnBerechnen.IsEnabled = False

            Dim umsatzText As String = txtUmsatz.Text.Replace(".", ",").Replace(",", ".")
            Dim umsatz As Decimal

            If Not Decimal.TryParse(umsatzText, umsatz) Then
                ShowError("Bitte gültigen Umsatz eingeben!")
                Return
            End If

            Dim kategorie As Kategorie = Select Case cmbKategorie.SelectedIndex
                Case 0
                    Kategorie.A
                Case 1
                    Kategorie.B
                Case 2
                    Kategorie.C
                Case Else
                    Kategorie.A
            End Select

            Dim eingabe As New ProvisionsEingabe(
                umsatz,
                kategorie,
                chkVIP.IsChecked.GetValueOrDefault(),
                chkJubilaeum.IsChecked.GetValueOrDefault()
            )

            Dim ergebnis As ProvisionsErgebnis = _rechner.Berechne(eingabe)

            ' Ergebnis anzeigen
            lblErgebnis.Text = $"{ergebnis.EndProvision:C}"

            Dim details As String = $"Basisprovision: {ergebnis.BasisProvision:C}" & vbCrLf &
                                   $"Zuschläge: {ergebnis.Zuschlaege:C}" & vbCrLf &
                                   $"Angewandter Satz: {ergebnis.AngewandterSatz:P1}"

            If ergebnis.AngewandteBoni.Count > 0 Then
                details &= vbCrLf & vbCrLf & "Aktive Boni:" & vbCrLf &
                          String.Join(vbCrLf, ergebnis.AngewandteBoni.Select(Function(b) "✓ " & b))
            End If

            lblDetails.Text = details
            lblStatus.Text = $"✅ Berechnung erfolgreich | {DateTime.Now:T}"

        Catch ex As Exception
            ShowError($"Fehler: {ex.Message}")
        Finally
            btnBerechnen.IsEnabled = True
        End Try
    End Sub

    Private Sub ShowError(message As String)
        lblErgebnis.Text = "❌ Fehler"
        lblDetails.Text = message
        lblStatus.Text = "Berechnung fehlgeschlagen"
        MessageBox.Show(message, "Fehler", MessageBoxButton.OK, MessageBoxImage.Error)
    End Sub

End Class
