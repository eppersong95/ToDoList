$(document).ready(function () {
    //process toast messages here
});

function changeStatusOfItem(id, isChecked) {
    $.post("https://localhost:44307/ToDo/ChangeItemStatus",
        {
            itemID: parseInt(id),
            isComplete: isChecked
        },
        function (data) {
            if (data.isSuccess === "1") {
                makeToast(true, "Item status updated");
            } else {
                makeToast(false, "Unable to update item status");
            }
        });
}

function deleteItem(id) {
    $.ajax({
        url: '/ToDo/DeleteToDoItem',
        type: 'DELETE',
        data: {
            itemID: parseInt(id)
        },
        success: function (result) {
            if (result.isSuccess === "true") {
                makeToast(true, "Item deleted");
            } else {
                makeToast(false, "Unable to delete item");
            }
        }
    });
}

function editItem(id) {
    window.location.href = "https://localhost:44307/ToDo/AddEditToDoItem/" + id;
}

function makeToast(isSuccess, message) {
    if (isSuccess) {
        toastr.success(message, "Success!");
    } else {
        toastr.error(message, "Error");
    }
}