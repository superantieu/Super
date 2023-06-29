console.log(1);
$(document).ready(function () {
    console.log(1);
    // lưu giá trị search hiện tại
    var currentSearchData = '';

    var supcua = $(".supcua");
    console.log(supcua);

    $('#searchInput').on('input', function () {

        // lấy value từ input
        var searchData = $(this).val();
        if (searchData === '') {

            $('#searchLists').empty();

            $('#searchLists').append(supcua);


        } else {

            // dùng ajax gọi hàm lấy dữ liệu từ controller Search 
            $.ajax({
                url: '/hang/search',
                type: 'GET',
                data: { searchData: searchData },
                success: function (searchResults) {
                    console.log(searchData)
                    // so sánh giá trị search hiện tại và giá trị trước đó
                    if (searchData !== currentSearchData || searchData !== '') {
                        $('#searchLists').empty();
                        currentSearchData = searchData;
                    }


                    // lặp qua phần tử trong kết quả trả về từ ajax và thêm vào tag trong html
                    $.each(searchResults, function (index, result) {
                        console.log(result)

                        var search = ` <tr class="supcua">
                        <td class="text-center"><div>${index + 1}</div></td>
                        <td class="text-center">${result.maHang}</td>
                        <td class="text-center">${result.tenHang}</td>
                        <td class="text-center">${result.donGiaHang}</td>
                        <td class="text-center">${result.maNhanHieu}</td>
                        <td class="text-center">${result.moTa}</td>
                        <td class="text-center" ><img src="${result.hinhAnh}" /></td>
                           <td class="text-center ${result.isActive ? "text-success" : "text-danger"}">
             ${result.isActive ? "Kích hoạt" : "Tạm khóa"}
                          </td >
                        <td class="text-center">${result.url}</td>
                        <td class="text-center">${result.src}</td>
                                                    
                       
                        <td class="text-center">
                            <a class="px-2" href="/hang/xoa?mahang=${result.maHang}&isActive=${result.isActive}">
                                <i class="fa ${result.isActive ? "text-danger"  : "text-success"} ${result.isActive ? "fa-trash" :  "fa-recycle"} " aria-hidden="true"></i>
                            </a >
            <a class="px-2" href="/hang/cap-nhat?mahang=${result.maHang}&isUpdate=False" >
                <i class="fa fa-wrench" aria-hidden="true"></i>
                            </a >
                        </td >
                    </tr > `
                    console.log(search);

                        $('#searchLists').append(search);
                    });

                }
            })
        }

    });
});
