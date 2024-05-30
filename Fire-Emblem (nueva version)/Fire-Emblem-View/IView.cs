using ConsoleApp1.EncapsulatedLists;
using ConsoleApp1.GameDataStructures;

namespace Fire_Emblem_View;
public interface IView
{
    int AskPlayerForTheChosenFile(string[] files);
    void AnnounceTeamsAreNotValid();
    int AskAPlayerForTheChosenUnit(int playerNumber, UnitsList units);
    void ShowRoundInformation(int currentRound, string attackersName, int playersNumber);
    void AnnounceAdvantage(Unit unitWithAdvantage, Unit unitWithoutAdvantage);
    void AnnounceThereIsNoAdvantage();
    void ShowAllSkills(Unit unit);
    void ShowAttack(String attackersName, String defensorsName, int damage);
    void AnnounceNoUnitCanDoAFollowup();
    void ShowHp(Unit roundStarterUnit, Unit opponentsUnit);
    void AnnounceWinner(int winnersNumber); 
}