-- Script para crear cuentas para clientes que no tienen ninguna cuenta
-- Esto soluciona el problema de clientes con saldo 0 que no permiten operaciones

USE CalculatorDb;
GO

-- Ver clientes sin cuenta
SELECT c.chr_cliecodigo, 
       c.vch_clienombre + ' ' + c.vch_cliepaterno + ' ' + c.vch_cliematerno AS nombre_completo
FROM cliente c
WHERE NOT EXISTS (
    SELECT 1 FROM cuenta cu WHERE cu.chr_cliecodigo = c.chr_cliecodigo
);
GO

-- Crear cuentas para clientes que no tienen
DECLARE @cliente_codigo CHAR(4);
DECLARE @nueva_cuenta CHAR(5);
DECLARE @contador INT;

-- Obtener el contador actual de cuentas
SELECT @contador = ISNULL(MAX(CAST(SUBSTRING(chr_cuencodigo, 3, 3) AS INT)), 0)
FROM cuenta;

-- Cursor para recorrer clientes sin cuenta
DECLARE cliente_cursor CURSOR FOR
SELECT chr_cliecodigo
FROM cliente
WHERE NOT EXISTS (
    SELECT 1 FROM cuenta WHERE cuenta.chr_cliecodigo = cliente.chr_cliecodigo
);

OPEN cliente_cursor;
FETCH NEXT FROM cliente_cursor INTO @cliente_codigo;

WHILE @@FETCH_STATUS = 0
BEGIN
    -- Incrementar contador
    SET @contador = @contador + 1;
    
    -- Generar código de cuenta (formato: 00XXX donde XXX es el número)
    SET @nueva_cuenta = '00' + RIGHT('000' + CAST(@contador AS VARCHAR(3)), 3);
    
    -- Crear cuenta para el cliente
    INSERT INTO cuenta (
        chr_cuencodigo,
        chr_monecodigo,
        chr_sucucodigo,
        chr_cliecodigo,
        dec_cuensaldo,
        dtt_cuenfechacreacion,
        vch_cuenestado,
        chr_emplcreacuenta,
        chr_cuenclave,
        int_cuencontmov
    )
    VALUES (
        @nueva_cuenta,
        '01',                 -- Soles
        '001',                -- Sucursal Sipan
        @cliente_codigo,
        0.00,                 -- Saldo inicial 0
        GETDATE(),
        'ACTIVO',
        '0001',               -- Carlos Alberto Romero
        '1234',               -- Clave por defecto
        0                     -- Sin movimientos
    );
    
    PRINT 'Cuenta ' + @nueva_cuenta + ' creada para cliente ' + @cliente_codigo;
    
    FETCH NEXT FROM cliente_cursor INTO @cliente_codigo;
END;

CLOSE cliente_cursor;
DEALLOCATE cliente_cursor;

-- Verificar resultado
SELECT 
    c.chr_cliecodigo,
    c.vch_clienombre + ' ' + c.vch_cliepaterno AS nombre,
    COUNT(cu.chr_cuencodigo) AS total_cuentas,
    ISNULL(SUM(cu.dec_cuensaldo), 0) AS saldo_total
FROM cliente c
LEFT JOIN cuenta cu ON c.chr_cliecodigo = cu.chr_cliecodigo
GROUP BY c.chr_cliecodigo, c.vch_clienombre, c.vch_cliepaterno
ORDER BY total_cuentas ASC, c.chr_cliecodigo;
GO
