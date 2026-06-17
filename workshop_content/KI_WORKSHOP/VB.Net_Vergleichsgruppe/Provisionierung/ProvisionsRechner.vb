Imports Provisionierung.Domain

''' <summary>
''' Aufgabe: Implementiere die Provisionsberechnung basierend auf den Tests und dem Legacy-Code.
''' 
''' Analyse:
''' - Die Testfälle definieren das erwartete Verhalten
''' - Der Legacy-Code (siehe Projekt-Root) enthält die originale Berechnungslogik
''' 
''' Anforderungen:
''' - Keine UI-Abhängigkeiten (kein MessageBox, kein DoEvents)
''' - Nutze das IProvisionContext-Interface für zeitabhängige Logik
''' - Alle magischen Zahlen als Konstanten definieren
''' - Modernes Error-Handling statt On Error Resume Next
''' 
''' Hinweis: Du darfst das Domänenmodell anpassen wenn nötig.
''' </summary>
Public Class ProvisionsRechner

    ' Definiere hier deine Konstanten

    Private ReadOnly _context As IProvisionContext

    Public Sub New(Optional context As IProvisionContext = Nothing)
        _context = If(context, New SystemProvisionContext())
    End Sub

    Public Function Berechne(eingabe As ProvisionsEingabe) As ProvisionsErgebnis
        Throw New NotImplementedException("Implementiere die Berechnungslogik basierend auf den Tests.")
    End Function

End Class
