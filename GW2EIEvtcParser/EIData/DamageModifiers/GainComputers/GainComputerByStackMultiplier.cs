﻿using System;

namespace GW2EIEvtcParser.EIData
{
    internal class GainComputerByStackMultiplier : GainComputer
    {
        public GainComputerByStackMultiplier()
        {
            Multiplier = true;
        }

        public override double ComputeGain(double gainPerStack, int stack)
        {
            if (stack == 0)
            {
                return 0.0;
            }
            var pow = 100.0 * Math.Pow(1.0 + gainPerStack/100.0, stack) - 100.0;
            return pow / (100 + pow);
        }
    }
}
