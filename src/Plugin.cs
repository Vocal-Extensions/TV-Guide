#nullable enable

using System;
using System.Collections.Generic;
using MediaBrowser.Common.Configuration;
using MediaBrowser.Common.Plugins;
using MediaBrowser.Model.Plugins;
using MediaBrowser.Model.Serialization;

namespace Jellyfin.Plugin.TvGuide;

public class Plugin : BasePlugin<TvGuideConfiguration>, IHasWebPages
{
    public Plugin(IApplicationPaths applicationPaths, IXmlSerializer xmlSerializer)
        : base(applicationPaths, xmlSerializer)
    {
        Instance = this;
    }

    public static Plugin? Instance { get; private set; }

    public static TvGuideConfiguration GetCurrentConfiguration() => Instance?.Configuration ?? new TvGuideConfiguration();

    public override string Name => "TV Guide";

    public override string Description => "Virtual TV channels from library genres with weekly schedules";

    public override Guid Id => new Guid("3d5cf514-9a68-42c1-94bd-ce7fa3cc399b");

    public IEnumerable<PluginPageInfo> GetPages()
    {
        yield return new PluginPageInfo
        {
            Name = Name,
            EnableInMainMenu = Instance?.Configuration.EnableMainMenu ?? true,
            // NOTE: rules_dotnet v0.21.5 has a bug with resource name generation
            // The resource should be "TvGuide.Configuration.config.html" but due to
            // a bug in how the build system handles embedded resource paths, it becomes
            // "TvGuide.onfiguration.config.html" (missing "C").
            // See: https://github.com/bazelbuild/rules_dotnet/issues/474
            EmbeddedResourcePath = "TvGuide.onfiguration.config.html",
        };
    }
}