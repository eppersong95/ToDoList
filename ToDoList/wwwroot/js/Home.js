function changeStatusOfItem(id, isChecked) {
    $.post("https://localhost:44307/Home/ChangeItemStatus",
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