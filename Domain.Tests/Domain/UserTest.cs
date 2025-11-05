using Domain.Entities;
using Domain.Validation;
using System;
using FluentAssertions;
using Xunit;
using Domain.Enums;

namespace Domain.Tests.Entities
{
    public class UserTests
    {
        private readonly string _validName = "John Doe Test";
        private readonly string _validEmail = "john@test.com";
        private readonly string _validPasswordHash = "hashed_password";
        private readonly string _validCreatedBy = "joe doe";

    }
}