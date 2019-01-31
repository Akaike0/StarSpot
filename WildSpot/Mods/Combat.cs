using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarSpot
{
    class Combat
    {
        // Skill system for the combat system
        SkillsSystem skillsystem = new SkillsSystem();
        KeyusingSystem keyusing = new KeyusingSystem();
        Looting looting = new Looting();

        public void start()
        {
            // Looting class for better looting
            looting.loot();

            // Fight!
            skillsystem.skill_activation();
            keyusing.skill_activation();
        }
    }
}
