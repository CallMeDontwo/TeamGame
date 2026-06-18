using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using System.ComponentModel;

namespace ET
{
    [Config]
    public partial class MonsterConfigCategory : Singleton<MonsterConfigCategory>, IMerge
    {
        [BsonElement]
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        private Dictionary<int, MonsterConfig> dict = new();
		
        public void Merge(object o)
        {
            MonsterConfigCategory s = o as MonsterConfigCategory;
            foreach (var kv in s.dict)
            {
                this.dict.Add(kv.Key, kv.Value);
            }
        }
		
        public MonsterConfig Get(int id)
        {
            this.dict.TryGetValue(id, out MonsterConfig item);

            if (item == null)
            {
                throw new Exception($"配置找不到，配置表名: {nameof (MonsterConfig)}，配置id: {id}");
            }

            return item;
        }
		
        public bool Contain(int id)
        {
            return this.dict.ContainsKey(id);
        }

        public Dictionary<int, MonsterConfig> GetAll()
        {
            return this.dict;
        }

        public MonsterConfig GetOne()
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

	public partial class MonsterConfig: ProtoObject, IConfig
	{
		/// <summary>怪物ID</summary>
		public int Id { get; set; }
		/// <summary>名称</summary>
		public string Name { get; set; }
		/// <summary>描述</summary>
		public string Desc { get; set; }
		/// <summary>数值类型列表</summary>
		public int[] NumericType { get; set; }
		/// <summary>数值列表</summary>
		public int[] NumericValue { get; set; }
		/// <summary>AI配置ID</summary>
		public int AIConfigId { get; set; }
		/// <summary>显示缩放/100</summary>
		public int Scale { get; set; }
		/// <summary>骨骼资源路径</summary>
		public string SkeletonAsset { get; set; }
		/// <summary>是否精英(0/1)</summary>
		public int IsElite { get; set; }
		/// <summary>是否Boss(0/1)</summary>
		public int IsBoss { get; set; }
		/// <summary>AI配置ID</summary>
		public int AIconfigId { get; set; }
		/// <summary>碰撞半径</summary>
		public int CollisionRadius { get; set; }

	}
}