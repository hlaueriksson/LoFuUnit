using System;
using FluentAssertions;
using LoFuUnit.NUnit;

namespace LoFuUnit.Tests.Documentation
{
    /// <summary>
    /// Compare with https://github.com/machine/machine.specifications#overview
    /// </summary>
    public class AuthenticationTests
    {
        SecurityService Subject;
        UserToken Token;

        [LoFuTest]
        public void Authenticate_admin_users()
        {
            Subject = new SecurityService();

            void when_authenticating_an_admin_user() =>
                Token = Subject.Authenticate("username", "password");

            void should_indicate_the_user_s_role() =>
                Token.Role.Should().Be(Roles.Admin);

            void should_have_a_unique_session_id() =>
                Token.SessionId.Should().NotBeNull();
        }
    }

    public class SecurityService
    {
        public UserToken Authenticate(string username, string password)
        {
            return new UserToken(Roles.Admin);
        }
    }

    public class UserToken
    {
        public Roles Role { get; }
        public string SessionId { get; }

        public UserToken(Roles role)
        {
            Role = role;
            SessionId = Guid.NewGuid().ToString();
        }
    }

    public enum Roles
    {
        None,
        User,
        Admin
    }
}