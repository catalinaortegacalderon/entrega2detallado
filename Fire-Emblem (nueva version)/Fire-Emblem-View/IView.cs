using ConsoleApp1.GameDataStructures;

namespace Fire_Emblem_View
{
    public interface IView
    {
        void AnnounceWinner(int winnersNumber);
        void ShowAttack(string attacker, string defender, int damage);
        void ShowHp(Unit roundStarterUnit, Unit opponentsUnit);
        void ShowAllSkills(Unit unit);
        string AskUserForOption(string prompt, string[] options);
        void AnnounceTeamsAreNotValid();
    }
}