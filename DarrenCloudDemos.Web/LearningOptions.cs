using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarrenCloudDemos.Web
{
    [Serializable]
    public class LearningOptions
    {
        public decimal Years { get; set; }
        public List<string> Topic { get; set; }
        public List<SkillItem> Skill { get; set; }
    }

    [Serializable]
    public class SkillItem
    {
        public string Lang { get; set; }
        public decimal? Score { get; set; }
    }
}
