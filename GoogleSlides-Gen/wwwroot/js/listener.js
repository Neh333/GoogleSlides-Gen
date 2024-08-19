$(document).ready(function(){ 
    $('.incrementer').click( function() {
        let nextInput = $(this).next("input")
        let count = parseInt(nextInput.val()) + 1 
        nextInput.val(count)
    });
});