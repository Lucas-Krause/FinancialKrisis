---
name: nova-regra-de-validacao
description: Como adicionar uma nova regra de validação/negócio no FinancialKrisis — escolher DomainRuleException vs ApplicationRuleException, criar o código de erro, montar a mensagem em PT-BR com concordância de gênero e testar. Usar sempre que uma regra nova precisar lançar erro.
---

# Nova regra de validação

Referência completa: `.claude/context/05-erros-e-mensagens.md`.

## 1. Onde a regra mora?
- **Domain** (`DomainRuleException` / `DomainRuleErrorCode`): invariante que a entidade consegue checar
  sozinha com os próprios dados (obrigatório, não-negativo, formato, coerência entre campos dela).
  Fica em `X.Validations.cs` e é chamada no construtor e no `ChangeY` correspondente.
- **Application** (`ApplicationRuleException` / `ApplicationRuleErrorCode`): depende de outra entidade,
  do estado do banco ou do caso de uso (inativa, pertence a outra, unicidade). Fica no serviço ou em
  `Helpers/Validators/`.

Antes de criar um código novo, verifique se um existente serve (`RequiredField`, `NegativeValue`,
`EntityNotFound`, `EntityInactive`, ...). Códigos são **genéricos por tipo de violação**, não por campo.

## 2. Lançar
```csharp
throw new DomainRuleException(DomainRuleErrorCode.Codigo, typeof(Entidade), Entidade.Fields.Campo);
// com dono (ex.: campo de um value object pertencente a uma entidade):
throw new DomainRuleException(DomainRuleErrorCode.Codigo, typeof(Schedule), Schedule.Fields.Campo, typeof(PlannedTransaction));
```
Nunca passe texto na exceção — a mensagem é montada pelo resolver.

## 3. Mensagem (`Application/Helpers/ErrorMessages/ErrorMessageResolver.cs`)
- Adicione o `case` no `switch` do método certo (`GetDomainExceptionMessage` ou `GetApplicationRuleExceptionMessage`).
- Use as variáveis já calculadas: `fieldArticle`, `typeArticle`, `fieldMetadata.NamePt`, `typeMetadata.NamePt`, `ownedText`.
- Concordância: `"obrigatóri{fieldArticle}"`, `"d{typeArticle}"` → "do/da", `"{typeArticle.ToUpper()}"` → "O/A".
- Confirme que o tipo está em `TypeCatalog` e o campo no `{Entidade}FieldCatalog` (senão `KeyNotFoundException`).
- Leia a frase final em voz alta para checar o português, para masculino e feminino.

## 4. Testar
```csharp
.ShouldFailWithDomainRuleException(DomainRuleErrorCode.Codigo, typeof(Entidade), Entidade.Fields.Campo /*, typeof(Dono) */);
```
Crie o teste tanto no Create quanto no Update quando a regra se aplica aos dois. Para regras de
FinancialMovement, use `[Theory] [MemberData(nameof(FinancialMovementTypes))]`.

## 5. Documentar
- Atualizar a tabela da entidade em `.claude/context/04-dominio-e-regras.md`.
- Se a regra foi decidida com o Lucas, registrar em `09-decisoes.md`.
