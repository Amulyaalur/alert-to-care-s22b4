Write-Host "Starting Client..."
$ClientProcess = Start-Process npm -ArgumentList "start --prefix ..\AlertToCareUI"
Write-Host "Build Server Project"
$BuildServer = Start-Process dotnet -ArgumentList "build  ..\AlertToCareBackEnd\"
Start-Sleep -s 20
Write-Host "Starting Server..."
$ServerProcess = Start-Process dotnet -ArgumentList "run --project ..\AlertToCareBackEnd\AlertToCareAPI"

Write-Host "Sleeping to wait for Server to start"
Start-Sleep -s 20
Write-Host "Running Integration Test"
$IntegrationTest= Start-Process dotnet -ArgumentList "test ..\AlertToCareIntegrationTest\"