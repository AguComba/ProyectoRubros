//Funcion para generar la tabla con todos los rubros
function CompletarTablaRubros() {
    VaciarFormulario()
    $.ajax({
        type: "POST",
        url: '../../Rubros/BuscarRubros',
        data: {},
        success: function (listaRubros) {
            $('#body-rubros').empty();
            $.each(listaRubros, function (index, rubro) {

                let claseEliminado = '';
                let botones = `<btn type='button' class= 'btn btn-outline-success btn-sm me-3' onclick = "BuscarRubro(${rubro.rubroID})"><i class="bi bi-pencil-square"></i> Editar</btn>
                                <btn type='button' class = 'btn btn-outline-danger btn-sm'onclick = "EliminarRubro(${rubro.rubroID},1)"><i class="bi bi-trash3"></i> Eliminar</btn>`

                if (rubro.eliminado) {
                    claseEliminado = 'table-danger';
                    botones = `<btn type='button' class = 'btn btn-outline-warning btn-sm'onclick = "EliminarRubro(${rubro.rubroID},0)"><i class="bi bi-recycle"></i> Activar</btn>` 
                }
                $("#body-rubros").append(
                    `<tr class= 'tabla-hover ${claseEliminado}'>
                        <td class='texto'>${rubro.descripcion}</td>
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

function GuardarRubro() {
    //Toma el valor del input de tipo hiden
    let rubroID = $("#RubroID").val()
    let rubroNombre = $('#inputRubro').val().trim();
    let alertRubro = $('#alertRubro')
    if (rubroNombre != '' && rubroNombre != null) {
    $.ajax({
        type: "POST",
        url: '../../Rubros/GuardarRubro',
        data: { RubroID: rubroID, Descripcion: rubroNombre },
        success: function (resultado) {
            if (resultado == 0) {
                $('#modalCrearRubro').modal('hide')
                CompletarTablaRubros();
            }
            if (resultado == 2) {
                alertRubro.removeClass('visually-hidden').text('El rubro ingresado ya existe')
            }
        },
        error: function (data) {
        }
    });
    } else {
     alertRubro.removeClass('visually-hidden')
    }
    }


function BuscarRubro(rubroID) {
    $('#titulo-modal-rubro').text('Editar Rubro')
    $('#RubroID').val(rubroID);
    $.ajax({
        type: "POST",
        url: '../../Rubros/BuscarRubro',
        data: { RubroID: rubroID },
        success: function (rubro) {
            $('#inputRubro').val(rubro.descripcion);
            $('#modalCrearRubro').modal('show');
        }
    });
}

function AbrirModal() {
    $('#titulo-modal-rubro').text('Nuevo Rubro')
    $('#RubroID').val(0);
    $('#modalCrearRubro').modal('show');
    $('#alertRubro').addClass('visually-hidden')
}
function VaciarFormulario() {
    $("#RubroID").val(0);
    $('#inputRubro').val('');
    $('#alertRubro').addClass('visually-hidden')
}

function EliminarRubro(rubroID, elimina) {
    $.ajax({
        type: "POST",
        url: '../../Rubros/EliminarRubro',
        data: { RubroID: rubroID, Elimina: elimina },
        success: function (rubro) {
            CompletarTablaRubros();
        },
        error: function (data) { }
    });
}
