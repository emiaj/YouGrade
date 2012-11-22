using FubuMVC.Core;
using FubuMVC.Core.Assets;
using FubuMVC.Core.Behaviors;

namespace YouGrade.Policies.Asset
{
    public class IncludeBoostrapSet : BasicBehavior
    {
        private readonly IAssetRequirements _assetRequirements;

        public IncludeBoostrapSet(IAssetRequirements assetRequirements)
            : base(PartialBehavior.Ignored)
        {
            _assetRequirements = assetRequirements;
        }

        protected override DoNext performInvoke()
        {
            _assetRequirements.Require("twitterbootstrap");
            return DoNext.Continue;
        }
    }
}