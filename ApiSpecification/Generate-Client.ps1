$OpenApiDocument = ".\ProductDatabaseApi.yaml"
$GeneratedSourceOutputDir = ".\client-generated"
$GeneratedSourcePackageName = "TomiSoft.ProductCatalog.Client.OpenApiGenerated"

function Remove-DirectoryIfExists($directory) {
    $fullPath = ""

    try {
        $fullPath = Resolve-Path $directory -ErrorAction Stop
    }
    catch [System.Management.Automation.ItemNotFoundException] {
        return
    }
    
    if ([io.directory]::Exists($fullPath)) {
        Get-Item $fullPath | Remove-Item -Force -Recurse
    }
}

#remove previous generated code if exists
Remove-DirectoryIfExists $GeneratedSourceOutputDir

#run code generator
java -jar .\openapi-generator-cli.jar `
    generate `
    -i $OpenApiDocument `
    -o $GeneratedSourceOutputDir `
    -g csharp-netcore `
    -p netCoreProjectFile=true,targetFramework=netcoreapp3.1 `
    --package-name $GeneratedSourcePackageName

#copy necessary files to solution
$itemsToCopy = @(
    'Api',
    'Client',
    'Model'
)

$target = "..\$GeneratedSourcePackageName\"
foreach ($itemToCopy in $itemsToCopy) {
    $source = ".\$GeneratedSourceOutputDir\src\$GeneratedSourcePackageName\$itemToCopy"
    Remove-DirectoryIfExists $($target + $itemToCopy)
    Copy-Item -Path $source -Destination $target -Recurse
}

#remove generated code
Remove-DirectoryIfExists $GeneratedSourceOutputDir