window.onload = ListadoPartidos();

function NuevoRegistro() {
    $("#tituloModalPartidos").text("Nuevo Partido");
}

function LimpiarModal() {
    document.getElementById("PartidoID").value = 0;
    document.getElementById("JugadorID").value = 0;
    document.getElementById("FechaPartido").value = "";
    document.getElementById("Minutos").value = "";
    document.getElementById("Estadio").value = "";
}

function ListadoPartidos() {

    let jugadorIDBuscar = document.getElementById("JugadorIDBuscar").value;
    // let fechaPartidoBuscar = document.getElementById("FechaPartidoBuscar").value;

    $.ajax({
        // la URL para la petición
        url: '../../Partidos/ListadoPartidos',
        // la información a enviar
        // (también es posible utilizar una cadena de datos)
        data: { JugadorIDBuscar: jugadorIDBuscar,},
        // especifica si será una petición POST o GET
        type: 'POST',
        // el tipo de información que se espera de respuesta
        dataType: 'json',
        // código a ejecutar si la petición es satisfactoria;
        // la respuesta es pasada como argumento a la función
        success: function (listadoPartidosMostrar) {

            $("#modalPartidos").modal("hide");
            LimpiarModal();

            //$("#tbody-tipoejercicios").empty();
            let contenidoTabla = ``;

            $.each(listadoPartidosMostrar, function (index, listadoPartido) {

                contenidoTabla += `
                <tr>
                    <td>${listadoPartido.jugadorNombre}</td>
                    <td>${listadoPartido.fechaPartidoString}</td>
                    <td>${listadoPartido.minutosJugados}</td>
                    <td>${listadoPartido.estadio}</td>
                    <td class="text-center">
                    <button type="button" class="btn btn-success btn-sm" onclick="AbrirModalEditar(${listadoPartido.partidoID})">
                    Editar
                    </button>
                    </td>
                    <td class="text-center">
                    <button type="button" class="btn btn-danger btn-sm" onclick="EliminarPartido(${listadoPartido.partidoID})">
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

            document.getElementById("tbody-partidos").innerHTML = contenidoTabla;

        },

        // código a ejecutar si la petición falla;
        // son pasados como argumentos a la función
        // el objeto de la petición en crudo y código de estatus de la petición
        error: function (xhr, status) {
            console.log('Disculpe, existió un problema al cargar el listado');
        }
    });
}

function GuardarPartido() {
    let partidoID = document.getElementById("PartidoID").value;
    let jugadorID = document.getElementById("JugadorID").value;
    let fecha = document.getElementById("FechaPartido").value;
    let minutos = document.getElementById("Minutos").value;
    let estadio = document.getElementById("Estadio").value;

    console.log(estadio);
    $.ajax({
        // la URL para la petición
        url: '../../Partidos/GuardarPartidos',
        // la información a enviar
        // (también es posible utilizar una cadena de datos)
        data: { PartidoID: partidoID, JugadorID: jugadorID, Fecha: fecha, Minutos: minutos, Estadio: estadio },
        // especifica si será una petición POST o GET
        type: 'POST',
        // el tipo de información que se espera de respuesta
        dataType: 'json',
        // código a ejecutar si la petición es satisfactoria;
        // la respuesta es pasada como argumento a la función
        success: function (resultado) {
            if (resultado != "") {
                alert(resultado);
            }
            ListadoPartidos();
        },

        // código a ejecutar si la petición falla;
        // son pasados como argumentos a la función
        // el objeto de la petición en crudo y código de estatus de la petición
        error: function (xhr, status) {
            console.log('Disculpe, existió un problema al cargar el listado');
        }
    });
}

function AbrirModalEditar(partidoID) {

    $.ajax({
        // la URL para la petición
        url: '../../Partidos/ListadoPartidos',
        // la información a enviar
        // (también es posible utilizar una cadena de datos)
        data: { PartidoID: partidoID },
        // especifica si será una petición POST o GET
        type: 'POST',
        // el tipo de información que se espera de respuesta
        dataType: 'json',
        // código a ejecutar si la petición es satisfactoria;
        // la respuesta es pasada como argumento a la función
        success: function (listadoPartidosMostrar) {
            let listadoPartidosEditar = listadoPartidosMostrar[0]

            document.getElementById("PartidoID").value = partidoID;
            document.getElementById("JugadorID").value = listadoPartidosEditar.jugadorID;
            document.getElementById("FechaPartido").value = listadoPartidosEditar.fechaPartido;
            document.getElementById("Minutos").value = listadoPartidosEditar.minutosJugados;
            document.getElementById("Estadio").value = listadoPartidosEditar.estadio;
            $("#modalPartidos").modal("show");
            $("#tituloModalPartidos").text("Editar Partido");

        },

        // código a ejecutar si la petición falla;
        // son pasados como argumentos a la función
        // el objeto de la petición en crudo y código de estatus de la petición
        error: function (xhr, status) {
            console.log('Disculpe, existió un problema al cargar el listado');
        }
    });

}

function EliminarPartido(partidoID) {

    $.ajax({
        // la URL para la petición
        url: '../../Partidos/EliminarPartido',
        // la información a enviar
        // (también es posible utilizar una cadena de datos)
        data: { PartidoID: partidoID },
        // especifica si será una petición POST o GET
        type: 'POST',
        // el tipo de información que se espera de respuesta
        dataType: 'json',
        // código a ejecutar si la petición es satisfactoria;
        // la respuesta es pasada como argumento a la función
        success: function (eliminarPartido) {
            
            ListadoPartidos();
        },

        // código a ejecutar si la petición falla;
        // son pasados como argumentos a la función
        // el objeto de la petición en crudo y código de estatus de la petición
        error: function (xhr, status) {
            console.log('Disculpe, existió un problema al cargar el listado');
        }
    });

}