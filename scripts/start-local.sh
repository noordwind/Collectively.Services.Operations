#!/bin/bash
export ASPNETCORE_ENVIRONMENT=local
cd src/Collectively.Services.Operations
dotnet run --no-restore --urls "http://*:10001"