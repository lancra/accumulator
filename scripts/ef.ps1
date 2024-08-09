[CmdletBinding()]
param(
    [Parameter(Mandatory)]
    [ValidateSet('add', 'changes', 'drop', 'remove', 'update')]
    [string]$Action,
    [Parameter()]
    [string]$Name,
    [switch]$IncludeBuild
)

$expandedAction = switch ($Action.ToLower()) {
    'add' { "migrations add $Name" }
    'changes' { 'migrations has-pending-model-changes' }
    'drop' { 'database drop --force' }
    'remove' { "migrations remove" }
    'update' { 'database update' }
}

$rootPath = Resolve-Path -Path (Join-Path -Path $PSScriptRoot -ChildPath '..')
$projectPath = Join-Path -Path $rootPath -ChildPath 'src' -AdditionalChildPath 'Migrations'
$startupPath = Join-Path -Path $rootPath -ChildPath 'tools' -AdditionalChildPath 'Migrator'

$noBuildFlag = $IncludeBuild ? '' : ' --no-build'

$command = "dotnet ef $expandedAction --project $projectPath --startup-project $startupPath$noBuildFlag"
Write-Verbose "Executing '$command'"
Invoke-Command -ScriptBlock ([scriptblock]::Create($command))
