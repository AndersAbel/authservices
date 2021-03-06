﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;

namespace Kentor.AuthServices.Tests
{
    [TestClass]
    public class MessageBinderTests
    {
        [TestMethod]
        public void MessageBinder_Throws_On_Null_Binder()
        {
            Action a = () => MessageBinder.Bind(null, new Saml2AuthenticationRequest());

            a.ShouldThrow<ArgumentNullException>().And.ParamName.Should().Be("binding");

        }

        [TestMethod]
        public void MessageBinder_Throws_On_Null_Message()
        {
            Action a = () => MessageBinder.Bind(Saml2Binding.Get(Saml2BindingType.HttpPost), null);

            a.ShouldThrow<ArgumentNullException>().And.ParamName.Should().Be("message");

        }
    }
}
