# Testes

xUnit 2.9 + SQLite **in-memory** (conexão aberta por `TestContext`) + DI real
(`TestServiceProviderFactory`). São testes de **serviço** (integração Application + Domain +
Infrastructure), não unitários isolados. Não há mocks.

## DSL de cenários (fluent)

```csharp
new TestContext()
    .Account().Create().AsCurrentAccount().ShouldCreateSuccessfully()
    .Category().Create().AsCurrentCategory().ShouldCreateSuccessfully()
    .Subcategory().CreatingWithCurrentCategory().Create().AsCurrentSubcategory().ShouldCreateSuccessfully()
    .FinancialMovement(pMovementType)
        .CreatingWithCurrentAccount()
        .CreatingWith(CreateInput => CreateInput.Amount = -1)
        .Create()
        .ShouldFailWithDomainRuleException(DomainRuleErrorCode.NegativeValue, pMovementType, Transaction.Fields.Amount);
```

### Peças
- **`TestContext`** — cria provider/scope, expõe todos os serviços, guarda a entidade "atual" por tipo
  (`SetCurrent`/`TryGetCurrent`/`GetCurrentOrThrow`) e fabrica cenários (`.Account()`, `.Transaction()`, `.FinancialMovement(Type)`).
- **`Scenario<TScenario, TCreateInput, TUpdateInput, TEntity>`** — base genérica (CRTP):
  - `CreatingWith(...)` / `UpdatingWith(...)` configuram os DTOs;
  - `Create()` / `Update()` / `Deactivate()` executam o serviço e **capturam** a exceção em `LastException`;
  - `AsCurrent()` registra a entidade no contexto.
- **`{Entidade}Scenario`** — define valores default válidos no construtor, liga `CreateFunc`/`UpdateFunc`/`DeactivateFunc`
  e expõe `AsCurrent{Entidade}()` e helpers `CreatingWithCurrent{Relacionada}()`.
- **`FinancialMovementScenario`** + `IFinancialMovementScenario` — permite rodar o **mesmo teste para Transaction e
  PlannedTransaction** via `[Theory] [MemberData(nameof(FinancialMovementTypes))]`.
- **`BaseScenarioAssertions`** (blocos `extension` do C# 14):
  - `ShouldCreateSuccessfully()` / `ShouldUpdateSuccessfully()` — compara por **reflexão** as propriedades do DTO com as
    de mesmo nome na entidade (Optional indefinido é ignorado; `Guid.Empty` ↔ `null` é aceito);
  - `ShouldFailWithDomainRuleException(...)` / `ShouldFailWithApplicationRuleException(...)` — confere tipo, código,
    tipo da entidade, campo, dono e que a mensagem não é vazia;
  - `ShouldDeactivateSuccessfully()` / `ShouldBeInactive()`.
  - As assertions retornam o `TestContext` para encadear o próximo cenário.
  - Toda assertion de sucesso/estado (`ShouldCreateSuccessfully`, `ShouldBeActive`, `ShouldBeInactive`, ...) começa relançando
    `LastException`, para a falha mostrar o erro real em vez de um "esperava X, mas está Y" enganoso.
- **`FinancialMovementScenarioAssertions`** — assertions específicas de movimento quando a comparação DTO × entidade não serve
  (ex.: `ShouldHaveCurrentCategory()`, para quando a categoria é preenchida automaticamente).

## Mensagens de falha dos asserts
São para o **desenvolvedor**, não para o usuário — por isso **não** passam pelo `ErrorMessageResolver`.
Padrão: português, `"Esperava que ..., mas ..."`, com nomes técnicos (tipo, propriedade, Id) entre aspas simples,
em uma única linha de `throw new XunitException(...)`.
As mensagens para o **usuário** são testadas indiretamente: `ShouldFailWith*RuleException` garante que a mensagem não é vazia
e confere os dados estruturados (`ErrorCode`, `Type`, `Field`, `OwnerType`).
**Não criar testes que comparem o texto exato das mensagens** (✅ decisão do Lucas, 2026-10-21): o `ErrorMessageResolver` existe
justamente para não ter texto fixo espalhado; asserts de frase quebrariam a cada ajuste de redação. Teste os dados estruturados.

## Organização
`FinancialKrisis.Tests/ServiceTests/{EntidadePlural}/{Verbo}{Entidade}ServiceTests.cs`.
Testes comuns aos movimentos ficam em `ServiceTests/FinancialMovement/`.

## Cobertura típica por entidade
- Create: válido; cada campo obrigatório/inválido; relacionadas inexistentes; relacionadas inativas.
- Update: válido; campo inválido via `Optional<T>.Remove()`; entidade inexistente; entidade inativa; relacionadas inexistentes/inativas.
- Deactivate: válido; inexistente; cascata (Category → Subcategory).

## Lacunas conhecidas
- `CreatePlannedTransactionServiceTests` está vazio (validações do Schedule sem teste).
- A comparação por reflexão **não verifica o Schedule** (campos do DTO não existem na entidade com o mesmo nome → ignorados).
- Delete de Transaction/PlannedTransaction e os serviços `Get*` não têm teste.
- Pacote `Microsoft.EntityFrameworkCore.InMemory` referenciado mas não usado.

## Rodar
```bash
dotnet test FinancialKrisis.slnx
```
