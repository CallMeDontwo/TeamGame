using System;
using System.Collections.Generic;
using System.Linq;

namespace ET.ShiTuoLingFuMo
{
    public struct MatchItem
    {
        public int SlotType;
        public int Count;
    }

    public struct RemoveData
    {
        public ListComponent<MatchItem> removes;

        public static RemoveData Create()
        {
            RemoveData removeData = new RemoveData();
            removeData.removes = ListComponent<MatchItem>.Create();
            return removeData;
        }
    }

    public struct GameResult
    {
        public int Credit;
        public int Bet;
        public int Win;
        public ListComponent<int> Slots;

        public static GameResult Create()
        {
            GameResult result = new GameResult();
            result.Slots = ListComponent<int>.Create();
            return result;
        }
    }

    [ComponentOf(typeof(Scene))]
    public sealed class ShiTuoLingFuMoManager : Entity, IAwake
    {
        public const int SlotQuantity = 7;
        public const int SlotTypeQuantity = 8;

        //游玩数据
        public int Credit;
        public int Bet;
        public int Win;

        //上下文数据
        public int[] Slots = new int[SlotQuantity];
        public int Combo;

        //临时数据
        public int[] SlotCount = new int[SlotTypeQuantity - 1];
    }

    [FriendOf(typeof(ShiTuoLingFuMoManager))]
    [EntitySystemOf(typeof(ShiTuoLingFuMoManager))]
    public static class ShiTuoLingFuMoManagerSystem
    {
        public static void Awake(this ShiTuoLingFuMoManager self)
        {
            using ListComponent<int> slots = self.GenerateNewSlots();
            slots.CopyTo(self.Slots);
        }

        public static void SetBet(this ShiTuoLingFuMoManager self, int bet)
        {
            self.Bet = bet;
        }

        public static ListComponent<int> GenerateNewSlots(this ShiTuoLingFuMoManager self)
        {
            ListComponent<int> result = ListComponent<int>.Create();
            for (int i = 0; i < self.Slots.Length; i++)
            {
                result.Add(RandomHelper.RandInt32(0, 8));
            }
            return result;
        }

        public static async ETTask Run(this ShiTuoLingFuMoManager self)
        {
            await ETTask.CompletedTask;
        }

        private static void StartGame(this ShiTuoLingFuMoManager self)
        {
            using ListComponent<int> slots = self.GenerateNewSlots();
            slots.CopyTo(self.Slots);
        }

        private static void CountineGame(this ShiTuoLingFuMoManager self)
        {
        }

        public static RemoveData CheckRemoveData(this ShiTuoLingFuMoManager self, in IList<int> slots, in int bet)
        {
            RemoveData removeData = new RemoveData();
            Array.Clear(self.SlotCount, 0, self.SlotCount.Length);
            slots.Foreach(slot => self.SlotCount[slot]++);
            //判断是否只有百搭和特殊图标
            if (self.SlotCount[7] + self.SlotCount[8] == ShiTuoLingFuMoManager.SlotQuantity)
            {
                if (self.SlotCount[7] >= 3)
                {

                }
            }
            else
            {

            }
            return removeData;
        }
    }
}