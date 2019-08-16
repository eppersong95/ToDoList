$(document).ready(function () {
    $('#sortSelection').change(function () {
        queryList($(this).val());
    });

    queryList($('#sortSelection').val());
});

function queryList(sortId) {
    $.ajax({
        url: '/ToDo/GetToDoList',
        type: 'GET',
        data: {
            sortID: parseInt(sortId)
        },
        success: function (result) {
            if (result.length == 0) {
                $('#toDoList').html("<h2> There's nothing here. Add a todo item now! </h2>");
            } else {
                buildList(result);
            }
        }
    });
}

//it's ugly, but it works
function buildList(itemArray) {
    var newHtml = '<div class="container">';

    for (i = 0; i < itemArray.length; i++) {
        var changeStatusEvent = 'changeStatusOfItem(' + itemArray[i].id + ',' + '$(this).prop("checked"))';
        var deleteItemEvent = 'deleteItem(' + itemArray[i].id + ')';
        var editItemEvent = 'addEditItem(' + itemArray[i].id + ')';
        var descriptionCss = getCssBasedOnPriority(itemArray[i].priorityID);

        newHtml += '<div class="row top-buffer item-row">';
        newHtml += '<div class="col-sm-1 toDo-cb">';
        newHtml += '<input type="checkbox"';
        if (itemArray[i].isComplete) {
            newHtml += ' checked'
        }
        newHtml += ' onclick = ' + changeStatusEvent + '>';
        newHtml += '</div >';
        newHtml += '<div class="col-sm-7 ' + descriptionCss + '">';
        newHtml += '<p>' + itemArray[i].description + '</p>';
        newHtml += '</div>';
        newHtml += '<div class="col-sm-4 btn-toolbar toDo-buttonGroup">';
        newHtml += '<button onclick=' + editItemEvent + ' class="btn toDo-button btn-warning"> Edit </button>';
        newHtml += '<button onclick=' + deleteItemEvent + ' class="btn toDo-button btn-danger"> Delete </button>';
        newHtml += '</div>';
        newHtml += '</div>';
    }
    newHtml += '</div>';
    $('#toDoList').html(newHtml);
}

function getCssBasedOnPriority(priorityId) {
    if (priorityId == 1) {
        return 'toDo-lowPriority';
    } else if (priorityId == 2) {
        return 'toDo-mediumPriority';
    } else {
        return 'toDo-highPriority';
    }
};

//returns base URL for easier redirects
function getBaseUrl() {
    return window.location.protocol + "//" + window.location.host;
}

//just redirects to the AddEdit page
function addEditItem(id = 0) {
    window.location.href = getBaseUrl() + "/ToDo/AddEditToDoItem/" + id;
}

//displays toastr notifications
function makeToast(isSuccess, message) {
    if (isSuccess) {
        toastr.success(message, "Success!");
    } else {
        toastr.error(message, "Error");
    }
}

function changeStatusOfItem(id, isChecked) {
    $.post("https://localhost:44307/ToDo/ChangeItemStatus",
        {
            itemID: parseInt(id),
            isComplete: isChecked
        },
        function (data) {
            if (data.isSuccess === true) {
                //reload grid if sort by selected is chosen
                if ($('#sortSelection').val() == '5') {
                    queryList('5');
                }
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
            if (result.isSuccess === true) {
                queryList();
                makeToast(true, "Item deleted");
            } else {
                makeToast(false, "Unable to delete item");
            }
        }
    });
}

function deleteCompletedItems() {
    $.ajax({
        url: '/ToDo/DeleteCompletedToDoItems',
        type: 'DELETE',
        success: function (result) {
            if (result.isSuccess === true) {
                queryList();
                makeToast(true, "Items deleted");
            } else {
                makeToast(false, "No completed items found");
            }
        }
    });
}