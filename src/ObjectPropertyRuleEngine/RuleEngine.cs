using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Text;

namespace ObjectPropertyRuleEngine
{
    public class RuleEngine
    {
        public RuleEngine()
        {   
        }

        public RuleEngine(RuleSet ruleSet)
        {
            RuleSet = ruleSet;
        }

        public RuleSet RuleSet { get; set; }

        public RuleSetCheckResult RunRuleSetAgainstObject(object dataStructureObject)
        {
            return RuleSet.RunRuleSetAgainstObject(dataStructureObject);
        }

        public void LoadRuleSetFromYaml(StringReader reader)
        {
            YamlDotNet.Serialization.Deserializer der = new YamlDotNet.Serialization.Deserializer();
            RuleSet = der.Deserialize<RuleSet>(reader);
        }
        public void LoadRuleSetFromYamlString(string yamlString)
        {
            YamlDotNet.Serialization.Deserializer der = new YamlDotNet.Serialization.Deserializer();
            RuleSet =  der.Deserialize<RuleSet>(yamlString);
        }

        public void LoadRulesetFromYamlFileUrl(string urlToYaml)
        {
            WebClient c = new WebClient();
            var yaml = new WebClient().DownloadString(urlToYaml);
            LoadRuleSetFromYamlString(yaml);
        }

    }
}
