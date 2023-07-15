using Xunit;
using System.Collections;
using DelegatesSample.Extensions;
using DelegatesSample.Tests.Models;

namespace DelegatesSample.Tests
{
    public partial class GetMaxTest
    {
        [Theory]
        [MemberData(nameof(HumanDataSet))]
        public void GetMaxValuesByHuman(IEnumerable dataSet, Func<Human, float> getParameter, Human equial)
        {
            var max = dataSet.GetMax(getParameter);
            if (max == null)
            {
                Assert.Null(max);
                Assert.Null(equial);
                return;
            }

            Assert.True(max.Equals(equial));
        }

        [Theory]
        [MemberData(nameof(AlienDataSet))]
        public void GetMaxValuesByAlien(IEnumerable dataSet, Func<Alien, float> getParameter, Alien equial)
        {
            var max = dataSet.GetMax(getParameter);
            if (max == null)
            {
                Assert.Null(max);
                Assert.Null(equial);
                return;
            }

            Assert.True(max.Equals(equial));
        }
    }
}