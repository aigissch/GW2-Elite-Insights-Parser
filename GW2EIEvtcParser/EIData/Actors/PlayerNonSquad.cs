﻿using System.IO;
using System.Linq;
using GW2EIEvtcParser.ParsedData;

namespace GW2EIEvtcParser.EIData
{
    public class PlayerNonSquad : AbstractPlayer
    {

        private static int NonSquadPlayers = 0;
        // Constructors
        internal PlayerNonSquad(AgentItem agent) : base(agent)
        {
            if (agent.Type == AgentItem.AgentType.Player)
            {
                throw new InvalidDataException("Agent is not a squad Player");
            }
            Account = "Non Squad Player " + (++NonSquadPlayers);
        }
        protected override void TrimCombatReplay(ParsedEvtcLog log)
        {
            if (!AgentItem.IsNotInSquadFriendlyPlayer)
            {
                TrimCombatReplay(log, CombatReplay, AgentItem);
            }
        }

    }
}
