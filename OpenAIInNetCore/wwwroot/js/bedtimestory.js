$(document).ready(function () {
    $('#userInputForm').on('submit', function (event) {
        event.preventDefault();

        var userInput = $('#userInput').val(); // Fetching the user input

        // AJAX request to the endpoint
        $.ajax({
            url: '/api/getbedtimestory',
            method: 'GET',
            data: { topic: userInput },
            dataType: 'json',
            success: function () {
            },
            error: function (xhr, status, error) {
                // Handle error scenario
                console.log(xhr);
                console.log(status);
                console.log(error);
                alert('An error occurred');
            }
        });
    });
});