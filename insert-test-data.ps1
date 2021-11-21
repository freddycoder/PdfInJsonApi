# A script that insert about 1000 pdfs into the database

# Clone the git repository
if ([System.IO.Directory]::Exists("pdfs") -eq $false) {
    git clone https://github.com/tpn/pdfs.git
}

# Set call to the rest api to insert the pdfs
foreach ($pdfFile in (Get-ChildItem -Filter "*.pdf" -Recurse)) {
    $pdfFileName = $pdfFile.Name
    $pdfFilePath = $pdfFile.FullName
    $pdfFileNameWithoutExtension = $pdfFile.Name.Replace($pdfFile.Extension, "")

    $base64Content = [System.Convert]::ToBase64String([System.IO.File]::ReadAllBytes($pdfFilePath))

    $jsonPdf = @{
        "id" = New-Guid
        "name" = $pdfFileName
        "dateAjout" = [System.DateTimeOffset]::Now.ToString("o")
        "pdfInBase64" = $base64Content
    }

    Write-Host "Inserting $pdfFileName"

    $jsonBody = $jsonPdf | ConvertTo-Json -Compress

    $response = try { 
        Invoke-RestMethod -Uri https://localhost:7083/pdf -Method POST -Body $jsonBody -ContentType "application/json" -Headers @{
            "Accept" = "text/plain"
            } 
        } 
        catch { 
            Write-Host "Error while inserting $pdfFileName"
            Write-Host $_.Exception.Response.StatusCode 
            exit 1
        }

    Write-Host "Successfully inserted $pdfFileName"
}
