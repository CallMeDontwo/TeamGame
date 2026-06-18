using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using System.ComponentModel;

namespace ET
{
    [Config]
    public partial class SkillEventConfigCategory : Singleton<SkillEventConfigCategory>, IMerge
    {
        [BsonElement]
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        private Dictionary<int, SkillEventConfig> dict = new();
		
        public void Merge(object o)
        {
            SkillEventConfigCategory s = o as SkillEventConfigCategory;
            foreach (var kv in s.dict)
            {
                this.dict.Add(kv.Key, kv.Value);
            }
        }
		
        public SkillEventConfig Get(int id)
        {
            this.dict.TryGetValue(id, out SkillEventConfig item);

            if (item == null)
            {
                throw new Exception($"配置找不到，配置表名: {nameof (SkillEventConfig)}，配置id: {id}");
            }

            return item;
        }
		
        public bool Contain(int id)
        {
            return this.dict.ContainsKey(id);
        }

        public Dictionary<int, SkillEventConfig> GetAll()
        {
            return this.dict;
        }

        public SkillEventConfig GetOne()
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

	public partial class SkillEventConfig: ProtoObject, IConfig
	{
		/// <summary>事件ID</summary>
		public int Id { get; set; }
		/// <summary>事件类型</summary>
		public int EventType { get; set; }
		/// <summary>整型参数1</summary>
		public int IntParam1 { get; set; }
		/// <summary>整型参数2</summary>
		public int IntParam2 { get; set; }
		/// <summary>整型参数3</summary>
		public int IntParam3 { get; set; }
		/// <summary>字符串参数</summary>
		public string StringParam { get; set; }

	}
}