[CmdletBinding()]
param(
    [Parameter(Mandatory)]
    [ValidateSet('add', 'bundle', 'changes', 'list', 'remove')]
    [string]$Action,
    [Parameter()]
    [string]$Name,
    [switch]$IncludeBuild
)

$rootPath = Resolve-Path -Path (Join-Path -Path $PSScriptRoot -ChildPath '..')
$projectPath = Join-Path -Path $rootPath -ChildPath 'src' -AdditionalChildPath 'Migrations'
$startupPath = Join-Path -Path $rootPath -ChildPath 'tools' -AdditionalChildPath 'Migrator'

$noBuildFlag = $IncludeBuild ? '' : ' --no-build'

$toolCommandFormat = "dotnet ef migrations {0} --project $projectPath --startup-project $startupPath --configuration Release$noBuildFlag"

$bundlePath = Join-Path -Path $rootPath -ChildPath 'artifacts' -AdditionalChildPath 'db', 'efbundle.exe'

$command = switch ($Action.ToLower()) {
    'add' { $toolCommandFormat -f "add $Name" }
    'bundle' { "$bundlePath --connection '$env:ACCUMULATOR_DATABASE' $Name" }
    'changes' { $toolCommandFormat -f 'has-pending-model-changes' }
    'list' { $toolCommandFormat -f 'list' }
    'remove' { $toolCommandFormat -f 'remove' }
}

Write-Verbose "Executing '$command'"
Invoke-Command -ScriptBlock ([scriptblock]::Create($command))
