using BLL;

public class PlayerAnalyzerTests
{
	private readonly PlayerAnalyzer analyzer;

	public PlayerAnalyzerTests()
	{
		// Initialize the analyzer instance
		analyzer = new PlayerAnalyzer();
	}

	[Fact]
	public void CalculateScore_SinglePlayer_ReturnsCorrectScore()
	{
		// Arrange
		var players = new List<Player>
		{
			new Player { Age = 25, Experience = 5, Skills = new List<int> { 2, 2, 2 } }
		};

		// Act
		var result = analyzer.CalculateScore(players);

		// Assert
		Assert.Equal(250, result);
	}

	[Fact]
	public void CalculateScore_SinglePlayer_WithUnderage_ReturnsCorrectScore()
	{
		// Arrange
		var players = new List<Player>
		{
			new Player { Age = 15, Experience = 3, Skills = new List<int> { 3, 3, 3 } }
		};

		// Act
		var result = analyzer.CalculateScore(players);

		// Assert
		Assert.Equal(67.5, result);
	}

	[Fact]
	public void CalculateScore_SinglePlayer_WithHighExperience_ReturnsCorrectScore()
	{
		// Arrange
		var players = new List<Player>
		{
			new Player { Age = 35, Experience = 15, Skills = new List<int> { 4, 4, 4 } }
		};

		// Act
		var result = analyzer.CalculateScore(players);

		// Assert
		Assert.Equal(2520, result);
	}

	[Fact]
	public void CalculateScore_MultiplePlayers_ReturnsSumOfScores()
	{
		// Arrange
		var players = new List<Player>
		{
			new Player { Age = 25, Experience = 5, Skills = new List<int> { 2, 2, 2 } },
			new Player { Age = 35, Experience = 15, Skills = new List<int> { 4, 4, 4 } },
			// Add more player objects as needed
		};

		double expectedScoreSum = 0;

		foreach (var player in players)
		{
			double skillsAverage = player.Skills.Sum() / (double)player.Skills.Count;
			double contribution = player.Age * player.Experience * skillsAverage;

			if (player.Age < 18)
			{
				contribution *= 0.5;
			}

			if (player.Experience > 10)
			{
				contribution *= 1.2;
			}

			expectedScoreSum += contribution;
		}

		// Act
		var result = analyzer.CalculateScore(players);

		// Assert
		Assert.Equal(expectedScoreSum, result);
	}

	[Fact]
	public void CalculateScore_PlayerWithNullSkills_ThrowsException()
	{
		// Arrange
		var players = new List<Player>
		{
			new Player { Age = 25, Experience = 5, Skills = null }
		};

		// Act and Assert
		Assert.Throws<ArgumentNullException>(() => analyzer.CalculateScore(players));
	}

	[Fact]
	public void CalculateScore_EmptyArray_ReturnsZero()
	{
		// Arrange
		var players = new List<Player>();

		// Act
		var result = analyzer.CalculateScore(players);

		// Assert
		Assert.Equal(0, result);
	}
}
