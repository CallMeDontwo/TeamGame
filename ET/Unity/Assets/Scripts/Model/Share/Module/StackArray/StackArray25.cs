using System;

namespace ET
{
    public struct StackArray25<T>
    {
        private T item0;
        private T item1;
        private T item2;
        private T item3;
        private T item4;
        private T item5;
        private T item6;
        private T item7;
        private T item8;
        private T item9;
        private T item10;
        private T item11;
        private T item12;
        private T item13;
        private T item14;
        private T item15;
        private T item16;
        private T item17;
        private T item18;
        private T item19;
        private T item20;
        private T item21;
        private T item22;
        private T item23;
        private T item24;

        public T this[int index]
        {
            readonly get => index switch
            {
                0 => this.item0,
                1 => this.item1,
                2 => this.item2,
                3 => this.item3,
                4 => this.item4,
                5 => this.item5,
                6 => this.item6,
                7 => this.item7,
                8 => this.item8,
                9 => this.item9,
                10 => this.item10,
                11 => this.item11,
                12 => this.item12,
                13 => this.item13,
                14 => this.item14,
                15 => this.item15,
                16 => this.item16,
                17 => this.item17,
                18 => this.item18,
                19 => this.item19,
                20 => this.item20,
                21 => this.item21,
                22 => this.item22,
                23 => this.item23,
                24 => this.item24,
                _ => throw new IndexOutOfRangeException(),
            };
            set
            {
                switch (index)
                {
                    case 0:
                        this.item0 = value;
                        break;
                    case 1:
                        this.item1 = value;
                        break;
                    case 2:
                        this.item2 = value;
                        break;
                    case 3:
                        this.item3 = value;
                        break;
                    case 4:
                        this.item4 = value;
                        break;
                    case 5:
                        this.item5 = value;
                        break;
                    case 6:
                        this.item6 = value;
                        break;
                    case 7:
                        this.item7 = value;
                        break;
                    case 8:
                        this.item8 = value;
                        break;
                    case 9:
                        this.item9 = value;
                        break;
                    case 10:
                        this.item10 = value;
                        break;
                    case 11:
                        this.item11 = value;
                        break;
                    case 12:
                        this.item12 = value;
                        break;
                    case 13:
                        this.item13 = value;
                        break;
                    case 14:
                        this.item14 = value;
                        break;
                    case 15:
                        this.item15 = value;
                        break;
                    case 16:
                        this.item16 = value;
                        break;
                    case 17:
                        this.item17 = value;
                        break;
                    case 18:
                        this.item18 = value;
                        break;
                    case 19:
                        this.item19 = value;
                        break;
                    case 20:
                        this.item20 = value;
                        break;
                    case 21:
                        this.item21 = value;
                        break;
                    case 22:
                        this.item22 = value;
                        break;
                    case 23:
                        this.item23 = value;
                        break;
                    case 24:
                        this.item24 = value;
                        break;
                    default:
                        throw new IndexOutOfRangeException();
                }
            }
        }
    }
}