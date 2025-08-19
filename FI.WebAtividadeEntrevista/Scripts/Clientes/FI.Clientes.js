
$(document).ready(function () {

    $('#CPF').mask('000.000.000-00', { reverse: true });
    $('#CPFBeneficiario').mask('000.000.000-00', { reverse: true });
    $('#Telefone').mask('(00) 00000-0000');

    $('#formCadastro').submit(function (event) {
        event.preventDefault();
        incluirClientes();
    });
})

function incluirClientes() {
    var cliente = {
        Nome: $('#formCadastro #Nome').val(),
        Sobrenome: $('#formCadastro #Sobrenome').val(),
        CPF: $('#formCadastro #CPF').val(),
        Nacionalidade: $('#formCadastro #Nacionalidade').val(),
        CEP: $('#formCadastro #CEP').val(),
        Estado: $('#formCadastro #Estado').val(),
        Cidade: $('#formCadastro #Cidade').val(),
        Logradouro: $('#formCadastro #Logradouro').val(),
        Email: $('#formCadastro #Email').val(),
        Telefone: $('#formCadastro #Telefone').val(),
        Beneficiarios: []
    };

    $('#listaBeneficiarios tbody tr').each(function () {
        var beneficiario = {
            Id: $(this).find('td:eq(0)').text().trim(),
            CPF: $(this).find('td:eq(1)').text().trim(),
            Nome: $(this).find('td:eq(2)').text().trim()
        };

        cliente.Beneficiarios.push(beneficiario);
    });

    $.ajax({
        url: urlPost,
        type: 'POST',
        data: JSON.stringify(cliente),
        contentType: 'application/json',
        success: function (result) {
            const modalId = ModalDialog("Sucesso", "O Cliente foi incluído com sucesso!");

            $('#' + modalId).on('hidden.bs.modal', function () {
                window.location.href = urlRetorno;
            });
        },
        error: function (error) {
            const response = JSON.parse(error.responseText);

            ModalDialog("Erro", "Não foi possível incluir o cliente. Detalhes: <br/>" + response);
        }
    });
}
