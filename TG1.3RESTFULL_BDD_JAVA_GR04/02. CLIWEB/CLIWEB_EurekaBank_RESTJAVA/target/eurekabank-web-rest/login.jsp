<%@ page contentType="text/html;charset=UTF-8" language="java" %>
<%@ page session="true" %>
<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>EurekaBank - Login</title>

    <!-- ESTILOS DIRECTOS -->
    <style>
        body {
            margin: 0;
            padding: 0;
            height: 100vh;
            display: flex;
            justify-content: center;
            align-items: center;
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            font-family: Arial, sans-serif;
        }

        .form-container {
            width: 350px;
            background: white;
            padding: 30px;
            border-radius: 12px;
            box-shadow: 0px 4px 20px rgba(0,0,0,0.2);
        }

        .form-title {
            text-align: center;
            margin-bottom: 20px;
            font-size: 26px;
            font-weight: bold;
        }

        .form-group {
            margin-bottom: 15px;
        }

        .form-label {
            display: block;
            font-weight: bold;
            margin-bottom: 5px;
        }

        .form-input {
            width: 100%;
            padding: 10px;
            border-radius: 5px;
            border: 1px solid #aaa;
            font-size: 16px;
        }

        .form-button {
            width: 100%;
            padding: 12px;
            background-color: #28a745; /* Verde */
            color: white;
            border: none;
            border-radius: 5px;
            font-size: 16px;
            font-weight: bold;
            cursor: pointer;
            transition: 0.3s;
            margin-top: 10px;
        }

        .form-button:hover {
            background-color: #218838; /* Verde más oscuro */
        }

        .alert-error {
            background: #ffdddd;
            color: #b30000;
            padding: 10px;
            border-left: 4px solid red;
            margin-bottom: 15px;
            border-radius: 5px;
        }
    </style>

</head>
<body>

    <div class="form-container">
        <h1 class="form-title">Iniciar Sesión</h1>

        <% if (request.getAttribute("error") != null) { %>
            <div class="alert-error">
                <%= request.getAttribute("error") %>
            </div>
        <% } %>

        <form action="login" method="post">
            <div class="form-group">
                <label class="form-label" for="username">Usuario</label>
                <input type="text" id="username" name="username" class="form-input" placeholder="Ingrese su usuario">
            </div>

            <div class="form-group">
                <label class="form-label" for="password">Contraseña</label>
                <input type="password" id="password" name="password" class="form-input" placeholder="Ingrese su contraseña">
            </div>

            <button type="submit" class="form-button">Iniciar Sesión</button>
        </form>
    </div>

    <!-- NO LO OLVIDÉ :) -->
    <script src="js/scripts.js"></script>

</body>
</html>
