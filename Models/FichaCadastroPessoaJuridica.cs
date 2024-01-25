namespace Empreendimento.Models
{
    public class FichaCadastroPessoaJuridica
    {
        // Dados da empresa
        public string txtRazaoSocial { get; set; }
        public string txtNomeFantasia { get; set; }
        public string txtCNPJ { get; set; }
        public string txtInscricaoMunicipal { get; set; }
        public string txtDataAbertura { get; set; }
        public string txtEnderecoComercial { get; set; }
        public string txtNumero { get; set; }
        public string txtCep { get; set; }
        public string txtCidade { get; set; }
        public string txtUf { get; set; }
        public string txtTelefoneComercial { get; set; }
        public string txtTelefoneCelular { get; set; }
        public string txtTelefoneRecado { get; set; }
        public decimal txtFaturamentoMensal { get; set; }
        public string txtEmail { get; set; }

        // Dados do sócio majoritário
        public string txtSocioMajoritario { get; set; }
        public string txtRG { get; set; }
        public string txtCpf { get; set; }
        public string txtDataNascimento { get; set; }
        public string txtEnderecoResidencial { get; set; }
        public string txtBairro { get; set; }
        public string NumeroSocio { get; set; }
        public string CEPSocio { get; set; }
        public decimal txtRendaMensal { get; set; }

        // Referências Bancárias
        public string txtBanco1 { get; set; }
        public string txtOperacao1 { get; set; }
        public string txtAgencia1 { get; set; }
        public string txtConta1 { get; set; }
        public string txtBanco2 { get; set; }
        public string txtOperação2 { get; set; }
        public string txtOperacao2 { get; set; }
        public string txtAgencia2 { get; set; }
        public string txtConta2 { get; set; }

        // Dados do Crédito
        public decimal txtValorCredito { get; set; }
        public string txtVendedor { get; set; }
    }
}
