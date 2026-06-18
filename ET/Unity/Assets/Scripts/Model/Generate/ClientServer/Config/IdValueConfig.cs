using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using System.ComponentModel;

namespace ET
{
    [Config]
    public partial class IdValueConfigCategory : Singleton<IdValueConfigCategory>, IMerge
    {
        [BsonElement]
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        private Dictionary<int, IdValueConfig> dict = new();
		
        public void Merge(object o)
        {
            IdValueConfigCategory s = o as IdValueConfigCategory;
            foreach (var kv in s.dict)
            {
                this.dict.Add(kv.Key, kv.Value);
            }
        }
		
        public IdValueConfig Get(int id)
        {
            this.dict.TryGetValue(id, out IdValueConfig item);

            if (item == null)
            {
                throw new Exception($"配置找不到，配置表名: {nameof (IdValueConfig)}，配置id: {id}");
            }

            return item;
        }
		
        public bool Contain(int id)
        {
            return this.dict.ContainsKey(id);
        }

        public Dictionary<int, IdValueConfig> GetAll()
        {
            return this.dict;
        }

        public IdValueConfig GetOne()
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

	public partial class IdValueConfig: ProtoObject, IConfig
	{
		/// <summary>Id</summary>
		public int Id { get; set; }
		/// <summary>性别模型</summary>
		public string SexModel { get; set; }
		/// <summary>皮肤</summary>
		public string Skin { get; set; }
		/// <summary>背饰皮肤</summary>
		public string TrimSkin_back { get; set; }
		/// <summary>载具</summary>
		public string Vehicle { get; set; }

	}
}