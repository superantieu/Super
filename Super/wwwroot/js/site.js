// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
  
    // lưu giá trị search hiện tại
    var currentSearchData = '';

    var supcua = $(".supcua");


    $('#searchInput').on('input', function () {

        // lấy value từ input
        var searchData = $(this).val();
        if (searchData === '') {

            $('#searchListss').empty();

            


        } else {

            // dùng ajax gọi hàm lấy dữ liệu từ controller Search 
            $.ajax({
                url: '/Hang/Search',
                type: 'GET',
                data: { searchData: searchData },
                success: function (searchResults) {

                    // so sánh giá trị search hiện tại và giá trị trước đó
                    if (searchData !== currentSearchData) {
                        $('#searchListss').empty();
                        currentSearchData = searchData;
                    }


                    // lặp qua phần tử trong kết quả trả về từ ajax và thêm vào tag trong html
                    $.each(searchResults, function (index, result) {
                        console.log(result)
                        
                        var search = `<li class="search-item" style="text-decoration: none"><a class="search-itemm" href = "/Hang/Super?mahang=${result.maHang}&url=${result.url}">${result.tenHang}</a></li>`;

                        if (result.isActive == true) { $('#searchListss').append(search) }
                        
                    });

                }
            })
        }

    });

   
    

});

function myFunction() {
    $('#searchListss').attr('style', 'display: block')
}

function youFunction() {
    $('#searchListss').attr('style', 'display: none')
}