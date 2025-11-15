<%@ page contentType="text/html;charset=UTF-8" language="java" %>

<!-- ESTILOS AQUÍ MISMO -->
<style>
    /* FONDO DEL SIDEBAR */
    .sidebar {
        width: 220px;
        height: 100vh;
        padding: 20px;
        background: white;
        display: flex;
    }

    .nav-menu {
        display: flex;
        flex-direction: column;
        gap: 12px;
        width: 100%;
    }

    .nav-button {
        display: block;
        padding: 12px 18px;
        text-decoration: none;
        font-weight: bold;
        color: white;
        border-radius: 8px;
        transition: 0.3s;
    }

    /* COLORES PEDIDOS POR TI */
    .btn-retiro {
        background-color: #e74c3c;   /* ROJO */
    }

    .btn-deposito {
        background-color: #2ecc71;   /* VERDE */
    }

    .btn-trans {
        background-color: #f1c40f;   /* AMARILLO */
        color: black;                 /* Para buena lectura */
    }

    /* Hover */
    .nav-button:hover {
        filter: brightness(0.85);
    }

    /* Activo */
    .nav-button.active {
        border: 2px solid white;
        filter: brightness(1.15);
    }
</style>


<!-- NAVBAR COMPLETO -->
<div class="sidebar">
    <nav class="nav-menu">

        <a href="movimientos.jsp" 
           class="nav-button <%= request.getRequestURI().contains("movimientos") ? "active" : "" %>">
            Movimientos
        </a>

        <a href="retiro.jsp" 
           class="nav-button btn-retiro <%= request.getRequestURI().contains("retiro") ? "active" : "" %>">
            Retiro
        </a>

        <a href="deposito.jsp" 
           class="nav-button btn-deposito <%= request.getRequestURI().contains("deposito") ? "active" : "" %>">
            Depósito
        </a>

        <a href="transferencia.jsp" 
           class="nav-button btn-trans <%= request.getRequestURI().contains("transferencia") ? "active" : "" %>">
            Transferencia
        </a>
    </nav>
</div>
