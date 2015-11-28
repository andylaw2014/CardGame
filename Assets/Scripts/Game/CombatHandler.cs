using System.Collections.Generic;

public class CombatHandler
{
    public readonly HashSet<Match> BattleSet;

    public readonly HashSet<int> SelectAttackSet;

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
            GameController2.Instance.RpcAddAttackor(selected);
        }
    }

    public void AddMatch(int attackor, int defencor)
    {
        BattleSet.Add(new Match(attackor, defencor));
    }

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
}