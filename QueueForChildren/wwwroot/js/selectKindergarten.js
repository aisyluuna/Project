$('.kindergartenRow').dblclick(function (){
    $('#sendApplication').removeAttr('disabled');
    let kindergartenId = $(this).children('.kindergartenId').html();
    $('#childSelectModal').modal('show');
    $('#sendApplication').on('click', function (event) {
        $(this).attr('disabled', 'disabled');
        let data = {
            childId: $('#ChildId').val(),
            kindergartenId: kindergartenId
        }
        $('#childSelectModal').modal('hide');
        $('#exampleModalCenter').modal('show');
        fetch('/KindergartenQueue/SendApplication', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json;charset=utf-8'
            },
            body: JSON.stringify(data)
        }).then(function (response) {
            response.json().then(function (json){
                $('#modal-body').html(json.data);
            });
        });
    });
}); 