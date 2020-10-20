

$(document).ready(function () {
    $('#txtConductor').on('input', function () {
        this.value = this.value.replace(/[^0-9]/g, '');
    });
});

var arrayConductor = [];
function Buscar_Placa() {

    var placa = $('#txtBusquedaPlaca').val()

    $.ajax({
        url: URL_LIST_BUSCAR_PLACA,
        data: {
            placa: placa
        },
        dataType: 'json',
        success: function (result) {

            $('#txtBusquedaPlaca').val('')

            $('#table_resultado').empty()
            var strHTML = ""

            if (result.BS_PLACA == null) {

                strHTML =
                    '<tr>' +
                '<tbody' +
                    '<tr>' +
                '<tbody' +
                '<tr>' +
                '<td id="sectionEstadoIcono" colspan="18" style="width:12%;text-align:center;vertical-align: top;">' +
                 '<div style="color:#ff0c0a" id="noEncontroUnidad">' +
                        '<span style="font-size: 100px;" class="fa fa-times-circle"></span>' +
                        '<p><strong>VEHÍCULO <br>NO AUTORIZADO</strong></p>' +
                    '</div>' +
                '</td>' +
            '</tr>' +
                '/<tbody' +
                   '</tr>';
            } else {
                strHTML = '<tr>' +
                    '<tbody' +
                    '<tr>' +
                    '<td id="sectionEstadoIcono" colspan="18" style="width:12%;text-align:center;vertical-align: top;">' +
                     '<div style="color: #000000;" id="encontroUnidad" >' +
                            '<span style="font-size: 100px;" class="fa fa-check-circle"></span>' +
                            '<p><strong>VEHÍCULO <br>AUTORIZADO</strong></p>' +
                        '</div>' +
                    '</td>' +
                '</tr>' +

                 ' <tr>' +

                 ' <td style="font-weight: bolder;">Corredor</td>' +
                ' <td colspan="6" class="text-left">' +
                       ' <div>' +
                           ' <span>' + result.CORREDOR_NOMBRE + '</span>' +
                        '</div>' +
                    '</td>' +
                      '</tr>' +
              ' <tr>' +
                ' <td style="font-weight: bolder;">Placa</td>' +
                ' <td colspan="4" class="text-left">' +
                       ' <div style="width: 30%;">' +
                           ' <span>' + result.BS_PLACA + '</span>' +
                        '</div>' +
                    '</td>' +
                '</tr>' +

                 ' <tr>' +
                ' <td style="font-weight: bolder;">Concesionario</td>' +
                ' <td colspan="8" class="text-left">' +
                       ' <div>' +
                           ' <span>' + result.BS_NOM_EMPRE + '</span>' +
                        '</div>' +
                    '</td>' +
                '</tr>' +


                 ' <tr>' +
                ' <td style="font-weight: bolder;">Marca</td>' +
                ' <td colspan="6" class="text-left">' +
                       ' <div>' +
                           ' <span>' + result.BS_MARCA + '</span>' +
                        '</div>' +
                    '</td>' +
                '</tr>' +


                 ' <tr>' +
                ' <td style="font-weight: bolder;">Modelo</td>' +
                ' <td colspan="4" class="text-left">' +
                       ' <div>' +
                           ' <span>' + result.BS_MODELO + '</span>' +
                        '</div>' +
                    '</td>' +
                '</tr>' +



                 ' <tr>' +
                ' <td style="font-weight: bolder;">CVS</td>' +
                ' <td colspan="4" class="text-left">' +
                       ' <div>' +
                           ' <span>' + (result.BS_CODIGO_CVS == null ? "" : result.BS_CODIGO_CVS) + '</span>' +
                        '</div>' +
                    '</td>' +

                    ' <td style="font-weight: bolder;" >Estado</td>' +
                ' <td colspan="2" class="text-left">' +
                       ' <div style="width: 30%;">' +
                           ' <span>' + (result.BS_ESTADO == null ? "" : result.BS_ESTADO) + '</span>' +
                        '</div>' +
                    '</td>' +
                '</tr>' +
                    '/<tbody' +

                    '</tr>';
            }


            $('#table_resultado').append(strHTML);
            console.log(result, 'result')
        }
    }, JSON);
}

