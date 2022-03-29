-- Obtener la lista de precios de todos los productos
select Codigo, Nombre, PrecioVenta from TblProductos

--Obtener la lista de productos cuya existencia en el inventario haya llegado al mínimo
--permitido (5 unidades)
select Codigo, Nombre, ExistenciaActual
from TblProductos
where ExistenciaActual < 5

--Obtener una lista de clientes no mayores de 35 años que hayan realizado compras entre el 
--1 de febrero de 2000 y el 25 de mayo de 2000
SELECT TblClientes.Id, TblClientes.TipoDocumento, TblClientes.NumeroDocumento, TblClientes.Nombre,
TblClientes.FechaNacimiento, TblClientes.Direccion, TblClientes.Telefono
FROM TblFacturas
INNER JOIN TblClientes ON TblClientes.Id = TblFacturas.IdCliente
WHERE DateDiff(YEAR,TblClientes.FechaNacimiento, TblFacturas.Fecha) < 35 AND 
TblFacturas.Fecha BETWEEN '2000-02-01' and '2000-05-25'


--Obtener el valor total vendido por cada producto en el año 2000
SELECT TblProductos.Nombre Producto, SUM(TblFacturaProductos.ValorTotal) VentaAnual
FROM TblFacturaProductos
INNER JOIN TblFacturas ON TblFacturas.Id = TblFacturaProductos.IdFactura
INNER JOIN TblProductos ON TblProductos.Id = TblFacturaProductos.IdProducto
WHERE
DATEPART(YEAR, TblFacturas.Fecha) = 2000
GROUP BY TblProductos.Id, TblProductos.Nombre 

--Obtener la última fecha de compra de un cliente y según su frecuencia de compra estimar 
--en qué fecha podría volver a comprar

SELECT TblClientes.Id, MAX(TblFacturas.Fecha) UltimaCompra
FROM TblFacturas
INNER JOIN TblClientes ON TblClientes.Id = TblFacturas.IdCliente
WHERE TblClientes.Id = 1003
GROUP BY TblClientes.Id

SELECT TblFacturas.*
FROM TblFacturas
INNER JOIN TblClientes ON TblClientes.Id = TblFacturas.IdCliente
WHERE TblClientes.Id = 1003
