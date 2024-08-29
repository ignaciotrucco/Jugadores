window.onload = ListadoEventos();

function ListadoEventos() {

    $.ajax({
        // la URL para la petición
        url: '../../InformeTablas/ListadoInforme',
        // la información a enviar
        // (también es posible utilizar una cadena de datos)
        data: {  },
        // especifica si será una petición POST o GET
        type: 'POST',
        // el tipo de información que se espera de respuesta
        dataType: 'json',
        // código a ejecutar si la petición es satisfactoria;
        // la respuesta es pasada como argumento a la función
        success: function (listadoInformeMostrar) {

            let contenidoTabla = ``;

            $.each(listadoInformeMostrar, function (index, listadoJugador) {

                contenidoTabla += `
                <tr>
                    <td>${listadoJugador.nombreJugador}</td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
             `;

                $.each(listadoJugador.vistaEventos, function (index, listadoEvento) {
                    contenidoTabla += `
                        <tr>
                            <td></td>
                            <td>${listadoEvento.puestoJugador}</td>
                            <td>${listadoEvento.fechaPartido}</td>
                            <td>${listadoEvento.minutosJugados}</td>
                            <td>${listadoEvento.estadioPartido}</td>
                            <td>${listadoEvento.descripcion}</td>
                        </tr>
                    `;
                })

            });

            document.getElementById("tbody-informe").innerHTML = contenidoTabla;

        },

        // código a ejecutar si la petición falla;
        // son pasados como argumentos a la función
        // el objeto de la petición en crudo y código de estatus de la petición
        error: function (xhr, status) {
            console.log('Disculpe, existió un problema al cargar el listado');
        }
    });
}