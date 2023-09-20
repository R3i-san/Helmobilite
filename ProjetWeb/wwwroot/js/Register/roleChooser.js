var form = document.getElementById("dynamicForm");
var roleField = document.getElementById("role");

roleField.addEventListener("change",
    function (event) {
        form.innerHTML = ""
        if (roleField.value === "Client") {
            customerForm()
        } else if (roleField.value === "Chauffeur") {
            driverForm()
        } else if (roleField.value === "Dispatcher") {
            dispatcherForm()
        }        

    })

function driverForm() {

    var mat = document.createElement("input")
    mat.placeholder = "Matricule"
    var select = document.createElement("select")
    var license = ["Permis B", "Permis C", "Permis CE"]

    license.forEach(function (l) {
        var o = document.createElement("option")
        o.text = l
        select.appendChild(o)
    })

    form.appendChild(mat)
    form.appendChild(select)



}


function customerForm() {
   
}


function dispatcherForm() {
    var lvl = document.createElement("input")
    lvl.placeholder = "Niveau d'etude"
    form.appendChild(lvl)

}