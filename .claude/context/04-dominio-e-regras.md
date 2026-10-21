# Domínio e regras de negócio

## Hierarquia

```
Entity (Id, CreatedAt, UpdatedAt)
├── ActivatableEntity (IsActive, DeactivatedAt)  ── soft-delete, nunca é apagada
│   ├── Account
│   ├── Category ──< Subcategory
│   └── Payee
└── FinancialMovement (abstrata, IDeletable)     ── hard-delete
    ├── Transaction
    └── PlannedTransaction ── Schedule (value object, owned)
```

## Entidades de cadastro (ativáveis)

> Legenda extra: 🔧 = decisão tomada, código ainda **não** reflete (pendente de implementação).

### Account
| Campo | Regra | Status |
|---|---|---|
| Name | obrigatório (não vazio/whitespace); DB max 100 | 🟡 |
| AccountNumber | obrigatório | ✅ D-C1 (provisória) |
| InitialBalance | `>= 0`, nunca negativo; sem data de saldo inicial | ✅ D-C2, D-C3 |

### Category
| Campo | Regra | Status |
|---|---|---|
| Name | obrigatório; DB max 200 | 🟡 |
| Deactivate | **desativa em cascata todas as Subcategories** | ✅ (testado) |

### Subcategory
| Campo | Regra | Status |
|---|---|---|
| Name | obrigatório; DB max 100 | 🟡 |
| Category | obrigatória; deve existir e estar ativa (create e update) | 🟡 |

### Payee (beneficiário/favorecido)
| Campo | Regra | Status |
|---|---|---|
| Name | obrigatório; DB max 150 | 🟡 |

### Regras comuns às ativáveis
- **Reativar** deve existir; reativar Category reativa só as Subcategories desativadas em cascata por ela. 🔧 D-E2, D-E3
- **Não é possível atualizar** uma entidade inativa (`UpdateEntityService` → `EntityInactive`). ✅ testado
- Não é possível **associar** entidade inativa a outra (movimento, subcategoria). ✅ testado
- Nomes devem ser **únicos** (Subcategory: única dentro da Category). 🔧 D-E1 — detalhes em 08 (E1a/E1b)
- Tamanho máximo dos nomes validado **no domínio**. 🔧 D-E4 — limites em 08 (E4a)

## Movimentos financeiros

### FinancialMovement (base)
| Campo | Regra | Status |
|---|---|---|
| Account | obrigatória, existente, ativa (create **e** update) | ✅ testado |
| Amount | `> 0` (sinal vem de `Direction`). Hoje o código aceita 0 | 🔧 D-D1 |
| Direction | `In` / `Out`, precisa ser valor definido do enum | ✅ testado |
| Memo | opcional, livre | 🟡 |
| Identifier | opcional; id externo (ex.: FITID do OFX) para evitar duplicidade na importação | ✅ D-D2 (provisória) |
| Payee | opcional; existente e ativo | ✅ testado |
| Category | opcional; existente e ativa; **preenchida automaticamente** se só a Subcategory for informada | 🔧 D-D4 |
| Subcategory | opcional; existente, ativa e **sempre coerente** com a Category (inclusive ao trocar/remover a Category) | ✅ create / 🔧 update D-D5 |

- Movimentos são **apagados fisicamente** (`Delete*Service`), não desativados. 🟡
- A conta de um movimento **pode ser trocada** (confirmação fica na UI). Hoje o Update DTO não tem `AccountId`. 🔧 D-D3
- App pessoal/familiar: preferir regras **lenientes** a bloqueios rígidos quando houver dúvida (D-A3).

### Transaction (liquidação real)
| Campo | Regra | Status |
|---|---|---|
| DateTime | obrigatório (≠ `default`); inclui hora | ✅ testado |

### PlannedTransaction (compromisso futuro)
| Campo | Regra | Status |
|---|---|---|
| Schedule | obrigatório (value object owned) | 🟡 |
| Status | `Planned` (inicial) / `Fulfilled` / `Ignored` — **nenhum comportamento muda o status ainda** | 🟡 ❓ |

### Schedule (value object de recorrência)
| Campo | Regra | Status |
|---|---|---|
> ⚠️ PlannedTransaction + Schedule estão marcados para **redesenho** (D-A2). As regras abaixo descrevem o código atual.

| RecurrenceType | `Once` (avulso), `Daily`, `Weekly`, `Monthly` | ✅ D-B4 (Once) / 🟡 resto |
| StartDate | obrigatório | 🟡 |
| EndDate | opcional; **não** valida `EndDate >= StartDate` | ❓ |
| Interval | `>= 1` (default 1) — "a cada N dias/semanas/meses" | 🟡 |
| DaysOfWeek | obrigatório (≥1) quando `Weekly` | 🟡 |
| DayOfMonth | obrigatório e entre 1–31 quando `Monthly`; em mês sem esse dia, usa o **último dia do mês** (regra para a futura geração de ocorrências) | ✅ D-B5 |

- Cada `ChangeX` do Schedule chama `Validate()` inteiro na hora.
- Não há cálculo de ocorrências ainda (gerar as datas do agendamento). 📐

## Perguntas centrais de modelagem (ver 08)
- PlannedTransaction é o **template da recorrência** ou **cada ocorrência**? O `Status` vale para a série toda?
- Como uma Transaction liquida (total/parcial) uma ou várias PlannedTransactions?
