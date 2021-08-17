﻿using System.Collections.Generic;
using static GW2EIEvtcParser.ArcDPSEnums;
using static GW2EIEvtcParser.EIData.Buff;
using static GW2EIEvtcParser.EIData.DamageModifier;
using static GW2EIEvtcParser.ParserHelper;

namespace GW2EIEvtcParser.EIData
{
    internal static class HarbingerHelper
    {

        internal static readonly List<InstantCastFinder> InstantCastFinder = new List<InstantCastFinder>()
        {
            new BuffGainCastFinder(62567, 59964, EIData.InstantCastFinder.DefaultICD), // Harbinger shroud
            new BuffLossCastFinder(62540, 59964, EIData.InstantCastFinder.DefaultICD), // Harbinger shroud
        };


        internal static readonly List<DamageModifier> DamageMods = new List<DamageModifier>
        {
            new BuffDamageModifier(62653, "Wicked Corruption", "1% per blight stack", DamageSource.NoPets, 1.0, DamageType.Strike, DamageType.All, Source.Harbinger, ByPresence, "https://wiki.guildwars2.com/images/0/00/Wicked_Corruption.png", 118697, ulong.MaxValue, DamageModifierMode.All),
            new BuffDamageModifier(62653, "Septic Corruption", "1% per blight stack", DamageSource.NoPets, 1.0, DamageType.Condition, DamageType.All, Source.Harbinger, ByPresence, "https://wiki.guildwars2.com/images/f/f7/Septic_Corruption.png", 118697, ulong.MaxValue, DamageModifierMode.All),
        };

        internal static readonly List<Buff> Buffs = new List<Buff>
        {
                new Buff("Harbinger Shroud",59964, Source.Harbinger, BuffNature.GraphOnlyBuff, "https://wiki.guildwars2.com/images/thumb/a/ab/Harbinger_Shroud.png/48px-Harbinger_Shroud.png", 118697, ulong.MaxValue),
                new Buff("Blight",62653, Source.Harbinger, BuffStackType.Stacking, 25, BuffNature.GraphOnlyBuff, "https://wiki.guildwars2.com/images/a/ac/Blight.png", 118697, ulong.MaxValue),
        };
    }
}
