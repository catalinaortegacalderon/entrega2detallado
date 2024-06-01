using Fire_Emblem_View;

namespace Fire_Emblem.Tests;

public class Tests
{
    [Theory]
    [MemberData(nameof(GetTestsAssociatedWithThisFolder), "E1-BasicCombat")]
    public void TestE1_BasicCombat(string teamsFolder, string testFile)
    {
        RunTest(teamsFolder, testFile);
    }

    [Theory]
    [MemberData(nameof(GetTestsAssociatedWithThisFolder), "E1-InvalidTeams")]
    public void TestE1_InvalidTeams(string teamsFolder, string testFile)
    {
        RunTest(teamsFolder, testFile);
    }

    [Theory]
    [MemberData(nameof(GetTestsAssociatedWithThisFolder), "E2")]
    public void TestE2(string teamsFolder, string testFile)
    {
        RunTest(teamsFolder, testFile);
    }

    [Theory]
    [MemberData(nameof(GetTestsAssociatedWithThisFolder), "E2-Random")]
    public void TestE2_Random(string teamsFolder, string testFile)
    {
        RunTest(teamsFolder, testFile);
    }

    [Theory]
    [MemberData(nameof(GetTestsAssociatedWithThisFolder), "E2-Mix")]
    public void TestE2_Mix(string teamsFolder, string testFile)
    {
        RunTest(teamsFolder, testFile);
    }

    [Theory]
    [MemberData(nameof(GetTestsAssociatedWithThisFolder), "E3")]
    public void TestE3(string teamsFolder, string testFile)
    {
        RunTest(teamsFolder, testFile);
    }

    [Theory]
    [MemberData(nameof(GetTestsAssociatedWithThisFolder), "E3-Random")]
    public void TestE3_Random(string teamsFolder, string testFile)
    {
        RunTest(teamsFolder, testFile);
    }

    [Theory]
    [MemberData(nameof(GetTestsAssociatedWithThisFolder), "E3-Mix")]
    public void TestE3_Mix(string teamsFolder, string testFile)
    {
        RunTest(teamsFolder, testFile);
    }

    [Theory]
    [MemberData(nameof(GetTestsAssociatedWithThisFolder), "E4-1")]
    public void TestE4_1(string teamsFolder, string testFile)
    {
        RunTest(teamsFolder, testFile);
    }

    [Theory]
    [MemberData(nameof(GetTestsAssociatedWithThisFolder), "E4-1-Random")]
    public void TestE4_1_Random(string teamsFolder, string testFile)
    {
        RunTest(teamsFolder, testFile);
    }

    [Theory]
    [MemberData(nameof(GetTestsAssociatedWithThisFolder), "E4-1-Mix")]
    public void TestE4_1_Mix(string teamsFolder, string testFile)
    {
        RunTest(teamsFolder, testFile);
    }

    [Theory]
    [MemberData(nameof(GetTestsAssociatedWithThisFolder), "E4-2")]
    public void TestE4_2(string teamsFolder, string testFile)
    {
        RunTest(teamsFolder, testFile);
    }

    [Theory]
    [MemberData(nameof(GetTestsAssociatedWithThisFolder), "E4-2-Random")]
    public void TestE4_2_Random(string teamsFolder, string testFile)
    {
        RunTest(teamsFolder, testFile);
    }

    [Theory]
    [MemberData(nameof(GetTestsAssociatedWithThisFolder), "E4-2-Mix")]
    public void TestE4_2_Mix(string teamsFolder, string testFile)
    {
        RunTest(teamsFolder, testFile);
    }

    public static IEnumerable<object[]> GetTestsAssociatedWithThisFolder(string teamsFolder)
    {
        teamsFolder = Path.Combine("data", teamsFolder);
        var testFolder = teamsFolder + "-Tests";
        var testFiles = GetAllTestFilesFrom(testFolder);
        return ConvertDataIntoTheAppropriateFormat(teamsFolder, testFiles);
    }

    private static string[] GetAllTestFilesFrom(string folder)
    {
        return Directory.GetFiles(folder, "*.txt", SearchOption.TopDirectoryOnly);
    }

    private static IEnumerable<object[]> ConvertDataIntoTheAppropriateFormat(string teamsFolder, string[] testFiles)
    {
        var allData = new List<object[]>();
        foreach (var testFile in testFiles)
            allData.Add(new object[] { teamsFolder, testFile });
        return allData;
    }

    private static void RunTest(string teamsFolder, string testFile)
    {
        var view = View.BuildTestingView(testFile);
        var gameView = new GameView(view);
        var game = new Game(gameView, teamsFolder);
        game.Play();

        var actualScript = view.GetScript();
        var expectedScript = File.ReadAllLines(testFile);
        CompareScripts(actualScript, expectedScript);
    }

    private static void CompareScripts(IReadOnlyList<string> actualScript, IReadOnlyList<string> expectedScript)
    {
        var numberOfLines = Math.Max(expectedScript.Count, actualScript.Count);
        for (var i = 0; i < numberOfLines; i++)
        {
            var expected = GetTheItemOrEmptyIfOutOfIndex(i, expectedScript);
            var actual = GetTheItemOrEmptyIfOutOfIndex(i, actualScript);
            Assert.Equal($"[L{i + 1}] " + expected, $"[L{i + 1}] " + actual);
        }
    }

    private static string GetTheItemOrEmptyIfOutOfIndex(int index, IReadOnlyList<string> array)
    {
        return index < array.Count ? array[index] : "";
    }
}