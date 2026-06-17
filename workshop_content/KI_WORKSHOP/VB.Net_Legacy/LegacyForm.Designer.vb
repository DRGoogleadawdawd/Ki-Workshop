<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class LegacyForm
    Inherits System.Windows.Forms.Form

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private components As System.ComponentModel.IContainer

    Friend WithEvents txtUmsatz As System.Windows.Forms.TextBox
    Friend WithEvents cmbKategorie As System.Windows.Forms.ComboBox
    Friend WithEvents chkVIP As System.Windows.Forms.CheckBox
    Friend WithEvents chkJubilaeum As System.Windows.Forms.CheckBox
    Friend WithEvents btnBerechnen As System.Windows.Forms.Button
    Friend WithEvents lblErgebnis As System.Windows.Forms.Label
    Friend WithEvents prgFortschritt As System.Windows.Forms.ProgressBar
    Friend WithEvents lblStatus As System.Windows.Forms.Label


    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.txtUmsatz = New System.Windows.Forms.TextBox()
        Me.cmbKategorie = New System.Windows.Forms.ComboBox()
        Me.chkVIP = New System.Windows.Forms.CheckBox()
        Me.chkJubilaeum = New System.Windows.Forms.CheckBox()
        Me.btnBerechnen = New System.Windows.Forms.Button()
        Me.lblErgebnis = New System.Windows.Forms.Label()
        Me.prgFortschritt = New System.Windows.Forms.ProgressBar()
        Me.lblStatus = New System.Windows.Forms.Label()
        Dim lblUmsatz As New System.Windows.Forms.Label()
        Dim lblKat As New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'lblUmsatz
        '
        lblUmsatz.Text = "Umsatz (EUR):"
        lblUmsatz.Location = New System.Drawing.Point(20, 20)
        lblUmsatz.AutoSize = True
        '
        'txtUmsatz
        '
        Me.txtUmsatz.Location = New System.Drawing.Point(150, 18)
        Me.txtUmsatz.Width = 150
        '
        'lblKat
        '
        lblKat.Text = "Kategorie:"
        lblKat.Location = New System.Drawing.Point(20, 55)
        lblKat.AutoSize = True
        '
        'cmbKategorie
        '
        Me.cmbKategorie.Location = New System.Drawing.Point(150, 52)
        Me.cmbKategorie.Width = 150
        Me.cmbKategorie.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbKategorie.Items.AddRange(New Object() {"A", "B", "C"})
        Me.cmbKategorie.SelectedIndex = 0
        '
        'chkVIP
        '
        Me.chkVIP.Text = "VIP-Kunde"
        Me.chkVIP.Location = New System.Drawing.Point(20, 90)
        Me.chkVIP.AutoSize = True
        '
        'chkJubilaeum
        '
        Me.chkJubilaeum.Text = "Jubiläumsaktion"
        Me.chkJubilaeum.Location = New System.Drawing.Point(150, 90)
        Me.chkJubilaeum.AutoSize = True
        '
        'btnBerechnen
        '
        Me.btnBerechnen.Text = "Berechnen"
        Me.btnBerechnen.Location = New System.Drawing.Point(20, 130)
        Me.btnBerechnen.Width = 120
        '
        'lblErgebnis
        '
        Me.lblErgebnis.Text = "Provision: ---"
        Me.lblErgebnis.Location = New System.Drawing.Point(20, 170)
        Me.lblErgebnis.AutoSize = True
        Me.lblErgebnis.Font = New System.Drawing.Font("Microsoft Sans Serif", 10, System.Drawing.FontStyle.Bold)
        '
        'prgFortschritt
        '
        Me.prgFortschritt.Location = New System.Drawing.Point(20, 210)
        Me.prgFortschritt.Width = 350
        Me.prgFortschritt.Maximum = 100
        '
        'lblStatus
        '
        Me.lblStatus.Text = ""
        Me.lblStatus.Location = New System.Drawing.Point(20, 240)
        Me.lblStatus.AutoSize = True
        '
        'LegacyForm
        '
        Me.Text = "Provisionsberechnung v2.3 (2008)"
        Me.Size = New System.Drawing.Size(450, 400)
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Controls.AddRange(New System.Windows.Forms.Control() {lblUmsatz, Me.txtUmsatz, lblKat, Me.cmbKategorie, Me.chkVIP, Me.chkJubilaeum, Me.btnBerechnen, Me.lblErgebnis, Me.prgFortschritt, Me.lblStatus})
        Me.ResumeLayout(False)
        Me.PerformLayout()
    End Sub
End Class