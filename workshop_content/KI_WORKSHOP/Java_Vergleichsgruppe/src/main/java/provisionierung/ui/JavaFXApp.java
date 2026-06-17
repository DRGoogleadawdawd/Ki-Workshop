package provisionierung.ui;

import javafx.application.Application;
import javafx.geometry.Insets;
import javafx.geometry.Pos;
import javafx.scene.Scene;
import javafx.scene.control.*;
import javafx.scene.layout.*;
import javafx.scene.paint.Color;
import javafx.scene.text.Font;
import javafx.scene.text.FontWeight;
import javafx.stage.Stage;
import provisionierung.ProvisionsRechner;
import provisionierung.domain.*;

import java.math.BigDecimal;

/**
 * JavaFX UI für die Provisionsberechnung.
 * Moderne Alternative zum alten VB.Net-Formular.
 */
public class JavaFXApp extends Application {

    private ProvisionsRechner rechner;
    private TextField txtUmsatz;
    private ComboBox<String> cmbKategorie;
    private CheckBox chkVIP;
    private CheckBox chkJubilaeum;
    private Label lblErgebnis;
    private Label lblDetails;
    private Label lblStatus;

    @Override
    public void start(Stage primaryStage) {
        rechner = new ProvisionsRechner();

        primaryStage.setTitle("Provisionsrechner - Legacy Migration");

        // Root Layout
        VBox root = new VBox(15);
        root.setPadding(new Insets(20));
        root.setStyle("-fx-background-color: #F5F5F5;");

        // Header
        Label lblHeader = new Label("📊 Provisionsberechnung");
        lblHeader.setFont(Font.font("System", FontWeight.BOLD, 24));
        lblHeader.setTextFill(Color.web("#333"));

        // Umsatz
        Label lblUmsatz = new Label("Umsatz (€)");
        lblUmsatz.setFont(Font.font("System", FontWeight.SEMI_BOLD, 12));
        txtUmsatz = new TextField("1000");
        txtUmsatz.setPromptText("Umsatz eingeben...");

        // Kategorie
        Label lblKategorie = new Label("Kategorie");
        lblKategorie.setFont(Font.font("System", FontWeight.SEMI_BOLD, 12));
        cmbKategorie = new ComboBox<>();
        cmbKategorie.getItems().addAll(
                "A - Premium (10%)",
                "B - Standard (7%)",
                "C - Basis (5%)"
        );
        cmbKategorie.setValue("A - Premium (10%)");
        cmbKategorie.setMaxWidth(Double.MAX_VALUE);

        // Optionen
        chkVIP = new CheckBox("VIP-Kunde (+2% Provision + 50€ Pauschale)");
        chkJubilaeum = new CheckBox("Jubiläumsaktion (+5% Provision, nur ab 5000€)");

        // Berechnen Button
        Button btnBerechnen = new Button("🚀 Berechnen");
        btnBerechnen.setStyle(
            "-fx-background-color: #007ACC;" +
            "-fx-text-fill: white;" +
            "-fx-font-size: 14px;" +
            "-fx-padding: 12 24;" +
            "-fx-cursor: hand;"
        );
        btnBerechnen.setOnMouseEntered(e -> btnBerechnen.setStyle(
            "-fx-background-color: #005A9E;" +
            "-fx-text-fill: white;" +
            "-fx-font-size: 14px;" +
            "-fx-padding: 12 24;" +
            "-fx-cursor: hand;"
        ));
        btnBerechnen.setOnMouseExited(e -> btnBerechnen.setStyle(
            "-fx-background-color: #007ACC;" +
            "-fx-text-fill: white;" +
            "-fx-font-size: 14px;" +
            "-fx-padding: 12 24;" +
            "-fx-cursor: hand;"
        ));
        btnBerechnen.setOnAction(e -> berechne());

        // Ergebnis Panel
        VBox ergebnisPanel = new VBox(10);
        ergebnisPanel.setPadding(new Insets(20));
        ergebnisPanel.setStyle(
            "-fx-background-color: #E3F2FD;" +
            "-fx-border-color: #2196F3;" +
            "-fx-border-width: 2;" +
            "-fx-border-radius: 8;" +
            "-fx-background-radius: 8;"
        );
        ergebnisPanel.setAlignment(Pos.CENTER);

        Label lblErgebnisTitle = new Label("Ihre Provision");
        lblErgebnisTitle.setFont(Font.font("System", 14));
        lblErgebnisTitle.setTextFill(Color.web("#666"));

        lblErgebnis = new Label("--");
        lblErgebnis.setFont(Font.font("System", FontWeight.BOLD, 36));
        lblErgebnis.setTextFill(Color.web("#1976D2"));

        lblDetails = new Label("Geben Sie einen Umsatz ein und klicken Sie auf Berechnen");
        lblDetails.setFont(Font.font("System", 12));
        lblDetails.setTextFill(Color.web("#888"));
        lblDetails.setWrapText(true);

        ergebnisPanel.getChildren().addAll(lblErgebnisTitle, lblErgebnis, lblDetails);

        // Status
        lblStatus = new Label("Bereit");
        lblStatus.setFont(Font.font("System", 11));
        lblStatus.setTextFill(Color.web("#999"));

        // Alles zusammenfügen
        root.getChildren().addAll(
                lblHeader,
                new VBox(5, lblUmsatz, txtUmsatz),
                new VBox(5, lblKategorie, cmbKategorie),
                new VBox(5, chkVIP, chkJubilaeum),
                btnBerechnen,
                ergebnisPanel,
                lblStatus
        );
        VBox.setVgrow(ergebnisPanel, Priority.ALWAYS);

        Scene scene = new Scene(root, 500, 550);
        primaryStage.setScene(scene);
        primaryStage.setResizable(false);
        primaryStage.show();
    }

