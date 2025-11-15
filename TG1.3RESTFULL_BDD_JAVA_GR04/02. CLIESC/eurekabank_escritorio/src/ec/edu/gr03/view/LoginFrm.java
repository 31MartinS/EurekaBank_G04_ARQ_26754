package ec.edu.gr03.view;

import java.awt.BorderLayout;
import java.awt.Color;
import java.awt.Cursor;
import java.awt.Dimension;
import java.awt.EventQueue;
import java.awt.Font;
import java.awt.GradientPaint;
import java.awt.Graphics;
import java.awt.Graphics2D;
import java.awt.GridBagConstraints;
import java.awt.GridBagLayout;
import java.awt.Insets;
import java.awt.event.FocusAdapter;
import java.awt.event.FocusEvent;
import javax.swing.BorderFactory;
import javax.swing.JButton;
import javax.swing.JLabel;
import javax.swing.JPanel;
import javax.swing.JPasswordField;
import javax.swing.JTextField;
import javax.swing.SwingConstants;
import javax.swing.border.Border;
import javax.swing.border.MatteBorder;

public class LoginFrm extends javax.swing.JFrame {

    private JButton btnLogin;
    private JPasswordField txtPassword;
    private JTextField txtUsername;

    private final Color COLOR_PRINCIPAL = new Color(34, 49, 63);
  private final Color COLOR_SECUNDARIO = new Color(39, 174, 96); // Verde oscuro
// Verde

    private final Color COLOR_HINT = Color.GRAY;

    public LoginFrm() {

        // --- Fondo con degradado ---
        JPanel background = new JPanel() {
            @Override
            protected void paintComponent(Graphics g) {
                super.paintComponent(g);
                Graphics2D g2 = (Graphics2D) g;
                GradientPaint gradient = new GradientPaint(
                        0, 0, new Color(102, 126, 234),     // #667eea
                        getWidth(), getHeight(), new Color(118, 75, 162) // #764ba2
                );
                g2.setPaint(gradient);
                g2.fillRect(0, 0, getWidth(), getHeight());
            }
        };

        background.setLayout(new GridBagLayout());
        setContentPane(background);

        setTitle("Bienvenido a EurekaBank");
        setDefaultCloseOperation(javax.swing.WindowConstants.EXIT_ON_CLOSE);
        setMinimumSize(new Dimension(700, 500));
        setLocationRelativeTo(null);

        // === FORMULARIO CENTRADO ===
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
        JLabel lblLoginTitle = new JLabel("INICIAR SESIÓN");
        lblLoginTitle.setFont(new Font("Segoe UI", Font.BOLD, 24));
        lblLoginTitle.setForeground(COLOR_PRINCIPAL);
        lblLoginTitle.setHorizontalAlignment(SwingConstants.CENTER);
        pnlForm.add(lblLoginTitle, gbcForm);

        gbcForm.insets = new Insets(10, 5, 10, 5);

        // Usuario
        gbcForm.gridy = 1;
        JLabel lblUsername = new JLabel("Usuario");
        lblUsername.setFont(new Font("Segoe UI", Font.BOLD, 14));
        lblUsername.setForeground(COLOR_PRINCIPAL);
        pnlForm.add(lblUsername, gbcForm);

        gbcForm.gridy = 2;
        txtUsername = new JTextField(20);
        txtUsername.setFont(new Font("Segoe UI", Font.PLAIN, 16));
        txtUsername.setBorder(createFieldBorder());
        txtUsername.setBackground(Color.WHITE);
        addPlaceholder(txtUsername, "Ingrese su usuario");
        pnlForm.add(txtUsername, gbcForm);

        // Contraseña
        gbcForm.gridy = 3;
        JLabel lblPassword = new JLabel("Contraseña");
        lblPassword.setFont(new Font("Segoe UI", Font.BOLD, 14));
        lblPassword.setForeground(COLOR_PRINCIPAL);
        pnlForm.add(lblPassword, gbcForm);

        gbcForm.gridy = 4;
        txtPassword = new JPasswordField(20);
        txtPassword.setFont(new Font("Segoe UI", Font.PLAIN, 16));
        txtPassword.setBorder(createFieldBorder());
        txtPassword.setBackground(Color.WHITE);
        addPlaceholder(txtPassword, "Ingrese su contraseña");
        pnlForm.add(txtPassword, gbcForm);

        // Botón login
        gbcForm.gridy = 5;
        gbcForm.insets = new Insets(30, 5, 10, 5);
        btnLogin = new JButton("Iniciar Sesión");
        btnLogin.setFont(new Font("Segoe UI", Font.BOLD, 16));
        btnLogin.setBackground(COLOR_SECUNDARIO);
        btnLogin.setForeground(Color.WHITE);
        btnLogin.setFocusPainted(false);
        btnLogin.setCursor(new Cursor(Cursor.HAND_CURSOR));
        btnLogin.setBorder(BorderFactory.createEmptyBorder(12, 12, 12, 12));
        pnlForm.add(btnLogin, gbcForm);

        // Listener de login
        btnLogin.addActionListener(evt -> btnLoginActionPerformed(evt));

        // Centro el formulario en la ventana
        background.add(pnlForm, new GridBagConstraints());

        pack();
    }

