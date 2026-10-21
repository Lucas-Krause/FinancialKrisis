# Avaliação dos padrões (para D-A4)

Opinião técnica do Claude sobre o que foi feito, pensando no futuro do projeto (saldo calculado,
compromissos, liquidação/conciliação, importação). **Nada aqui é decisão** — é insumo para discutir.

## ✅ Manter — são bons padrões

| Padrão | Por que é bom | Ressalva |
|---|---|---|
| Entidades ricas (setters privados, `ChangeX` validando) | Invariantes ficam garantidas em um lugar só; difícil criar estado inválido | — |
| Exceções sem texto + `ErrorMessageResolver` com metadados gramaticais | Domínio limpo, mensagens PT-BR consistentes, a UI pode usar `ErrorCode`/`Field` para destacar o campo com erro | Esquecer de registrar um campo no catálogo gera `KeyNotFoundException`. Vale um teste que varre todos os `Fields` por reflexão e confere os catálogos |
| `Optional<T>` nos Update DTOs | Encaixa perfeitamente em formulários de edição (só manda o que mudou, distingue "não mexer" de "limpar") | `Remove()` para `Guid` usa `Guid.Empty` como sentinela — funciona, mas é implícito |
| Camadas (Domain / Application / Infrastructure / UI) | Permite trocar UI ou banco sem mexer nas regras | Domain depende do EF só por `[Owned]` — fácil de remover |
| DSL de cenários nos testes, com SQLite real | Testes legíveis, testam o fluxo de verdade, `[Theory]` por tipo de movimento evita duplicação | Ver "Ajustar" abaixo |
| `FieldKey` tipado por entidade | Sem strings mágicas espalhadas | — |

## ⚠️ Ajustar — bons, mas vão doer quando o sistema crescer

1. **Repositório salvando a cada operação (sem Unit of Work).** Hoje cada `AddAsync`/`UpdateAsync` chama `SaveChanges`.
   Quando uma Transaction liquidar vários compromissos (B2), tudo precisa ser salvo **junto ou nada**. Recomendo
   mover o `SaveChanges` para o fim do serviço (um commit por caso de uso) antes de implementar liquidação.
2. **`try/catch` + `Resolve` em cada serviço base.** Funciona, mas espalha a tradução e **descarta a exceção original**
   (perde stack trace). Melhor: traduzir num ponto só na fronteira (o ViewModel/handler de erro da UI) ou num
   decorator, guardando a original como `InnerException`.
3. **Herança genérica profunda nos serviços** (`CreateFinancialMovementService<TMovement, TRepository, TCreateDTO> : CreateEntityService<...>`).
   Ótima para CRUD, mas casos de uso que não são CRUD (liquidar, conciliar, calcular saldo, importar) não vão caber nela.
   Sugestão: manter as bases para CRUD e escrever serviços "normais" (sem herdar) para o resto.
4. **Comparação por reflexão nos asserts** ignora em silêncio propriedades que não existem com o mesmo nome na entidade
   (por isso o Schedule nunca é conferido). Sugestão: falhar quando uma propriedade do DTO não tem correspondente, com
   uma lista explícita de exceções.
5. **Dependência de navegação já carregada** (`pMovement.Category!`). Funciona nos testes por causa do tracking do EF;
   no app real vai quebrar. Repositórios de movimento devem dar `Include` ou o serviço deve buscar pelo Id.
6. **`ActiveEntityValidator` não genérico** → casts em todo lugar. `T EnsureIsActive<T>(T) where T : IActivatable` resolve.

## 🔄 Repensar — no redesenho da PlannedTransaction (D-A2)

- **`Transaction` e `PlannedTransaction` herdando a mesma base.** Pela regra de ouro, compromisso (futuro) e liquidação
  (passado) são **conceitos diferentes**: um tem vencimento, status e recorrência; o outro tem data real e afeta saldo.
  Hoje compartilham tudo (conta, valor, categoria...), o que é cômodo, mas acopla os dois. Alternativas a discutir:
  manter a base só com o que é realmente comum, ou compor (ex.: um "conteúdo do lançamento" reutilizado pelos dois).
- **Recorrência dentro da própria PlannedTransaction** (sem ocorrências) — é a raiz das dúvidas B1/B2/B3.
- **Status único para a série** — não comporta "março pago, abril não".

## 💡 Multi-moeda "preparado" (D-A3)
Forma barata: adicionar `Currency` (ex.: `"BRL"`) em `Account`, com default BRL, e assumir que todo movimento usa a moeda
da sua conta. Não exige mudar `Amount` agora; só bloqueia (ou converte, no futuro) transferências entre moedas diferentes.

## Sugestão de ordem para retomar
1. ~~Decidir **G1** (o que reverter) e deixar o build verde.~~ Feito em 2026-10-21 (sem reverter; build verde).
2. Aplicar as correções pequenas já decididas (lista 🔧 em `07-estado-atual.md`).
3. Ajustes estruturais 1, 2 e 5 acima (pré-requisitos da liquidação).
4. **Sessão de design** dos compromissos: B1 → B2 → B3 → B6 → C4 → D6.
5. Cálculo de saldo (atual e futuro).
6. UI WPF.
