
$(document).ready(function () {
    $('#CPF').mask('000.000.000-00', { reverse: true });
    $('#CPFBeneficiario').mask('000.000.000-00', { reverse: true });
    $('#Telefone').mask('(00) 00000-0000');

    if (obj) {
        $('#formCadastro #Nome').val(obj.Nome);
        $('#formCadastro #CEP').val(obj.CEP);
        $('#formCadastro #Email').val(obj.Email);
        $('#formCadastro #Sobrenome').val(obj.Sobrenome);
        $('#formCadastro #Nacionalidade').val(obj.Nacionalidade);
        $('#formCadastro #Estado').val(obj.Estado);
        $('#formCadastro #Cidade').val(obj.Cidade);
        $('#formCadastro #Logradouro').val(obj.Logradouro);
        $('#formCadastro #Telefone').val(obj.Telefone);
        $('#formCadastro #CPF').val(obj.CPF).mask('000.000.000-00');
    }

    $('#listaBeneficiarios tbody').empty();

    $.each(obj.Beneficiarios, function (index, beneficiario) {

        const novoCpf = cpfFormatado(beneficiario.CPF);

        var novaLinha = `
                    <tr>
                    <td class="hidden-xs hidden">${beneficiario.Id}</td>
                    <td>${novoCpf}</td>
                    <td>${beneficiario.Nome}</td>
                    <td class="text-center">
                        <button type="button" class="btn btn-sm btn-primary btnAlterarBeneficiario" style="margin-right: 0.4rem">Alterar</button>
                        <button type="button" class="btn btn-sm btn-danger btnExcluirBeneficiario">Excluir</button>
                    </td>
                </tr>
                `;

        $('#listaBeneficiarios tbody').append(novaLinha);
    });

    $('#formCadastro').submit(function (e) {
        e.preventDefault();
        alterarCliente();
    })
})

function alterarCliente() {
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
            const modalId = ModalDialog("Sucesso", result);

            $('#' + modalId).on('hidden.bs.modal', function () {
                window.location.href = urlRetorno;
            });
        },
        error: function (error) {
            const response = JSON.parse(error.responseText);

            ModalDialog("Erro", "Erro ao alterar cliente. Detalhes: <br/>" + response);
        }
    });
}

function cpfFormatado(cpf) {
    cpf = cpf.replace(/\D/g, '');

    cpf = cpf.slice(0, 11);

    return cpf.replace(/(\d{3})(\d{3})(\d{3})(\d{2})/, '$1.$2.$3-$4');
}
