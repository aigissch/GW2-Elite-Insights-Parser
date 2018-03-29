﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuckParser.Models.ParseModels
{
    public class Duration:AbstractBoon
    {
        // Constructor
        public  Duration(int capacity):base(capacity)
        {
            //super(capacity);
        }

        // Public Methods
        
    public override int getStackValue()
        {
            // return boon_stack.stream().mapToInt(Integer::intValue).sum();
            return boon_stack.Sum();
        }

        
    public override void update(int time_passed)
        {

            if (boon_stack.Count() > 0)
            {
                // Clear stack
                if (time_passed >= getStackValue())
                {
                    boon_stack.Clear();
                    return;
                }
                // Remove from the longest duration
                else
                {
                    boon_stack[0] = (boon_stack[0] - time_passed);
                    if (boon_stack[0] <= 0)
                    {
                        // Spend leftover time
                        time_passed = Math.Abs(boon_stack[0]);
                        boon_stack.RemoveAt(0);
                        update(time_passed);
                    }
                }
            }
        }

        
    public override void addStacksBetween(List<int> boon_stacks, int time_between)
        {
        }
    }
}