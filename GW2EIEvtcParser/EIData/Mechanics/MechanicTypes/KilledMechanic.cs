﻿using System.Collections.Generic;
using GW2EIEvtcParser.ParsedData;

namespace GW2EIEvtcParser.EIData
{

    internal class KilledMechanic : Mechanic
    {

        public KilledMechanic(long mechanicID, string inGameName, MechanicPlotlySetting plotlySetting, string shortName, int internalCoolDown) : this(mechanicID, inGameName, plotlySetting, shortName, shortName, shortName, internalCoolDown)
        {
        }

        public KilledMechanic(long mechanicID, string inGameName, MechanicPlotlySetting plotlySetting, string shortName, string description, string fullName, int internalCoolDown) : base(mechanicID, inGameName, plotlySetting, shortName, description, fullName, internalCoolDown)
        {
            IsEnemyMechanic = true;
        }

        internal override void CheckMechanic(ParsedEvtcLog log, Dictionary<Mechanic, List<MechanicEvent>> mechanicLogs, Dictionary<int, AbstractSingleActor> regroupedMobs)
        {
            foreach (long mechanicID in MechanicIDs)
            {
                foreach (AgentItem a in log.AgentData.GetNPCsByID((int)mechanicID))
                {
                    if (!regroupedMobs.TryGetValue(a.ID, out AbstractSingleActor amp))
                    {
                        amp = log.FindActor(a, true);
                        if (amp == null)
                        {
                            continue;
                        }
                        regroupedMobs.Add(amp.ID, amp);
                    }
                    foreach (DeadEvent devt in log.CombatData.GetDeadEvents(a))
                    {
                        mechanicLogs[this].Add(new MechanicEvent(devt.Time, this, amp));
                    }
                }
            }          
        }
    }
}
