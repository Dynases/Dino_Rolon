SELECT
p.Id as ProductoId, p.Descrip as Producto, i.iccven as 'Stock Disponible',
a.Descrip as Almacen, pr.Precio as Precio, l.Descrip as 'Unidad de Venta',
prc.Descrip as 'Cat. Precio'
FROM
    dbo.TI001 i
    JOIN REG.Producto p ON i.iccprod = p.Id
    JOIN INV.Almacen a ON i.icalm = a.Id
    JOIN REG.Precio pr ON pr.IdProduc = p.Id
    JOIN INV.Sucursal s ON s.Id = a.IdSuc
    JOIN REG.PrecioCat prc ON prc.Id = pr.IdPrecioCat
    JOIN ADM.Libreria l ON l.IdLibrer = p.UniVen
WHERE
    s.Id = 2 and a.Id = 2 and prc.Id = 1 and l.IdGrupo = 3 and l.IdOrden = 6
GROUP BY
    s.Id, a.Id, p.Id, p.Descrip, a.Descrip, i.iccven, pr.Precio, l.Descrip, prc.Descrip