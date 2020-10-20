function Util() {

}

Util.prototype.ConvertTimeformat24H = function (time) {
    var hours = Number(time.match(/^(\d+)/)[1]);
    var minutes = Number(time.match(/:(\d+)/)[1]);
    var AMPM = time.match(/\s(.*)$/)[1];
    if (AMPM == "PM" && hours < 12) hours = hours + 12;
    if (AMPM == "AM" && hours == 12) hours = hours - 12;
    var sHours = hours.toString();
    var sMinutes = minutes.toString();
    if (hours < 10) sHours = "0" + sHours;
    if (minutes < 10) sMinutes = "0" + sMinutes;

    return sHours + ":" + sMinutes;
};


Util.prototype.formatAMPM = function (timestamp) {

    var date = new Date(timestamp);

    var hours = date.getHours();
    var minutes = date.getMinutes();
    var ampm = hours >= 12 ? 'PM' : 'AM';
    hours = hours % 12;
    hours = hours ? hours : 12; // the hour '0' should be '12'
    hours = hours < 10 ? '0' + hours : hours;
    minutes = minutes < 10 ? '0' + minutes : minutes;

    var strTime = (date.getDay() < 10 ? "0" + date.getDay() : date.getDay()) + '/' +
                  (date.getMonth() < 10 ? "0" + date.getMonth() : date.getMonth()) + '/' +
                    date.getFullYear() + ' ' + hours + ':' + minutes + ' ' + ampm;
    return strTime;
};



Util.prototype.convertDatetoTimeStamp = function (fechahora) { //  hh:ss:ii to
    if (!fechahora ? true : fechahora.length == 0) { return; }
    var dateString = fechahora,
    dateTimeParts = dateString.split(' '),
    timeParts = dateTimeParts[1].split(':'),
    dateParts = dateTimeParts[0].split('/'),
    date;
    date = new Date(dateParts[2], parseInt(dateParts[1], 10) - 1, dateParts[0], timeParts[0], timeParts[1], timeParts[2]);
    var fechaentimestamp = date.getTime()
    return fechaentimestamp;
};

Util.prototype.convertTimestampToDate = function (timestamp) { // ####### to  dd/mm/yyyy hh:ss:ii
    var dia = new Date(timestamp);
    var dd = dia.getDate();
    var mm = dia.getMonth() + 1; //January is 0!
    var yyyy = dia.getFullYear();

    var hh = dia.getHours();
    var ii = dia.getMinutes();
    var ss = dia.getSeconds();

    if (dd < 10) {
        dd = '0' + dd;
    }
    if (mm < 10) {
        mm = '0' + mm;
    }
    if (hh < 10) {
        hh = '0' + hh;
    }
    if (ii < 10) {
        ii = '0' + ii;
    }
    if (ss < 10) {
        ss = '0' + ss;
    }
    var today = dd + '/' + mm + '/' + yyyy + ' ' + hh + ':' + ii + ':' + ss;
    return today;
};

Util.prototype.obtenerHorasEnTimestamp = function (hinicio, hfin, salto) {
    var rpta = [];
    var fhorainicialtimestamp = Util.prototype.convertDatetoTimeStamp(hinicio);
    var fhorafinaltimestamp = Util.prototype.convertDatetoTimeStamp(hfin);
    var stopgenerahoras = true;
    var horaenmilisegundos = 0;
    //
    var nuevoSalto = 0;
    nuevoSalto = salto - 1;
    var _turno = 'MAÑANA';
    while (stopgenerahoras) {
        var horaMinima = fhorainicialtimestamp + horaenmilisegundos;
        horaenmilisegundos += (60000 * nuevoSalto);
        var nuevoTiempo = horaMinima + (60000 * nuevoSalto);
        var item = {
            inicioTimestamp: horaMinima,
            finTimestamp: nuevoTiempo,
            hInicio: Util.prototype.convertTimestampToDate(horaMinima).split(' ')[1],
            hFin: Util.prototype.convertTimestampToDate(nuevoTiempo).split(' ')[1],
            turno: _turno,
            arrViajes: []
        }
        if (horaenmilisegundos >= 32340000 || horaenmilisegundos >= 30540000 || horaenmilisegundos >= 29640000) { //medio dia en 1h,30min,15min
            item.turno = 'TARDE';
        }
        rpta.push(item);
        horaenmilisegundos += 60000; //aumenta 1 minuto en la hora del inicio
        if (horaMinima >= fhorafinaltimestamp) {
            stopgenerahoras = false;
        }
    }
    return rpta;
};

