$('.schoolRow').dblclick(function (){
    $('#sendApplication').removeAttr('disabled');
    let schoolId = $(this).children('.schoolId').html();
    $('#childSelectModal').modal('show');
    $('#sendApplication').on('click', function (event) {
        $(this).attr('disabled', 'disabled');
        let data = {
            childId: $('#ChildId').val(),
            schoolId: schoolId
        }
        $('#childSelectModal').modal('hide');
        $('#exampleModalCenter').modal('show');
        fetch('/SchoolQueue/SendApplication', {
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