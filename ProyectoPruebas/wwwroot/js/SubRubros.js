function CompletarTablaSubRubros() {
    VaciarFormulario()
    $.ajax({
        type: "POST",
        url: '../../SubRubros/BuscarSubRubros',
        data: {},
        success: function (listadoSubRubroMostrar) {
            $('#body-subRubros').empty();
            $.each(listadoSubRubroMostrar, function (index, subRubro) {

                let claseEliminado = '';
                let botones = `<btn type='button' class= 'btn btn-outline-success btn-sm me-3' onclick = "BuscarSubRubro(${subRubro.subRubroID}, ${subRubro.rubroID})"><i class="bi bi-pencil-square"></i> Editar</btn>
                                <btn type='button' class = 'btn btn-outline-danger btn-sm'onclick = "EliminarSubRubro(${subRubro.subRubroID},1)"><i class="bi bi-trash3"></i> Eliminar</btn>`

                if (subRubro.eliminado) {
                    claseEliminado = 'table-danger';
                    botones = `<btn type='button' class = 'btn btn-outline-warning btn-sm'onclick = "EliminarSubRubro(${subRubro.subRubroID},0)"><i class="bi bi-recycle"></i> Activar</btn>` 
                }
                $("#body-subRubros").append(
                    `<tr class= 'tabla-hover ${claseEliminado}'>
                        <td class= 'texto'>${subRubro.descripcion}</td>
                        <td class='texto'>${subRubro.rubroDescripcion}</td>
                        <td class = 'text-center'>
                            ${botones}
                        </td>
                    </tr>`



                )
            });
        },
        error: function (data) {}
    });
}

function GuardarSubRubro() {
    //Toma el valor del input de tipo hiden
    let subRubroID = $("#SubRubroID").val();
    let subRubroNombre = $('#inputSubRubro').val().trim();
    let alertRubro = $('#alertSubRubro');
    let rubroID = $('#RubroID').val();
    if (subRubroNombre != '' && subRubroNombre != null) {
        if (rubroID != 0) {
            $.ajax({
                type: "POST",
                url: '../../SubRubros/GuardarSubRubro',
                data: { SubRubroID: subRubroID, Descripcion: subRubroNombre, RubroID: rubroID },
                success: function (resultado) {
                    if (resultado == 0) {
                        $('#modalCrearSubRubro').modal('hide')
                        CompletarTablaSubRubros();
                    }
                    if (resultado == 2) {
                        alertRubro.removeClass('visually-hidden').text('El sub rubro ingresado ya existe')
                    }
               },
                error: function (data) {
                }
            });
        } else {
            alertRubro.removeClass('visually-hidden').text('Seleccione un Rubro')
        }
    } else {
        alertRubro.removeClass('visually-hidden').text('Ingrese una descripcion')
    }
    }


function BuscarSubRubro(subRubroID, rubroID) {
    $('#titulo-modal-subRubro').text('Editar SubRubro')
    $('#SubRubroID').val(subRubroID);
    $('#RubroID').val(rubroID);
    $.ajax({
        type: "POST",
        url: '../../SubRubros/BuscarSubRubro',
        data: { SubRubroID: subRubroID, },
        success: function (subRubro) {
            $('#inputSubRubro').val(subRubro.descripcion);
            $('#modalCrearSubRubro').modal('show');
        }
    });
}

function AbrirModal() {
    $('#titulo-modal-rubro').text('Nuevo SubRubro')
    $('#SubRubroID').val(0);
    $('#modalCrearSubRubro').modal('show');
    $('#alertSubRubro').addClass('visually-hidden')
}
function VaciarFormulario() {
    $("#SubRubroID").val(0);
    $('#inputSubRubro').val('');
    $('#RubroID').val(0);
    $('#alertSubRubro').addClass('visually-hidden')
}

function EliminarSubRubro(subRubroID, elimina) {
    $.ajax({
        type: "POST",
        url: '../../SubRubros/EliminarSubRubro',
        data: { SubRubroID: subRubroID, Elimina: elimina },
        success: function (subRubro) {
            CompletarTablaSubRubros();
        },
        error: function (data) { }
    });
}