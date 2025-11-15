/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package ec.edu.eureka.bank.test;

import ec.edu.eurekabank.db.AccesoDB;
import java.sql.Connection;

/**
 *
 * @author Rabedon1
 */
public class TestConexion {
    public static void main(String[] args) {
        Connection cn = null;
        try {
            cn = AccesoDB.getConnection();
            System.out.println("✔ Conexión exitosa a la base de datos.");
        } catch (Exception e) {
            System.err.println("✖ Error en la conexión: " + e.getMessage());
        } finally {
            try {
                if (cn != null) cn.close();
                System.out.println("Conexión cerrada.");
            } catch (Exception e) {
                System.err.println("Error al cerrar la conexión.");
            }
        }
    }
}