function Buscar_Conductor() {



    var nrodocuento = $('#txtConductor').val()


    $.ajax({
        url: URL_LIST_BUSCAR_CONDUCTOR,
        data: {
            nrodocuento: nrodocuento
        },
        dataType: 'json',
        success: function (result) {
            $('#txtConductor').val('');

            $('#table_resultado').empty()
            var strHTML = ""


            if (result.ID_ESTADO == 0) {

                strHTML =
                    '<tr>' +
                '<tbody' +
                    '<tr>' +
                '<tbody' +
                '<tr>' +
                '<td id="sectionEstadoIcono" colspan="18" style="width:12%;text-align:center;vertical-align: top;">' +
                 '<div style="color:#ff0c0a" id="noEncontroUnidad">' +
                        '<span style="font-size: 100px;" class="fa fa-times-circle"></span>' +
                        '<p><strong>CONDUCTOR O PERSONAL <br>NO AUTORIZADO</strong></p>' +
                    '</div>' +
                '</td>' +
            '</tr>' +
                '/<tbody' +
                   '</tr>';

            } else {

                if (result.DNI == null) {
                    strHTML =
                       '<tr>' +
          '<tbody' +
          '<tr>' +
          '<td id="sectionEstadoIcono" colspan="18" style="width:12%;text-align:center;vertical-align: top;">' +
           '<div style="color: #000000;" id="encontroUnidad" >' +
                  '<span style="font-size: 100px;" class="fa fa-check-circle"></span>' +
                  '<p><strong>CONDUCTOR <br>AUTORIZADO</strong></p>' +
              '</div>' +
          '</td>' +
      '</tr>' +

       ' <tr>' +

       ' <td style="font-weight: bolder;">Número de licencia</td>' +
      ' <td colspan="6" class="text-left">' +
             ' <div>' +
                 ' <span>' + result.CONNUMLIC + '</span>' +
              '</div>' +
          '</td>' +
            '</tr>' +
    ' <tr>' +
      ' <td style="font-weight: bolder;">Conductor</td>' +
      ' <td colspan="4" class="text-left">' +
             ' <div>' +
                 ' <span>' + result.APELLIDOS + ',' + result.NOMBRES + '</span>' +
              '</div>' +
          '</td>' +
      '</tr>' +

       ' <tr>' +
      ' <td style="font-weight: bolder;">Empresa</td>' +
      ' <td colspan="8" class="text-left">' +
             ' <div>' +
                 ' <span>' + result.EMPRESA + '</span>' +
              '</div>' +
          '</td>' +
      '</tr>' +


       ' <tr>' +
      ' <td style="font-weight: bolder;">Nro de Documento</td>' +
      ' <td colspan="6" class="text-left">' +
             ' <div>' +
                 ' <span>' + result.NUMDOC + '</span>' +
              '</div>' +
          '</td>' +
      '</tr>' +


       ' <tr>' +
      ' <td style="font-weight: bolder;">Tipo de lincencia </td>' +
      ' <td colspan="4" class="text-left">' +
             ' <div>' +
                 ' <span>' + result.CONTIPLIC + '</span>' +
              '</div>' +
          '</td>' +
      '</tr>';
                } else {
                    strHTML =
                         '<tr>' +
            '<tbody' +
            '<tr>' +
            '<td id="sectionEstadoIcono" colspan="18" style="width:12%;text-align:center;vertical-align: top;">' +
             '<div style="color: #000000;" id="encontroUnidad" >' +
                    '<span style="font-size: 100px;" class="fa fa-check-circle"></span>' +
                    '<p><strong>PERSONAL <br>AUTORIZADO</strong></p>' +
                '</div>' +
            '</td>' +
        '</tr>' +

         ' <tr>' +

         ' <td style="font-weight: bolder;">Nombres Completos</td>' +
        ' <td colspan="6" class="text-left">' +
               ' <div>' +
                   ' <span>' + result.NOMBRES_COMPLETOS + '</span>' +
                '</div>' +
            '</td>' +
              '</tr>' +
      ' <tr>' +
        ' <td style="font-weight: bolder;">Número de Documento</td>' +
        ' <td colspan="4" class="text-left">' +
               ' <div>' +
                   ' <span>' + result.DNI + '</span>' +
                '</div>' +
            '</td>' +
        '</tr>' +

         ' <tr>' +
        ' <td style="font-weight: bolder;">Cargo</td>' +
        ' <td colspan="8" class="text-left">' +
               ' <div>' +
                   ' <span>' + result.CARGO + '</span>' +
                '</div>' +
            '</td>' +
        '</tr>' +


         ' <tr>' +
        ' <td style="font-weight: bolder;">Empresa</td>' +
        ' <td colspan="6" class="text-left">' +
               ' <div>' +
                   ' <span>' + result.EMPRESA + '</span>' +
                '</div>' +
            '</td>' +
        '</tr>';

                }
            }

            $('#table_resultado').append(strHTML);

        }
    }, JSON);




    //$('#table_resultado').empty()
    //var strHTML = ""

    //if (array_encontrado.length<1) {

    //    strHTML =
    //        '<tr>' +
    //    '<tbody' +
    //        '<tr>' +
    //    '<tbody' +
    //    '<tr>' +
    //    '<td id="sectionEstadoIcono" colspan="18" style="width:12%;text-align:center;vertical-align: top;">' +
    //     '<div style="color:#ff0c0a" id="noEncontroUnidad">' +
    //            '<span style="font-size: 100px;" class="fa fa-times-circle"></span>' +
    //            '<p><strong>CONDUCTOR <br>NO AUTORIZADO</strong></p>' +
    //        '</div>' +
    //    '</td>' +
    //'</tr>' +
    //    '/<tbody' +
    //       '</tr>';
    //} else {
    //    strHTML = '<tr>' +
    //        '<tbody' +
    //        '<tr>' +
    //        '<td id="sectionEstadoIcono" colspan="18" style="width:12%;text-align:center;vertical-align: top;">' +
    //         '<div style="color: #000000;" id="encontroUnidad" >' +
    //                '<span style="font-size: 100px;" class="fa fa-check-circle"></span>' +
    //                '<p><strong>CONDUCTOR <br>AUTORIZADO</strong></p>' +
    //            '</div>' +
    //        '</td>' +
    //    '</tr>' +

    //     ' <tr>' +

    //     ' <td style="font-weight: bolder;">Número de licencia</td>' +
    //    ' <td colspan="6" class="text-left">' +
    //           ' <div>' +
    //               ' <span>' + array_encontrado[0].CONNUMLIC + '</span>' +
    //            '</div>' +
    //        '</td>' +
    //          '</tr>' +
    //  ' <tr>' +
    //    ' <td style="font-weight: bolder;">Conductor</td>' +
    //    ' <td colspan="4" class="text-left">' +
    //           ' <div>' +
    //               ' <span>' + array_encontrado[0].APELLIDOS + ',' + array_encontrado[0].NOMBRES + '</span>' +
    //            '</div>' +
    //        '</td>' +
    //    '</tr>' +

    //     ' <tr>' +
    //    ' <td style="font-weight: bolder;">Empresa</td>' +
    //    ' <td colspan="8" class="text-left">' +
    //           ' <div>' +
    //               ' <span>' + array_encontrado[0].EMPRESA + '</span>' +
    //            '</div>' +
    //        '</td>' +
    //    '</tr>' +


    //     ' <tr>' +
    //    ' <td style="font-weight: bolder;">Nro de Documento</td>' +
    //    ' <td colspan="6" class="text-left">' +
    //           ' <div>' +
    //               ' <span>' + array_encontrado[0].NUMDOC + '</span>' +
    //            '</div>' +
    //        '</td>' +
    //    '</tr>' +


    //     ' <tr>' +
    //    ' <td style="font-weight: bolder;">Tipo de lincencia </td>' +
    //    ' <td colspan="4" class="text-left">' +
    //           ' <div>' +
    //               ' <span>' + array_encontrado[0].CONTIPLIC + '</span>' +
    //            '</div>' +
    //        '</td>' +
    //    '</tr>' ;

    //}
}

