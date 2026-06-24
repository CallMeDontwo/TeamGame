using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using System.ComponentModel;

namespace ET
{
    [Config]
    public partial class HeroConfigCategory : Singleton<HeroConfigCategory>, IMerge
    {
        [BsonElement]
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        private Dictionary<int, HeroConfig> dict = new();
		
        public void Merge(object o)
        {
            HeroConfigCategory s = o as HeroConfigCategory;
            foreach (var kv in s.dict)
            {
                this.dict.Add(kv.Key, kv.Value);
            }
        }
		
        public HeroConfig Get(int id)
        {
            this.dict.TryGetValue(id, out HeroConfig item);

            if (item == null)
            {
                throw new Exception($"配置找不到，配置表名: {nameof (HeroConfig)}，配置id: {id}");
            }

            return item;
        }
		
        public bool Contain(int id)
        {
            return this.dict.ContainsKey(id);
        }

        public Dictionary<int, HeroConfig> GetAll()
        {
            return this.dict;
        }

        public HeroConfig GetOne()
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

	public partial class HeroConfig: ProtoObject, IConfig
	{
		/// <summary>英雄ID</summary>
		public int Id { get; set; }
		/// <summary>名称</summary>
		public string Name { get; set; }
		/// <summary>描述</summary>
		public string Desc { get; set; }
		/// <summary>数值类型列表</summary>
		public int[] NumericType { get; set; }
		/// <summary>数值列表</summary>
		public int[] NumericValue { get; set; }
		/// <summary>显示缩放/100</summary>
		public int Scale { get; set; }
		/// <summary>骨骼资源路径</summary>
		public string SkeletonAsset { get; set; }
		/// <summary>AI配置ID</summary>
		public int AIconfigId { get; set; }
		/// <summary>技能ID列表</summary>
		public int[] SkillIds { get; set; }
		/// <summary>碰撞半径</summary>
		public int CollisionRadius { get; set; }
		/// <summary>普攻</summary>
		public int BasicAttackSkillId { get; set; }
		/// <summary>高度</summary>
		public int Height { get; set; }

	}
}