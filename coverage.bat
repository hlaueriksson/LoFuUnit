dotnet test tests\LoFuUnit.Tests\LoFuUnit.Tests.csproj --collect "Code Coverage" --settings coverage.runsettings
reportgenerator -reports:"tests\**\coverage.xml" -targetdir:"TestResults\Coverage" -reporttypes:Html -filefilters:"-*ReflectionMagic*"
