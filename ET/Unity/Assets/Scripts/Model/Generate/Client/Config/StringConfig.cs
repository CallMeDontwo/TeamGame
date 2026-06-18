using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using System.ComponentModel;

namespace ET
{
    [Config]
    public partial class StringConfigCategory : Singleton<StringConfigCategory>, IMerge
    {
        [BsonElement]
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        private Dictionary<int, StringConfig> dict = new();
		
        public void Merge(object o)
        {
            StringConfigCategory s = o as StringConfigCategory;
            foreach (var kv in s.dict)
            {
                this.dict.Add(kv.Key, kv.Value);
            }
        }
		
        public StringConfig Get(int id)
        {
            this.dict.TryGetValue(id, out StringConfig item);

            if (item == null)
            {
                throw new Exception($"配置找不到，配置表名: {nameof (StringConfig)}，配置id: {id}");
            }

            return item;
        }
		
        public bool Contain(int id)
        {
            return this.dict.ContainsKey(id);
        }

        public Dictionary<int, StringConfig> GetAll()
        {
            return this.dict;
        }

        public StringConfig GetOne()
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

	public partial class StringConfig: ProtoObject, IConfig
	{
		/// <summary>ID</summary>
		public int Id { get; set; }
		/// <summary>索引</summary>
		public string Key { get; set; }
		/// <summary>中文</summary>
		public string Chinese { get; set; }
		/// <summary>英文</summary>
		public string English { get; set; }
		/// <summary>繁體</summary>
		public string ChineseTw { get; set; }

	}
}