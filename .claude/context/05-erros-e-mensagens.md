# Erros de regra e mensagens

## Princípio 🟡
O **Domain e a Application lançam exceções sem texto**, carregando só dados estruturados.
O texto em português é montado **uma única vez**, no `catch` dos serviços, pelo
`ErrorMessageResolver` — usando metadados gramaticais (nome PT + gênero) para concordância.

## Peças

| Peça | Onde | Papel |
|---|---|---|
| `RuleException<TErrorCode>` | Common | Base: `ErrorCode`, `Type` (entidade), `Field` (`FieldKey?`), `OwnerType` (`Type?`) |
| `DomainRuleException` + `DomainRuleErrorCode` | Domain | `RequiredField`, `NegativeValue` (< 0), `ValueMustBePositive` (<= 0), `EntityNotFound`, `EntityAlreadyActive`, `EntityAlreadyInactive`, `WeeklyScheduleRequiresDaysOfWeek`, `MonthlyScheduleRequiresValidDayOfMonth` |
| `ApplicationRuleException` + `ApplicationRuleErrorCode` | Application | `SubcategoryDoesNotBelongToCategory`, `EntityInactive`, `ValueAlreadyInUse` |
| `FieldKey` | Common | `record FieldKey(string Value)` — identifica um campo; declarado em `Entidade.Fields` |
| `GrammarMetadata(NamePt, Gender)` | Application | Nome em PT + `GrammaticalGender` (Masculine/Feminine) |
| `TypeCatalog` | Application | `Type` → GrammarMetadata (ex.: `Account` → "Conta", feminino) |
| `{Entidade}FieldCatalog` | Application | `FieldKey` → GrammarMetadata. Transaction/PlannedTransaction herdam de `FinancialMovementFieldCatalog` via construtor estático |
| `GrammaticalGenderExtensions.ToPtArticle()` / `ToPtIndefiniteArticle()` | Application | "o"/"a" (também "do/da", "obrigatório/obrigatória", "mesmo/mesma") e "um"/"uma" |
| `ErrorMessageResolver.Resolve(Exception)` | Application | Recria a exceção com mensagem; exceções desconhecidas viram `Exception("Ocorreu um erro inesperado: ...")` |

## Exemplos de mensagens geradas
- `RequiredField`, Account, Name → "O nome da conta é obrigatório."
- `NegativeValue`, Account, InitialBalance → "O saldo inicial da conta não pode ser negativo."
- `ValueMustBePositive`, Transaction, Amount → "O montante da transação deve ser maior que zero."
- `EntityNotFound`, Payee → "O beneficiário não foi encontrado."
- `EntityAlreadyActive`, Category → "A categoria já está ativa."
- `EntityAlreadyInactive`, Payee → "O beneficiário já está inativo."
- `ValueAlreadyInUse`, Category, Name → "Já existe uma categoria com o mesmo nome." (`if (await EntityRepository.ExistsByNameAsync(...)) throw ...` direto no serviço)
- `EntityInactive`, Category → "A categoria não está ativa."
- `EntityInactive`, Category, dono Subcategory → "A categoria da subcategoria não está ativa."
  (usado quando a categoria é preenchida automaticamente pela subcategoria — `ActiveEntityValidator.EnsureIsActive(entidade, typeof(Dono))`)
- `SubcategoryDoesNotBelongToCategory` (Subcategory, campo Category, dono Transaction) →
  "A subcategoria não pertence à categoria da transação."

## Checklist para um novo erro
1. Adicionar o código no enum certo (`DomainRuleErrorCode` se for invariante da entidade;
   `ApplicationRuleErrorCode` se depende de outras entidades/repositório).
2. Lançar com `(código, typeof(Entidade), Entidade.Fields.Campo, typeof(Dono)?)`.
3. Adicionar o `case` em `ErrorMessageResolver` (respeitando concordância de gênero).
4. Garantir que o tipo está em `TypeCatalog` e o campo no `{Entidade}FieldCatalog`
   (senão `KeyNotFoundException` no resolver!).
5. Teste com `ShouldFailWith{Domain|Application}RuleException(código, tipo, campo, dono)`.

Skill relacionada: [`nova-regra-de-validacao`](../skills/nova-regra-de-validacao/SKILL.md).

## Princípio de contexto (✅ conversa de 2026-10-21)
Quando o usuário não informou diretamente a entidade que causou o erro (ex.: categoria derivada da subcategoria),
a mensagem deve dizer **de onde ela veio** usando o `OwnerType`, em vez de uma mensagem genérica.

O texto final das mensagens **não** é testado por igualdade de string (ver `06-testes.md`).

## Pontos de atenção
- `SubcategoryDoesNotBelongToCategory` usa `OwnerType = typeof(Transaction)` fixo, mesmo para PlannedTransaction.
- `Resolve` descarta a exceção original (sem `InnerException`) — perde stack trace para debug.
- Erros de Schedule às vezes passam `OwnerType = PlannedTransaction`, às vezes não (StartDate/Interval).
