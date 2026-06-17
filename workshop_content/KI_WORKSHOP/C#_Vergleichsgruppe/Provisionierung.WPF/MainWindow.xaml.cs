using Provisionierung.Domain;
using System.Windows;
using System.Windows.Input;

namespace Provisionierung.WPF;

public partial class MainWindow : Window
{
    private readonly ProvisionsRechner _rechner;

    public MainWindow()
    {
        InitializeComponent();
        _rechner = new ProvisionsRechner();
    }

    private void OnPreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        // Erlaube nur Zahlen, Komma und Punkt
        e.Handled = !char.IsDigit(e.Text, 0) && e.Text != "," && e.Text != "." && e.Text != "-";
    }

    private void OnBerechnenClick(object sender, RoutedEventArgs e)
    {
        try
        {
            lblStatus.Text = "Berechne...";
            btnBerechnen.IsEnabled = false;

            if (!decimal.TryParse(txtUmsatz.Text.Replace(".", ","), out decimal umsatz))
            {
                ShowError("Bitte gültigen Umsatz eingeben!");
                return;
            }

            var kategorie = cmbKategorie.SelectedIndex switch
            {
                0 => Kategorie.A,
                1 => Kategorie.B,
                2 => Kategorie.C,
                _ => Kategorie.A
            };

            var eingabe = new ProvisionsEingabe(
                umsatz,
                kategorie,
                chkVIP.IsChecked ?? false,
                chkJubilaeum.IsChecked ?? false
            );

            var ergebnis = _rechner.Berechne(eingabe);

            // Ergebnis anzeigen
            lblErgebnis.Text = $"{ergebnis.EndProvision:C}";
            
            // Details
            var details = $"Basisprovision: {ergebnis.BasisProvision:C}\n" +
                         $"Zuschläge: {ergebnis.Zuschlaege:C}\n" +
                         $"Angewandter Satz: {ergebnis.AngewandterSatz:P1}";
            
            if (ergebnis.AngewandteBoni.Count > 0)
            {
                details += "\n\nAktive Boni:\n" + string.Join("\n", 
                    ergebnis.AngewandteBoni.Select(b => "✓ " + b));
            }
            
            lblDetails.Text = details;
            lblStatus.Text = $"✅ Berechnung erfolgreich | {DateTime.Now:T}";
        }
        catch (Exception ex)
        {
            ShowError($"Fehler: {ex.Message}");
        }
        finally
        {
            btnBerechnen.IsEnabled = true;
        }
    }

    private void ShowError(string message)
    {
        lblErgebnis.Text = "❌ Fehler";
        lblDetails.Text = message;
        lblStatus.Text = "Berechnung fehlgeschlagen";
        MessageBox.Show(message, "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
    }
}
