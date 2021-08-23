﻿using System;
using System.Collections.Generic;
using System.Linq;
using GW2EIEvtcParser.ParsedData;

namespace GW2EIEvtcParser.Extensions
{
    internal class HealingStatsRev1ExtensionHandler : HealingStatsExtensionHandler
    {

        private readonly List<EXTAbstractHealingEvent> _healingEvents = new List<EXTAbstractHealingEvent>();

        private static bool IsHealingEvent(CombatItem c)
        {
            return (c.IsBuff == 0 && c.Value < 0) || (c.IsBuff != 0 && c.Value == 0 && c.BuffDmg < 0);
        }

        internal HealingStatsRev1ExtensionHandler(CombatItem c) : base()
        {
            Revision = 1;
            SetVersion(c);
        }

        internal override bool HasTime(CombatItem c)
        {
            return true;
        }

        internal override bool SrcIsAgent(CombatItem c)
        {
            return IsHealingEvent(c);
        }
        internal override bool DstIsAgent(CombatItem c)
        {
            return IsHealingEvent(c);
        }

        internal override bool IsDamage(CombatItem c)
        {
            return IsHealingEvent(c);
        }

        internal override void InsertEIExtensionEvent(CombatItem c, AgentData agentData, SkillData skillData)
        {
            if (!IsHealingEvent(c))
            {
                return;
            }
            if (c.IsBuff == 0 && c.Value < 0)
            {
                _healingEvents.Add(new EXTDirectHealingEvent(c, agentData, skillData, Revision > 1));
            }
            else if (c.IsBuff != 0 && c.Value == 0 && c.BuffDmg < 0)
            {
                _healingEvents.Add(new EXTNonDirectHealingEvent(c, agentData, skillData, Revision > 1));
            }
        }

        internal override void AttachToCombatData(CombatData combatData, ParserController operation, ulong gw2Build)
        {
            var addongRunning = new HashSet<AgentItem>();
            operation.UpdateProgressWithCancellationCheck("Attaching healing extension revision 2 combat events");
            var healData = _healingEvents.GroupBy(x => x.From).ToDictionary(x => x.Key, x => x.ToList());
            foreach (KeyValuePair<AgentItem, List<EXTAbstractHealingEvent>> pair in healData)
            {
                if (EXTHealingCombatData.SanitizeForSrc(pair.Value))
                {
                    addongRunning.Add(pair.Key);
                }
            }
            var healReceivedData = _healingEvents.GroupBy(x => x.To).ToDictionary(x => x.Key, x => x.ToList());
            foreach (KeyValuePair<AgentItem, List<EXTAbstractHealingEvent>> pair in healReceivedData)
            {
                if (EXTHealingCombatData.SanitizeForDst(pair.Value))
                {
                    addongRunning.Add(pair.Key);
                }
            }
            var healDataById = _healingEvents.GroupBy(x => x.SkillId).ToDictionary(x => x.Key, x => x.ToList());
            operation.UpdateProgressWithCancellationCheck(addongRunning.Count + " has the addon running");
            operation.UpdateProgressWithCancellationCheck("Attached " + _healingEvents.Count + " heal events to CombatData");
            combatData.EXTHealingCombatData = new EXTHealingCombatData(healData, healReceivedData, healDataById, GetHybridIDs(gw2Build));
            operation.UpdateProgressWithCancellationCheck("Attached healing extension combat events");
        }

    }
}
