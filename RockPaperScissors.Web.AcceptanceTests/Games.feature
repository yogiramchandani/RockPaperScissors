Feature: Games
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

Scenario: Start a new ComputerVsComputer game
	Given I have entered "ComputerVsComputer" into GetGame
	When I send "" to play
	Then the result should have "1" item
	
Scenario: Start a new ComputerVsComputer game and play 2 times
	Given I have entered "ComputerVsComputer" into GetGame
	When I send "" to play
	When I send "" to play
	Then the result should have "2" item

Scenario: Start a new ComputerVsComputer game and play 3 times
	Given I have entered "ComputerVsComputer" into GetGame
	When I send "" to play
	When I send "" to play
	When I send "" to play
	Then the result should have "3" item

Scenario: Start a new PersonVsComputer game and play 3 times
	Given I have entered "PersonVsComputer" into GetGame
	When I send "Rock" to play
	When I send "Scissors" to play
	When I send "Paper" to play
	Then the result should have "3" item

Scenario: Start a new PersonVsComputer game and play with empty input
	Given I have entered "PersonVsComputer" into GetGame
	When I send invalid "" to play
	Then expect bad request status code

Scenario: Start a new PersonVsComputer game and play with invalid input
	Given I have entered "PersonVsComputer" into GetGame
	When I send invalid "Rocks" to play
	Then expect bad request status code