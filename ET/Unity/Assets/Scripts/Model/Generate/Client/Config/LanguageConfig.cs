using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using System.ComponentModel;

namespace ET
{
    [Config]
    public partial class LanguageConfigCategory : Singleton<LanguageConfigCategory>, IMerge
    {
        [BsonElement]
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        private Dictionary<int, LanguageConfig> dict = new();
		
        public void Merge(object o)
        {
            LanguageConfigCategory s = o as LanguageConfigCategory;
            foreach (var kv in s.dict)
            {
                this.dict.Add(kv.Key, kv.Value);
            }
        }
		
        public LanguageConfig Get(int id)
        {
            this.dict.TryGetValue(id, out LanguageConfig item);

            if (item == null)
            {
                throw new Exception($"配置找不到，配置表名: {nameof (LanguageConfig)}，配置id: {id}");
            }

            return item;
        }
		
        public bool Contain(int id)
        {
            return this.dict.ContainsKey(id);
        }

        public Dictionary<int, LanguageConfig> GetAll()
        {
            return this.dict;
        }

        public LanguageConfig GetOne()
        {
            if (this.dict == null || this.dict.Count <= 0)
            {
                return null;
            }
            
            var enumerator = this.dict.Values.GetEnumerator();
            enumerator.MoveNext();
            return enumerator.Current; 
        }
    }

	public partial class LanguageConfig: ProtoObject, IConfig
	{
		/// <summary>ID</summary>
		public int Id { get; set; }
		/// <summary>名字</summary>
		public string Name { get; set; }
		/// <summary>资源</summary>
		public string Asset { get; set; }
		/// <summary>UI分支</summary>
		public string UIBranch { get; set; }

	}
}