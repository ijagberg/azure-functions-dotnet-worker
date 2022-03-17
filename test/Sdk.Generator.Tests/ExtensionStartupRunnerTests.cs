﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Threading.Tasks;
using Microsoft.Azure.Functions.Tests.WorkerExtensionsSample;
using Microsoft.Azure.Functions.Worker.Sdk.Generators;
using Xunit;
namespace Sdk.Generator.Tests
{
    public class ExtensionStartupRunnerTests
    {
        const string expectedGeneratedFileName = $"ExtensionStartupRunner.g.cs";
        [Fact]
        public async Task StartupCodeGetsGenerated()
        {
            string inputCode = @"
public class Foo
{
}";
            var referencedExtensionAssemblies = new[]
            {
                typeof(SampleExtensionStartup).Assembly,
            };

            string expectedOutput = @"// <auto-generated/>
using System;
namespace Microsoft.Azure.Functions.Worker
{
    [AttributeUsage(AttributeTargets.Class)]
    public class WorkerExtensionStartupAttribute : Attribute
    {
    }
    [WorkerExtensionStartup]
    internal class WorkerExtensionStartupRunner
    {
        public void RunStartupForExtensions(IFunctionsWorkerApplicationBuilder builder)
        {
            try
            {
                new Microsoft.Azure.Functions.Tests.WorkerExtensionsSample.SampleExtensionStartup().Configure(builder);
            }
            catch(Exception ex)
            {
                Console.WriteLine('Error calling Configure on Microsoft.Azure.Functions.Tests.WorkerExtensionsSample.SampleExtensionStartup instance.'+ex.ToString());
            }
        }
    }
}
".Replace("'","\"");

            await TestHelpers.RunTestAsync<ExtensionStartupRunnerGenerator>(
                referencedExtensionAssemblies,
                inputCode,
                expectedGeneratedFileName,
                expectedOutput);
        }
    }
}
