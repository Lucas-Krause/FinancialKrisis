# Estado atual (análise de 2026-10-21)

Histórico: 22 commits entre 2026-01-15 e 2026-02-01, todos do Lucas, branch `master`,
remoto `github.com/Lucas-Krause/FinancialKrisis`. O último commit diz:
"falta fazer o resto das validações e do metadata do Schedule, e consequentemente os testes do
PlannedTransaction".

## Pronto
- CRUD back-end (services + repos + testes) de Account, Category, Subcategory, Payee, Transaction.
- PlannedTransaction + Schedule: criação/atualização/remoção nos services; testes só da parte comum de FinancialMovement.
- Sistema de erros com mensagens PT-BR com concordância de gênero.
- DSL de testes de cenário.

## Não existe ainda
- Frontend (WPF só template), composição de DI/connection string da aplicação, migrations.
- Mudança de status da PlannedTransaction, geração de ocorrências do Schedule.
- Vínculo/conciliação Transaction ↔ PlannedTransaction.
- Cálculo de saldo (atual/futuro), relatórios.
- Importação de extratos, regras automáticas.

## 🟢 Build e testes
Build verde, **93 testes passando** (2026-10-21), após corrigir um `)` sobrando em `Schedule.Validations.cs` (D-G1).
Esse trabalho (`Type`/`OwnerType` nas exceções, mensagens do Schedule, `ValidateInterval()`) foi commitado em `b1908c2`.
O arquivo RESUMO foi removido (conteúdo coberto por `01-visao-de-negocio.md`).
PlannedTransaction + Schedule = **área a redesenhar** (não investir em correções pontuais lá).

## Bugs / riscos encontrados (não corrigidos — aguardando OK)
1. **Update de Schedule depende da ordem**: cada `Change*` valida o Schedule inteiro. Trocar para `Weekly`
   e definir `DaysOfWeek` no mesmo update falha, porque `ChangeRecurrenceType` valida antes de `DaysOfWeek` ser setado.
2. **Categoria trocada sem revalidar subcategoria**: no update, mudar só `CategoryId` (ou removê-la) mantém a
   subcategoria antiga, que pode não pertencer mais à nova categoria.
3. **Navegação não carregada fora dos testes**: os repositórios de movimento usam `FindAsync` sem `Include`. Nos testes
   funciona porque tudo roda no mesmo `DbContext` (fix-up de navegação). O uso de `pMovement.Category!` no update foi
   trocado por comparação de Ids (D-D4), mas o risco geral continua para qualquer código que leia navegações de movimentos.
4. **Possível relacionamento duplicado Subcategory→Category**: `SubcategoryConfiguration` usa `HasOne<Category>().WithMany()`
   (sem navegações) enquanto as entidades têm `Subcategory.Category` e `Category.Subcategories`. EF pode criar uma
   segunda FK sombra (`CategoryId1`). *A verificar quando o build voltar.*
5. ~~Mensagem de dia do mês "(1-28)" vs. validação 1–31.~~ Corrigido para "(1-31)".
6. `OwnerType = typeof(Transaction)` fixo no erro de subcategoria, também para PlannedTransaction.
7. `Schedule.ChangeEndDate(DateTime)` não aceita `null` → não dá para remover a data de término; e não valida `EndDate >= StartDate`.
8. `DeactivatedAt` é `DateTime` não-nulo → entidades ativas ficam com `0001-01-01`.
9. `ErrorMessageResolver` perde a exceção original em erros inesperados.
10. Domain referencia EF Core só por causa de `[Owned]` no Schedule (o `OwnsOne` no DbContext já basta).

## Decisões já tomadas que o código ainda não reflete (🔧)
Trocar conta do movimento · revalidar Subcategory ao trocar Category ·
reativar entidades (com cascata seletiva na Category) · nomes únicos · tamanho máximo no domínio.
Para o redesenho do Schedule: dia 29–31 em mês curto → último dia do mês (D-B5; ainda não há geração de ocorrências). Detalhes em `09-decisoes.md`.

## Dívida técnica / limpeza
- Typos: `GetEntityEntityByIdService`, arquivo `TransactionFieldCataglog.cs`, testes `NonExistant*` vs `NonExistent*`.
- `ActiveEntityValidator.EnsureIsActive` retorna `IActivatable` e força casts — poderia ser genérico (`T EnsureIsActive<T>(T)`).
- `CreateEntity` marcados `async` sem `await` (warning CS1998).
- Sem configuração Fluent para Transaction/PlannedTransaction (precisão de decimal, tamanhos, índices).
- `BaseRepository` salva a cada operação (sem Unit of Work / transação entre agregados).
- Aviso NU1903: `SQLitePCLRaw.lib.e_sqlite3 2.1.11` com vulnerabilidade alta conhecida (transitivo do EF Sqlite 10.0.2).
- `git` não está no PATH do PowerShell desta máquina (usa o Git do Visual Studio).
