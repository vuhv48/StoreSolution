// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.



// Write your JavaScript code.

$('body').on('click', '#quantity', function (e) {
    e.preventDefault();
    var quantity = document.getElementById("quantity").value;
    var ProductId = document.getElementById("ProductId").value;
    
    
    $.ajax({
        type: "POST",
        url: "/Cart/UpdateCart",
        data: {
            quantity: quantity,
            productId: ProductId
        },
        success: function (res) {
            
        },
        error: function (err) {
            console.log(err);
        }

    });
});

$('body').on('click', '#add-to-cart', function (e) {
    e.preventDefault();
    var ProductId = document.getElementById("ProductId").value;
    $.ajax({
        type: "POST",
        url: "/Cart/UpdateCart",
        data: {
            quantity: 1,
            productId: ProductId
        },
        success: function (res) {
            console.log("OK OK");
        },
        error: function (err) {
            console.log(err);
        }

    });
    
    if (confirm("You add product success. Do you know to CART?") == true) {
        document.location = "https://localhost:7024/Cart/Basket";
    }

});



