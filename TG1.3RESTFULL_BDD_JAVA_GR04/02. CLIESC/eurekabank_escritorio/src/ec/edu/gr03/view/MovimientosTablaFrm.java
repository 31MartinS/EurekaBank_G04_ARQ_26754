package ec.edu.gr03.view;

import java.awt.BorderLayout;
import java.awt.Color;
import java.awt.Dimension;
import java.awt.EventQueue;
import java.awt.Font;
import java.awt.GridBagConstraints;
import java.awt.GridBagLayout;
import java.awt.Insets;
import java.util.List;
import javax.swing.BorderFactory;
import javax.swing.JLabel;
import javax.swing.JPanel;
import javax.swing.JScrollPane;
import javax.swing.JTable;
import javax.swing.table.DefaultTableModel;
import javax.swing.table.JTableHeader;
// Importación de tu modelo local
import ec.edu.gr03.model.Movimiento;

public class MovimientosTablaFrm extends javax.swing.JFrame {

    // --- Variables de componentes ---
    private javax.swing.JLabel lblCuenta;
    private javax.swing.JTable tblMovimientos;

    // Colores del diseño
    private final Color COLOR_HEADER = new Color(52, 152, 219); // Azul brillante para encabezado
    private final Color COLOR_SECUNDARIO = new Color(52, 152, 219); // Azul brillante para selección
    private final Color COLOR_GRIS_FONDO = new Color(245, 245, 245); // Fondo claro
    private final Color COLOR_FILAS_ALTERNAS = new Color(245, 245, 245); // Azul muy claro para filas alternas

    public MovimientosTablaFrm(String numeroCuenta) {

        // 1. OBTENER DATOS
        List<Movimiento> lista = ec.edu.gr03.controller.EurekaBankClient.traerMovimientos(numeroCuenta);

        // 2. CONFIGURAR EL JFRAME
        setTitle("EurekaBank - Movimientos");
        setDefaultCloseOperation(javax.swing.WindowConstants.DISPOSE_ON_CLOSE);
        setMinimumSize(new Dimension(800, 600));
        setLocationRelativeTo(null);
        getContentPane().setLayout(new BorderLayout());

        // 3. PANEL PRINCIPAL
        JPanel pnlMain = new JPanel(new BorderLayout(10, 10));
        pnlMain.setBackground(COLOR_GRIS_FONDO);
        pnlMain.setBorder(BorderFactory.createEmptyBorder(20, 20, 20, 20));

        // 4. PANEL DE TÍTULO
        JPanel pnlHeader = new JPanel(new GridBagLayout());
        pnlHeader.setBackground(Color.WHITE);
        pnlHeader.setBorder(BorderFactory.createEmptyBorder(15, 20, 15, 20));

        GridBagConstraints gbc = new GridBagConstraints();
        gbc.gridx = 0;
        gbc.gridy = 0;
        gbc.anchor = GridBagConstraints.WEST;
        gbc.weightx = 1.0;

        JLabel lblTitle = new JLabel("Movimientos");
        lblTitle.setFont(new Font("Segoe UI", Font.BOLD, 24));
        lblTitle.setForeground(COLOR_HEADER);
        pnlHeader.add(lblTitle, gbc);

        // Etiqueta para el número de cuenta
        gbc.gridy = 1;
        gbc.insets = new Insets(5, 0, 0, 0);
        lblCuenta = new JLabel();
        lblCuenta.setFont(new Font("Segoe UI", Font.PLAIN, 16));
        lblCuenta.setForeground(Color.GRAY);
        pnlHeader.add(lblCuenta, gbc);

        pnlMain.add(pnlHeader, BorderLayout.NORTH);

        // 5. CONFIGURAR LA TABLA Y EL MODELO
        DefaultTableModel modelo = new DefaultTableModel();
        modelo.addColumn("Movimiento");
        modelo.addColumn("Fecha");
        modelo.addColumn("Tipo");
        modelo.addColumn("Acción");
        modelo.addColumn("Importe");

        if (lista.isEmpty()) {
            lblCuenta.setText("No se encontraron registros para la cuenta: " + numeroCuenta);
        } else {
            lblCuenta.setText("Mostrando movimientos de la cuenta: " + numeroCuenta);
            for (Movimiento mov : lista) {
                modelo.addRow(new Object[]{
                        mov.getNromov(),
                        mov.getFecha().toString(),
                        mov.getTipo(),
                        mov.getAccion(),
                        mov.getImporte()
                });
            }
        }

        tblMovimientos = new JTable(modelo) {
            // Para filas alternas de color azul claro
            public java.awt.Component prepareRenderer(javax.swing.table.TableCellRenderer renderer, int row, int column) {
                java.awt.Component c = super.prepareRenderer(renderer, row, column);
                if (!isRowSelected(row)) {
                    c.setBackground(row % 2 == 0 ? Color.WHITE : COLOR_FILAS_ALTERNAS);
                }
                return c;
            }
        };

        // --- ESTILO DE LA TABLA ---
        tblMovimientos.setFont(new Font("Segoe UI", Font.PLAIN, 14));
        tblMovimientos.setRowHeight(30);
        tblMovimientos.setGridColor(COLOR_SECUNDARIO);
        tblMovimientos.setShowGrid(true);
        tblMovimientos.setShowHorizontalLines(true);
        tblMovimientos.setShowVerticalLines(false);
        tblMovimientos.setSelectionBackground(COLOR_SECUNDARIO);
        tblMovimientos.setSelectionForeground(Color.WHITE);
        tblMovimientos.setBorder(null);

        // --- ESTILO DEL HEADER ---
        JTableHeader header = tblMovimientos.getTableHeader();
        header.setFont(new Font("Segoe UI", Font.BOLD, 16));
        header.setBackground(COLOR_HEADER);
        header.setForeground(Color.WHITE);
        header.setOpaque(true);
        header.setPreferredSize(new Dimension(100, 40));
        header.setBorder(BorderFactory.createLineBorder(COLOR_HEADER));

        // 6. SCROLL PANE
        JScrollPane jScrollPane1 = new JScrollPane(tblMovimientos);
        jScrollPane1.setBorder(BorderFactory.createEmptyBorder());
        jScrollPane1.getViewport().setBackground(Color.WHITE);

        pnlMain.add(jScrollPane1, BorderLayout.CENTER);

        // 7. ENSAMBLAJE FINAL
        getContentPane().add(pnlMain, BorderLayout.CENTER);

        pack();
    }

    // MAIN
    public static void main(String args[]) {
        try {
            for (javax.swing.UIManager.LookAndFeelInfo info : javax.swing.UIManager.getInstalledLookAndFeels()) {
                if ("Nimbus".equals(info.getName())) {
                    javax.swing.UIManager.setLookAndFeel(info.getClassName());
                    break;
                }
            }
        } catch (Exception ex) {
            ex.printStackTrace();
        }

        EventQueue.invokeLater(new Runnable() {
            public void run() {
                new MovimientosTablaFrm("").setVisible(true);
            }
        });
    }
}
