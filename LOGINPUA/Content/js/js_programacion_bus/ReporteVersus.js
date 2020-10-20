
var Validaciones = new validaciones();


$(document).ready(function () {
    getCorredoresByModalidad();
    $('#rangoFechaConsulta').val(FECHA_HOY)
    $('#menu-toggle').click()

});


$('#rangoFechaConsulta').datepicker({
    format: "dd/mm/yyyy"
});



function getCorredoresByModalidad() {

    var modalidadTransporteSelecionado = $('#mod').val();
    $('#selectCorredores, #selectRuta').empty();
    var cantidadRegistros = 0;

    $.each(JSON_DATA_CORREDORES, function () {
        if (this.ID_MODALIDAD_TRANS == Number(modalidadTransporteSelecionado)) {
            cantidadRegistros++;
            $('#selectCorredores').append('<option value="' + this.ID_CORREDOR + '">' + this.ABREVIATURA + '</option>');
            $('#selectCorredores_dos').append('<option value="' + this.ID_CORREDOR + '">' + this.ABREVIATURA + '</option>');

        }
    });
    if (cantidadRegistros == 0) {
        $('#selectCorredores, #selectRuta').append('<option value="0">' + '-- No hay información --' + '</option>');
        $('#tbRutatipoServicio tbody').empty();
        return false
    }
    //$('#selectCorredores').val(4); //DURACEL PARA PRUEBAS EN CORREDOR
    getRutaPorCorredor_dos();
}


function getRutaPorCorredor_dos() {

    $.ajax({
        url: URL_GET_RUTA_X_CORREDOR,
        dataType: 'json',
        data: { idCorredor: $('#selectCorredores_dos').val() },
        success: function (result) {
            $('#selectRuta_dos').empty();
            if (result.length == 0) { // si la lista esta vacia
                $('#selectRuta_dos').append('<option value="0">' + '--No hay información--' + '</option>');
                return false;
            } else {
                $.each(result, function () {
                    $('#selectRuta_dos').append('<option value="' + this.ID_RUTA + '">' + this.NRO_RUTA + '</option>');
                });
            }
        }, error: function (xhr, status, error) {

            Validaciones.ValidarSession();
        },
    }, JSON);
}





