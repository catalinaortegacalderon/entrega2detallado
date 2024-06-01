using Fire_Emblem;
using Fire_Emblem_View;

/*
 * Este código permite replicar un test case. Primero pregunta por el grupo de test
 * case a replicar. Luego pregunta por el test case específico que se quiere replicar.
 *
 * Por ejemplo, si tu programa está fallando el test case:
 *      "data/E1-BasicCombat-Tests/006.txt"
 * ... puedes ver qué está ocurriendo mediante correr este programa y decir que quieres
 * replicar del grupo "E1-BasicCombat-Tests" el test case 6.
 *
 * Al presionar enter, se ingresa el input del test case en forma automática. Si el
 * color es azúl significa que el output de tu programa es el esperado. Si es rojo
 * significa que el output de tu programa es distinto al esperado (i.e., el test falló).
 *
 * Si, por algún motivo, quieres ejecutar tu programa de modo manual (sin replicar un
 * test case específico), puedes cambiar la línea:
 *      var view = View.BuildManualTestingView(test);
 * por:
 *      var view = View.BuildConsoleView();
 */
var testFolder = SelectTestFolder();
var test = SelectTest(testFolder);
var teamsFolder = testFolder.Replace("-Tests", "");
AnnounceTestCase(test);

var view = View.BuildManualTestingView(test);
var game = new Game(new GameView(view), teamsFolder);
game.Play();

string SelectTestFolder()
{
    Console.WriteLine("¿Qué grupo de test quieres usar?");
    var dirs = GetAvailableTestsInOrder();
    ShowArrayOfOptions(dirs);
    return AskUserToSelectAnOption(dirs);
}

string[] GetAvailableTestsInOrder()
{
    var dirs = Directory.GetDirectories("data", "*-Tests", SearchOption.TopDirectoryOnly);
    Array.Sort(dirs);
    return dirs;
}

void ShowArrayOfOptions(string[] options)
{
    for (var i = 0; i < options.Length; i++)
        Console.WriteLine($"{i}- {options[i]}");
}

string AskUserToSelectAnOption(string[] options)
{
    var minValue = 0;
    var maxValue = options.Length - 1;
    var selectedOption = AskUserToSelectNumber(minValue, maxValue);
    return options[selectedOption];
}

int AskUserToSelectNumber(int minValue, int maxValue)
{
    Console.WriteLine($"(Ingresa un número entre {minValue} y {maxValue})");
    int value;
    bool wasParsePossible;
    do
    {
        var userInput = Console.ReadLine();
        wasParsePossible = int.TryParse(userInput, out value);
    } while (!wasParsePossible || IsValueOutsideTheValidRange(minValue, value, maxValue));

    return value;
}

bool IsValueOutsideTheValidRange(int minValue, int value, int maxValue)
{
    return value < minValue || value > maxValue;
}

string SelectTest(string testFolder)
{
    Console.WriteLine("¿Qué test quieres ejecutar?");
    var tests = Directory.GetFiles(testFolder, "*.txt");
    Array.Sort(tests);
    return AskUserToSelectAnOption(tests);
}

void AnnounceTestCase(string test)
{
    Console.WriteLine("----------------------------------------");
    Console.WriteLine($"Replicando test: {test}");
    Console.WriteLine("----------------------------------------\n");
}