﻿using System;
using FluentAssertions;
using WorkspaceManager.Configuration;
using Xunit;

namespace WorkspaceManager.Test
{
    public class ContainerTests
    {
        [Fact]
        public void ContainerConfigurationTest()
        {
            var container = Bootstrapper.Container;
            container.Verify();
        }

        [Fact]
        public void ConfigurationFileTests()
        {
            var config = Bootstrapper.GetConfiguration();
            var uri = config["projectCollectionUri"];
            uri.Should().Be("http://chtfs1:8080/tfs/defaultcollection");
            new Uri(uri).Should().NotBeNull();
        }
    }
}