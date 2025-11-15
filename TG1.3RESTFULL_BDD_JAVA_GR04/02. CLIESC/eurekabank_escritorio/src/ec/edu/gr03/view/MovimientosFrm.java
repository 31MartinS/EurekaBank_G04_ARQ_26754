package ec.edu.gr03.view;

import java.awt.BorderLayout;
import java.awt.Color;
import java.awt.Cursor;
import java.awt.Dimension;
import java.awt.EventQueue;
import java.awt.Font;
import java.awt.GridBagConstraints;
import java.awt.GridBagLayout;
import java.awt.Image;
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

public class MovimientosFrm extends javax.swing.JFrame {

    // --- Variables de componentes ---
    private JButton btnVerMovimientos;
    private JTextField txtCuenta;

    // Botones de navegación
    private JButton btnMovimientos;
    private JButton btnRetiro;
    private JButton btnDeposito;
    private JButton btnTransferencia;

    // Colores del diseño
    private final Color COLOR_PANEL_IZQUIERDO = Color.WHITE;
    private final Color COLOR_GRIS_FONDO = new Color(245, 245, 245); // Fondo panel derecho
    private final Color COLOR_HINT = Color.GRAY;

    // Colores para los botones
    private final Color COLOR_MOVIMIENTOS = new Color(52, 152, 219); // Azul
    private final Color COLOR_RETIRO = new Color(231, 76, 60); // Rojo
    private final Color COLOR_DEPOSITO = new Color(39, 174, 96); // Verde oscuro
    private final Color COLOR_TRANSFERENCIA = new Color(241, 196, 15); // Amarillo

    public MovimientosFrm() {
        // --- Configuración del JFrame ---
        setTitle("EurekaBank - Movimientos");
        setDefaultCloseOperation(javax.swing.WindowConstants.EXIT_ON_CLOSE);
        setMinimumSize(new Dimension(800, 600));
        setLocationRelativeTo(null);
        getContentPane().setLayout(new BorderLayout());

        // --- Panel izquierdo (navegación) ---
        JPanel pnlLeft = new JPanel(new GridBagLayout());
        pnlLeft.setBackground(COLOR_PANEL_IZQUIERDO);
        pnlLeft.setPreferredSize(new Dimension(200, 600));

        GridBagConstraints gbcLeft = new GridBagConstraints();
        gbcLeft.gridx = 0;
        gbcLeft.fill = GridBagConstraints.HORIZONTAL;
        gbcLeft.insets = new Insets(10, 10, 10, 10);
        gbcLeft.weightx = 1;

        // --- Botones de navegación ---
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

        // Título
        gbcForm.gridy = 0;
        gbcForm.insets = new Insets(0, 5, 25, 5);
        JLabel lblTitle = new JLabel("CONSULTAR MOVIMIENTOS");
        lblTitle.setFont(new Font("Segoe UI", Font.BOLD, 24));
        lblTitle.setForeground(Color.BLACK);
        lblTitle.setHorizontalAlignment(SwingConstants.CENTER);
        pnlForm.add(lblTitle, gbcForm);

        gbcForm.insets = new Insets(10, 5, 10, 5);

        // Campo número de cuenta
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

        // Botón Ver Movimientos
        gbcForm.gridy = 3;
        gbcForm.insets = new Insets(30, 5, 10, 5);
        btnVerMovimientos = new JButton("Ver Movimientos");
        btnVerMovimientos.setFont(new Font("Segoe UI", Font.BOLD, 16));
        btnVerMovimientos.setBackground(COLOR_MOVIMIENTOS);
        btnVerMovimientos.setForeground(Color.WHITE);
        btnVerMovimientos.setFocusPainted(false);
        btnVerMovimientos.setCursor(new Cursor(Cursor.HAND_CURSOR));
        btnVerMovimientos.setBorder(BorderFactory.createEmptyBorder(12, 12, 12, 12));
        pnlForm.add(btnVerMovimientos, gbcForm);

        pnlRight.add(pnlForm, new GridBagConstraints());

        // --- Listeners ---
        btnVerMovimientos.addActionListener(e -> btnVerMovimientosActionPerformed());
        btnMovimientos.addActionListener(e -> {});
        btnRetiro.addActionListener(e -> irARetiro());
        btnDeposito.addActionListener(e -> irADeposito());
        btnTransferencia.addActionListener(e -> irATransferencia());

        // --- Ensamblaje final ---
        getContentPane().add(pnlLeft, BorderLayout.WEST);
        getContentPane().add(pnlRight, BorderLayout.CENTER);

        pack();
    }

    // Método para crear botones de navegación con color específico
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

    // Borde inferior para JTextField
    private Border createFieldBorder() {
        return BorderFactory.createCompoundBorder(
                new MatteBorder(0, 0, 2, 0, Color.GRAY),
                BorderFactory.createEmptyBorder(5, 5, 5, 5)
        );
    }

    // Placeholder
    private void addPlaceholder(JTextField field, String placeholder) {
        field.setText(placeholder);
        field.setForeground(COLOR_HINT);
        field.addFocusListener(new FocusAdapter() {
            @Override
            public void focusGained(java.awt.event.FocusEvent e) {
                if (field.getText().equals(placeholder)) {
                    field.setText("");
                    field.setForeground(Color.BLACK);
                }
            }
            @Override
            public void focusLost(java.awt.event.FocusEvent e) {
                if (field.getText().isEmpty()) {
                    field.setText(placeholder);
                    field.setForeground(COLOR_HINT);
                }
            }
        });
    }

    // Navegación
    private void irARetiro() {
        RetiroFrm retiroFrm = new RetiroFrm();
        retiroFrm.setVisible(true);
        this.dispose();
    }

    private void irADeposito() {
        DepositoFrm depositoFrm = new DepositoFrm();
        depositoFrm.setVisible(true);
        this.dispose();
    }

    private void irATransferencia() {
        TransferenciasFrm transFrm = new TransferenciasFrm();
        transFrm.setVisible(true);
        this.dispose();
    }

    // Botón Ver Movimientos
    private void btnVerMovimientosActionPerformed() {
        String numeroCuenta = txtCuenta.getText().trim();
        if (numeroCuenta.equals("Ej: 000123456")) numeroCuenta = "";

        if (numeroCuenta.isEmpty()) {
            JOptionPane.showMessageDialog(this, "Debe ingresar un número de cuenta.", "Error", JOptionPane.ERROR_MESSAGE);
            return;
        }

        MovimientosTablaFrm movimientosFrm = new MovimientosTablaFrm(numeroCuenta);
        movimientosFrm.setVisible(true);
    }

    public static void main(String args[]) {
        EventQueue.invokeLater(() -> new MovimientosFrm().setVisible(true));
    }
}
