package ec.edu.gr03.view;

import java.awt.*;
import java.awt.event.FocusAdapter;
import java.awt.event.FocusEvent;
import javax.swing.*;
import javax.swing.border.Border;
import javax.swing.border.MatteBorder;

public class TransferenciasFrm extends JFrame {

    private JButton btnTransferir;
    private JTextField txtCuentaDestino;
    private JTextField txtCuentaOrigen;
    private JTextField txtValor;

    private JButton btnMovimientos;
    private JButton btnRetiro;
    private JButton btnDeposito;
    private JButton btnTransferencia;

    private final Color COLOR_PANEL_IZQUIERDO = Color.WHITE;
    private final Color COLOR_GRIS_FONDO = new Color(245, 245, 245);
    private final Color COLOR_HINT = Color.GRAY;

    private final Color COLOR_MOVIMIENTOS = new Color(52, 152, 219); // Azul
    private final Color COLOR_RETIRO = new Color(231, 76, 60);       // Rojo
    private final Color COLOR_DEPOSITO = new Color(39, 174, 96);     // Verde
    private final Color COLOR_TRANSFERENCIA = new Color(241, 196, 15); // Amarillo

    public TransferenciasFrm() {
        setTitle("EurekaBank - Transferencias");
        setDefaultCloseOperation(WindowConstants.EXIT_ON_CLOSE);
        setMinimumSize(new Dimension(800, 600));
        setLocationRelativeTo(null);
        getContentPane().setLayout(new BorderLayout());

        // --- Panel izquierdo ---
        JPanel pnlLeft = new JPanel(new GridBagLayout());
        pnlLeft.setBackground(COLOR_PANEL_IZQUIERDO);
        pnlLeft.setPreferredSize(new Dimension(200, 600));

        GridBagConstraints gbcLeft = new GridBagConstraints();
        gbcLeft.gridx = 0;
        gbcLeft.fill = GridBagConstraints.HORIZONTAL;
        gbcLeft.insets = new Insets(10, 10, 10, 10);
        gbcLeft.weightx = 1;

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

        // --- Panel derecho (formulario) ---
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

        gbcForm.gridy = 0;
        gbcForm.insets = new Insets(0, 5, 25, 5);
        JLabel lblTitle = new JLabel("REALIZAR TRANSFERENCIA");
        lblTitle.setFont(new Font("Segoe UI", Font.BOLD, 24));
        lblTitle.setForeground(Color.BLACK);
        lblTitle.setHorizontalAlignment(SwingConstants.CENTER);
        pnlForm.add(lblTitle, gbcForm);

        gbcForm.insets = new Insets(10, 5, 10, 5);

        // Cuenta origen
        gbcForm.gridy = 1;
        JLabel lblCuentaOrigen = new JLabel("Cuenta Origen");
        lblCuentaOrigen.setFont(new Font("Segoe UI", Font.BOLD, 14));
        pnlForm.add(lblCuentaOrigen, gbcForm);

        gbcForm.gridy = 2;
        txtCuentaOrigen = new JTextField(20);
        txtCuentaOrigen.setFont(new Font("Segoe UI", Font.PLAIN, 16));
        txtCuentaOrigen.setBorder(createFieldBorder());
        txtCuentaOrigen.setBackground(pnlForm.getBackground());
        addPlaceholder(txtCuentaOrigen, "Ej: 000123456");
        pnlForm.add(txtCuentaOrigen, gbcForm);

        // Cuenta destino
        gbcForm.gridy = 3;
        JLabel lblCuentaDestino = new JLabel("Cuenta Destino");
        lblCuentaDestino.setFont(new Font("Segoe UI", Font.BOLD, 14));
        pnlForm.add(lblCuentaDestino, gbcForm);

        gbcForm.gridy = 4;
        txtCuentaDestino = new JTextField(20);
        txtCuentaDestino.setFont(new Font("Segoe UI", Font.PLAIN, 16));
        txtCuentaDestino.setBorder(createFieldBorder());
        txtCuentaDestino.setBackground(pnlForm.getBackground());
        addPlaceholder(txtCuentaDestino, "Ej: 000987654");
        pnlForm.add(txtCuentaDestino, gbcForm);

        // Importe
        gbcForm.gridy = 5;
        JLabel lblValor = new JLabel("Importe ($)");
        lblValor.setFont(new Font("Segoe UI", Font.BOLD, 14));
        pnlForm.add(lblValor, gbcForm);

        gbcForm.gridy = 6;
        txtValor = new JTextField(20);
        txtValor.setFont(new Font("Segoe UI", Font.PLAIN, 16));
        txtValor.setBorder(createFieldBorder());
        txtValor.setBackground(pnlForm.getBackground());
        addPlaceholder(txtValor, "Ej: 100.00");
        pnlForm.add(txtValor, gbcForm);

        // Botón Transferir
        gbcForm.gridy = 7;
        gbcForm.insets = new Insets(30, 5, 10, 5);
        btnTransferir = new JButton("Transferir");
        btnTransferir.setFont(new Font("Segoe UI", Font.BOLD, 16));
        btnTransferir.setBackground(COLOR_TRANSFERENCIA);
        btnTransferir.setForeground(Color.WHITE);
        btnTransferir.setFocusPainted(false);
        btnTransferir.setCursor(new Cursor(Cursor.HAND_CURSOR));
        btnTransferir.setBorder(BorderFactory.createEmptyBorder(12, 12, 12, 12));
        pnlForm.add(btnTransferir, gbcForm);

        pnlRight.add(pnlForm, new GridBagConstraints());

        // Listeners
        btnTransferir.addActionListener(e -> transferir());
        btnMovimientos.addActionListener(e -> irAMovimientos());
        btnRetiro.addActionListener(e -> irARetiro());
        btnDeposito.addActionListener(e -> irADeposito());
        btnTransferencia.addActionListener(e -> {});

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
    private void irAMovimientos() { new MovimientosFrm().setVisible(true); dispose(); }
    private void irARetiro() { new RetiroFrm().setVisible(true); dispose(); }
    private void irADeposito() { new DepositoFrm().setVisible(true); dispose(); }

    // Lógica de transferencia
    private void transferir() {
        String cuentaOrigen = txtCuentaOrigen.getText().trim();
        String cuentaDestino = txtCuentaDestino.getText().trim();
        String textoImporte = txtValor.getText().trim();

        if (cuentaOrigen.equals("Ej: 000123456")) cuentaOrigen = "";
        if (cuentaDestino.equals("Ej: 000987654")) cuentaDestino = "";
        if (textoImporte.equals("Ej: 100.00")) textoImporte = "";

        try {
            double importe = Double.parseDouble(textoImporte);

            if (cuentaOrigen.isEmpty() || cuentaDestino.isEmpty()) {
                JOptionPane.showMessageDialog(this, "Debe ingresar ambas cuentas.", "Error", JOptionPane.ERROR_MESSAGE);
                return;
            }
            if (cuentaOrigen.equals(cuentaDestino)) {
                JOptionPane.showMessageDialog(this, "La cuenta origen y destino no pueden ser iguales.", "Error", JOptionPane.ERROR_MESSAGE);
                return;
            }
            if (importe <= 0) {
                JOptionPane.showMessageDialog(this, "El importe debe ser mayor que cero.", "Error", JOptionPane.ERROR_MESSAGE);
                return;
            }

            int resultado = ec.edu.gr03.controller.EurekaBankClient.regTransferencia(cuentaOrigen, cuentaDestino, importe);

            if (resultado == 1) {
                JOptionPane.showMessageDialog(this, "Transferencia realizada con éxito.", "Éxito", JOptionPane.INFORMATION_MESSAGE);
                addPlaceholder(txtCuentaOrigen, "Ej: 000123456");
                addPlaceholder(txtCuentaDestino, "Ej: 000987654");
                addPlaceholder(txtValor, "Ej: 100.00");
            } else {
                JOptionPane.showMessageDialog(this, "No se pudo realizar la transferencia.", "Error", JOptionPane.ERROR_MESSAGE);
            }

        } catch (NumberFormatException ex) {
            JOptionPane.showMessageDialog(this, "El importe debe ser un número válido.", "Error", JOptionPane.ERROR_MESSAGE);
        }
    }

    public static void main(String[] args) {
        EventQueue.invokeLater(() -> new TransferenciasFrm().setVisible(true));
    }
}