    private void berechne() {
        try {
            lblStatus.setText("Berechne...");

            String umsatzText = txtUmsatz.getText().replace(".", ",").replace(",", ".");
            BigDecimal umsatz;
            try {
                umsatz = new BigDecimal(umsatzText);
            } catch (NumberFormatException e) {
                showError("Bitte gültigen Umsatz eingeben!");
                return;
            }

            Kategorie kategorie = switch (cmbKategorie.getSelectionModel().getSelectedIndex()) {
                case 0 -> Kategorie.A;
                case 1 -> Kategorie.B;
                case 2 -> Kategorie.C;
                default -> Kategorie.A;
            };

            ProvisionsEingabe eingabe = new ProvisionsEingabe(
                    umsatz,
                    kategorie,
                    chkVIP.isSelected(),
                    chkJubilaeum.isSelected()
            );

            ProvisionsErgebnis ergebnis = rechner.berechne(eingabe);

            // Formatierung
            lblErgebnis.setText(String.format("%,.2f €", ergebnis.getEndProvision()));

            String details = String.format(
                "Basisprovision: %,.2f €%nZuschläge: %,.2f €%nAngewandter Satz: %.1f%%",
                ergebnis.getBasisProvision(),
                ergebnis.getZuschlaege(),
                ergebnis.getAngewandterSatz() * 100
            );

            if (!ergebnis.getAngewandteBoni().isEmpty()) {
                details += "\n\nAktive Boni:\n" + String.join("\n", ergebnis.getAngewandteBoni());
            }

            lblDetails.setText(details);
            lblStatus.setText("✅ Berechnung erfolgreich");

        } catch (Exception e) {
            showError("Fehler: " + e.getMessage());
        }
    }

    private void showError(String message) {
        lblErgebnis.setText("❌ Fehler");
        lblDetails.setText(message);
        lblStatus.setText("Berechnung fehlgeschlagen");

        Alert alert = new Alert(Alert.AlertType.ERROR);
        alert.setTitle("Fehler");
        alert.setHeaderText(null);
        alert.setContentText(message);
        alert.showAndWait();
    }

    public static void main(String[] args) {
        launch(args);
    }
}
