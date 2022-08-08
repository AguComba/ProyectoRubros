
const CompletarTablaArticulos = () => {
    VaciarFormulario();
    $.ajax({
        type: "POST",
        url: '../../Articulos/BuscarArticulos',
        data: {},
        success: listadoArticuloMostrar => {
            $('#tbody-articulos').empty()
            $.each(listadoArticuloMostrar, (index, articulo) => {
                let claseEliminado = '';
                let botones = `<btn type='button' class= 'btn btn-outline-success btn-sm me-3' onclick = "BuscarArticulo(${articulo.articuloID}, ${articulo.rubroID}, ${articulo.subRubroID})"><i class="bi bi-pencil-square"></i> Editar</btn>
                                <btn type='button' class = 'btn btn-outline-danger btn-sm'onclick = "EliminarArticulo(${articulo.articuloID},1)"><i class="bi bi-trash3"></i> Eliminar</btn>`
                if (articulo.eliminado) {
                    claseEliminado = 'table-danger'
                    botones = `<btn type='button' class = 'btn btn-outline-warning btn-sm'onclick = "EliminarArticulo(${articulo.articuloID},0)"><i class="bi bi-recycle"></i> Activar</btn>`
                }

                $('#tbody-articulos').append(
                    `<tr class= 'tabla-hover ${claseEliminado}'>
                        <td class='texto'>${articulo.descripcion}</td>
                        <td class='texto ocultar1200'>${articulo.rubroDescripcion}</td>
                        <td class= 'texto'>${articulo.subRubroDescripcion}</td> 
                        <td class = 'text-center ocultar550'>$${articulo.precioCosto}</td> 
                        <td class = 'text-center ocultar768'>${articulo.porcentajeGanancia}%</td>
                        <td class = 'text-center'>$${articulo.precioVenta}</td>
                        <td class = 'text-center'>
                            ${botones}
                        </td>
                    </tr>`
                )
            });
        },
        error: data => { }
    });
}

const CalcularImportes = origen => {
    let costo = $('#PrecioCosto').val();
    let porcentaje = $('#PorcentajeGanancia').val();
    let venta = $('#PrecioVenta').val()

    let calculo = parseFloat(porcentaje) / 100 * parseFloat(costo) + parseFloat(costo);   
    let costoCalculado = parseFloat(venta) * 100 / parseFloat(costo) - 100;
    console.log(typeof calculo);
    console.log(typeof costoCalculado);
    
    (origen == 1 || origen == 2) ? $('#PrecioVenta').val(calculo): $('#PrecioVenta').val()
    origen == 3 ? $("#PorcentajeGanancia").val(costoCalculado) : $("#PorcentajeGanancia").val()
}

const GuardarArticulo = () => {
    let alertArticulo = $('#alertArticulo');
    let articuloID = $('#ArticuloID').val();
    let nombreArticulo = $('#nombreArticulo').val().trim();
    let subRubroID = $('#SubRubroID').val()
    let precioCosto = $('#PrecioCosto').val()
    let porcentajeGanancia = $('#PorcentajeGanancia').val()
    let precioVenta = $('#PrecioVenta').val()
    let rubroID = $('#RubroID').val();
    if (nombreArticulo != '' && nombreArticulo != null) {
        if (rubroID != 0) {
            if (subRubroID != 0) {
            $.ajax({
                type: "POST",
                url: '../../Articulos/GuardarArticulo',
                data: { ArticuloID: articuloID, Descripcion: nombreArticulo, SubRubroID: subRubroID, PrecioCosto: precioCosto, PorcentajeGanancia: porcentajeGanancia, PrecioVenta: precioVenta },
                success: resultado => {
                    if (resultado == 0) {
                        console.log(resultado)
                        $('#modalCrearArticulo').modal('hide');
                        CompletarTablaArticulos();
                    }
                    if (resultado == 2) {
                        console.log(resultado)
                        alertArticulo.removeClass('visually-hidden').text('El Articulo ingresado ya existe')
                    }
                }

            })
        } else {
            alertArticulo.removeClass('visually-hidden').text('Debe crear un Sub Rubro para este Rubro')
        }

        } else {
            alertArticulo.removeClass('visually-hidden').text('Seleccione un Rubro')
        }
    } else {
        alertArticulo.removeClass('visually-hidden').text('Ingrese una descripcion')
    }
    
}

//const Seleccionar = (id) => $(`#SubRubroID option[value = ${id}]`).attr("selected", true);

const BuscarArticulo = (articuloID, rubroID, subRubroID) => {
    $('#titulo-modal-articulo').text('Editar Articulo');
    $('#BtnCrearEditar').text(`Guardar`);
    $('#ArticuloID').val(articuloID);
    $('#RubroID').val(rubroID);
    BuscarSubRubro();
    $.ajax({
        type: "POST",
        url: '../../Articulos/BuscarArticulo',
        data: { ArticuloID: articuloID, },
        success: articulo => {
            $('#nombreArticulo').val(articulo.descripcion);
            $('#SubRubroID').val(subRubroID);
            $('#PrecioCosto').val(articulo.precioCosto);
            $('#PorcentajeGanancia').val(articulo.porcentajeGanancia);
            $('#PrecioVenta').val(articulo.precioVenta);
            $('#modalCrearArticulo').modal('show')
        }
    })
}

$("#RubroID").change(() => {
    BuscarSubRubro();
})

const BuscarSubRubro = () => {
    $('#SubRubroID').empty();
    $.ajax({
        type: "POST",
        url: '../../SubRubros/ComboSubRubro',
        typeof: 'json',
        data: { id: $('#RubroID').val() },
        success: subRubros => {
            if (subRubros.length === 0) {
                $('#SubRubroID').append(`<option value=${0}>[NO EXISTEN SUBRUBROS]</option>`); 
            }
            else {
                $.each(subRubros, (i, subRubro) => {
                    $('#SubRubroID').append(`<option value=${subRubro.value}>${subRubro.text}</option>`)
                });
            }
        },
        error: ex => {

        }
    })
    return false;
}

const AbrirModal = () => {
    $('#titulo-modal-articulo').text('Nuevo Articulo');
    $('#BtnCrearEditar').text('Crear');
    $('#ArticuloID').val(0);
    $('#modalCrearArticulo').modal('show');
    $('#alertArticulo').addClass('visually-hidden');
    $("#RubroID").val(0);
    $("#SubRubroID").val(0)
}

const VaciarFormulario = () => {
    $('#ArticuloID').val(0);
    $('#nombreArticulo').val('');
    $('#alertArticulo').addClass('visually-hidden');
    $('#PrecioCosto').val(0);
    $('#PorcentajeGanancia').val(0);
    $('#PrecioVenta').val(0);
    $('#RubroID').val(0);
    $('#SubRubroID').val(0);
}

const EliminarArticulo = (articuloID, elimina) => {
    $.ajax({
        type: "POST",
        url: '../../Articulos/EliminarArticulo',
        data: { ArticuloID: articuloID, Elimina: elimina },
        success: articulo => CompletarTablaArticulos(),
        error: data => {}
    })
}