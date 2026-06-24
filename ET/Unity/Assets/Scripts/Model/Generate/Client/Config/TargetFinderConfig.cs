using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using System.ComponentModel;

namespace ET
{
    [Config]
    public partial class TargetFinderConfigCategory : Singleton<TargetFinderConfigCategory>, IMerge
    {
        [BsonElement]
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        private Dictionary<int, TargetFinderConfig> dict = new();
		
        public void Merge(object o)
        {
            TargetFinderConfigCategory s = o as TargetFinderConfigCategory;
            foreach (var kv in s.dict)
            {
                this.dict.Add(kv.Key, kv.Value);
            }
        }
		
        public TargetFinderConfig Get(int id)
        {
            this.dict.TryGetValue(id, out TargetFinderConfig item);

            if (item == null)
            {
                throw new Exception($"配置找不到，配置表名: {nameof (TargetFinderConfig)}，配置id: {id}");
            }

            return item;
        }
		
        public bool Contain(int id)
        {
            return this.dict.ContainsKey(id);
        }

        public Dictionary<int, TargetFinderConfig> GetAll()
        {
            return this.dict;
        }

        public TargetFinderConfig GetOne()
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

	public partial class TargetFinderConfig: ProtoObject, IConfig
	{
		/// <summary>ID</summary>
		public int Id { get; set; }
		/// <summary>寻敌类型</summary>
		public int FinderType { get; set; }
		/// <summary>搜索范围</summary>
		public int Range { get; set; }
		/// <summary>锚点类型</summary>
		public int AnchorType { get; set; }
		/// <summary>参数1</summary>
		public int Param1 { get; set; }
		/// <summary>参数2</summary>
		public int Param2 { get; set; }
		/// <summary>偏移X</summary>
		public int OffsetX { get; set; }
		/// <summary>偏移Y</summary>
		public int OffsetY { get; set; }

	}
}