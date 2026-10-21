# FinancialKrisis

App desktop (WPF, .NET 10, EF Core + SQLite) de planejamento financeiro pessoal.
Regra de ouro: futuro = compromisso, passado = liquidação, saldo = cálculo.

A base de conhecimento completa do projeto fica em `.claude/context/` — comece pelo
[README](.claude/context/README.md). Leia o arquivo relevante antes de mexer em uma área.

@.claude/context/03-convencoes-de-codigo.md
@.claude/context/07-estado-atual.md

## Comandos
- Build: `dotnet build FinancialKrisis.slnx`
- Testes: `dotnet test FinancialKrisis.slnx`

## Git
`git` não está no PATH; use `C:\Program Files\Microsoft Visual Studio\18\Community\Common7\IDE\CommonExtensions\Microsoft\TeamFoundation\Team Explorer\Git\cmd\git.exe`.
Para commitar, siga a skill `commit` (sempre confirmar a mensagem com o Lucas).

## Regras de trabalho
- Código em inglês; mensagens ao usuário, asserts de teste e commits em português.
- Não invente regra de negócio: se algo não estiver em `04-dominio-e-regras.md` como confirmado,
  pergunte e registre a dúvida em `08-duvidas-abertas.md`; decisões confirmadas vão em `09-decisoes.md`.
- Ao mudar comportamento, atualize os docs de contexto correspondentes.
