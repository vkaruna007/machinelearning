﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using ApprovalTests;
using ApprovalTests.Reporters;
using Microsoft.ML.CodeGenerator.Templates.Console;
using Microsoft.ML.TestFramework;
using Xunit;
using Xunit.Abstractions;

namespace Microsoft.ML.CodeGenerator.Tests
{
    [UseReporter(typeof(DiffReporter))]
    public class TemplateTest : BaseTestClass
    {
        public TemplateTest(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        [UseReporter(typeof(DiffReporter))]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void TestPredictProgram_WithSampleData()
        {
            var predictProgram = new PredictProgram()
            {
                SampleData = new Dictionary<string, string>()
                {
                    { "key1", "\"key1\"" },
                    { "key2", "\"key2\"" },
                    { "key3", "\"key\\\"3\"" },
                },
                TaskType = "null",
                Features = new List<string>(),
                Namespace = "Namespace",
                LabelName = "LabelName",
                Separator = ','
            };
            Approvals.Verify(predictProgram.TransformText());
        }

        [Fact]
        [UseReporter(typeof(DiffReporter))]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void TestConsumeModel()
        {
            var consumeModel = new ConsumeModel()
            {
                Namespace = "Namespace",
                HasNormalizeMapping = true,
                HasLabelMapping = true,
                MLNetModelpath = @"/path/to/model",
            };

            Approvals.Verify(consumeModel.TransformText());
        }

        [Fact]
        [UseReporter(typeof(DiffReporter))]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void TestModelBuilder_noOnnx_noTestData()
        {
            var modelBuilder = new ModelBuilder()
            {
                Namespace = "Namespace",
                HasOnnxModel = false,
                Path = "Path",
                Separator = ',',
                PreTrainerTransforms = new string[] { "PreTrainerTransformer1" },
                Trainer = "Trainer",
                TaskType = "Task1",
                GeneratedUsings = "Using package1",
                AllowQuoting = true,
                AllowSparse = true,
                LabelName = "Label",
                CacheBeforeTrainer = true,
                PostTrainerTransforms = new string[] { "PostTrainerTransformer1" },
                MLNetModelpath = "/path/to/model",
            };

            Approvals.Verify(modelBuilder.TransformText());
        }
    }
}
