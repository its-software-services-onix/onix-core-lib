#!/bin/bash
coverlet ./OnixCoreTest/bin/Debug/netcoreapp2.2/OnixCoreTest.dll --target "dotnet" --targetargs "test . --no-build" --format lcov
