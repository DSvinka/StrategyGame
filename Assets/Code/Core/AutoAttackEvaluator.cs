using System.Collections.Concurrent;
using System.Threading.Tasks;
using Abstractions.Commands;
using Codice.Client.Common;
using UniRx;
using UnityEngine;

namespace Core
{
    public sealed class AutoAttackEvaluator: MonoBehaviour
    {
        public sealed class FactionMemberParallelInfo
		{
    		public Vector3 Position;
    		public int Faction;
	
    		public FactionMemberParallelInfo(Vector3 position, int faction)
    		{
        		Position = position;
        		Faction = faction;
    		}
		}

		public sealed class AttackerParallelInfo
		{
    		public float VisionRadius;
    		public object CurrentCommand;

    		public AttackerParallelInfo(float visionRadius, object currentCommand)
	    	{
      	  		VisionRadius = visionRadius;
        		CurrentCommand = currentCommand;
    		}
		}

		public sealed class Command
		{
	    	public GameObject Attacker;
    		public GameObject Target;

    		public Command(GameObject attacker, GameObject target)
    		{
        		Attacker = attacker;
        		Target = target;
    		}
		}

		public static ConcurrentDictionary<GameObject, AttackerParallelInfo> AttackersInfo = new ConcurrentDictionary<GameObject, AttackerParallelInfo>();
		public static ConcurrentDictionary<GameObject, FactionMemberParallelInfo> FactionMembersInfo = new ConcurrentDictionary<GameObject, FactionMemberParallelInfo>();

		public static Subject<Command> AutoAttackCommands = new Subject<Command>();

		private void Update()
		{
    		Parallel.ForEach(AttackersInfo, kvp => Evaluate(kvp.Key, kvp.Value));
		}

		private void Evaluate(GameObject go, AttackerParallelInfo info)
		{
    		if (info.CurrentCommand is IMoveCommand)
    		{
        		return;
    		}

    		if (info.CurrentCommand is IAttackCommand && !(info.CurrentCommand is Command))
    		{
        		return;
    		}
            
    		if (!FactionMembersInfo.TryGetValue(go, out var factionInfo))
    		{
        		return;
    		}

    		foreach (var factionMember in FactionMembersInfo)
    		{
        		if (factionInfo.Faction == factionMember.Value.Faction)
        		{
            		continue;
        		}

        		var distance = Vector3.Distance(factionInfo.Position, factionMember.Value.Position);
        		if (distance > info.VisionRadius)
        		{
            		continue;
        		}

        		AutoAttackCommands.OnNext(new Command(go, factionMember.Key));
        		break;
    		}
		}
    }
}