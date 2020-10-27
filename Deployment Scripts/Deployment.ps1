Write-Host "Starting Server..."
$ServerProcess = Start-Process dotnet -ArgumentList "run --project ..\AlertToCareBackEnd\AlertToCareAPI"
Write-Host "Starting Client..."
$ClientProcess = Start-Process npm -ArgumentList "start --prefix ..\AlertToCareUI"