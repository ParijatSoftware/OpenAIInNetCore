$(document).ready(function () {
    $('#userInputForm').on('submit', function (event) {
        event.preventDefault();
        document.getElementById("gptResponse").textContent = "";
        var userInput = $('#userInput').val(); // Fetching the user input

        // AJAX request to the endpoint
        $.ajax({
            url: '/api/ask',
            method: 'POST',
            data: JSON.stringify({ query: userInput }),
            contentType: 'application/json',
            dataType: 'json',
            success: function () {
            },
            error: function (xhr, status, error) {
                alert('An error occurred');
            }
        });
    });
});