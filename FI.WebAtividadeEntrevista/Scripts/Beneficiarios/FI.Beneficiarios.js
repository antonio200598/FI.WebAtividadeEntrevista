$(document).ready(function () {
    $('#btnIncluirBeneficiario').on('click', function () {
        const cpf = $('#CPFBeneficiario').val().trim();
        const nome = $('#NomeBeneficiario').val().trim();

        if (!cpf || !nome) {
            ModalDialog("Erro", "Preencha todos os campos do beneficiario");
            return;
        }

        var novaLinha = `
            <tr>
                <td class="hidden-xs hidden">0</td>
                <td>${formatarCPF(cpf)}</td>
                <td>${nome}</td>
                <td class="text-center">
                    <button type="button" class="btn btn-sm btn-primary btnAlterarBeneficiario" style="margin-right: 0.4rem">Alterar</button>
                    <button type="button" class="btn btn-sm btn-danger btnExcluirBeneficiario">Excluir</button>
                </td>
            </tr>
        `;

        $('#listaBeneficiarios tbody').append(novaLinha);

        $('#CPFBeneficiario').val('');
        $('#NomeBeneficiario').val('');
    });

    $('#listaBeneficiarios').on('click', '.btnExcluirBeneficiario', function () {
        const linha = $(this).closest('tr');
        const id = parseInt(linha.find('td:eq(0)').text());

        if (id > 0) {
            if (confirm("Deseja realmente excluir este beneficiario?")) {
                $.ajax({
                    url: '/Cliente/DeletarBeneficiario',
                    method: 'POST',
                    data: { id: id },
                    success: function (response) {
                        linha.remove();
                        ModalDialog("Sucesso", response);

                    },
                    error: function (error) {
                        ModalDialog("Erro", error.responseJSON);
                    }
                });
            }
        } else {
            linha.remove();
        }
    });

    $('#listaBeneficiarios').on('click', '.btnAlterarBeneficiario', function () {
        var linha = $(this).closest('tr');
        var cpfColuna = linha.find('td:eq(1)');
        var nomeColuna = linha.find('td:eq(2)');

        if (linha.hasClass('em-edicao')) {
            var novosValores = linha.find('input').map(function () {
                return $(this).val();
            }).get();

            cpfColuna.text(novosValores[0]);
            nomeColuna.text(novosValores[1]);

            $(this).text('Alterar').removeClass('btn-success');
            linha.removeClass('em-edicao');
        } else {
            cpfColuna.html(`<div class="input-group"><input id="beneficiario_Alt_CPF" type="text" class="form-control" style="width: 13rem;" value="${cpfColuna.text()}"></div>`);
            nomeColuna.html(`<div class="input-group"><input type="text" class="form-control" style="width: 150px;" value="${nomeColuna.text()}"></div>`);
            $('#beneficiario_Alt_CPF').mask('000.000.000-00', { reverse: true });

            $(this).text('Salvar').addClass('btn-success');
            linha.addClass('em-edicao');
        }
    });
});