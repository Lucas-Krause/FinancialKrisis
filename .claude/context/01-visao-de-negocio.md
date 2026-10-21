# Visão de negócio

> Fonte: antigo arquivo "RESUMO DAS COISAS A FAZER NO FINANCIAL KRISIS" (marcado pelo autor como
> desatualizado; removido em 2026-10-21 — este documento é agora a referência da visão) + leitura do código.

## Propósito 📐

O FinancialKrisis **não é um app de banco**. É uma ferramenta pessoal de planejamento financeiro
que responde:

- Quanto dinheiro eu realmente tenho?
- Quanto vou ter até uma data específica?
- O que é dinheiro livre vs. comprometido?
- Qual é meu cenário financeiro realista?

## Regra de ouro ✅ (D-A1, provisória)

> Tudo que é futuro é **compromisso**.
> Tudo que é passado é **liquidação**.
> Tudo que é "saldo" é **cálculo**.

- Saldo **nunca é armazenado**; é calculado: `saldo base da conta + recebíveis até X − pagáveis até X`.
- O core do sistema são **compromissos futuros**, não transações.

## Modelo conceitual (RESUMO) vs. modelo no código

| Conceito no RESUMO | No código hoje | Status |
|---|---|---|
| **Account** — container único de dinheiro real, sem separar conta corrente e cartão | `Account` (Name, AccountNumber, InitialBalance) | 🟡 |
| **Payable** (conta a pagar) — qualquer saída futura | `PlannedTransaction` com `Direction = Out` | ✅ D-A2 (desenho a refazer) |
| **Receivable** (conta a receber) — qualquer entrada futura | `PlannedTransaction` com `Direction = In` | ✅ D-A2 (desenho a refazer) |
| **Transaction** — liquidação real (pago/recebido), impacta saldo | `Transaction` (com `DateTime`) | 🟡 |
| Transaction liquida total/parcialmente 1..N compromissos | Não existe vínculo Transaction ↔ PlannedTransaction | 📐 |
| Recorrência gera compromissos automaticamente | `Schedule` (value object) dentro de `PlannedTransaction` | 🟡 parcial |
| Conciliação = relação compromisso ↔ liquidação (auto/manual/revisão) | Não existe | 📐 |
| Importação OFX/CSV/PDF → `ImportedTransaction` (não persistida) → Payables/Receivables | Não existe | 📐 |
| Payees configuráveis, regras automáticas de categorização | `Payee` existe (só Name); regras não existem | 🟡 / 📐 |
| Relatórios / saldo futuro / simulações | Não existe | 📐 |

## Cartão de crédito (visão) 📐

- Cartão **não** é conta separada; fatura **não** é entidade especial.
- Compra no cartão **não** é transação ainda — é um compromisso (Payable), um por compra/parcela.
- Fatura aberta = conjunto de Payables. Pagamento da fatura = 1 Transaction que liquida esses Payables.

## Formas de criação 📐

1. **Manual** — usuário cria compromisso ou transação (alta confiança).
2. **Importação** — sistema cria compromissos, pode sugerir conciliações.
3. **Recorrência** — gera compromissos automaticamente.

Tudo converge para Payable/Receivable.

## Plataforma ✅ (D-A3)

- Desktop Windows (WPF, .NET 10), uso **pessoal/familiar**, single-user; regras podem ser lenientes.
- Sem multi-moeda por ora; deixar preparado se for barato (ver `10-avaliacao-dos-padroes.md`).
- Persistência SQLite via EF Core.
