---
name: commit
description: Como fazer commits no FinancialKrisis — rascunhar a mensagem em português, mostrar ao Lucas e só commitar depois da confirmação explícita. Usar sempre que for commitar algo neste repositório.
---

# Commit no FinancialKrisis

Regra (decisão D-F5): o Claude pode commitar pelo Lucas, mas **sempre confirma a mensagem antes**.
Nunca commitar sem um "ok" explícito para aquela mensagem específica. Nunca dar push sem pedido.

## Git nesta máquina
`git` não está no PATH. Usar o Git do Visual Studio:
```powershell
$git = "C:\Program Files\Microsoft Visual Studio\18\Community\Common7\IDE\CommonExtensions\Microsoft\TeamFoundation\Team Explorer\Git\cmd\git.exe"
& $git status --short
```

## Passos
1. `& $git status --short` e `& $git diff --stat` — conferir o que entra. Não incluir `bin/`, `obj/`, `.vs/`.
2. Se houver mudanças de assuntos diferentes, sugerir commits separados.
3. Rascunhar a mensagem seguindo o padrão abaixo e **mostrar ao Lucas**, com a lista de arquivos.
4. Esperar confirmação. Se ele ajustar a mensagem, usar o texto dele.
5. Commitar com here-string (PowerShell) para preservar acentos e quebras de linha:
   ```powershell
   & $git add <arquivos>
   & $git commit -m @'
   <mensagem>
   '@
   ```
6. Mostrar o hash resultante (`& $git log --oneline -1`).

## Padrão de mensagem
O Lucas ainda está definindo o padrão. Histórico atual: frases em português, no particípio/gerúndio,
narrando o que foi feito (ex.: "Finalizado os testes da Category").

Proposta em avaliação (não confirmada — perguntar se ele quer adotar):
```
<Tipo>: <resumo curto no imperativo, até ~72 caracteres>

<corpo opcional: o porquê e o que mudou, em tópicos>
```
Tipos sugeridos: `Feat`, `Fix`, `Refactor`, `Test`, `Docs`, `Chore`.

Quando o padrão for decidido, atualizar esta seção e registrar em `.claude/context/09-decisoes.md`.
