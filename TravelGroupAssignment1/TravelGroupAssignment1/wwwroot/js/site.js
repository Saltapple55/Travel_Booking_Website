// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function newElement(parentId, elementTag, elementId, html) {
	const id = document.getElementById(parentId);
	const newElement = document.createElement(elementTag);
	newElement.setAttribute('id', elementId);
	newElement.innerHTML = html;
	id.appendChild(newElement);

}
function newField(index) {
	//document.body.style.backgroundColor = "red";
    let html =`<div class="passenger">
<div class="form-group">
    <label asp-for="@Model.Passengers[${index}].FirstName" class="control-label">First Name</label>
    <input asp-for="@Model.Passengers[${index}].FirstName" class="form-control" />
    <span asp-validation-for="@Model.Passengers[${index}].FirstName" class="text-danger"></span>
</div>
<div class="form-group">
    <label asp-for="@Model.Passengers[${index}].LastName" class="control-label">Last Name</label>
    <input asp-for="@Model.Passengers[${index}].LastName" class="form-control" />
    <span asp-validation-for="@Model.Passengers[${index}].LastName" class="text-danger"></span>
</div>
<div class="form-group">
    <label asp-for="@Model.Passengers[${index}].Phone" class="control-label">Phone</label>
    <input asp-for="@Model.Passengers[${index}].Phone" class="form-control" />
    <span asp-validation-for="@Model.Passengers[${index}].Phone" class="text-danger"></span>
</div>
<div class="form-group">
    <label asp-for="@Model.Passengers[${index}].PassportNo" class="control-label">PassportNo</label>
    <input asp-for="@Model.Passengers[${index}].PassportNo" class="form-control" />
    <span asp-validation-for="@Model.Passengers[${index}].PassportNo" class="text-danger"></span>
</div>
</div>
`
	//let html="<p>clicked</p>"
    document.getElementById("add-passengers").innerHTML += html;
}
/*
jQuery(function ($)){
    $(#newPass).click(function () {
        $.ajax({
            url: '/FlightBooking/_AddPassenger',
            type: 'GET',
            success: function (result) {
                $(#add - passengers).append("<partial name=\"_AddPassenger\"></partial>")
            }
            error: function (xhr, status, error) {
                console.log(shr.responseText)
            }
        })
    })
}*/

