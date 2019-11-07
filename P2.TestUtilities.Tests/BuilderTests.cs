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
            var builder = new Builder<TestModel>(() => new TestModel());

            var result = builder.Build();

            result.Should().BeOfType<TestModel>();
        }

        [Test]
        public void Build_WhenNoChangesAreMade_ReturnsDefaultObject()
        {
            var builder = new Builder<TestModel>(() => new TestModel
            {
                MyGuid = Guid.Parse("11111111-1111-1111-1111-111111111111")
            });

            var result = builder.Build();

            result.Should().BeEquivalentTo(new TestModel
            {
                MyGuid = Guid.Parse("11111111-1111-1111-1111-111111111111")
            });
        }

        [Test]
        public void Build_WhenPropertiesAreChanged_ReturnsObjectWithUpdatedProperties()
        {
            var builder = new Builder<TestModel>(() => new TestModel
            {
                MyGuid = Guid.Parse("11111111-1111-1111-1111-111111111111")
            });

            var result = builder
                .With(b => b.MyGuid, Guid.Parse("22222222-2222-2222-2222-222222222222"))
                .With(b => b.MyInt, 2)
                .With(b => b.MyString, "Changed Value")
                .Build();

            result.Should().BeEquivalentTo(new TestModel
            {
                MyGuid = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                MyInt = 2,
                MyString = "Changed Value"
            });
        }

        [Test]
        public void Build_WhenGivenANewBaseModel_ReturnsNewBaseModel()
        {
            var builder = new Builder<TestModel>(() => new TestModel());

            var result = builder
                .From(() => new TestModel
                {
                    MyGuid = Guid.Parse("11111111-1111-1111-1111-111111111111")
                })
                .Build();

            result.Should().BeEquivalentTo(new TestModel
            {
                MyGuid = Guid.Parse("11111111-1111-1111-1111-111111111111")
            });
        }

        [Test]
        public void Build_WhenGivenANewDefaultModelAndPropertiesAreModified_ReturnsObjectWithUpdatedProperties()
        {
            var builder = new Builder<TestModel>(() => new TestModel());

            var result = builder
                .From(() => new TestModel
                {
                    MyGuid = Guid.Parse("11111111-1111-1111-1111-111111111111")
                })
                .With(b => b.MyGuid, Guid.Parse("22222222-2222-2222-2222-222222222222"))
                .With(b => b.MyInt, 2)
                .With(b => b.MyString, "Changed Value")
                .Build();

            result.Should().BeEquivalentTo(new TestModel
            {
                MyGuid = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                MyInt = 2,
                MyString = "Changed Value"
            });
        }
        
        [Test]
        public void Build_WhenADefaultObjectWithANewGuid_ReturnsDifferentGuidForEachBuild()
        {
            var builder = new Builder<TestModel>(() => new TestModel
            {
                MyGuid = Guid.NewGuid()
            });

            var object1 = builder.Build();
            var object2 = builder.Build();

            object1.MyGuid.Should().NotBe(object2.MyGuid);
        }

    }
}