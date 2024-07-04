using System.Collections.Generic;
using System.Diagnostics;

namespace Clones;

public class Clone
{
    public List<string> programs = new();
    public LinkedList<string> LastProgram = new();
    public LinkedList<string> stackRollback = new();
    public LinkedList<string> stackRelearn = new();
}

public class CloneVersionSystem : ICloneVersionSystem
{
    public readonly Dictionary<string, Clone> clones = new();

    public string Execute(string query)
    {
        string[] commands = query.Split(' ');
        var command = commands[0];
        var id = commands[1];
        if (command == "learn")
        {
            LearnClone(commands, id);
        }
        else if (command == "rollback")
        {
            DoRollBack(id);
        }
        else if (command == "relearn")
        {
            DoRelearn(id);
        }
        else if (command == "clone")
        {
            MakeClone(id);
        }
        else
        {
            return CheckCLone(id);
        }

        return null;
    }

    private string CheckCLone(string id)
    {
        if (clones[id].stackRollback.Count == 0)
            return "basic";
        return clones[id].LastProgram.Last.Value;
    }

    private void MakeClone(string id)
    {
        if (clones.Count == 0)
            clones.Add(id, new Clone());
        var newId = (int.Parse(id) + 1).ToString();
        while (true)
        {
            if (clones.ContainsKey(newId))
                newId = (int.Parse(newId) + 1).ToString();
            else
                break;
        }
        clones.Add(newId, new Clone());
        clones[newId].programs = new List<string>(clones[id].programs);
        clones[newId].stackRelearn =
            new LinkedList<string>(clones[id].stackRelearn);
        clones[newId].stackRollback =
            new LinkedList<string>(clones[id].stackRollback);
        clones[newId].LastProgram =
            new LinkedList<string>(clones[id].LastProgram);
    }

    private void DoRelearn(string id)
    {
        var relearn = clones[id].stackRelearn.Last.Value;
        clones[id].stackRelearn.RemoveLast();
        clones[id].stackRollback.AddLast(relearn);
        clones[id].programs.Add(relearn);
        clones[id].LastProgram.AddLast(relearn);
    }

    private void DoRollBack(string id)
    {
        var roll = clones[id].stackRollback.Last.Value;
        clones[id].stackRollback.RemoveLast();
        clones[id].stackRelearn.AddLast(roll);
        clones[id].programs.Remove(roll);
        clones[id].LastProgram.RemoveLast();
    }

    private void LearnClone(string[] commands, string id)
    {
        if (!clones.ContainsKey(id))
            clones.Add(id, new Clone());
        if (!clones[id].programs.Contains(commands[2]))
        {
            if (clones[id].stackRelearn.Count > 0)
            {
                clones[id].stackRelearn.Clear();
                clones[id].stackRollback.Clear();
            }
            clones[id].programs.Add(commands[2]);
            clones[id].LastProgram.AddLast(commands[2]);
            clones[id].stackRollback.AddLast(commands[2]);
        }
    }
}