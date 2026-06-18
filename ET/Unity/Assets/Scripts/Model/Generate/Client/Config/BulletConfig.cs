using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using System.ComponentModel;

namespace ET
{
    [Config]
    public partial class BulletConfigCategory : Singleton<BulletConfigCategory>, IMerge
    {
        [BsonElement]
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        private Dictionary<int, BulletConfig> dict = new();
		
        public void Merge(object o)
        {
            BulletConfigCategory s = o as BulletConfigCategory;
            foreach (var kv in s.dict)
            {
                this.dict.Add(kv.Key, kv.Value);
            }
        }
		
        public BulletConfig Get(int id)
        {
            this.dict.TryGetValue(id, out BulletConfig item);

            if (item == null)
            {
                throw new Exception($"配置找不到，配置表名: {nameof (BulletConfig)}，配置id: {id}");
            }

            return item;
        }
		
        public bool Contain(int id)
        {
            return this.dict.ContainsKey(id);
        }

        public Dictionary<int, BulletConfig> GetAll()
        {
            return this.dict;
        }

        public BulletConfig GetOne()
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

	public partial class BulletConfig: ProtoObject, IConfig
	{
		/// <summary>ID</summary>
		public int Id { get; set; }
		/// <summary>速度</summary>
		public int Speed { get; set; }
		/// <summary>伤害</summary>
		public int Damage { get; set; }
		/// <summary>飞行类型</summary>
		public int FlightType { get; set; }
		/// <summary>飞行参数</summary>
		public int[] FlightValue { get; set; }
		/// <summary>子弹类型</summary>
		public int BulletType { get; set; }
		/// <summary>子弹类型参数</summary>
		public int[] BulletTypeValue { get; set; }
		/// <summary>锁定追踪</summary>
		public int IsHoming { get; set; }
		/// <summary>最大飞行距离</summary>
		public int MaxDistance { get; set; }
		/// <summary>寻敌配置ID</summary>
		public int TargetFinderId { get; set; }
		/// <summary>碰撞半径</summary>
		public int CollisionRadius { get; set; }
		/// <summary>预制体路径</summary>
		public string PrefabPath { get; set; }

	}
}