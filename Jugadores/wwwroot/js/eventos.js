window.onload = ListadoEventos();

function LimpiarModal() {
    $("#EventoPartidoID").val(0);
    $("#PartidoID").val(0);
    $("#Jugador").val("");
    $("#Fecha").val("");
    $("#Descripcion").val("");
}

function TraerDetallePartido() {
    let partidoID = $("#PartidoID").val();

    $.ajax({
        // la URL para la petición
        url: '../../Eventos/TraerDetallePartido',
        // la información a enviar
        // (también es posible utilizar una cadena de datos)
        data: { PartidoID: partidoID },
        // especifica si será una petición POST o GET
        type: 'POST',
        // el tipo de información que se espera de respuesta
        dataType: 'json',
        // código a ejecutar si la petición es satisfactoria;
        // la respuesta es pasada como argumento a la función
        success: function (partido) {

            if (partido == null) {
                alert("No se encontraron detalles para el partido seleccionado.");
                LimpiarModal();
            }
            else {
                $("#Jugador").val(partido.nombre);
                $("#Fecha").val(partido.fechaPartido);
            }

        },

        // código a ejecutar si la petición falla;
        // son pasados como argumentos a la función
        // el objeto de la petición en crudo y código de estatus de la petición
        error: function (xhr, status) {
            console.log('Disculpe, existió un problema al cargar el listado');
        }
    });
}

function ListadoEventos() {

    let jugador = $("#JugadorID").val();

    $.ajax({
        // la URL para la petición
        url: '../../Eventos/ListadoEventos',
        // la información a enviar
        // (también es posible utilizar una cadena de datos)
        data: { JugadorID: jugador },
        // especifica si será una petición POST o GET
        type: 'POST',
        // el tipo de información que se espera de respuesta
        dataType: 'json',
        // código a ejecutar si la petición es satisfactoria;
        // la respuesta es pasada como argumento a la función
        success: function (listadoEventosMostrar) {

            $("#modalEventos").modal("hide");
            LimpiarModal();

            //$("#tbody-tipoejercicios").empty();
            let contenidoTabla = ``;

            $.each(listadoEventosMostrar, function (index, listadoEvento) {

                contenidoTabla += `
                <tr>
                    <td>${listadoEvento.estadioPartido}</td>
                    <td>${listadoEvento.nombreJugador}</td>
                    <td>${listadoEvento.fechaPartido}</td>
                    <td>${listadoEvento.descripcion}</td>
                    <td class="text-center">
                    <button type="button" class="btn btn-success btn-sm" onclick="">
                    Editar
                    </button>
                    </td>
                    <td class="text-center">
                    <button type="button" class="btn btn-danger btn-sm" onclick="">
                    Eliminar
                    </button>
                    </td>
                </tr>
             `;

                //  $("#tbody-tipoejercicios").append(`
                //     <tr>
                //         <td>${tipoDeEjercicio.descripcion}</td>
                //         <td class="text-center"></td>
                //         <td class="text-center"></td>
                //     </tr>
                //  `);
            });

            document.getElementById("tbody-eventos").innerHTML = contenidoTabla;

        },

        // código a ejecutar si la petición falla;
        // son pasados como argumentos a la función
        // el objeto de la petición en crudo y código de estatus de la petición
        error: function (xhr, status) {
            console.log('Disculpe, existió un problema al cargar el listado');
        }
    });
}

function GuardarEvento() {
    let partidoID = $("#PartidoID").val();
    let fecha = $("#Fecha").val();
    let descripcion = $("#Descripcion").val();

    $.ajax({
        // la URL para la petición
        url: '../../Eventos/GuardarEvento',
        // la información a enviar
        // (también es posible utilizar una cadena de datos)
        data: { PartidoID: partidoID, FechaEvento: fecha, Descripcion: descripcion },
        // especifica si será una petición POST o GET
        type: 'POST',
        // el tipo de información que se espera de respuesta
        dataType: 'json',
        // código a ejecutar si la petición es satisfactoria;
        // la respuesta es pasada como argumento a la función
        success: function (result) {

            if (result != "") {
                alert(result);
            }
            ListadoEventos();

        },

        // código a ejecutar si la petición falla;
        // son pasados como argumentos a la función
        // el objeto de la petición en crudo y código de estatus de la petición
        error: function (xhr, status) {
            console.log('Disculpe, existió un problema al cargar el listado');
        }
    });
    
}