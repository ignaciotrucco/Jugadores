window.onload = GraficoPartidos();

let graficoPartidos;

document.addEventListener("DOMContentLoaded", function () {
    const currentMonth = new Date().getMonth() + 1; // los meses recorren de 0 a 11, entonces agregamos +1
    document.getElementById("MesPartidoBuscar").value = currentMonth;
});

$("#JugadorID ,#MesPartidoBuscar, #AnioPartidoBuscar").change(function () {
    graficoPartidos.destroy();
    GraficoPartidos();
});

function GraficoPartidos() {
    let jugadorID = document.getElementById("JugadorID").value;
    let mesPartidoBuscar = document.getElementById("MesPartidoBuscar").value;
    let anioPartidoBuscar = document.getElementById("AnioPartidoBuscar").value;

    $.ajax({
        // la URL para la petición
        url: '../../Graficos/GraficoPartidosMes',
        // la información a enviar
        // (también es posible utilizar una cadena de datos)
        data: { JugadorID: jugadorID, Mes: mesPartidoBuscar, Anio: anioPartidoBuscar },
        // especifica si será una petición POST o GET
        type: 'POST',
        // el tipo de información que se espera de respuesta
        dataType: 'json',
        // código a ejecutar si la petición es satisfactoria;
        // la respuesta es pasada como argumento a la función
        success: function (partidosPorDias) {

            let labels = [];
            let data = []; 
            let diasConPartidos = 0;
            let minutosTotales = 0;

            $.each(partidosPorDias, function (index, partidoDia) { 
                labels.push(partidoDia.dia + " " + partidoDia.mes);
                data.push(partidoDia.cantidadMinutos);

                minutosTotales += partidoDia.cantidadMinutos;
                
                if (partidoDia.cantidadMinutos > 0){
                    diasConPartidos += 1;
                }
            });

            // Obtener el elemento <select>
            // var inputTipoEjercicioID = document.getElementById("TipoEjercicioID");
        
            // Obtener el texto de la opción seleccionada
            // var ejercicioNombre = inputTipoEjercicioID.options[inputTipoEjercicioID.selectedIndex].text;

            // let diasSinEjercicios = ejerciciosPorDias.length - diasConEjercicios;
            // $("#texto-card-total-ejercicios").text(minutosTotales + " MINUTOS EN " + diasConEjercicios + " DÍAS");
            // $("#texto-card-sin-ejercicios").text(diasSinEjercicios + " DÍAS SIN "+ ejercicioNombre);

            const ctx = document.getElementById('myChart');

            graficoPartidos = new Chart(ctx, {
                type: 'line',
                data: {
                    labels: labels,
                    datasets: [{
                        label: 'MINUTOS JUGADOS',
                        data: data,
                        borderWidth: 2,
                        borderRadius: 3,
                        backgroundColor: "rgba(0,129,112,0.2)",
                        borderColor: "rgba(0,129,112,1)",
                        pointRadius: 5,
                        pointBackgroundColor: "rgba(0,129,112,1)",
                        pointBorderColor: "rgba(255,255,255,0.8)",
                        pointHoverRadius: 5,
                        pointHoverBackgroundColor: "rgba(0,116,100,1)",
                        pointHitRadius: 50,
                        pointBorderWidth: 2,
                    }]
                },
                options: {
                    scales: {
                        y: {
                            beginAtZero: true
                        }
                    }
                }
            });
        },

        // código a ejecutar si la petición falla;
        // son pasados como argumentos a la función
        // el objeto de la petición en crudo y código de estatus de la petición
        error: function (xhr, status) {
            console.log('Disculpe, existió un problema al crear el gráfico.');
        }
    });
}





// const ctx = document.getElementById('myChart');

//   new Chart(ctx, {
//     type: 'bar',
//     data: {
//       labels: ['Red', 'Blue', 'Yellow', 'Green', 'Purple', 'Orange'],
//       datasets: [{
//         label: '# of Votes',
//         data: [21, 19, 3, 5, 2, 3],
//         borderWidth: 1
//       }]
//     },
//     options: {
//       scales: {
//         y: {
//           beginAtZero: true
//         }
//       }
//     }
//   });