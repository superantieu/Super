// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    console.log(1);
    // lưu giá trị search hiện tại
    var currentSearchData = '';

    var supcua = $(".supcua");


    $('#searchInput').on('input', function () {

        // lấy value từ input
        var searchData = $(this).val();
        if (searchData === '') {

            $('#searchList').empty();

            $('#searchList').append(supcua);


        } else {

            // dùng ajax gọi hàm lấy dữ liệu từ controller Search 
            $.ajax({
                url: '/Hang/Search',
                type: 'GET',
                data: { searchData: searchData },
                success: function (searchResults) {

                    // so sánh giá trị search hiện tại và giá trị trước đó
                    if (searchData !== currentSearchData || searchData !== '') {
                        $('#searchList').empty();
                        currentSearchData = searchData;
                    }


                    // lặp qua phần tử trong kết quả trả về từ ajax và thêm vào tag trong html
                    $.each(searchResults, function (index, result) {
                        console.log(result)
                        var search = ` <div class="supcua col l-2-4 m-4 s-6">
                                            <a class="product" href="/Hang/innerIndex?mahang=${result.maHang}">
                                                <div class="product-image-01" style="background-image: url(${result.hinhAnh});">
                                                </div>
                                                 <div class="product-info">
                                                <h4 class="product-info__heading">${result.tenHang}</h4>
                                                <span class="product-info__hastag">#${result.maNhanHieu}</span>
                                                <div class="product-info__price">
                                                        <span class="product-info__price-old">52.000.000đ</span>
                                                        <span class="product-info__price-new">${result.donGiaHang} đ</span>
                                                </div>
                                                <div class="product-react">
                                                     <span href="" class="product-react__like product-react__like--liked">
                                                         <i class="far fa-heart product__like-false"></i>
                                                         <i class="fas fa-heart product__like-true"></i>
                                                     </span>
                                                     <div class="product-sold">
                                                        <span class="product-sold__star-point">
                                                            <i class="product-sold__star-point--gold fas fa-star"></i>
                                                            <i class="product-sold__star-point--gold fas fa-star"></i>
                                                            <i class="product-sold__star-point--gold fas fa-star"></i>
                                                            <i class="product-sold__star-point--gold fas fa-star"></i>
                                                            <i class="fas fa-star"></i>
                                                        </span>
                                                        <p class="product-sold__text">Đã bán</p>
                                                        <p class="product-sold__amount">41,7k</p>
                                                    </div>
                                                </div>
                                                 <span class="product-location">Hà Nội</span>
                                        </div>
                                <div class="product-label">
                                    <div class="product-label__like">
                                        <i class="fas fa-check"></i>
                                        <span class="product-label__like-text">Yêu thích</span>
                                    </div>
                                    <div class="product-label__saleoff">
                                        <span class="product-label__saleoff-percent">SALE</span>
                                        <span class="product-label__saleoff-text">OFF</span>
                                    </div>
                                </div>
                            </a>
                                    </div > `


                        $('#searchList').append(search);
                    });

                }
            })
        }

    });

  



});

