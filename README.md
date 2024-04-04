# WorldCupScoreboard

**Task requirements:**

You are working in a sports data company, and we would like you to develop a new Live Football
World Cup Scoreboard library that shows all the ongoing matches and their scores.

The scoreboard supports the following operations:

1. Start a new match, assuming initial score 0 – 0 and adding it the scoreboard.
This should capture following parameters:
a. Home team
b. Away team

2. Update score. This should receive a pair of absolute scores: home team score and away
team score.

3. Finish match currently in progress. This removes a match from the scoreboard.

4. Get a summary of matches in progress ordered by their total score. The matches with the
same total score will be returned ordered by the most recently started match in the
scoreboard.


For example, if following matches are started in the specified order and their scores
respectively updated:

 a. Mexico 0 - Canada 5
 
 b. Spain 10 - Brazil 2
 
 c. Germany 2 - France 2
 
 d. Uruguay 6 - Italy 6
 
 e. Argentina 3 - Australia 1

The summary should be as follows:
1. Uruguay 6 - Italy 6
2. Spain 10 - Brazil 2
3. Mexico 0 - Canada 5
4. Argentina 3 - Australia 1
5. Germany 2 - France 2

**Realization the task:**

There are two libraries WorldCupScoreboard and WorldCupScoreboard.Test in .Net 8.0.
The task ask was implemented with the TDD technique.

WorldCupScoreboard contains the implementation and the validation of the requiments

Class FootballScoreboardService implements this interface


     public void StartMatch(Team homeTeam, Team awayTeam);

     public IEnumerable<Match> GetMatchesInProgress();

     public void UpdateScore(MatchUpdateInfo matchUpdateInfo);

     public void FinishMatch(MatchUpdateInfo matchUpdateInfo);

 
 All input validation was done in the separete services by using FluentValidator.

 **Team Class Implementation:**

Implement a Team class representing a football team.
Include properties such as Country and TeamType to identify the team's country and type (e.g., home or away).
Consider including validation within the class itself, such as ensuring that the country name is not empty.
Override Equals and GetHashCode methods for equality comparison if necessary.
By including the Team class in the implementation, you'll have a complete set of components for managing football matches, updating scores, and ensuring data integrity through validation.

**Match Class Implementation:**

Create a Match class representing a football match, including properties for HomeTeam, AwayTeam, HomeScore, AwayScore, and StartTime.
Implement methods for updating scores and calculating total scores, and override Equals and GetHashCode methods for equality comparison.


**MatchUpdateInfo Class Implementation:**

Define a MatchUpdateInfo class to encapsulate data required to update a match's score, containing properties for HomeTeam, AwayTeam, HomeScore, and AwayScore.

**FootballScoreboardService Implementation:**

Responsible for managing football matches and scoreboard operations.
Implement methods to start new matches, update scores, finish matches, and retrieve match summaries, utilizing dependency injection for validator injection.

**Unit Tests:**

Unit tests for TeamValidator, MatchUpdateInfoValidator, and FootballScoreboardService.
Test various scenarios, including valid and invalid inputs, boundary cases, etc., using a testing framework like NUnit.

**Integration Testing:**

Integration testing to ensure that components work together as expected.
Test interactions between the FootballScoreboardService, validators
Moq is used to mock dependencies (like validators) in tests.

 
 
