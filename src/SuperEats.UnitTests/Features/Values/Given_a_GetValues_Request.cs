﻿using FluentAssertions;
using SuperEats.Features.Values;
using Xunit;

namespace SuperEats.UnitTests.Features.Values
{
    public partial class GetValuesValidator
    {
        public class Given_a_GetValues_Request
        {
            private readonly GetValues.Validator validator;
            private GetValues.Request request;

            public Given_a_GetValues_Request()
            {
                validator = new GetValues.Validator();
                request = new GetValues.Request
                {
                    Param1 = "12345678",
                    Param2 = "1234"
                };
            }

            [Theory]
            [InlineData("1234as", false)]
            [InlineData("123456789asas", false)]
            [InlineData("123456AA", false)]
            [InlineData("", false)]
            [InlineData("12345678", true)]
            public void Should_validate_Param1(string value, bool isValid)
            {
                request.Param1 = value;

                validator.Validate(request).IsValid.Should().Be(isValid);
            }

            [Theory]
            [InlineData(null, false)]
            [InlineData("", false)]
            [InlineData("1234", true)]
            public void Should_validate_Param2(string value, bool isValid)
            {
                request.Param2 = value;

                validator.Validate(request).IsValid.Should().Be(isValid);
            }
        }
    }
}
