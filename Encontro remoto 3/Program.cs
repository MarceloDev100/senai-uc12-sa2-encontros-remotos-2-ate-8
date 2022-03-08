using Curso.Classes;

PessoaFisica pf1 = new PessoaFisica();
Endereco e1 = new Endereco();

e1.Logradouro = "Rua Um";
e1.Numero = "100";
e1.Complemento = "Casa";
e1.EndComercial = false;

pf1.Nome = "Jéssica";
pf1.DataNascimento = new DateTime(1990, 03, 23);
pf1.Cpf = "111.111.111-11";
pf1.Rendimento = 3500.00F;
pf1.Endereco = e1;


/* Outra forma de passar valores para um objeto. */

PessoaFisica pf2 = new PessoaFisica(){
   Nome = "Amanda",
   DataNascimento = new DateTime(2007, 02, 15),
   Cpf = "222.222.222-22",
   Rendimento = 1300.00F,
   Endereco = new Endereco(){
      Logradouro = "Rua Dois",
      Numero = "150",
      Complemento = "AP",
      EndComercial = false
   }
};

/* Passagem de valores para um objeto por meio dos parâmetros
 contidos na sobrecarga do método construtor */

PessoaFisica pf3 = new PessoaFisica(
  "Carla", 
  new Endereco("Rua Três", "734", "Casa", false),
  2000.00F,
  "333.333.333-33",
  new DateTime(1985, 08, 23)
);


/*****************************  EXIBIÇÃO DOS DADOS  *******************************/


/* Forma comum de saída de dados na tela: */

// Console.WriteLine($"Nome: {pf1.Nome}");
// Console.WriteLine($"Endereço: {pf1.Endereco.Logradouro} , {pf1.Endereco.Numero}");


/* Outra forma de exibir dados na tela: */

/*  Mudança na cor da fonte do console para vermelho */
Console.ForegroundColor = ConsoleColor.Red;

Console.WriteLine(@$"
**************** DADOS DE PESSOAS FÍSICAS *****************
    **************************************************
                          ______
                          |    |
                        __|    |__
                        \        /
                         \ SIGA /
                          \    /
                           \  /
                            \/
");

/* Mudança na cor da fonte do console para ciano escuro */
Console.ForegroundColor = ConsoleColor.DarkCyan;

Console.WriteLine(@$"
-----------------------------------------------------------
Pessoa Física 1:                                        
-----------------------------------------------------------
{ pf1 }
-----------------------------------------------------------
Pessoa Física 2:
-----------------------------------------------------------
{ pf2 }
-----------------------------------------------------------
Pessoa Física 3:
-----------------------------------------------------------
{ pf3 }
-----------------------------------------------------------
");


/*****************************  CONCEITO DE POLIMORFISMO  *******************************/

/*
  Apesar de uma classe abstrata Pessoa não poder ser instanciada, é perfeitamente possível
  criar uma lista de objetos do tipo Pessoa (classe genérica), adicionando a esta lista, 
  objetos de classes filhas relacionadas , tais como Pessoa Física e Pessoa Jurídica.
  A associação de um tipo específico com um tipo genérico é feita em tempo de execução. 
  Quando percorrer a lista para exibir os objetos, cada um deles terá um comportamento, 
  mostrando os dados de acordo com o contexto. (Polimorfismo).
*/

/* Criação de uma lista de objetos do tipo Pessoa. */
List<Pessoa> pessoas = new List<Pessoa>();


/* Adição de objetos (filhos de Pessoa) na lista, já que toda Pessoa Física é uma Pessoa, bem como toda Pessoa Jurídica também é uma Pessoa. */
pessoas.Add(new PessoaFisica(){
    Nome = "Ricardo",
    Cpf = "444.444.444-44",
    DataNascimento = new DateTime(2000, 07, 21),
    Rendimento = 1500.00F,
    Endereco = new Endereco(){
        Logradouro = "Rua Quatro",
        Numero = "1200",
        Complemento = "Casa",
        EndComercial = false
    }
});

pessoas.Add(new PessoaJuridica(
             "Escola de Tecnologia para Todos", 
             new Endereco("Rua Cinco", "175", "Prédio", true),
             50000.00F, 
             "00.000.000/0000-12",
             "Empresa Tecnologia LTDA")
            );

/************************  LISTAGEM DE PESSOAS (FÍSICAS E JURÍDICAS) *************************/

/* Mudança na cor da fonte do console para verde */
Console.ForegroundColor = ConsoleColor.Green;

Console.WriteLine("LISTA DE PESSOAS FÍSICAS E JURÍDICAS: ");
Console.WriteLine();
foreach(Pessoa pessoa in pessoas)
{ 
   Console.WriteLine($"{ pessoa }");
}

/* Reestabelece a cor padrão para fonte no console */
Console.ResetColor();


