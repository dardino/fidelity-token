param (
    [switch]$dev = $false,
    [int16]$scale = 2
)
Write-Host "Dev mode: " -NoNewline
Write-Host $dev -NoNewline
Write-Host " (use -dev argument to enable Dev mode)" -ForegroundColor Green
Write-Host "Scale: " -NoNewline
Write-Host $scale -NoNewline
Write-Host " (use -scale:n argument to change the scale, default is 2)" -ForegroundColor Green
Write-Host "Starting Docker containers..." -ForegroundColor Yellow

# $currentPath = (Get-Location).path

if ($dev) {
    docker-compose -f ./docker-compose.yml -f ./docker-compose.dev.yml up -d --build --scale eth=$scale
} else {
    docker-compose -f ./docker-compose.yml up -d --build --scale eth=$scale
}

Write-Host "Containers started" -ForegroundColor Green

Write-Host "Waiting for angular app (CTRL+C to stop) " -NoNewline -ForegroundColor Yellow
do {
    # First we create the request.
    $HTTP_Request = [System.Net.WebRequest]::Create('http://localhost:4200')
    # We then get a response from the site.
    try {
        $HTTP_Response = $HTTP_Request.GetResponse()
        # We then get the HTTP code as an integer.
        $HTTP_Status = [int]$HTTP_Response.StatusCode
        If ($HTTP_Status -eq 200) {
            Write-Host ""
            Write-Host "Angular is OK!" -ForegroundColor Green
        }
    } catch {
        Write-Host -NoNewline "."
        Start-Sleep -Seconds 2
    }
} until ($HTTP_Status -eq 200)

Start-Process http://localhost:3000
Start-Process http://localhost:4200
Start-Process https://localhost:5001/api/AccountBalance/007ccffb7916f37f7aeef05e8096ecfbe55afc2f
