using System;
using NUnit.Framework;
using TaschenrechnerCore;
using TechTalk.SpecFlow;

namespace TaschenrechnerGherkinTest
{
    [Binding]
    public class SpecFlowFeature1Steps
    {
        [Given(@"I have entered (.*) into the calculator")]
        public void GivenIHaveEnteredIntoTheCalculator(int p0)
        {
            TaschenrechnerLogik.Zahl1 = p0;
            Assert.That(TaschenrechnerLogik.Zahl1, Is.EqualTo(p0));
        }

        [Given(@"I have also entered (.*) into the calculator")]
        public void GivenIHaveAlsoEnteredIntoTheCalculator(int p0)
        {
            TaschenrechnerLogik.Zahl2 = p0;
            Assert.That(TaschenrechnerLogik.Zahl2, Is.EqualTo(p0));
        }


        [When(@"i press add")]
        public void WhenIPressAdd()
        {
            TaschenrechnerLogik.AddTwoNumbers();
        }
        
        [Then(@"the result is (.*)")]
        public void ThenTheResultIs(int p0)
        {
            Assert.That(TaschenrechnerLogik.Result, Is.EqualTo(p0));
        }
    }
}
