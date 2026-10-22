# Registro de decisões

Só entra aqui o que o Lucas confirmou. "Provisória" = vale por enquanto, mas ele sinalizou que pode mudar.
O id (`D-xx`) referencia a pergunta original de `08-duvidas-abertas.md`.

## 2026-10-21 — primeira rodada de respostas

### Visão e escopo
| Id | Decisão | Força |
|---|---|---|
| D-A1 | A "regra de ouro" (futuro = compromisso, passado = liquidação, saldo = cálculo) **vale**. | Provisória — pode amadurecer |
| D-A2 | `PlannedTransaction` **substitui** Payable/Receivable do RESUMO. Mas o Lucas não gostou de como ficou: está **aberto a redesenhar ou recomeçar** essa parte inteira. | Firme quanto à substituição; desenho em aberto |
| D-A3 | Uso **pessoal/familiar**, single-user. **Sem multi-moeda** por ora; deixar preparado se não for complexo. Por ser pessoal/familiar, certas ações podem ser mais **lenientes**. | Firme |
| D-A4 | Prioridade atual: **rever os conceitos**, entender onde parou, avaliar se os padrões são bons para o futuro — antes de implementar coisas novas. | Firme |

### Recorrência
| Id | Decisão | Força |
|---|---|---|
| D-B4 | `RecurrenceType.Once` = compromisso avulso. | Provisória |
| D-B5 | Recorrência num dia que não existe no mês (29–31) → usa o **último dia do mês**. Validação continua 1–31; a mensagem "(1-28)" está errada. | Firme |

### Account
| Id | Decisão | Força |
|---|---|---|
| D-C1 | `AccountNumber` continua obrigatório. | Provisória |
| D-C2 | `InitialBalance` **nunca negativo** (regra atual mantida). | Firme |
| D-C3 | Não precisa de data do saldo inicial. | Firme |

### Movimentos
| Id | Decisão | Força |
|---|---|---|
| D-D1 | `Amount` deve ser **> 0** (zero não faz sentido). **Feito** (`ValueMustBePositive`). | Firme |
| D-D2 | `Identifier` = identificador externo (ex.: FITID do OFX) para **evitar duplicidade na importação**. | Provisória |
| D-D3 | Deve ser possível **trocar a conta** de um movimento; a confirmação é responsabilidade da UI. **Feito** (`AccountId` no update; remover a conta → `RequiredField`). | Firme |
| D-D4 | Informar só a Subcategory → **preencher a Category automaticamente** a partir dela. **Feito** (create e update). | Firme |
| D-D5 | Subcategory deve ser **sempre coerente** com a Category. Ao trocar/remover a Category, a Subcategory incoerente é **removida automaticamente** (sem erro). **Feito.** | Firme |

### Cadastros
| Id | Decisão | Força |
|---|---|---|
| D-E1 | Nomes **únicos** (Account, Category, Payee; Subcategory dentro da Category). Ainda definir: case-insensitive? só entre ativos? | Firme no princípio |
| D-E2 | Deve existir **reativar**. **Feito** (Account, Category, Subcategory, Payee). Só pode reativar o que está **inativo** (senão `EntityAlreadyActive`) e só pode desativar o que está **ativo** (senão `EntityAlreadyInactive`). | Firme |
| D-E3 | Reativar Category reativa **só as Subcategories que foram desativadas junto com a Category** (não as desativadas manualmente antes). **Feito** (`Subcategory.DeactivatedByCategory`). | Firme |
| D-E3a | Reativar Subcategory com a Category inativa → **erro** ("A categoria da subcategoria não está ativa."), sem reativar a Category junto. **Feito.** | Firme |
| D-E4 | Validar **tamanho máximo dos nomes no domínio**. | Firme |

### Técnico / processo
| Id | Decisão | Força |
|---|---|---|
| D-F1 / D-G1 | **Não reverter nada no git.** Corrigido o `)` sobrando em `Schedule.Validations.cs`, mantido o trabalho não commitado (`Type`/`OwnerType` nas exceções). PlannedTransaction + Schedule passam a ser **área a redesenhar**. Build verde, 93 testes passando (2026-10-21). | Firme |
| D-F2 | A pasta órfã `Common/` pode ser apagada. **Feito.** | Firme |
| D-F3 | Prefixo `p` em parâmetros é **obrigatório**. Garantido por hook (`.claude/hooks/check-param-prefix.ps1`). | Firme |
| D-F4 | Hooks: sim, sem bloqueios excessivos. Implementados: aviso de prefixo `p` ao editar `.cs`; `dotnet build` + `dotnet test` ao final do turno quando houve edição em `.cs`. | Firme |
| D-F5 | Claude pode commitar, mas **sempre confirma a mensagem** antes. Padrão de mensagem ainda a definir (skill `commit`). | Firme |
| D-F6 | Datas em **hora local** (`DateTime.Now`). | Firme |
| D-T1 | **Não testar o texto exato** das mensagens de erro; testar `ErrorCode`/`Type`/`Field`/`OwnerType`. | Firme |
| D-M1 | Quando a entidade com erro não foi informada diretamente (ex.: categoria derivada da subcategoria), a mensagem usa o `OwnerType` para dizer de onde ela veio. | Firme |
