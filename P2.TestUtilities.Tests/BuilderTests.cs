using System;
using FluentAssertions;
using NUnit.Framework;

namespace P2.TestUtilities.Tests
{
    public class BuilderTests
    {
        [Test]
        public void Build_WhenGivenAType_ReturnsObjectOfThatType()
        {
            var builder = new Builder<TestModel>(TestModel.GetModel());

            var result = builder.Build();

            result.Should().BeOfType<TestModel>();
        }

        [Test]
        public void Build_WhenNoChangesAreMade_ReturnsDefaultObject()
        {
            var builder = new Builder<TestModel>(TestModel.GetModel());

            var result = builder.Build();

            result.Should().BeEquivalentTo(TestModel.GetModel());
        }

        [Test]
        public void Build_WhenPropertiesAreChanged_ReturnsObjectWithUpdatedProperties()
        {
            var builder = new Builder<TestModel>(TestModel.GetModel());

            var result = builder
                .With(b => b.MyGuid, Guid.Parse("22222222-2222-2222-2222-222222222222"))
                .With(b => b.MyInt, 2)
                .With(b => b.MyString, "Changed Value")
                .Build();

            result.Should().BeEquivalentTo(TestModel.GetModel(1));
        }

        [Test]
        public void Build_WhenGivenANewBaseModel_ReturnsNewBaseModel()
        {
            var builder = new Builder<TestModel>(TestModel.GetModel());

            var result = builder
                .From(TestModel.GetModel(1))
                .Build();

            result.Should().BeEquivalentTo(TestModel.GetModel(1));
        }

        [Test]
        public void Build_WhenGivenANewDefaultModelAndPropertiesAreModified_ReturnsObjectWithUpdatedProperties()
        {
            var builder = new Builder<TestModel>(TestModel.GetModel());

            var result = builder
                .From(TestModel.GetModel())
                .With(b => b.MyGuid, Guid.Parse("22222222-2222-2222-2222-222222222222"))
                .With(b => b.MyInt, 2)
                .With(b => b.MyString, "Changed Value")
                .Build();

            result.Should().BeEquivalentTo(TestModel.GetModel(1));
        }

        [Test]
        public void Build_WhenGivenANewDefaultModelAndPropertiesAreModified_DoesNotModifyNewDefaultObject()
        {
            var baseModel = TestModel.GetModel();
            var builder = new Builder<TestModel>(baseModel);

            builder
                .From(TestModel.GetModel(1))
                .With(b => b.MyGuid, Guid.Parse("33333333-3333-3333-3333-333333333333"))
                .With(b => b.MyInt, 3)
                .With(b => b.MyString, "Other Value")
                .Build();

            baseModel.Should().BeEquivalentTo(TestModel.GetModel());
        }

    }
}