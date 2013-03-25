using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoNSubstitute;
using Ploeh.AutoFixture.Xunit;

namespace RockPaperScissors.Core.Tests
{
    public class GameTestConventionsAttribute : AutoDataAttribute
    {
        public GameTestConventionsAttribute() :
            base(new Fixture().Customize(new AutoNSubstituteCustomization()))
        {

        }
    }
}
