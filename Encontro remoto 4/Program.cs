using Curso.Classes;

Endereco endPj1 = new Endereco("Rua Um", "320", "Prédio", true);
PessoaJuridica pj1 = new PessoaJuridica();

pj1.Nome =  "Empresa XYZ";
pj1.RazaoSocial = "XYZ LTDA";
pj1.Cnpj = "77.863.461/0001-12";
pj1.Endereco = endPj1;
pj1.Rendimento = 50000F;


Console.WriteLine(@$"
-----------------------------------------------
Pessoa Jurídica 1 (Usuário insere CNPJ válido).
-----------------------------------------------
{ pj1 }

");


pj1.Cnpj = "77863461000112";

Console.WriteLine(@$"
--------------------------------------------
Pessoa Jurídica 1 (Usuário insere apenas 
números no CNPJ).
--------------------------------------------
{ pj1 }

");


pj1.Cnpj = "77.863-461/0001-12";
Console.WriteLine(@$"
----------------------------------------------
Pessoa Jurídica 1 (Usuário insere caractere 
separador 'traço' ao invés de 'ponto' no 
número-base do CNPJ, sendo portanto, inválido).
----------------------------------------------
{ pj1 }

");

Endereco endPj2 = new Endereco("Rua Dois", "100", "Prédio", true);
PessoaJuridica pj2 = new PessoaJuridica();

pj2.Nome =  "Empresa ABC";
pj2.RazaoSocial = "ABC LTDA";
pj2.Cnpj = "77.863.461/0091-12";
pj2.Endereco = endPj2;
pj2.Rendimento = 30000F;


Console.WriteLine(@$"
-------------------------------------------------
Pessoa Jurídica 2 (Usuário insere CNPJ inválido).
-------------------------------------------------
{ pj2 }

");


Endereco endPj3 = new Endereco("Rua Três", "1240", "Prédio", true);
PessoaJuridica pj3 = new PessoaJuridica();

pj3.Nome =  "Empresa Super";
pj3.RazaoSocial = "Super LTDA";
pj3.Cnpj = "36.338.235/0001-35";
pj3.Endereco = endPj3;
pj3.Rendimento = 75000F;

Console.WriteLine(@$"
-----------------------------------------------
Pessoa Jurídica 3 (Usuário insere CNPJ válido).
-----------------------------------------------
{ pj3 }

");