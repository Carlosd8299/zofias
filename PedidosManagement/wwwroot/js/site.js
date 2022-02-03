// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).on('submit', '  #SetDetalle', function (e) {
    e.preventDefault();
    if (!$('#SetEncabezado').valid()) {
        return false;
    }
    var IdCompra = 0;
    $.ajax({
        type: 'POST',
        url: '/Ordens/SetEncabezado/',
        data: $('#SetEncabezado').serialize(),
        success: function (data) {
            if (data.estado) {
                $('#SetDetalle #Item2_IdOrden').val(data.resultado);
                $('#SetEncabezado #Item1_ID').val(data.resultado);
                $.ajax({
                    type: 'POST',
                    url: '/Ordens/SetDetalle/',
                    data: $('#SetDetalle').serialize(),
                    success: function (data) {
                        var tbl = $('#tbl-detalle-compra tbody');
                        var detalle = data.resultado;
                        tbl.append('<tr><td>' + detalle.producto.nombre +
                            '<td>' + detalle.producto.descripcion + '</td>' +
                            '<td>' + detalle.cantidad + '</td>' +
                            '</tr>');
                    }
                });
            }
            else {
                alert(data.mensaje);
            }
        }
    });
})