Util.prototype.obtenerHorasEnTimestampValPasajero = function (hinicio, hfin, salto) {
    var rpta = [];
    var fhorainicialtimestamp = Util.prototype.convertDatetoTimeStamp(hinicio);
    var fhorafinaltimestamp = Util.prototype.convertDatetoTimeStamp(hfin);
    var stopgenerahoras = true;
    var horaenmilisegundos = 0;
    //
    var nuevoSalto = 0;
    nuevoSalto = salto - 1;
    var _turno = 'MAÑANA';
    while (stopgenerahoras) {
        var horaMinima = fhorainicialtimestamp + horaenmilisegundos;
        horaenmilisegundos += (60000 * nuevoSalto);
        var nuevoTiempo = horaMinima + (60000 * nuevoSalto);
        var item = {
            hInicio: Util.prototype.convertTimestampToDate(horaMinima).split(' ')[1],
            hFin: Util.prototype.convertTimestampToDate(nuevoTiempo).split(' ')[1],
            arrViajes: []
        }
        //if (horaenmilisegundos >= 32340000 || horaenmilisegundos >= 30540000 || horaenmilisegundos >= 29640000) { //medio dia en 1h,30min,15min
        //    item.turno = 'TARDE';
        //}
        rpta.push(item);
        horaenmilisegundos += 60000; //aumenta 1 minuto en la hora del inicio
        if (horaMinima >= fhorafinaltimestamp) {
            stopgenerahoras = false;
        }
    }
    return rpta;
};

Util.prototype.obtenerHorasEnTimestampPOG = function (hinicio, hfin, salto) {
    var rpta = [];
    var fhorainicialtimestamp = Util.prototype.convertDatetoTimeStamp(hinicio);
    var fhorafinaltimestamp = Util.prototype.convertDatetoTimeStamp(hfin);
    var stopgenerahoras = true;
    var horaenmilisegundos = 0;
    //
    var nuevoSalto = 0;
    nuevoSalto = salto - 1;
    var _turno = 'MAÑANA';
    while (stopgenerahoras) {
        var horaMinima = fhorainicialtimestamp + horaenmilisegundos;
        horaenmilisegundos += (60000 * nuevoSalto);
        var nuevoTiempo = horaMinima + (60000 * nuevoSalto);
        var item = {
            inicioTimestamp: horaMinima,
            finTimestamp: nuevoTiempo,
            hInicio: Util.prototype.convertTimestampToDate(horaMinima).split(' ')[1],
            hFin: Util.prototype.convertTimestampToDate(nuevoTiempo).split(' ')[1],
            turno: _turno,
            arrViajes: [],
            CamposPOG: []
        }
        if (horaenmilisegundos >= 32340000 || horaenmilisegundos >= 30540000 || horaenmilisegundos >= 29640000) { //medio dia en 1h,30min,15min
            item.turno = 'TARDE';
        }
        rpta.push(item);
        horaenmilisegundos += 60000; //aumenta 1 minuto en la hora del inicio
        if (horaMinima >= fhorafinaltimestamp) {
            stopgenerahoras = false;
        }
    }
    return rpta;
};

Util.prototype.decimalTommss = function (minutes) {
    var sign = minutes < 0 ? "-" : "";
    var min = Math.floor(Math.abs(minutes));
    var sec = Math.floor((Math.abs(minutes) * 60) % 60);
    return sign + (min < 10 ? "0" : "") + min + ":" + (sec < 10 ? "0" : "") + sec;
};

Util.prototype.getRandomColor = function () {
    var letters = '0123456789ABCDEF';
    var color = '#';
    for (var i = 0; i < 6; i++) {
        color += letters[Math.floor(Math.random() * 16)];
    }
    return color;
};

Util.prototype.guardarJson = function (n, t) { var i = new Date, u = t + i.getDate() + "/" + (i.getMonth() + 1) + "/" + i.getFullYear() + " " + i.getHours() + ":" + i.getMinutes() + ":" + i.getSeconds(); if (!n) { console.error("Console.save: No data"); return } u || (u = "console.json"), typeof n == "object" && (n = JSON.stringify(n, undefined, 4)); var e = new Blob([n], { type: "text/json" }), f = document.createEvent("MouseEvents"), r = document.createElement("a"); r.download = u, r.href = window.URL.createObjectURL(e), r.dataset.downloadurl = ["text/json", r.download, r.href].join(":"), f.initMouseEvent("click", !0, !1, window, 0, 0, 0, 0, 0, !1, !1, !1, !1, 0, null), r.dispatchEvent(f) }

