# PostToolUse (Edit|Write): para arquivos .cs, marca que houve alteração de código (usado pelo
# build-and-test.ps1) e avisa o Claude sobre parâmetros de método/construtor sem o prefixo "p".
$ErrorActionPreference = 'Stop'
[Console]::OutputEncoding = [System.Text.Encoding]::UTF8

$payload = [Console]::In.ReadToEnd() | ConvertFrom-Json
$filePath = $payload.tool_input.file_path
if (-not $filePath -and $payload.tool_response) { $filePath = $payload.tool_response.filePath }
if (-not $filePath -or $filePath -notmatch '\.cs$' -or $filePath -match '[\\/](bin|obj)[\\/]') { exit 0 }
if (-not (Test-Path -LiteralPath $filePath)) { exit 0 }

$marker = Join-Path $env:TEMP ("claude-fk-dirty-" + $payload.session_id)
Set-Content -LiteralPath $marker -Value $filePath -Encoding UTF8

$source = Get-Content -LiteralPath $filePath -Raw -Encoding UTF8
$source = [regex]::Replace($source, '//[^\r\n]*', '')

function Split-TopLevel([string]$text) {
    $parts = @(); $depth = 0; $current = ''
    foreach ($ch in $text.ToCharArray()) {
        if ($ch -eq '<' -or $ch -eq '(' -or $ch -eq '[') { $depth++ }
        elseif ($ch -eq '>' -or $ch -eq ')' -or $ch -eq ']') { $depth-- }
        if ($ch -eq ',' -and $depth -eq 0) { $parts += $current; $current = '' } else { $current += $ch }
    }
    if ($current.Trim()) { $parts += $current }
    return $parts
}

$signatures = @()
$declaration = '\b(?:public|private|protected|internal)\b(?<head>[^;{}()=]*?)\b(?<name>\w+)\s*(?:<[^<>()]*(?:<[^<>()]*>[^<>()]*)*>)?\s*\((?<params>[^()]*)\)'
foreach ($m in [regex]::Matches($source, $declaration)) {
    if ($m.Groups['head'].Value -match '\brecord\b') { continue }
    $signatures += [pscustomobject]@{ Name = $m.Groups['name'].Value; Params = $m.Groups['params'].Value; Index = $m.Index }
}
foreach ($m in [regex]::Matches($source, '\bextension\s*(?:<[^()]*>)?\s*\((?<params>[^()]*)\)')) {
    $signatures += [pscustomobject]@{ Name = 'extension'; Params = $m.Groups['params'].Value; Index = $m.Index }
}

$violations = @()
foreach ($sig in $signatures) {
    if (-not $sig.Params.Trim()) { continue }
    foreach ($param in (Split-TopLevel $sig.Params)) {
        $clean = ($param -replace '\[[^\]]*\]', '' -replace '=.*$', '').Trim()
        if (-not $clean) { continue }
        $paramName = ($clean -split '\s+')[-1]
        if ($paramName -notmatch '^p[A-Z]') {
            $line = ($source.Substring(0, $sig.Index) -split "`n").Count
            $violations += "linha ~${line}: '$paramName' em $($sig.Name)(...)"
        }
    }
}

if ($violations.Count -gt 0) {
    $fileName = Split-Path $filePath -Leaf
    $message = "Convenção FinancialKrisis: parâmetros devem usar o prefixo 'p' (ex.: pName). Em ${fileName}:`n - " + ($violations -join "`n - ")
    @{ hookSpecificOutput = @{ hookEventName = 'PostToolUse'; additionalContext = $message } } | ConvertTo-Json -Compress -Depth 4
}
exit 0
