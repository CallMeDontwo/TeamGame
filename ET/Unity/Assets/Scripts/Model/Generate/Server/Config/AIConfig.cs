using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using System.ComponentModel;

namespace ET
{
    [Config]
    public partial class AIConfigCategory : Singleton<AIConfigCategory>, IMerge
    {
        [BsonElement]
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        private Dictionary<int, AIConfig> dict = new();
		
        public void Merge(object o)
        {
            AIConfigCategory s = o as AIConfigCategory;
            foreach (var kv in s.dict)
            {
                this.dict.Add(kv.Key, kv.Value);
            }
        }
		
        public AIConfig Get(int id)
        {
            this.dict.TryGetValue(id, out AIConfig item);

            if (item == null)
            {
                throw new Exception($"配置找不到，配置表名: {nameof (AIConfig)}，配置id: {id}");
            }

            return item;
        }
		
        public bool Contain(int id)
        {
            return this.dict.ContainsKey(id);
        }

        public Dictionary<int, AIConfig> GetAll()
        {
            return this.dict;
        }

        public AIConfig GetOne()
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

	public partial class AIConfig: ProtoObject, IConfig
	{
		/// <summary>配置ID</summary>
		public int Id { get; set; }
		/// <summary>AI名称描述</summary>
		public string AIName { get; set; }
		/// <summary>行为Handler序列</summary>
		public int[] HandlerIds { get; set; }
		/// <summary>决策间隔(ms)</summary>
		public int CheckInterval { get; set; }
		/// <summary>视野范围/100</summary>
		public int SightRange { get; set; }
		/// <summary>是否推进</summary>
		public int EnablePatrol { get; set; }
		/// <summary>推进距离/100</summary>
		public int PatrolRadius { get; set; }
		/// <summary>推进等待(ms)</summary>
		public int PatrolWaitTime { get; set; }
		/// <summary>最大追击距离/100</summary>
		public int ChaseMaxDistance { get; set; }
		/// <summary>攻击距离/100</summary>
		public int AttackRange { get; set; }

	}
}