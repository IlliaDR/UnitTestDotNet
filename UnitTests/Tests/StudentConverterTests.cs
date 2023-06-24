using BLL;

namespace Tests;

public class StudentConverterTests
{
	[Fact]
	public void ConvertStudents_WhenCalled_ReturnsConvertedStudents()
	{
		// Arrange
		var students = new List<Student>
		{
			new Student { Name = "Alice", Age = 22, Grade = 95 },
			new Student { Name = "Bob", Age = 21, Grade = 95 },
			new Student { Name = "Charlie", Age = 22, Grade = 95 }
		};

		var converter = new StudentConverter();

		// Act
		var convertedStudents = converter.ConvertStudents(students);

		// Assert
		Assert.Equal(3, convertedStudents.Count);

		Assert.False(convertedStudents[0].Exceptional);
		Assert.True(convertedStudents[0].HonorRoll);
		Assert.False(convertedStudents[0].Passed);

		Assert.False(convertedStudents[1].Exceptional);
		Assert.True(convertedStudents[1].HonorRoll);
		Assert.False(convertedStudents[1].Passed);

		Assert.False(convertedStudents[2].Exceptional);
		Assert.True(convertedStudents[2].HonorRoll);
		Assert.False(convertedStudents[2].Passed);
	}

	[Fact]
	public void ConvertStudents_WhenStudentBelow21AndGradeAbove90_ReturnsExceptionalStudent()
	{
		// Arrange
		var students = new List<Student>
		{
			new Student { Name = "John", Age = 20, Grade = 95 }
		};

		var converter = new StudentConverter();

		// Act
		var convertedStudents = converter.ConvertStudents(students);

		// Assert
		Assert.Single(convertedStudents);
		Assert.Equal("John", convertedStudents[0].Name);
		Assert.Equal(20, convertedStudents[0].Age);
		Assert.Equal(95, convertedStudents[0].Grade);
		Assert.True(convertedStudents[0].Exceptional);
		Assert.False(convertedStudents[0].HonorRoll);
		Assert.False(convertedStudents[0].Passed);
	}

	[Fact]
	public void ConvertStudents_WhenStudentGradeBetween71And90_ReturnsPassedStudent()
	{
		// Arrange
		var students = new List<Student>
		{
			new Student { Name = "Mary", Age = 22, Grade = 75 }
		};

		var converter = new StudentConverter();

		// Act
		var convertedStudents = converter.ConvertStudents(students);

		// Assert
		Assert.Single(convertedStudents);
		Assert.Equal("Mary", convertedStudents[0].Name);
		Assert.Equal(22, convertedStudents[0].Age);
		Assert.Equal(75, convertedStudents[0].Grade);
		Assert.False(convertedStudents[0].Exceptional);
		Assert.False(convertedStudents[0].HonorRoll);
		Assert.True(convertedStudents[0].Passed);
	}

	[Fact]
	public void ConvertStudents_WhenStudentGrade70OrLess_ReturnsFailedStudent()
	{
		// Arrange
		var students = new List<Student>
		{
			new Student { Name = "Tom", Age = 21, Grade = 65 }
		};

		var converter = new StudentConverter();

		// Act
		var convertedStudents = converter.ConvertStudents(students);

		// Assert
		Assert.Single(convertedStudents);
		Assert.Equal("Tom", convertedStudents[0].Name);
		Assert.Equal(21, convertedStudents[0].Age);
		Assert.Equal(65, convertedStudents[0].Grade);
		Assert.False(convertedStudents[0].Exceptional);
		Assert.False(convertedStudents[0].HonorRoll);
		Assert.False(convertedStudents[0].Passed);
	}

	[Fact]
	public void ConvertStudents_WhenEmptyArray_ReturnsEmptyArray()
	{
		// Arrange
		var students = new List<Student>();
		var converter = new StudentConverter();

		// Act
		var convertedStudents = converter.ConvertStudents(students);

		// Assert
		Assert.Empty(convertedStudents);
	}

	[Fact]
	public void ConvertStudents_WhenNullInput_ThrowsArgumentNullException()
	{
		// Arrange
		List<Student> students = null;
		var converter = new StudentConverter();

		// Act & Assert
		Assert.Throws<ArgumentNullException>(() => converter.ConvertStudents(students));
	}
}