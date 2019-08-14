function changeStatusOfItem(id, isChecked) {
    $.post("https://localhost:44307/ToDo/ChangeItemStatus",
        {
            itemID: parseInt(id),
            isComplete: isChecked
        },
        function (data) {
            if (data.isSuccess === "1") {
                alert('winner');
            } else {
                alert('loser');
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
                alert('deleted');
            } else {
                alert('failed');
            }
        }
    });
}

function editItem(id) {
    window.location.href = "https://localhost:44307/ToDo/AddEditToDoItem/" + id;
}