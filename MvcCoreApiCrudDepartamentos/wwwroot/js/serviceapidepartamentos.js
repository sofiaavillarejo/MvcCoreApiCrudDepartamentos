//CONSUMIR API EN CLIENTE
let url = "https://apicorecruddepartamentossvr.azurewebsites.net/";
function getDepartamentosAsync(callBack) {
    let request = "api/departamento";
	$.ajax({
		url: url + request,
		type: "GET",
		dataType: "json",
		success: function (data) {
			callBack(data);
		}
	})
}

function convertDeptToJson(id, nombre, loc) {
	let dept = new Object();
	dept.idDepartamento = id;
	dept.nombre = nombre;
	dept.localidad = loc;
	var json = JSON.stringify(dept);
	return json;
}

function insertDptoAsync(id, nombre, localidad, callBack) {
	let json = convertDeptToJson(id, nombre, localidad);
	var request = "api/departamento";
	$.ajax({
		url: url + request,
		type: "POST",
		data: json,
		contentType: "application/json",
		success: function () {
			callBack();
		}
	})
}

function updateDptoAsync(id, nombre, localidad, callBack) {
	let json = convertDeptToJson(id, nombre, localidad);
	var request = "api/departamento";
	$.ajax({
		url: url + request,
		type: "PUT",
		data: json,
		contentType: "application/json",
		success: function () {
			callBack();
		}
	})
}

function deleteDptoAsync(id, callBack) {
	let request = "api/departamento/" + id;
	$.ajax({
		url: url + request,
		type: "DELETE",
		success: function () {
			callBack();
		}
	})
}
/*
	function myFunctionAsync(callBack) {
		//SE REALIZAN TODAS LAS ACCIONES QUE DESEEMOS Y CUANDO HEMOS FINALIZADO Y DESEAMOS VOLVER, USAMOS CALLBACK
		callBack(parametros);
	}

	function testFunction() {
		myFunctionAsync(function (data) {
			//aqui tenemos el codigo cuando ha finalizado 
		})
	}
*/