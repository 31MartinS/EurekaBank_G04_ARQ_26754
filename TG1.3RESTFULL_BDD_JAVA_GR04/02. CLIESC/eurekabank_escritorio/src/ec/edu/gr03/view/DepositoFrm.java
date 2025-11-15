package ec.edu.gr03.view;

import java.awt.BorderLayout;
import java.awt.Color;
import java.awt.Cursor;
import java.awt.Dimension;
import java.awt.EventQueue;
import java.awt.Font;
import java.awt.GridBagConstraints;
import java.awt.GridBagLayout;
import java.awt.Insets;
import java.awt.event.FocusAdapter;
import java.awt.event.FocusEvent;
import javax.swing.BorderFactory;
import javax.swing.JButton;
import javax.swing.JLabel;
import javax.swing.JOptionPane;
import javax.swing.JPanel;
import javax.swing.JTextField;
import javax.swing.SwingConstants;
import javax.swing.border.Border;
import javax.swing.border.MatteBorder;

public class DepositoFrm extends javax.swing.JFrame {

    private JButton btnDepositar;
    private JTextField txtCuenta;
    private JTextField txtValor;

    // Botones de navegación
    private JButton btnMovimientos;
    private JButton btnRetiro;
    private JButton btnDeposito;
    private JButton btnTransferencia;

    // Colores de diseño
    private final Color COLOR_PANEL_IZQUIERDO = Color.WHITE;
    private final Color COLOR_GRIS_FONDO = new Color(245, 245, 245);
    private final Color COLOR_HINT = Color.GRAY;

    // Colores botones
    private final Color COLOR_MOVIMIENTOS = new Color(52, 152, 219); // Azul
    private final Color COLOR_RETIRO = new Color(231, 76, 60); // Rojo oscuro
    private final Color COLOR_DEPOSITO = new Color(39, 174, 96); // Verde
    private final Color COLOR_TRANSFERENCIA = new Color(241, 196, 15); // Amarillo

    public DepositoFrm() {
        setTitle("EurekaBank - Depósitos");
        setDefaultCloseOperation(javax.swing.WindowConstants.EXIT_ON_CLOSE);
        setMinimumSize(new Dimension(800, 600));
        setLocationRelativeTo(null);
        getContentPane().setLayout(new BorderLayout());

        // Panel izquierdo
        JPanel pnlLeft = new JPanel(new GridBagLayout());
        pnlLeft.setBackground(COLOR_PANEL_IZQUIERDO);
        pnlLeft.setPreferredSize(new Dimension(200, 600));

        GridBagConstraints gbcLeft = new GridBagConstraints();
        gbcLeft.gridx = 0;
        gbcLeft.fill = GridBagConstraints.HORIZONTAL;
        gbcLeft.insets = new Insets(10, 10, 10, 10);
        gbcLeft.weightx = 1;

        // Botones de navegación
        gbcLeft.gridy = 0;
        btnMovimientos = createNavButton("Movimientos", COLOR_MOVIMIENTOS);
        pnlLeft.add(btnMovimientos, gbcLeft);

        gbcLeft.gridy = 1;
        btnRetiro = createNavButton("Retiro", COLOR_RETIRO);
        pnlLeft.add(btnRetiro, gbcLeft);

        gbcLeft.gridy = 2;
        btnDeposito = createNavButton("Depósito", COLOR_DEPOSITO);
        pnlLeft.add(btnDeposito, gbcLeft);

        gbcLeft.gridy = 3;
        btnTransferencia = createNavButton("Transferencia", COLOR_TRANSFERENCIA);
        pnlLeft.add(btnTransferencia, gbcLeft);

        // Panel derecho
        JPanel pnlRight = new JPanel(new GridBagLayout());
        pnlRight.setBackground(COLOR_GRIS_FONDO);

        JPanel pnlForm = new JPanel(new GridBagLayout());
        pnlForm.setBackground(Color.WHITE);
        pnlForm.setBorder(BorderFactory.createCompoundBorder(
                BorderFactory.createLineBorder(Color.LIGHT_GRAY, 1),
                BorderFactory.createEmptyBorder(40, 40, 40, 40)
        ));

        GridBagConstraints gbcForm = new GridBagConstraints();
        gbcForm.gridx = 0;
        gbcForm.gridwidth = GridBagConstraints.REMAINDER;
        gbcForm.fill = GridBagConstraints.HORIZONTAL;
        gbcForm.insets = new Insets(10, 5, 10, 5);

        // Título
        gbcForm.gridy = 0;
        gbcForm.insets = new Insets(0, 5, 25, 5);
        JLabel lblTitle = new JLabel("REALIZAR DEPÓSITO");
        lblTitle.setFont(new Font("Segoe UI", Font.BOLD, 24));
        lblTitle.setForeground(Color.BLACK);
        lblTitle.setHorizontalAlignment(SwingConstants.CENTER);
        pnlForm.add(lblTitle, gbcForm);

        gbcForm.insets = new Insets(10, 5, 10, 5);

        // Número de cuenta
        gbcForm.gridy = 1;
        JLabel lblCuenta = new JLabel("Número de Cuenta");
        lblCuenta.setFont(new Font("Segoe UI", Font.BOLD, 14));
        pnlForm.add(lblCuenta, gbcForm);

        gbcForm.gridy = 2;
        txtCuenta = new JTextField(20);
        txtCuenta.setFont(new Font("Segoe UI", Font.PLAIN, 16));
        txtCuenta.setBorder(createFieldBorder());
        txtCuenta.setBackground(pnlForm.getBackground());
        addPlaceholder(txtCuenta, "Ej: 000123456");
        pnlForm.add(txtCuenta, gbcForm);

        // Importe
        gbcForm.gridy = 3;
        JLabel lblValor = new JLabel("Importe ($)");
        lblValor.setFont(new Font("Segoe UI", Font.BOLD, 14));
        pnlForm.add(lblValor, gbcForm);

        gbcForm.gridy = 4;
        txtValor = new JTextField(20);
        txtValor.setFont(new Font("Segoe UI", Font.PLAIN, 16));
        txtValor.setBorder(createFieldBorder());
        txtValor.setBackground(pnlForm.getBackground());
        addPlaceholder(txtValor, "Ej: 100.00");
        pnlForm.add(txtValor, gbcForm);

        // Botón Depositar
        gbcForm.gridy = 5;
        gbcForm.insets = new Insets(30, 5, 10, 5);
        btnDepositar = new JButton("Depósito");
        btnDepositar.setFont(new Font("Segoe UI", Font.BOLD, 16));
        btnDepositar.setBackground(COLOR_DEPOSITO);
        btnDepositar.setForeground(Color.WHITE);
        btnDepositar.setFocusPainted(false);
        btnDepositar.setCursor(new Cursor(Cursor.HAND_CURSOR));
        btnDepositar.setBorder(BorderFactory.createEmptyBorder(12, 12, 12, 12));
        pnlForm.add(btnDepositar, gbcForm);

        pnlRight.add(pnlForm, new GridBagConstraints());

        // Listeners
        btnDepositar.addActionListener(e -> btnDepositarActionPerformed());
        btnMovimientos.addActionListener(e -> irAMovimientos());
        btnRetiro.addActionListener(e -> irARetiro());
        btnDeposito.addActionListener(e -> {});
        btnTransferencia.addActionListener(e -> irATransferencia());

        getContentPane().add(pnlLeft, BorderLayout.WEST);
        getContentPane().add(pnlRight, BorderLayout.CENTER);

        pack();
    }

