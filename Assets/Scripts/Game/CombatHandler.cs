using System.Collections.Generic;
public class CombatHandler
{
    public class Match
    {
        public int Attackor;
        public int Defencor;

        public Match(int attackor, int defencor)
        {
            Attackor = attackor;
            Defencor = defencor;
        }
    }

    public readonly HashSet<int> SelectAttackSet;
    public readonly HashSet<Match> BattleSet;

    public CombatHandler()
    {
        SelectAttackSet = new HashSet<int>();
        BattleSet = new HashSet<Match>();
    }

    public void AddAttackor(int id)
    {
        SelectAttackSet.Add(id);
    }

    public void SumbitAttackor()
    {
        foreach (var selected in SelectAttackSet)
        {
            GameController.Instance.RpcAddAttackor(selected);
        }
    }

    public void AddMatch(int attackor, int defencor)
    {
        BattleSet.Add(new Match(attackor, defencor));
    }
}
