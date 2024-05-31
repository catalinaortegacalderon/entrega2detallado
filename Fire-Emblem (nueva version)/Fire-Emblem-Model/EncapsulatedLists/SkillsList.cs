using System.Collections;
using ConsoleApp1.SkillsManagement.Skills.BaseSkills;
using ConsoleApp1.SkillsManagement.Skills.BonusSkills;
using ConsoleApp1.SkillsManagement.Skills.PenaltySkills;

namespace ConsoleApp1.EncapsulatedLists;


public class SkillsList : IEnumerable<Skill>
{
    private List<Skill> _skills = new List<Skill> { new EmptySkill(), new EmptySkill(), new EmptySkill() };

    public Skill GetSkillByIndex(int index)
    {
        return _skills[index];
    }
    
    public void AddSkill(int index, Skill skill)
    {
        _skills[index] = skill;
    }
    
    public IEnumerator<Skill> GetEnumerator()
    {
        foreach (Skill skill in _skills)
        {
            yield return skill;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _skills.GetEnumerator();
    }
    
}