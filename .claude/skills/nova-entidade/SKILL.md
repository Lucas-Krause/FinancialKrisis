---
name: nova-entidade
description: Checklist para criar uma nova entidade (ou value object) no FinancialKrisis de ponta a ponta — Domain, Application, Infrastructure, metadados de erro, DI e testes de cenário — seguindo os padrões do projeto. Usar ao adicionar qualquer entidade nova ou um novo tipo de FinancialMovement.
---

# Nova entidade no FinancialKrisis

Antes de começar, leia `.claude/context/03-convencoes-de-codigo.md` e `04-dominio-e-regras.md`.
Escolha a base: `ActivatableEntity` (cadastro, soft-delete), `FinancialMovement` (movimento, hard-delete)
ou value object owned (sem Id).

Use uma entidade existente parecida como modelo e **copie o estilo dela**:
- cadastro simples → `Payee`;
- cadastro com relação obrigatória → `Subcategory`;
- movimento → `Transaction` / `PlannedTransaction`;
- value object → `Schedule`.

## 1. Domain (`FinancialKrisis.Domain`)
- [ ] `Entities/X.cs` — construtor de negócio: chama `ValidateX` de cada campo, `Id = Guid.NewGuid()`, atribui.
- [ ] `Entities/X.Properties.cs` — `public X() { }` (EF), `public static class Fields` com um `FieldKey` por campo, propriedades `{ get; private set; }`, navegações `= null!` / coleções `= []`.
- [ ] `Entities/X.Behaviors.cs` — `ChangeY(pNovoValor)` (valida + atribui); `RemoveY()` para opcionais; relação recebe a **entidade** e seta Id + navegação.
- [ ] `Entities/X.Validations.cs` — `private static void ValidateY(...)` lançando `DomainRuleException(código, typeof(X), Fields.Y)`.
- [ ] `Base/Interfaces/Repositories/IXRepository.cs` — `: IGenericRepository<X>` (ou `IFinancialMovementRepository<X>` para movimentos), namespace `FinancialKrisis.Domain.Interfaces`.
- [ ] Novo código de erro? → seguir a skill `nova-regra-de-validacao`.

## 2. Application (`FinancialKrisis.Application`)
- [ ] `DTOs/X/CreateXDTO.cs` (`: ICreateDTO`, props simples) e `UpdateXDTO.cs` (`: IUpdateDTO`, `Guid Id` + `Optional<T>` com `= Optional<T>.Undefined()`). Namespace `FinancialKrisis.Application.DTOs`.
- [ ] `Services/X/` — um arquivo por caso de uso herdando as bases genéricas (`CreateEntityService`, `UpdateEntityService`, `DeactivateEntityService` ou `DeleteEntityService`, `GetAllEntitiesService`, `GetEntityEntityByIdService`). Namespace `FinancialKrisis.Application.Services`.
  - Relações: `GetByIdOrThrowAsync` + `ActiveEntityValidator.EnsureIsActive` antes de associar.
  - Update de relação: `EntityRelationUpdateHelper.ShouldAssign/ShouldRemove`.
- [ ] `Metadata/Types/TypeCatalog.cs` — registrar `typeof(X)` com nome PT e gênero.
- [ ] `Metadata/Fields/XFieldCatalog.cs` — um item por `X.Fields.*`.
- [ ] `Helpers/ErrorMessages/ErrorMessageResolver.cs` — adicionar `X` no `ResolveField`.

## 3. Infrastructure (`FinancialKrisis.Infrastructure`)
- [ ] `Repositories/XRepository.cs` — `: BaseRepository<X>(pContext), IXRepository`; sobrescrever `GetByIdAsync` com `Include` se precisar de navegações.
- [ ] `Persistence/Context/FinancialKrisisDbContext.cs` — `DbSet<X>`.
- [ ] `Persistence/Configurations/XConfiguration.cs` — tabela, chave, `IsRequired`, `HasMaxLength`, `decimal(18,2)`, FKs com `DeleteBehavior.Restrict`, índices. Configure relações **usando as navegações existentes** (evitar `HasOne<T>()` sem navegação quando a entidade tem a navegação).

## 4. Testes (`FinancialKrisis.Tests`)
- [ ] `TestInfrastructure/TestServiceProviderFactory.cs` — `AddScoped` do repositório e de todos os serviços.
- [ ] `Scenarios/Base/TestContext.cs` — propriedades dos serviços, resolução no construtor, fábrica `.X()`.
- [ ] `Scenarios/Entities/XScenario.cs` — defaults válidos no construtor, `CreateFunc`/`UpdateFunc`/`DeactivateFunc`, `AsCurrentX()`, `CreatingWithCurrentY()`/`UpdatingWithCurrentY()`.
- [ ] `ServiceTests/XPlural/{Create,Update,Deactivate}XServiceTests.cs` — cobrir: válido; cada campo inválido; inexistente; inativo; relações inexistentes/inativas. Nomes `Condicao_ShouldResultado`.
- [ ] Movimento novo? Adicionar o tipo em `TestContext.FinancialMovement(Type)` e em `FinancialMovementTypes` dos testes comuns.

## 5. Fechamento
- [ ] `dotnet build FinancialKrisis.slnx` e `dotnet test FinancialKrisis.slnx` passando.
- [ ] Atualizar `.claude/context/04-dominio-e-regras.md` e `07-estado-atual.md`.
- [ ] Regras assumidas sem confirmação → registrar em `08-duvidas-abertas.md`.
