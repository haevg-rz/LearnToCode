Feature: SpecFlowFeature1
	In order to test my calculatuor's adding
	functionality i want to be told the sum
	of two numbers
	
	
@sometag
Scenario: Add two numbers
	Given I have entered 50 into the calculator
	And I have entered 20 into the calculator
	When i press add
	Then the result is 70