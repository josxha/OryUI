openapi-generator-cli.cmd generate -i "openapi-kratos.json" -g csharp -o "Ory.Kratos" --additional-properties=nullableReferenceTypes=true,packageName=Ory.Kratos.Client,packageVersion=1.1.0,netCoreProjectFile=true,sourceFolder=.
mv Ory.Kratos\Ory.Kratos.Client .\
rm -r -fo Ory.Kratos