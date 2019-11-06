using System;

namespace P2.TestUtilities.Tests
{
    public class TestModel
    {
        public Guid MyGuid { get; set; }

        public string MyString { get; set; }

        public int MyInt { get; set; }

        public TestModel MyNestedModel { get; set; }

        public static TestModel GetModel(int modelId = 0) =>
            modelId switch
            {
                1 => new TestModel
                {
                    MyGuid = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    MyInt = 2,
                    MyString = "Changed Value"
                },
                2 => new TestModel
                {
                    MyGuid = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    MyInt = 3,
                    MyString = "Other Value"
                },
                _ => new TestModel
                {
                    MyGuid = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    MyInt = 1,
                    MyString = "Default Value"
                }
            };
    }
}