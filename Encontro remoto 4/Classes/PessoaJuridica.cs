using System.Text.RegularExpressions;
using Curso.Interfaces;

namespace Curso.Classes
{
    public class PessoaJuridica : Pessoa, IPessoaJuridica
    {

        public string? Cnpj { get; set; }
        public string? RazaoSocial { get; set; }

        public PessoaJuridica()
        {
        }

        public PessoaJuridica(string nome, Endereco endereco, float rendimento, string cnpj,
        string razaoSocial) : base(nome, endereco, rendimento)
        {
            Cnpj = cnpj;
            RazaoSocial = razaoSocial;
        }

        public override float PagarImposto(float rendimento)
        {
            throw new NotImplementedException();
        }

        public bool ValidarCnpj(string? cnpj)
        {

            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int somatorio;
            int resto;
            string digito;
            string cnpjAux;

            try
            {
                //Verifica se o CNPJ está nulo ou vazio
                if (String.IsNullOrEmpty(cnpj))
                    return false;

                //Remove os caracteres em branco do início e do final da string
                cnpj = cnpj.Trim();

                //Verifica o padrão de caracteres para um CNPJ
                if (Regex.IsMatch(cnpj, @"((\d{2}\.\d{3}\.\d{3}/\d{4}-\d{2})|(\d{14}))"))
                {

                    //Caso a string tenha caracteres (".", "/" e "-"), os mesmos serão removidos restando apenas números.
                    if (cnpj.Length == 18)
                        cnpj = cnpj.Replace(".", "").Replace("/", "").Replace("-", "");

                    //Verifica se o CNPJ possui todos os dígitos iguais.
                    if (cnpj.All(c => c.Equals(cnpj.First())))
                        return false;

                    //Define a variável acumuladora como zero
                    somatorio = 0;

                    //Obtém os 12 primeiros caracteres do CNPJ ( sem os dígitos de verificação )
                    cnpjAux = cnpj.Substring(0, 12);

                    //Percorre cada número dos 12 primeiros e faz a multiplicação pelos valores estabelecidos
                    for (int i = 0; i < cnpjAux.Length; i++)
                        somatorio += int.Parse(cnpjAux[i].ToString()) * multiplicador1[i];

                    //Após acumular o somatório, obtém o resto da divisão por 11
                    resto = somatorio % 11;

                    if (resto < 2)
                        resto = 0;
                    else
                        resto = 11 - resto;

                    //Obtém 12 dígitos mais o primeiro dígito verificador
                    digito = resto.ToString();
                    cnpjAux = cnpjAux + digito;

                    //Redefine a variável acumuladora como zero
                    somatorio = 0;

                    //Percorre cada número dos 13 primeiros e faz a multiplicação pelos valores estabelecidos
                    for (int i = 0; i < cnpjAux.Length; i++)
                        somatorio += int.Parse(cnpjAux[i].ToString()) * multiplicador2[i];

                    //Após acumular o somatório, obtém o resto da divisão por 11
                    resto = somatorio % 11;

                    if (resto < 2)
                        resto = 0;
                    else
                        resto = 11 - resto;

                    //Obtém o valor último dígito verificador e concatena ao primeiro.
                    digito = digito + resto.ToString();

                }
                else
                    return false;

                //Compara os dígitos verificadores com os dígitos calculados
                return cnpj.EndsWith(digito);
            }
            catch (Exception)
            {
                return false;
            }

        }

        public override string ToString()
        {
            string cnpjValido = "";

            if (!String.IsNullOrEmpty(Cnpj))
                cnpjValido = ValidarCnpj(Cnpj) ? "Sim" : "Não";

            return base.ToString()
            + "CNPJ: " + Cnpj
            + "\nRazão Social: " + RazaoSocial
            + "\nCNPJ válido: " + cnpjValido
            + "\n";
        }
    }
}