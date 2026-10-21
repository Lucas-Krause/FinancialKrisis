# Arquitetura

.NET 10 (`net10.0`, WPF em `net10.0-windows`), C# `latest` (usa recursos do C# 14: `field`,
blocos `extension`). Solução em formato `.slnx`. Estilo Clean Architecture / DDD leve.

## Projetos e dependências

```
FinancialKrisis.Wpf ─────────► Application, Infrastructure         (FrontEnd — vazio hoje)
FinancialKrisis.Infrastructure ► Application, Domain + EF Core Sqlite
FinancialKrisis.Application ──► Domain, Common
FinancialKrisis.Domain ───────► Common + EF Core (só pelo atributo [Owned] no Schedule)
FinancialKrisis.Common         (sem dependências: RuleException<T>, FieldKey)
FinancialKrisis.Tests ────────► Application, Domain, Infrastructure + xUnit + Sqlite in-memory
```

Pastas da solução (`FinancialKrisis.slnx`): `/BackEnd/`, `/FrontEnd/`, `/Tests/`, `/Solution Items/`.


## Responsabilidade de cada camada

### Domain
- Entidades ricas com setters privados/protegidos; toda mutação passa por métodos `ChangeX`/`RemoveX`.
- Validações de **invariantes da própria entidade** (obrigatório, não-negativo, formato do Schedule).
- Lança `DomainRuleException` **sem mensagem** (só código + tipo + campo + dono).
- Define interfaces de repositório (`IGenericRepository<T>`, `IDeletableRepository<T>`, `IFinancialMovementRepository<T>` + uma por entidade).
- Interfaces marcadoras: `IEntity`, `IActivatable` (soft-delete via `Deactivate`), `IDeletable` (hard-delete).

### Application
- **Um serviço por caso de uso** (`CreateAccountService`, `UpdateAccountService`, ...), método único `ExecuteAsync`.
- Bases genéricas: `CreateEntityService`, `UpdateEntityService`, `DeactivateEntityService`,
  `DeleteEntityService`, `GetAllEntitiesService`, `GetEntityEntityByIdService`, e as especializadas
  `CreateFinancialMovementService` / `UpdateFinancialMovementService`.
- Regras que **envolvem mais de uma entidade** ou estado do repositório (entidade inativa,
  subcategoria pertence à categoria) → `ApplicationRuleException`.
- DTOs: `Create*DTO` (propriedades simples) e `Update*DTO` (`Optional<T>` = PATCH semântico).
- Tradução de erros para PT-BR (`ErrorMessageResolver` + catálogos de metadados gramaticais).

### Infrastructure
- `FinancialKrisisDbContext` (DbSets das 6 entidades; `OwnsOne(Schedule)`; aplica `IEntityTypeConfiguration` do assembly).
- Configurações Fluent só para Account, Category, Payee, Subcategory (não há para Transaction/PlannedTransaction).
- `AuditSaveChangesInterceptor` preenche `CreatedAt`/`UpdatedAt` (`DateTime.Now`).
- `BaseRepository<T>`: cada operação de escrita chama `SaveChangesAsync` (sem Unit of Work).
  `GetByIdOrThrowAsync` lança `DomainRuleException(EntityNotFound)`. `GetAllAsync` usa `AsNoTracking`.
- Sem migrations ainda; testes usam `EnsureCreated`.

### Wpf
- Só o template (App + MainWindow vazios). Sem composição de DI, sem connection string.

## Fluxo de uma operação (ex.: criar Transaction)

1. UI/teste monta `CreateTransactionDTO` e chama `CreateTransactionService.ExecuteAsync`.
2. `CreateEntityService.ExecuteAsync` → `CreateFinancialMovementService.CreateEntity`:
   - busca Account (obrigatória), Payee/Category/Subcategory (opcionais) via `GetByIdOrThrowAsync`;
   - cada uma passa por `ActiveEntityValidator.EnsureIsActive`;
   - valida se a subcategoria pertence à categoria (quando ambas informadas).
3. `CreateMovement` (abstrato) instancia a entidade → construtor valida invariantes.
4. `repository.AddAsync` → `SaveChangesAsync` → interceptor marca `CreatedAt`.
5. Qualquer exceção é capturada e passa por `ErrorMessageResolver.Resolve`, que devolve uma nova
   exceção do mesmo tipo **com mensagem em português**.
