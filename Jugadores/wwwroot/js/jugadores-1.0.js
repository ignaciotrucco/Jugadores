window.onload = ListadoJugadores();

function ListadoJugadores() {

    $.ajax({
        url: '../../Jugadores/ListadoJugadores',
        data: {},
        type: 'GET',
        dataType: 'json',
        success: function (jugadores) {

            $("#modalJugadores").modal('hide');
            LimpiarModal();

            let contenidoTabla = ``;

            $.each(jugadores, function (index, jugador) {

                contenidoTabla += `
                <tr>
                    <td>${jugador.nombre}</td>
                    <td>${jugador.puesto}</td>
                    <td>${jugador.anioNacimiento}</td>
                    <td>
                        <button type="button" class="btn btn-success" onclick="ModalEditar(${jugador.jugadorID})">
                            Editar
                        </button>
                    </td>
                    <td>
                        <button type="button" class="btn btn-danger" onclick="ValidacionEliminar(${jugador.jugadorID})">
                            Eliminar
                        </button>
                    </td>
                </tr>
                `;

            });
            document.getElementById("tbody-jugadores").innerHTML = contenidoTabla;

        },
        error: function (xhr, status) {
            console.log('Pagina no encontrada');
        }
    });
}

function LimpiarModal() {
    document.getElementById("NombreJugador").value = "";
    document.getElementById("PuestoJugador").value = "";
    document.getElementById("AnioNacimiento").value = null;
}

function NuevoJugador() {
    $("#tituloModal").text("Agregar nuevo jugador");
}

function GuardarJugador() {
    let jugadorID = document.getElementById("JugadorID").value;
    let nombre = document.getElementById("NombreJugador").value;
    let puesto = document.getElementById("PuestoJugador").value;
    let nacimiento = document.getElementById("AnioNacimiento").value;

    $.ajax({
        url: '../../Jugadores/GuardarJugador',
        data: { JugadorID: jugadorID, Nombre: nombre, Puesto: puesto, AnioNacimiento: nacimiento },
        type: 'POST',
        dataType: 'json',
        success: function (resultado) {
            if (resultado != "") {
                alert(resultado);
            }
            ListadoJugadores();
        },
        error: function (xhr, status) {
            console.log('Pagina no encontrada');
        }
    });
}

function ModalEditar(jugadorID) {
    $.ajax({
        url: '../../Jugadores/ListadoJugadores',
        data: { JugadorID: jugadorID },
        type: 'GET',
        dataType: 'json',
        success: function (jugadores) {

            let jugador = jugadores[0]

            document.getElementById("JugadorID").value = jugadorID;
            $("#tituloModal").text("Editar jugador");
            document.getElementById("NombreJugador").value = jugador.nombre;
            document.getElementById("PuestoJugador").value = jugador.puesto;
            document.getElementById("AnioNacimiento").value = jugador.anioNacimiento;
            $("#modalJugadores").modal('show');

        },
        error: function (xhr, status) {
            console.log('Pagina no encontrada');
        }
    });
}

function ValidacionEliminar(jugadorID) {
    var siElimina = confirm("¿Seguro que desea eliminar este jugador?")
    if (siElimina == true) {
        EliminarJugador(jugadorID)
    }
}

function EliminarJugador(jugadorID) {
    $.ajax({
        url: '../../Jugadores/EliminarJugador',
        data: { JugadorID: jugadorID },
        type: 'GET',
        dataType: 'json',
        success: function (resultado) {
            
            if(!resultado) {
                alert("No se puede eliminar este jugador porque ya existe en otra tabla")
            }
            ListadoJugadores();
        },
        error: function (xhr, status) {
            console.log('Pagina no encontrada');
        }
    });
}