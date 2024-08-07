using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace data_extractor.Model
{
    public class FailedFeature
    {
        public string Feature { get; set; }

        public List<string> BuildNumbers { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as FailedFeature);
        }

        public bool Equals(FailedFeature other)
        {
            return other != null &&
                   Feature == other.Feature;
        }

        public override int GetHashCode()
        {
            return Feature.GetHashCode();
        }
    }
}