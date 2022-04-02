using System.Text.RegularExpressions;
using Curso.Interfaces;

namespace Curso.Classes
{
    public class PessoaJuridica : Pessoa, IPessoaJuridica
    {
        public string? Cnpj { get; set; }
        public string? RazaoSocial { get; set; }
        public string Caminho { get; private set; } = "Database/PessoaJuridica.csv";

        public PessoaJuridica()
        {
        }

        public PessoaJuridica(string nome, Endereco endereco, float rendimento, string? cnpj,
        string? razaoSocial) : base(nome, endereco, rendimento)
        {
            Cnpj = cnpj;
            RazaoSocial = razaoSocial;
        }

        public PessoaJuridica(Pessoa pessoa, string? cnpj, string? razaoSocial) : base(pessoa)
        {
            Cnpj = cnpj;
            RazaoSocial = razaoSocial;
        }

        public override float PagarImposto(float rendimento)
        {
            float desconto;

            if (rendimento <= 3000)
            {
                desconto = rendimento * 0.03f;
            }
            else if (rendimento <= 6000)
            {
                desconto = rendimento * 0.05f;
            }
            else if (rendimento <= 10000)
            {
                desconto = rendimento * 0.07f;
            }
            else
            {
                desconto = rendimento * 0.09f;
            }

            return desconto;
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

        public void Inserir(PessoaJuridica pj)
        {
            VerificarPastaArquivo(Caminho);

            string[] pjString = { $"{ pj.Nome },{ pj.Endereco?.Logradouro },{ pj.Endereco?.Numero},{ pj.Endereco?.Complemento},{ pj.Endereco?.EndComercial },{ pj.Rendimento },{ pj.Cnpj },{ pj.RazaoSocial } " };

            File.AppendAllLines(Caminho, pjString);
        }

        public List<PessoaJuridica> Ler()
        {
            List<PessoaJuridica> listaPj = new List<PessoaJuridica>();

            VerificarPastaArquivo(Caminho);

            string[] linhas = File.ReadAllLines(Caminho);

            foreach (string cadaLinha in linhas)
            {
                string[] atributos = cadaLinha.Split(",");

                PessoaJuridica cadaPj = new PessoaJuridica();
                Endereco cadaEnder = new Endereco();

                cadaPj.Nome = atributos[0];
                cadaEnder.Logradouro = atributos[1];
                cadaEnder.Numero = atributos[2];
                cadaEnder.Complemento = atributos[3];
                cadaEnder.EndComercial = bool.Parse(atributos[4]);
                cadaPj.Endereco = cadaEnder;
                cadaPj.Rendimento = float.Parse(atributos[5]);
                cadaPj.Cnpj = InsereMascaraCnpj(atributos[6]);
                cadaPj.RazaoSocial = atributos[7];

                listaPj.Add(cadaPj);
            }
            return listaPj;
        }

        public bool ExisteCnpj(string? cnpj)
        {
            cnpj = RemoveMascaraCnpj(cnpj);

            VerificarPastaArquivo(Caminho);

            string[] cadastros = File.ReadAllLines(Caminho);

            string cnpjCadastrado;
            foreach (string cadastro in cadastros)
            {
                cnpjCadastrado = cadastro.Split(",")[6];
                if (cnpjCadastrado.Equals(cnpj))
                    return true;
            }
            return false;
        }

        public PessoaJuridica? BuscarPessoaJuridica(string? cnpj)
        {
            VerificarPastaArquivo(Caminho);

            string[] cadastros = File.ReadAllLines(Caminho);

            string cnpjCadastrado;
            foreach (string cadastro in cadastros)
            {
                cnpjCadastrado = cadastro.Split(",")[6];
                if (cnpjCadastrado.Equals(cnpj))
                {
                    PessoaJuridica? pj = new PessoaJuridica();
                    Endereco enderPj = new Endereco();
                    string[] atributos = cadastro.Split(",");

                    pj.Nome = atributos[0];
                    enderPj.Logradouro = atributos[1];
                    enderPj.Numero = atributos[2];
                    enderPj.Complemento = atributos[3];
                    enderPj.EndComercial = Boolean.Parse(atributos[4]);
                    pj.Endereco = enderPj;
                    pj.Rendimento = float.Parse(atributos[5]);
                    pj.Cnpj = InsereMascaraCnpj(atributos[6]);
                    pj.RazaoSocial = atributos[7];

                    return pj;
                }
            }
            return null;
        }

        public bool ExcluirPessoaJuridica(string? cnpj)
        {
            VerificarPastaArquivo(Caminho);

            if (ExisteCnpj(cnpj))
            {
                File.WriteAllLines(Caminho,
                 File.ReadAllLines(Caminho).Where(cadaLinha => cadaLinha.Split(",")[6] != cnpj).ToList());

                return true;
            }

            return false;
        }

        public void ExcluirTodasPessoasJuridicas()
        {
            VerificarPastaArquivo(Caminho);

            File.Delete(Caminho);
        }

        public void EditarPessoaJuridica(PessoaJuridica pj)
        {
            VerificarPastaArquivo(Caminho);

            File.WriteAllLines(Caminho,
                File.ReadAllLines(Caminho).Where(cadaLinha => cadaLinha.Split(",")[6] != pj.Cnpj).ToList());

            Inserir(pj);
        }

        public int TotalPessoasJuridicas()
        {
            VerificarPastaArquivo(Caminho);

            return File.ReadAllLines(Caminho).Count();
        }


        public string? RemoveMascaraCnpj(string? cnpj)
        {
            if (!String.IsNullOrEmpty(cnpj))
            {
                cnpj = cnpj.Replace(".", "").Replace("/", "").Replace("-", "");
                return cnpj;
            }
            return null;
        }

        public string? InsereMascaraCnpj(string? cnpj)
        {
            string? cnpjFormatado = "";

            if (!String.IsNullOrEmpty(cnpj))
            {
                cnpj = cnpj.Trim();

                if (cnpj.Length == 14)
                    cnpjFormatado = Convert.ToUInt64(cnpj).ToString(@"00\.000\.000/0000-00");
            }

            return cnpjFormatado;
        }

        public override string ToString()
        {
            return base.ToString()
            + "\tCNPJ: " + Cnpj
            + "\n\tRazão Social: " + RazaoSocial
            + "\n\tTaxa de imposto a ser pago: " + PagarImposto(Rendimento).ToString("C")
            + "\n";
        }
    }
}