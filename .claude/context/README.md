# Contexto do FinancialKrisis

Base de conhecimento do projeto para humanos e para o Claude. Mantida viva: toda regra
confirmada, decisão tomada ou dúvida resolvida deve ser registrada aqui.

## Índice

| Arquivo | Conteúdo |
|---|---|
| [01-visao-de-negocio.md](01-visao-de-negocio.md) | Propósito do sistema, modelo conceitual, "regra de ouro" |
| [02-arquitetura.md](02-arquitetura.md) | Camadas, dependências, fluxo de uma operação |
| [03-convencoes-de-codigo.md](03-convencoes-de-codigo.md) | Cultura do código: nomes, organização de arquivos, idiomas |
| [04-dominio-e-regras.md](04-dominio-e-regras.md) | Entidades, campos e regras de negócio implementadas |
| [05-erros-e-mensagens.md](05-erros-e-mensagens.md) | Sistema de exceções de regra e mensagens em PT-BR |
| [06-testes.md](06-testes.md) | DSL de cenários, padrões de teste |
| [07-estado-atual.md](07-estado-atual.md) | O que está pronto, o que falta, bugs e dívida técnica |
| [08-duvidas-abertas.md](08-duvidas-abertas.md) | Perguntas pendentes de resposta do Lucas |
| [09-decisoes.md](09-decisoes.md) | Registro de decisões confirmadas (ADR simplificado) |
| [10-avaliacao-dos-padroes.md](10-avaliacao-dos-padroes.md) | O que manter, ajustar e repensar; ordem sugerida para retomar |

## Automação
- Skills ([`../skills/`](../skills/)): `nova-entidade`, `nova-regra-de-validacao`, `commit`.
- Hooks ([`../settings.json`](../settings.json), scripts em [`../hooks/`](../hooks/)):
  - **PostToolUse Edit|Write** → `check-param-prefix.ps1`: avisa parâmetros sem prefixo `p` em `.cs` e marca que houve edição de código.
  - **Stop** → `build-and-test.ps1`: se houve edição em `.cs` no turno, roda `dotnet build` + `dotnet test`; se falhar, devolve o erro ao Claude (uma vez).

## Legenda de status usada nos documentos

- ✅ **Confirmado** — implementado e/ou validado pelo Lucas
- 🟡 **Inferido** — está no código, mas ainda não foi confirmado como regra intencional
- 📐 **Conceitual** — existe só na visão (`01-visao-de-negocio.md`), não está no código
- ❓ **Dúvida** — ver `08-duvidas-abertas.md`

## Como manter

1. Ao confirmar uma regra com o Lucas: mudar o status para ✅ e registrar em `09-decisoes.md`.
2. Ao resolver uma dúvida: mover de `08` para `09` com a resposta.
3. Ao implementar algo novo: atualizar `04` e `07`.