    private JButton createNavButton(String text, Color color) {
        JButton button = new JButton(text);
        button.setFont(new Font("Segoe UI", Font.BOLD, 16));
        button.setForeground(Color.WHITE);
        button.setBackground(color);
        button.setCursor(new Cursor(Cursor.HAND_CURSOR));
        button.setFocusPainted(false);
        button.setBorder(BorderFactory.createEmptyBorder(15, 20, 15, 20));
        return button;
    }

    private Border createFieldBorder() {
        return BorderFactory.createCompoundBorder(
                new MatteBorder(0, 0, 2, 0, Color.GRAY),
                BorderFactory.createEmptyBorder(5, 5, 5, 5)
        );
    }

    private void addPlaceholder(JTextField field, String placeholder) {
        field.setText(placeholder);
        field.setForeground(COLOR_HINT);
        field.addFocusListener(new FocusAdapter() {
            @Override
            public void focusGained(FocusEvent e) {
                if (field.getText().equals(placeholder)) {
                    field.setText("");
                    field.setForeground(Color.BLACK);
                }
            }
            @Override
            public void focusLost(FocusEvent e) {
                if (field.getText().isEmpty()) {
                    field.setText(placeholder);
                    field.setForeground(COLOR_HINT);
                }
            }
        });
    }

    // Navegación
    private void irAMovimientos() {
        MovimientosFrm movFrm = new MovimientosFrm();
        movFrm.setVisible(true);
        this.dispose();
    }

    private void irARetiro() {
        RetiroFrm retiroFrm = new RetiroFrm();
        retiroFrm.setVisible(true);
        this.dispose();
    }

    private void irATransferencia() {
        TransferenciasFrm transFrm = new TransferenciasFrm();
        transFrm.setVisible(true);
        this.dispose();
    }

    private void btnDepositarActionPerformed() {
        String cuenta = txtCuenta.getText().trim();
        String textoImporte = txtValor.getText().trim();

        if (cuenta.equals("Ej: 00012519")) cuenta = "";
        if (textoImporte.equals("Ej: 200.00")) textoImporte = "";

        try {
            double importe = Double.parseDouble(textoImporte);
            if (importe <= 0) {
                JOptionPane.showMessageDialog(this, "El importe debe ser mayor que cero.", "Error", JOptionPane.ERROR_MESSAGE);
                return;
            }
            if (cuenta.isEmpty()) {
                JOptionPane.showMessageDialog(this, "Debe ingresar un número de cuenta.", "Error", JOptionPane.ERROR_MESSAGE);
                return;
            }

            int resultado = ec.edu.gr03.controller.EurekaBankClient.regDeposito(cuenta, importe);
            if (resultado == 1) {
                JOptionPane.showMessageDialog(this, "Depósito realizado con éxito.", "Éxito", JOptionPane.INFORMATION_MESSAGE);
                addPlaceholder(txtCuenta, "Ej: 000123456");
                addPlaceholder(txtValor, "Ej: 100.00");
            } else {
                JOptionPane.showMessageDialog(this, "No se pudo realizar el depósito.", "Error", JOptionPane.ERROR_MESSAGE);
            }

        } catch (NumberFormatException ex) {
            JOptionPane.showMessageDialog(this, "El importe debe ser un número válido.", "Error", JOptionPane.ERROR_MESSAGE);
        }
    }

    public static void main(String[] args) {
        EventQueue.invokeLater(() -> new DepositoFrm().setVisible(true));
    }
}