function Listar_Viajes() {

    $('#msj_fecha').empty();

    strHTML = "";

    $('#tbtable_Viajes tbody').empty();
    $('.progress').removeClass('hide');

    var data = [];
    for (var i = 0; i < 100000; i++) {
        var tmp = [];
        for (var i = 0; i < 100000; i++) {
            tmp[i] = 'hue';
        }
        data[i] = tmp;
    };
    $.ajax({
        xhr: function () {
            var xhr = new window.XMLHttpRequest();
            xhr.upload.addEventListener("progress", function (evt) {
                if (evt.lengthComputable) {
                    var percentComplete = evt.loaded / evt.total;
                    console.log(percentComplete);
                    $('.progress').css({
                        width: percentComplete * 100 + '%'
                    });
                    if (percentComplete === 1) {
                        $('.progress').addClass('hide');
                    }
                }
            }, false);
            xhr.addEventListener("progress", function (evt) {
                if (evt.lengthComputable) {
                    var percentComplete = evt.loaded / evt.total;
                    console.log(percentComplete);
                    $('.progress').css({
                        width: percentComplete * 100 + '%'
                    });
                }
            }, false);
            return xhr;
        },
        url: URL_GET_DATA_VIAJES,
        data: {
            id_ruta: $('#selectRuta_dos').val(),
            fecha: $('#rangoFechaConsulta').val()
        },
        dataType: 'json',
        type: 'POST',
        success: function (result) {

            if (result.Table.length == 0) {
                $('#tbtable_Viajes tbody').append('<tr><td colspan="24">No existe información para esta fecha.</td></tr>');
                return false
            }

            var agrupadoPorServicio = _.groupBy(result.Table, function (d) { return d.SERVICIO })  //agrupado por fechas la data del comparativo A
            var ViajesServicio = []


            //AGRUPAR TIPO DE SERVICIO 
            var Lista_TipoServicios = _.uniqBy(result.Table, function (e) {
                return e.TIPO_SERVICIO;
            });

            var tiposerv_text = "";

            $.each(Lista_TipoServicios, function () {
                tiposerv_text += ' <div class="col-md-3">  <h6>' + this.TIPO_SERVICIO + '</h6> <p class=" col-sm-3 cuadrado" style="margin-left: 34%;margin-top: -2%;background:' + this.COLOR + '"> </p></div>';

            });
            $('#msj_fecha').append(tiposerv_text);


            var i = 0

            //AGRUPAR VIAJE POR SERVICIO Y LADO   
            $.each(agrupadoPorServicio, function (key, arrViajesServ) {

                var LADOS = { Ladoa: [], ladob: [], servicio: [], lados: [] }

                var Servicio = []
                var item = {}
                var item_lados = {}
                var unidad = []
                var empresa = []
                var conductor = []
                var fila_nueva = []
                var viaje_numA = 0
                var viaje_numB = 0
                $.each(arrViajesServ, function (key2, ArraySalida) {

                    item_lados = {
                        HSALIDA: ArraySalida.HSALIDA,
                        CAC: ArraySalida.CAC,
                        LADO: ArraySalida.LADO,
                        SERVICIO: ArraySalida.SERVICIO,
                        HORA_SALIDA: ArraySalida.HORA_SALIDA,
                        COLOR: ArraySalida.COLOR,
                        UNIDAD: ArraySalida.PLACA,
                        EMPRESA: ArraySalida.EMPRESA,
                        CONDUCTOR: ArraySalida.CAC + " - " + ArraySalida.APELLIDOS + ", " + ArraySalida.NOMBRES,
                        OBSERVACIONES: ArraySalida.OBSERVACIONES
                    }

                    LADOS.lados.push(item)

                    if (ArraySalida.LADO == 'A') {
                        viaje_numA++;
                        item = {
                            HSALIDA: ArraySalida.HSALIDA,
                            CAC: ArraySalida.CAC,
                            LADO: ArraySalida.LADO,
                            SERVICIO: ArraySalida.SERVICIO,
                            HORA_SALIDA: ArraySalida.HORA_SALIDA,
                            COLOR: ArraySalida.COLOR,
                            UNIDAD: ArraySalida.PLACA,
                            EMPRESA: ArraySalida.EMPRESA,
                            CONDUCTOR: ArraySalida.CAC + " - " + ArraySalida.APELLIDOS + ", " + ArraySalida.NOMBRES,
                            NUM_VIAJE: viaje_numA,
                            OBSERVACIONES: ArraySalida.OBSERVACIONES

                        }

                        LADOS.Ladoa.push(item)
                    } else {
                        viaje_numB++
                        item = {

                            HSALIDA: ArraySalida.HSALIDA,
                            CAC: ArraySalida.CAC,
                            LADO: ArraySalida.LADO,
                            SERVICIO: ArraySalida.SERVICIO,
                            HORA_SALIDA: ArraySalida.HORA_SALIDA,
                            COLOR: ArraySalida.COLOR,
                            UNIDAD: ArraySalida.PLACA,
                            EMPRESA: ArraySalida.EMPRESA,
                            CONDUCTOR: ArraySalida.CAC + " - " + ArraySalida.APELLIDOS + ", " + ArraySalida.NOMBRES,
                            NUM_VIAJE: viaje_numB,
                            OBSERVACIONES: ArraySalida.OBSERVACIONES

                        }

                        LADOS.ladob.push(item)
                    }
                    Servicio.push(ArraySalida.SERVICIO)
                });


                var uniqueval = Servicio.filter(function (value, index, self) {
                    return self.indexOf(value) === index;
                });

                LADOS.servicio.push(uniqueval)
                ViajesServicio.push(LADOS)
                i++;
            });

            //FILA DE VIAJES POR SERVICIO 

            var ruta = ""
            var servicio_P = 0
            $.each(ViajesServicio, function (key, arrayMaestro) {//ORDENADO POR SERVICIOS


                var count_tdA = 0
                var count_tdB = 0
                var n_a = 0
                var n_b = 0
                var n_Hora_SALIDA_A = 0
                var n_Hora_SALIDA_B = 0

                //agrupado por fechas la data del comparativo A 

                var column_num = 0
                var count_tdA_P = 0;
                var count_tdB_P = 0;



                // ------------------------------// OBTENER PROGRAMADOS 
                $.each(arrayMaestro, function (key2, arrayHijo) {//ORDENADO POR LADOS 
                    //console.log(arrayHijo, 'arrayHijo')

                    //AGREGAR LADO A
                    if (key2 === "Ladoa") {
                        td_salidasA = ""
                        $.each(arrayHijo, function (key3, arrayNieto) {
                            td_salidasA += "<td  style='background:" + (arrayNieto.COLOR == null ? "" : arrayNieto.COLOR) + "'>" + arrayNieto.HSALIDA + "</td>"
                            if (arrayNieto.HORA_SALIDA != null) {
                                n_Hora_SALIDA_A++
                            }
                            count_tdA_P++;
                            n_a++;
                        });
                    }
                    //AGREGAR LADO B
                    if (key2 === "ladob") {
                        td_salidasB = ""
                        $.each(arrayHijo, function (key3, arrayNieto) {
                            td_salidasB += "<td  style='background:" + (arrayNieto.COLOR == null ? "" : arrayNieto.COLOR) + "'>" + arrayNieto.HSALIDA + "</td>"
                            if (arrayNieto.HORA_SALIDA != null) {
                                n_Hora_SALIDA_B++
                            }
                            n_b++;
                            count_tdB_P++;
                        });
                    }
                });

                var agregar_tdA = ""
                var agregar_tdB = ""

                //AGREGAR VIAJES VACIOS QUE FALTAN 
                if (count_tdA_P < 9) {
                    count_tdA_P = 9 - count_tdA_P
                    for (var i = 0; i < count_tdA_P; i++) {
                        agregar_tdA += "<td></td>"
                    }
                }

                if (count_tdB_P < 9) {
                    count_tdB_P = 9 - count_tdB_P
                    for (var i = 0; i < count_tdB_P; i++) {
                        agregar_tdB += "<td></td>"
                    }
                }
                servicio_P++

                var servicio_PR = 0
                if (servicio_P < 10) {
                    servicio_PR = "0" + servicio_P;
                } else {
                    servicio_PR = servicio_P;
                }
                ruta = $('#selectRuta_dos option:selected').text()


                strHTML = "<tr style='border-block-start: 4px solid #19459d;background-color: white;'><td>" + ruta + servicio_PR + "</td><td>" + servicio_P + "</td><td></td><td></td><td></td><td>P</td>" + td_salidasA + agregar_tdA + td_salidasB + agregar_tdB + "<td>" + n_a + "</td><td>" + n_b + "</td></tr>";

                $('#tbtable_Viajes tbody').append(strHTML);

                td_salidasA = ""
                td_salidasB = ""
                count_tdA_P = 0
                count_tdB_P = 0

                n_a = 0
                n_b = 0
                n_Hora_SALIDA_A = 0
                n_Hora_SALIDA_B = 0



                //------------------------------------------------------------ VIAJES PROGRAMADOS 
                var agrupadoPorPlaca = _.groupBy(arrayMaestro.lados, function (d) {
                    return d.UNIDAD
                })


                var Servicio = 0
                var count_service = 0;
                $.each(agrupadoPorPlaca, function (column, rows) {
                    var ruta = ""
                    td_hora_salidaA = ""
                    td_hora_salidaB = ""
                    var counttdA = 0;
                    var counttdB = 0;
                    var Observaciones_TotalA = ""
                    var Observaciones_TotalB = ""

                    var observaciones = ""

                    var numviaje_inicioA = 0;
                    var numviaje_inicioB = 0;

                    var totalTD = ""
                    var td1A = "<td></td>"
                    var td2A = "<td></td>"
                    var td3A = "<td></td>"
                    var td4A = "<td></td>"
                    var td5A = "<td></td>"
                    var td6A = "<td></td>"
                    var td7A = "<td></td>"
                    var td8A = "<td></td>"

                    var td1B = "<td></td>"
                    var td2B = "<td></td>"
                    var td3B = "<td></td>"
                    var td4B = "<td></td>"
                    var td5B = "<td></td>"
                    var td6B = "<td></td>"
                    var td7B = "<td></td>"
                    var td8B = "<td></td>"
                    if (column != "undefined" && column != "null") {

                        var rows_total = rows.length - 1;
                        $.each(rows, function (key2, arrayHijo) {

                            if (this.HORA_SALIDA != null) {
                                var conductor = this.UNIDAD
                                Servicio = this.SERVICIO

                                if (this.LADO == "A") {
                                    counttdA++;
                                    Observaciones_TotalA += " •  " + "<b>" + this.HORA_SALIDA + "</b>" + " - " + (this.OBSERVACIONES == null ? "<b>No tiene Observaciones </b>" : this.OBSERVACIONES) + "</br>"


                                    observaciones = (this.OBSERVACIONES == null ? "" : '<button data-html="true"  type="button" style="float: right;margin-top: -12px;border-width: 2px;border-style: groove;" class="btn btn-primary btn-sm" data-toggle="popover"  data-trigger="focus" title="Observaciones" data-content="' + this.OBSERVACIONES + '"></button>')
                                    td_hora_salidaA = "<td style='height: 53px;'>" + observaciones + this.HORA_SALIDA + "</td>"
                                    numviaje_inicioA = this.NUM_VIAJE

                                    if (numviaje_inicioA == 1) {
                                        td1A = td_hora_salidaA;
                                    } else if (numviaje_inicioA == 2) {
                                        td2A = td_hora_salidaA;
                                    } else if (numviaje_inicioA == 3) {
                                        td3A = td_hora_salidaA;
                                    } else if (numviaje_inicioA == 4) {
                                        td4A = td_hora_salidaA;
                                    } else if (numviaje_inicioA == 5) {
                                        td5A = td_hora_salidaA;
                                    } else if (numviaje_inicioA == 6) {
                                        td6A = td_hora_salidaA;
                                    } else if (numviaje_inicioA == 7) {
                                        td7A = td_hora_salidaA;
                                    }
                                    else if (numviaje_inicioA == 8) {
                                        td8A = td_hora_salidaA;
                                    }

                                } else {
                                    Observaciones_TotalB += " •  " + "<b>" + this.HORA_SALIDA + "</b>" + " - " + (this.OBSERVACIONES == null ? "<b>No tiene Observaciones </b>" : this.OBSERVACIONES) + "</br>"

                                    counttdB++;

                                    td_hora_salidaB = "<td style='height: 53px;'> " + this.HORA_SALIDA + "</td>"
                                    numviaje_inicioB = this.NUM_VIAJE

                                    if (numviaje_inicioB == 1) {
                                        td1B = td_hora_salidaB;
                                    } else if (numviaje_inicioB == 2) {
                                        td2B = td_hora_salidaB;
                                    } else if (numviaje_inicioB == 3) {
                                        td3B = td_hora_salidaB;
                                    } else if (numviaje_inicioB == 4) {
                                        td4B = td_hora_salidaB;
                                    } else if (numviaje_inicioB == 5) {
                                        td5B = td_hora_salidaB;
                                    } else if (numviaje_inicioB == 6) {
                                        td6B = td_hora_salidaB;
                                    } else if (numviaje_inicioB == 7) {
                                        td7B = td_hora_salidaB;
                                    }
                                    else if (numviaje_inicioB == 8) {
                                        td8B = td_hora_salidaB;
                                    }

                                }

                            } else {
                                return
                            }

                            var empresa = (this.EMPRESA == null ? "" : this.EMPRESA)
                            var conductor = this.CONDUCTOR
                            var textochicoconductor = conductor

                            if (conductor == "1 - null, null") {
                                conductor = "";
                                textochicoconductor = ""
                            } else if (conductor == "2 - null, null") {
                                conductor = "<span style='font-weight: bold;font-size: 13px;'> CONDUCTOR SIN CAC</span>";
                                textochicoconductor = ""
                            }

                            var disabled = ""

                            //if (rows_total == key2) {
                            //    if (this.OBSERVACIONES ==null) {
                            //        disabled = "disabled"
                            //    } else {
                            //        disabled = ""
                            //    }
                            //}



                            var BUTTONA = '<button ' + disabled + ' type="button" class="text-center btn btn-primary btn-sm" data-toggle="popover" data-trigger="focus" title="Observaciones" data-content="' + Observaciones_TotalA + '"> <i class="fas fa-eye"></i></button>'
                            var BUTTONB = '<button ' + disabled + 'type="button" class="text-center btn btn-primary btn-sm" data-toggle="popover" data-trigger="focus" title="Observaciones" data-content="' + Observaciones_TotalB + '"> <i class="fas fa-eye"></i></button>'


                            totalTD = "<td>" + this.SERVICIO + "</td><td>" + this.UNIDAD + "</td><td class='textoAchicado' title='" + empresa + "'>" + empresa + "</td><td class='textoAchicado' title='" + textochicoconductor + "'>" + conductor + "</td><td style='color:red'>M</td>" + td1A + td2A + td3A + td4A + td5A + td6A + td7A + td8A + "<td>" + BUTTONA + "</td>" + td1B + td2B + td3B + td4B + td5B + td6B + td7B + td8B + "<td>" + BUTTONB + "</td>" + "<td style='color:red'>" + counttdA + "</td><td style='color:red'>" + counttdB + "</td>";

                        });

                        if (totalTD != "") {
                            if (count_service == 0) {
                                ruta = $('#selectRuta_dos option:selected').text()
                            } else {
                                ruta = "CB-";
                            }
                            count_service++;

                            $('#tbtable_Viajes tbody').append("<tr style='background-color: #efeeed;'>" + "<td>" + ruta + Servicio + "</td>" + totalTD + "</tr>");
                        }


                    }
                })
            });

            $('#tbtable_Viajes').children('caption').remove()

            $('[data-toggle="popover"]').popover({
                placement: 'right',
                html: true
            });

            var fecha = new Date(); //Fecha actual
            var mes = fecha.getMonth() + 1; //obteniendo mes
            var dia = fecha.getDate(); //obteniendo dia
            var ano = fecha.getFullYear(); //obteniendo año

            $("#tbtable_Viajes").tableExport({
                formats: ["xlsx"], //Tipo de archivos a exportar ("xlsx","txt", "csv", "xls")
                position: 'top',  // Posicion que se muestran los botones puedes ser: (top, bottom)
                bootstrap: false,//Usar lo estilos de css de bootstrap para los botones (true, false)
                fileName: "Reporte_Comparativo" + dia + '-' + mes + '-' + ano,    //Nombre del archivo 
            });
            $('#tbtable_Viajes').children('caption').children().removeClass()
            $('#tbtable_Viajes').children('caption').children().addClass("btn btn-success btn-sm")
        }, error: function (xhr, status, error) {

            Validaciones.ValidarSession();
        },
    }, JSON);
}
