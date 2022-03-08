﻿using Curso.Classes;

//Construção do menu.
Console.WriteLine(@$"
********************************************************
|          Bem-vindo(a) ao sistema de cadastro de      |
|              Pessoas Físicas e Jurídicas             |
********************************************************
");

CarregarBarraDeProgresso("Carregando", ".", 10, 500,
 ConsoleColor.DarkGreen, ConsoleColor.DarkGray);

string? opcao;

do
{

    Console.Clear();
    Console.WriteLine(@$"
=====================================================
|            Escolha uma das opções abaixo:         |
|            1-Pessoa Física                        |
|            2-Pessoa Jurídica                      |
|                                                   |
|            0-Sair                                 |
=====================================================
");

    Console.Write("Insira uma opção: ");

    opcao = Console.ReadLine();

    switch (opcao)
    {
        case "1":
            PessoaFisica pf = new PessoaFisica();
            Endereco ender = new Endereco();

            ender.Logradouro = "Rua Seis";
            ender.Numero = "120";
            ender.Complemento = "casa";
            ender.EndComercial = false;

            pf.Nome = "Kátia";
            pf.DataNascimento = new DateTime(1988, 02, 20);
            pf.Rendimento = 2300F;
            pf.Endereco = ender;

            Console.Clear();
            Console.WriteLine($"Nome: { pf.Nome }");
            Console.Write($"Logradouro: { pf.Endereco.Logradouro }");
            Console.WriteLine($", { pf.Endereco.Numero } ");
          
           
            string idadeValida = pf.ValidarNascimento() ? "Sim" : "Não";
            Console.WriteLine($"Idade válida: { idadeValida }");

            Console.WriteLine();
            Console.WriteLine("Pressione qualquer tecla para continuar...");
            Console.ReadKey();
            break;

        case "2":
            PessoaJuridica pj = new PessoaJuridica(
                "Empresa XYZ",
                new Endereco("Rua Um", "320", "Prédio", true),
                50000F,
                "77.863.461/0001-12",
                "XYZ LTDA"
            );

            Console.Clear();
            Console.WriteLine(pj);
            Console.WriteLine();
            Console.WriteLine("Pressione qualquer tecla para continuar...");
            Console.ReadKey();
            break;

        case "0":
            Console.Clear();
            Console.WriteLine("Obrigado por utilizar nosso sistema!");
            CarregarBarraDeProgresso("Finalizando", "\tbye!", 2, 1000,
             ConsoleColor.Blue, ConsoleColor.White);
            break;

        default:
            Console.Write("Opção inválida!");
            Thread.Sleep(2000);
            break;
    }

} while (opcao != "0");


static void CarregarBarraDeProgresso(string status, string caracter, int repeticoes,
int tempo, ConsoleColor corDeFundo, ConsoleColor corDaFonte)
{
    Console.BackgroundColor = corDeFundo;
    Console.ForegroundColor = corDaFonte;

    Console.Write($"{ status } ");

    for (int i = 0; i < repeticoes; i++)
    {
        Thread.Sleep(tempo);
        Console.Write($"{ caracter }");
    }

    Console.ResetColor();
}

