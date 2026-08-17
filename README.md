# Formulário Dinâmico

Aplicação web em **ASP.NET Core MVC (.NET 8)** para criar formulários de forma dinâmica, definindo os campos pela interface e gerando a estrutura final em JSON.

## Tecnologias

- C# / .NET 8
- ASP.NET Core MVC
- Razor Views
- Bootstrap
- Newtonsoft.Json
- Session do ASP.NET Core

## Funcionalidades

- Adição, edição e remoção de campos do formulário.
- Suporte a texto, e-mail, número, data, textarea, select, radio, checkbox e range.
- Definição de campos obrigatórios.
- Configuração de opções para campos `select` e `radio`.
- Geração automática do nome técnico de cada campo a partir do título.
- Armazenamento temporário da configuração em sessão.
- Persistência da definição do formulário em arquivo JSON.
- Renderização do formulário gerado a partir da configuração salva.

## Como executar

Pré-requisito: **.NET SDK 8**.

```bash
git clone https://github.com/algomjo/FormularioDinamico.git
cd FormularioDinamico
dotnet restore
dotnet run
```

Abra no navegador o endereço informado pelo `dotnet run`.

## Estrutura principal

```text
Controllers/   Ações do construtor e do formulário gerado
Models/        Modelos usados para representar os campos
Views/         Interfaces Razor MVC
wwwroot/data/  Definição JSON do formulário
```

## Como funciona

O construtor mantém os campos criados na sessão do usuário. Ao selecionar **Salvar e Usar**, a configuração é serializada em JSON e passa a ser utilizada pela aplicação para montar o formulário dinamicamente.

---

Desenvolvido por [Alexandre Gomes de Araújo](https://github.com/algomjo).