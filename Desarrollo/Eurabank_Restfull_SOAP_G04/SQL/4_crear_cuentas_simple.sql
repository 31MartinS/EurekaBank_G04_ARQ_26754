-- Script simple para crear cuentas para clientes sin cuenta
USE CalculatorDb;
GO

-- Crear cuentas para cada cliente sin cuenta manualmente
INSERT INTO cuenta (chr_cuencodigo, chr_monecodigo, chr_sucucodigo, chr_cliecodigo, dec_cuensaldo, dtt_cuenfechacreacion, vch_cuenestado, chr_emplcreacuenta, chr_cuenclave, int_cuencontmov)
SELECT 
    '00' + CAST(ROW_NUMBER() OVER (ORDER BY c.chr_cliecodigo) + 300 AS VARCHAR(3)) AS chr_cuencodigo,
    '01' AS chr_monecodigo,  -- Soles
    '001' AS chr_sucucodigo,  -- Sucursal Sipan
    c.chr_cliecodigo,
    0.00 AS dec_cuensaldo,
    GETDATE() AS dtt_cuenfechacreacion,
    'ACTIVO' AS vch_cuenestado,
    '0001' AS chr_emplcreacuenta,  -- Carlos Alberto Romero
    '1234' AS chr_cuenclave,
    0 AS int_cuencontmov
FROM cliente c
WHERE NOT EXISTS (
    SELECT 1 FROM cuenta cu WHERE cu.chr_cliecodigo = c.chr_cliecodigo
);
GO

-- Verificar resultado
SELECT 
    c.chr_cliecodigo,
    c.vch_clienombre + ' ' + c.vch_cliepaterno AS nombre,
    COUNT(cu.chr_cuencodigo) AS total_cuentas,
    ISNULL(SUM(cu.dec_cuensaldo), 0) AS saldo_total
FROM cliente c
LEFT JOIN cuenta cu ON c.chr_cliecodigo = cu.chr_cliecodigo
GROUP BY c.chr_cliecodigo, c.vch_clienombre, c.vch_cliepaterno
HAVING COUNT(cu.chr_cuencodigo) = 0
ORDER BY c.chr_cliecodigo;
GO
