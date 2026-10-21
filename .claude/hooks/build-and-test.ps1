# Stop: se algum .cs foi editado no turno (marcador criado por check-param-prefix.ps1), roda
# dotnet build e dotnet test. Em caso de falha, devolve o erro ao Claude uma vez para correção.
$ErrorActionPreference = 'Continue'
[Console]::OutputEncoding = [System.Text.Encoding]::UTF8

$payload = [Console]::In.ReadToEnd() | ConvertFrom-Json
$marker = Join-Path $env:TEMP ("claude-fk-dirty-" + $payload.session_id)
if (-not (Test-Path -LiteralPath $marker)) { exit 0 }
Remove-Item -LiteralPath $marker -Force

$projectDir = $env:CLAUDE_PROJECT_DIR
if (-not $projectDir) { $projectDir = (Resolve-Path (Join-Path $PSScriptRoot '..\..')).Path }
$solution = Join-Path $projectDir 'FinancialKrisis.slnx'

function Write-Result([string]$step, [string[]]$output) {
    $details = ($output | Where-Object { $_ -match 'error|falhou|failed|Failed|\[FAIL\]' } | Select-Object -First 25) -join "`n"
    if (-not $details) { $details = ($output | Select-Object -Last 25) -join "`n" }
    if ($payload.stop_hook_active) {
        @{ systemMessage = "FinancialKrisis: $step ainda falhando (não bloqueado de novo)." } | ConvertTo-Json -Compress
    } else {
        @{ decision = 'block'; reason = "FinancialKrisis: $step falhou após as alterações em .cs. Corrija antes de encerrar:`n$details" } | ConvertTo-Json -Compress
    }
    exit 0
}

$build = & dotnet build $solution -nologo -v q -clp:ErrorsOnly 2>&1 | ForEach-Object { "$_" }
if ($LASTEXITCODE -ne 0) { Write-Result 'dotnet build' $build }

$test = & dotnet test $solution --no-build -nologo -v q 2>&1 | ForEach-Object { "$_" }
if ($LASTEXITCODE -ne 0) { Write-Result 'dotnet test' $test }

@{ systemMessage = 'FinancialKrisis: build e testes OK.' } | ConvertTo-Json -Compress
exit 0
