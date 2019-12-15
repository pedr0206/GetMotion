using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GetFitness02.Models.Activity
{
    public class ActivityItem
    {
        public int ActivityItemId { get; set; }
        public string ActivityName { get; set; }
        public int Calorie { get; set; } //in this case, it is meant to be MET(Metabolic Equivalent)
                                         // kcal = (MET*WeightinKg)*(DurationinMin/60)
    }
}
