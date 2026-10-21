# Convenções de código (cultura do projeto)

Inferido do código existente (🟡). Ao escrever código novo, **seguir estes padrões** a menos que o
Lucas decida mudar.

## Idiomas
- **Código** (classes, métodos, variáveis, nomes de teste): inglês.
- **Mensagens ao usuário** (erros) e **mensagens de assert nos testes**: português.
- **Commits**: português. O Claude pode commitar, mas sempre confirma a mensagem antes (✅ D-F5, skill `commit`). Padrão ainda em definição.
- **Datas**: hora local, `DateTime.Now` (✅ D-F6).

## Nomenclatura
- **Parâmetros de método/construtor com prefixo `p` — OBRIGATÓRIO** (✅ D-F3): `pName`, `pAccount`, `pCreateDTO`, `pEx`.
  Inclui primary constructors e blocos `extension(...)`. Também usado em lambdas de configuração (`pBuilder`, `pOptions`).
  - Verificado automaticamente pelo hook `.claude/hooks/check-param-prefix.ps1` ao editar `.cs`.
  - Exceções conhecidas: parâmetros posicionais de `record` (viram propriedades, PascalCase) e lambdas curtas (`p => p.Id`, `CreateInput => ...`).
  - Violações existentes hoje: `context` em `AccountRepository`/`CategoryRepository`/`PayeeRepository`, `category` em `Subcategory.BelongsToCategory`.
- Campos privados: `_camelCase` (`_dbSet`, `_entities`).
- Variáveis locais: `camelCase`, sem prefixo.
- Sufixo `DTO` em maiúsculas (`CreateAccountDTO`), não `Dto`.
- Serviços: `{Verbo}{Entidade}Service` — `Create`, `Update`, `Deactivate`, `Delete`, `GetAll{Plural}`, `Get{Entidade}ById`.
- Testes: `{Condição}_Should{Resultado}` (ex.: `NegativeAmount_ShouldFailWithDomainRuleException`).

## Organização das entidades (arquivos "nested" via `partial`)
Cada entidade/value object do Domain é dividida em 4 arquivos parciais:

| Arquivo | Conteúdo |
|---|---|
| `X.cs` | Construtor de negócio (valida, gera `Id = Guid.NewGuid()`, atribui) |
| `X.Properties.cs` | Construtor vazio público (para EF), classe estática aninhada `Fields` com `FieldKey`s, propriedades e navegações |
| `X.Behaviors.cs` | Métodos de mutação: `ChangeX(...)` (valida + atribui), `RemoveX()`, overrides como `Deactivate()` |
| `X.Validations.cs` | Métodos `ValidateX(...)` (normalmente `private static`) que lançam `DomainRuleException` |

- Setters `private set` (ou `protected set` em bases).
- Navegações obrigatórias: `= null!`; coleções: `= []`.
- Subclasses de `FinancialMovement` redeclaram `public static new class Fields` reexpondo os campos da base + os próprios.

## Estilo C#
- Namespaces **file-scoped**. Os namespaces **não espelham as pastas** de forma estrita
  (ex.: `Base/Entities/*` e `Entities/*` → `FinancialKrisis.Domain.Entities`; todas as interfaces → `FinancialKrisis.Domain.Interfaces`; todos os serviços → `FinancialKrisis.Application.Services`; todos os DTOs → `FinancialKrisis.Application.DTOs`).
- **Primary constructors** para serviços, repositórios e exceções.
- `if` de uma linha **sem chaves**.
- Tipos explícitos na maioria das declarações; `var` usado principalmente quando há cast (`var account = (Account)...`).
- Sem comentários no código (o código deve se explicar).
- Coleções com collection expressions (`[]`).
- Pattern matching (`is null or < 1 or > 31`, `switch` expressions).

## Padrões de aplicação
- Todo `ExecuteAsync` de serviço envolve a lógica em `try/catch (Exception pEx) { throw ErrorMessageResolver.Resolve(pEx); }`.
- Update parcial com `Optional<T>`:
  - `Undefined()` → não mexe no campo;
  - `From(v)` / conversão implícita → altera;
  - `Remove()` → define como `default` (null/0/Guid.Empty).
  - Para relações (`Optional<Guid>`): `EntityRelationUpdateHelper.ShouldAssign` (≠ Guid.Empty) / `ShouldRemove` (= Guid.Empty).
- Entidades relacionadas são sempre buscadas com `GetByIdOrThrowAsync` + `ActiveEntityValidator.EnsureIsActive` antes de associar.
- Associação de relação passa pela **entidade** (`ChangeCategory(Category)`), não só pelo Id — atualiza Id e navegação juntos.