Util.prototype.activarEnumeradoTabla = function (idTabla, botonActivaBusqueda) { //util para enumerado y busqueda de las tablas
    var posicionRowBusqueda = 0;
    var _posicionRowBusqueda = 0;

    var _idTHead = idTabla + ' thead';
    var _elementBusquedaTR = idTabla + ' thead > tr';
    var _activoBusqueda = null;
    //
    var _idTfoot = idTabla + ' tfoot';
    //
    activoBusqueda = false;
    //
    $.each($(_elementBusquedaTR), function (i) {
        _activoBusqueda = ($(this).find('th').find('input').length > 0 ? true : false);
        _posicionRowBusqueda = i;
    });
    //console.log('_activoBusqueda', _activoBusqueda);
    if (_activoBusqueda) {
        //alert("ya existe");
        $(_idTHead).find('tr').eq(_posicionRowBusqueda).remove();//borrando los input de busqueda 
        $(_idTfoot).remove();
        //borrando los enumerados de la tabla
    }

    $(idTabla).fancyTable({
        //sortColumn: 0,// column number for initial sorting
        //sortOrder: 'descending',// 'desc', 'descending', 'asc', 'ascending', -1 (descending) and 1 (ascending)
        //sortable: true,
        pagination: true,// default: false
        searchable: true,
        inputPlaceholder: "Buscar...",
        perPage: 15,
        // globalSearch: true,
        //globalSearchExcludeColumns: [2, 5]// exclude column 2 & 5
    });
    var idTHead = idTabla + ' thead';
    var elementBusquedaTR = idTabla + ' thead > tr';
    $.each($(elementBusquedaTR), function (i) {
        activoBusqueda = ($(this).find('th').find('input').length > 0 ? true : false);
        posicionRowBusqueda = i;
    });
    $(idTHead).find('tr').eq(posicionRowBusqueda).css('display', 'none')
    //agregandole el estado de la busqueda como un atributo de la tabla
    $(idTabla).attr('data-estadobusqueda', 'false');

    var eventosEnElemento = null;
    eventosEnElemento = $._data(botonActivaBusqueda[0], 'events');
    //
    if (eventosEnElemento) {
        $.each(eventosEnElemento.click, function (i) {
            eventosEnElemento.click.splice(i, 1)
        });
    }
    //
    botonActivaBusqueda.click(function () {
        var dataBusqueda = ($(idTabla).attr('data-estadobusqueda') == 'true' ? true : false);
        if (dataBusqueda) {
            $(this).css({
                'background-color': '#17a2b8',
                'border-color': '#17a2b8',
            });
            $(idTHead).find('tr').eq(posicionRowBusqueda).css('display', 'none');
        } else {
            $(this).css({
                'background-color': '#138496',
                'border-color': '#117a8b',
            });
            $(idTHead).find('tr').eq(posicionRowBusqueda).css('display', 'table-row')
        }
        $(idTabla).attr('data-estadobusqueda', (dataBusqueda ? 'false' : 'true'));
    });
};


Util.prototype.timeToDecimal = function (t) { 
    return t.split(':')
           .map(function (val) { return parseInt(val, 10); })
           .reduce(function (previousValue, currentValue, index, array) {
               return previousValue + currentValue / Math.pow(60, index);
           });
};

Util.prototype.decimaltoTime = function (decimalTimeString) {
 

    var decimalTime = parseFloat(decimalTimeString);
    decimalTime = decimalTime * 60 * 60;
    var hours = Math.floor((decimalTime / (60 * 60)));
    decimalTime = decimalTime - (hours * 60 * 60);
    var minutes = Math.floor((decimalTime / 60));
    decimalTime = decimalTime - (minutes * 60);
    var seconds = Math.round(decimalTime);
    if (hours < 10) {
        hours = "0" + hours;
    }
    if (minutes < 10) {
        minutes = "0" + minutes;
    }
    if (seconds < 10) {
        seconds = "0" + seconds;
    }
    var tiempo = "" + hours + ":" + minutes + ":" + seconds
    return tiempo;
};

 

Util.prototype.timeRedondear = function (time) {

    var minutos = 0
    var tiemposeparado = time.split(':')
    var hora = tiemposeparado[0];

    var tiempofinal = "";
    minutos = parseInt(tiemposeparado[1])

    if (tiemposeparado[2] > 59) {
        minutos = minutos + 1
        if (minutos > 9) {
            tiempofinal = hora + ":" + minutos
        } else {
            tiempofinal = hora + ":0" + minutos
        }
    } else {
        if (minutos > 9) {
            tiempofinal = hora + ":" + minutos
        } else {
            tiempofinal = hora + ":0" + minutos
        }
    }
    return tiempofinal
};

 