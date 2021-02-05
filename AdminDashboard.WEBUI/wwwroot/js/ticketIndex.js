$(document).ready(function () {

    /* DataTables start here. */

    $('#ticketsTable').DataTable({
        dom:
            "<'row'<'col-sm-3'l><'col-sm-6 text-center'B><'col-sm-3'f>>" +
            "<'row'<'col-sm-12'tr>>" +
            "<'row'<'col-sm-5'i><'col-sm-7'p>>",
        buttons: [
            {
                text: 'Ekle',
                attr: {
                    id: "btnAdd",
                },
                className: 'btn btn-success',
                action: function (e, dt, node, config) {
                }
            },         
        ],
        language: {
            "sDecimal": ",",
            "sEmptyTable": "Tabloda herhangi bir veri mevcut değil",
            "sInfoEmpty": "Kayıt yok",
            "sInfoFiltered": "(_MAX_ kayıt içerisinden bulunan)",
            "sInfoPostFix": "",
            "sInfoThousands": ".",
            "sLengthMenu": "Sayfada _MENU_ kayıt göster",
            "sLoadingRecords": "Yükleniyor...",
            "sProcessing": "İşleniyor...",
            "sSearch": "Ara:",
            "sZeroRecords": "Eşleşen kayıt bulunamadı",
            "oPaginate": {
                "sFirst": "İlk",
                "sLast": "Son",
                "sNext": "Sonraki",
                "sPrevious": "Önceki"
            },
            "oAria": {
                "sSortAscending": ": artan sütun sıralamasını aktifleştir",
                "sSortDescending": ": azalan sütun sıralamasını aktifleştir"
            },
            "select": {
                "rows": {
                    "_": "%d kayıt seçildi",
                    "0": "",
                    "1": "1 kayıt seçildi"
                }
            }
        }
    });

    /* DataTables end here */

    /* Ajax GET / Getting the _TicketAddPartial as Modal Form starts from here. */

    $(function () {
        const url = '/Admin/Ticket/Add/';
        const placeHolderDiv = $('#modalPlaceHolder');
        $('#btnAdd').click(function () {
            $.get(url).done(function (data) {
                placeHolderDiv.html(data);
                placeHolderDiv.find(".modal").modal('show');
            });
        });

        /* Ajax GET / Getting the _UserAddPartial as Modal Form ends here. */

        /* Ajax POST / Posting the FormData as UserAddDto starts from here. */

        placeHolderDiv.on('click',
            '#btnSave',
            function (event) {
                event.preventDefault();
                const form = $('#form-ticket-add');
                const actionUrl = form.attr('action');
                const dataToSend = new FormData(form.get(0));
                $.ajax({
                    url: actionUrl,
                    type: 'POST',
                    data: dataToSend,
                    processData: false,
                    contentType: false,
                    success: function (data) {
                        console.log(data);
                        const ticketAddAjaxModel = jQuery.parseJSON(data);
                        console.log(userAddAjaxModel);
                        const newFormBody = $('.modal-body', ticketAddAjaxModel.TicketAddPartial);
                        placeHolderDiv.find('.modal-body').replaceWith(newFormBody);
                        const isValid = newFormBody.find('[name="IsValid"]').val() === 'True';
                        if (isValid) {
                            placeHolderDiv.find('.modal').modal('hide');
                            dataTable.row.add([
                                ticketAddAjaxModel.TicketDto.Ticket.Id,
                                ticketAddAjaxModel.TicketDto.Ticket.CustomerName,
                                ticketAddAjaxModel.TicketDto.Ticket.TicketDetails,
                                `<img src="/img/${ticketAddAjaxModel.TicketDto.Ticket.CustomerPicture}" alt="${ticketAddAjaxModel.TicketDto.Ticket.CustomerName}" style="max-height: 50px; max-width: 50px;" />`,
                                ticketAddAjaxModel.TicketDto.Ticket.Priority,
                                ticketAddAjaxModel.TicketDto.Ticket.CreatedDate,
                                `<img src="/img/${ticketAddAjaxModel.TicketDto.Ticket.CustomerPicture}" alt="${ticketAddAjaxModel.TicketDto.Ticket.CustomerName}" style="max-height: 50px; max-width: 50px;" />`,
                                `<td>
                                <button class="btn btn-primary btn-sm btn-update" data-id="ticketAddAjaxModel.TicketDto.Ticket.Id"><span class="fas fa-edit"></span></button>
                                <button class="btn btn-danger btn-sm btn-delete" data-id="ticketAddAjaxModel.TicketDto.Ticket.Id"><span class="fas fa-minus-circle"></span></button>
                            </td>`
                            ]).draw();
                            toastr.success(`${ticketAddAjaxModel.TicketDto.Message}`, 'Başarılı İşlem!');
                        } else {
                            let summaryText = "";
                            $('#validation-summary > ul > li').each(function () {
                                let text = $(this).text();
                                summaryText = `*${text}\n`;
                            });
                            toastr.warning(summaryText);
                        }
                    },
                    error: function (err) {
                        console.log(err);
                    }
                });
            });
    });

    /* Ajax POST / Posting the FormData as TicketAddDto ends here. */

    /* Ajax POST / Deleting a Ticket starts from here */

    $(document).on('click',
        '.btn-delete',
        function (event) {
            event.preventDefault();
            const id = $(this).attr('data-id');
            const tableRow = $(`[name="${id}"]`);
            const ticketName = tableRow.find('td:eq(1)').text();
            Swal.fire({
                title: 'Silmek istediğinize emin misiniz?',
                text: `${ticketName} kullanıcısının bileti silinicektir!`,
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Evet, silmek istiyorum.',
                cancelButtonText: 'Hayır, silmek istemiyorum.'
            }).then((result) => {
                if (result.isConfirmed) {
                    $.ajax({
                        type: 'POST',
                        dataType: 'json',
                        data: { ticketId: id },
                        url: '/Admin/Ticket/Delete/',
                        success: function (data) {
                            const ticketDto = jQuery.parseJSON(data);
                            if (ticketDto.ResultStatus === 0) {
                                Swal.fire(
                                    'Silindi!',
                                    `Seçili Bilet Başarıyla Silindi.`,
                                    'success'
                                );

                                tableRow.fadeOut(3500);
                            } else {
                                Swal.fire({
                                    icon: 'error',
                                    title: 'Başarısız İşlem!',
                                    text: `${ticketDto.Message}`,
                                });
                            }
                        },
                        error: function (err) {
                            console.log(err);
                            toastr.error(`${err.responseText}`, "Hata!")
                        }
                    });
                }
            });
        });

    /* Ajax GET / Getting the _TicketUpdatePartial as Modal Form starts from here. */

    $(function () {
        const url = '/Admin/Ticket/Update/';
        const placeHolderDiv = $('#modalPlaceHolder');
        $(document).on('click',
            '.btn-update',
            function (event) {
                event.preventDefault();
                const id = $(this).attr('data-id');
                $.get(url, { ticketId: id }).done(function (data) {
                    placeHolderDiv.html(data);
                    placeHolderDiv.find('.modal').modal('show');
                }).fail(function () {
                    toastr.error("Bir hata oluştu.");
                });
            });

        /* Ajax POST / Updating a Ticket starts from here */

        placeHolderDiv.on('click',
            '#btnUpdate',
            function (event) {
                event.preventDefault();

                const form = $('#form-ticket-update');
                const actionUrl = form.attr('action');
                const dataToSend = form.serialize();
                $.post(actionUrl, dataToSend).done(function (data) {
                    const ticketUpdateAjaxModel = jQuery.parseJSON(data);
                    console.log(ticketUpdateAjaxModel);
                    const newFormBody = $('.modal-body', ticketUpdateAjaxModel.TicketUpdatePartial);
                    placeHolderDiv.find('.modal-body').replaceWith(newFormBody);
                    const isValid = newFormBody.find('[name="IsValid"]').val() === 'True';
                    if (isValid) {
                        placeHolderDiv.find('.modal').modal('hide');
                        const newTableRow = `
                                <tr name="${ticketUpdateAjaxModel.TicketDto.Ticket.Id}">
                                                    <td>${ticketUpdateAjaxModel.TicketDto.Ticket.CustomerName}</td>
                                                    <td>${ticketUpdateAjaxModel.TicketDto.Ticket.TicketDetails}</td>
                                                    <td>${ticketUpdateAjaxModel.TicketDto.Ticket.CustomerPicture}</td>
                                                    <td>${ticketUpdateAjaxModel.TicketDto.Ticket.Priority}</td>
                                                    <td>${ticketUpdateAjaxModel.TicketDto.Ticket.CreatedDate}</td>

                                                    <td>
                                                        <button class="btn btn-primary btn-sm btn-update" data-id="${ticketUpdateAjaxModel.TicketDto.Ticket.Id}"><span class="fas fa-edit"></span></button>
                                                        <button class="btn btn-danger btn-sm btn-delete" data-id="${ticketUpdateAjaxModel.TicketDto.Ticket.Id}"><span class="fas fa-minus-circle"></span></button>
                                                    </td>
                                                </tr>`;
                        const newTableRowObject = $(newTableRow);
                        const ticketTableRow = $(`[name="${ticketUpdateAjaxModel.TicketDto.Ticket.Id}"]`);
                        newTableRowObject.hide();
                        ticketTableRow.replaceWith(newTableRowObject);
                        newTableRowObject.fadeIn(3500);
                        toastr.success(`${ticketUpdateAjaxModel.TicketDto.Message}`, "Başarılı İşlem!");
                    } else {
                        let summaryText = "";
                        $('#validation-summary > ul > li').each(function () {
                            let text = $(this).text();
                            summaryText = `*${text}\n`;
                        });
                        toastr.warning(summaryText);
                    }
                }).fail(function (response) {
                    console.log(response);
                });
            });

    });
});











