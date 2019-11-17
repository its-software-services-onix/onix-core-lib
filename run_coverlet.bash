#!/bin/bash
coverlet ./OnixCoreTest/bin/Debug/netcoreapp3.0/OnixCoreTest.dll --target "dotnet" --targetargs "test . --no-build" --format lcov