    private Border createFieldBorder() {
        Border line = new MatteBorder(0, 0, 2, 0, COLOR_SECUNDARIO.darker());
        Border padding = BorderFactory.createEmptyBorder(5, 5, 5, 5);
        return BorderFactory.createCompoundBorder(line, padding);
    }

    private void addPlaceholder(JTextField field, String placeholder) {
        field.setText(placeholder);
        field.setForeground(COLOR_HINT);

        if (field instanceof JPasswordField) {
            ((JPasswordField) field).setEchoChar((char) 0);
        }

        field.addFocusListener(new FocusAdapter() {
            @Override
            public void focusGained(FocusEvent e) {
                if (field.getText().equals(placeholder)) {
                    field.setText("");
                    field.setForeground(Color.BLACK);
                    if (field instanceof JPasswordField) {
                        ((JPasswordField) field).setEchoChar('•');
                    }
                }
            }

            @Override
            public void focusLost(FocusEvent e) {
                if (field.getText().isEmpty()) {
                    field.setForeground(COLOR_HINT);
                    field.setText(placeholder);
                    if (field instanceof JPasswordField) {
                        ((JPasswordField) field).setEchoChar((char) 0);
                    }
                }
            }
        });
    }

    private void btnLoginActionPerformed(java.awt.event.ActionEvent evt) {

        String username = txtUsername.getText().trim();
        String password = new String(txtPassword.getPassword()).trim();

        if (username.equals("Ingrese su usuario")) username = "";
        if (password.equals("Ingrese su contraseña")) password = "";

        if (username.isEmpty() || password.isEmpty()) {
            javax.swing.JOptionPane.showMessageDialog(this, "Ingrese usuario y contraseña.", "Advertencia",
                    javax.swing.JOptionPane.WARNING_MESSAGE);
            return;
        }

        boolean loginExitoso = ec.edu.gr03.controller.EurekaBankClient.login(username, password);

        if (loginExitoso) {
            javax.swing.JOptionPane.showMessageDialog(this, "Login exitoso.", "Información",
                    javax.swing.JOptionPane.INFORMATION_MESSAGE);

            MovimientosFrm movFrm = new MovimientosFrm();
            movFrm.setVisible(true);
            this.dispose();
        } else {
            javax.swing.JOptionPane.showMessageDialog(this, "Usuario o contraseña incorrectos.", "Error",
                    javax.swing.JOptionPane.ERROR_MESSAGE);
        }
    }

    public static void main(String args[]) {

        try {
            for (javax.swing.UIManager.LookAndFeelInfo info : javax.swing.UIManager.getInstalledLookAndFeels()) {
                if ("Nimbus".equals(info.getName())) {
                    javax.swing.UIManager.setLookAndFeel(info.getClassName());
                    break;
                }
            }
        } catch (Exception ex) {
            java.util.logging.Logger.getLogger(LoginFrm.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        }

        EventQueue.invokeLater(() -> new LoginFrm().setVisible(true));
    }
}
