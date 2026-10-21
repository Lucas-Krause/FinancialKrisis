# Dúvidas abertas

Respostas confirmadas vão para `09-decisoes.md` e a pergunta sai daqui.
Perguntas que o Lucas respondeu "não sei" continuam aqui, mas reformuladas como **decisões de
design a tomar juntos** (com opções), já que dependem do redesenho da PlannedTransaction (D-A2).

## B. Compromissos e recorrência (bloco a redesenhar)
- **B1. Série vs. ocorrência.** Opções típicas:
  1. *Template + ocorrências geradas*: a `PlannedTransaction` guarda a regra (Schedule) e o sistema gera/calcula
     **ocorrências** (cada uma com data, valor, status). "Março pago" = ocorrência de março liquidada.
  2. *Só ocorrências*: cada compromisso é uma linha; a recorrência é só um gerador que cria várias linhas.
  3. *Template sem materializar*: ocorrências calculadas sob demanda; só persistem as exceções (pago, ignorado, valor alterado).
- **B2. Liquidação.** 1:1, 1:N (fatura liquida várias compras), parcial? (Ligado a B1: liquida-se uma ocorrência.)
- **B3. Ignored.** Provável significado: "não vai acontecer" → sai do saldo futuro. Reversível?
- **B6. Fim da recorrência.** Data de término, número de ocorrências (ex.: 10 parcelas), ou ambos?
- **C4. Desativar conta com compromissos futuros.** Bloquear? Ignorar os futuros? Depende de B1.
- **D6. Transferência entre contas próprias.** 2 Transactions ligadas? Entidade própria? (Sugestão: entra junto com a discussão de liquidação.)

## E. Detalhes de unicidade (de D-E1)
- **E1a.** Unicidade de nome ignora maiúsculas/minúsculas e espaços nas pontas? ("Mercado" = "mercado ")
- **E1b.** Vale só entre **ativos** (um nome desativado pode ser reutilizado) ou entre todos?
- **E4a.** Quais limites? Hoje o banco tem: Account 100, Category 200, Payee 150, Subcategory 100. Manter?

## F. Técnico
- **F7.** Testes: manter só os de integração atuais ou adicionar também testes unitários do Domain? (Explicação na conversa
  de 2026-10-21: integração = serviço + banco SQLite em memória; unitário = só a entidade, sem banco nem DI.)

## G. Novas (surgidas na 2ª rodada)
- **G2. Padrão de commit.** Adotar algo como `Tipo: resumo` (ver skill `commit`) ou manter o estilo narrativo atual?
- **G3. "Familiar".** Mais de uma pessoa da família vai usar o app? Precisaria identificar quem lançou o quê?
