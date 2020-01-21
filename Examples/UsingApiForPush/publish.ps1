dotnet publish -c Release -r win-x86 /p:PublishSingleFile=true /p:PublishTrimmed=true
dotnet publish -c Release -r linux-musl-x64 /p:PublishSingleFile=true /p:PublishTrimmed=true

Get-ChildItem -Path 'bin\Release\netcoreapp3.1\*\publish\*'