using System;

/*
 * 上移
 * -获取列数据，形成一维数组
 * --和 0 交换位置:去零
 * ---合并数据，相同且相邻，则合并(将下一个与前一个合并)
 * ----再去零
 * -----将一维数组赋值给原列
 * 2 2 0 0 => 4 0 0 0
 * 2 0 2 0 => 4 0 0 0
 * 2 2 2 0 => 4 0 2 0 => 4 2 0 0 
 * 2 0 0 2 => 2 2 0 0 => 4 0 0 0 
 * 0 2 2 2 => 2 0 2 2 => 2 2 0 2 => 2 2 2 0 => 4 0 2 0 => 4 2 0 0 
 * 
 * 下移
 * 左移
 * 右移
 */
namespace _2048
{
    class Program
    {

        static void Main(string[] args)
        {
            Fun2048 Fun2048 = new Fun2048();

            int[,] map = new int[4, 6] {
                {2,0,4,16,2,4},
                {2,4,0,16,2,2},
                {4,4,2,0,2,2},
                {8,4,2,16,2,4},
            };

            //foreach (int item in getArr(getCol(3, map)))
            //{
            //    Console.WriteLine(item);
            //}

            //Console.WriteLine();
            //for (int c = 0; c < map.GetLength(1); c++)
            //{
            //    int[] arr = getArr(getCol(c, map));
            //    for (int i = 0; i < arr.GetLength(0); i++)
            //    {
            //        map[i,c] = arr[i];
            //    }
            //}
            void start(){
                var key = Console.Read();
                ConsoleKeyInfo info = Console.ReadKey();
                Console.WriteLine(info.Key);
                switch (info.Key)
                {
                    case ConsoleKey.UpArrow:
                        Fun2048.MoveUp(map);
                        break;
                    case ConsoleKey.DownArrow:
                        Fun2048.MoveDown(map);
                        break;
                    case ConsoleKey.LeftArrow:
                        Fun2048.MoveLeft(map);
                        break;
                    case ConsoleKey.RightArrow:
                        Fun2048.MoveRight(map);
                        break;
                    default:
                        Environment.Exit(0);
                        break;
                }
                for (int r = 0; r < map.GetLength(0); r++)
                {
                    for (int c = 0; c < map.GetLength(1); c++)
                    {
                        Console.Write(map[r, c] + "\t");
                    }
                    Console.WriteLine();
                }
                start();
            }
            start();
        }


    }
    class Fun2048
    {
        //去零到右
        private int[] delZeroToRig(int[] arr)
        {
            int lefIndex = 0; //左下标
            int[] newArr = new int[arr.Length];
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] != 0) newArr[lefIndex++] = arr[i];
            }
            return newArr;
        }
        //去零到左
        private int[] delZeroToLef(int[] arr)
        {
            int rigIndex = arr.Length - 1; //右下标
            int[] newArr = new int[arr.Length];
            for (int i = rigIndex; i >= 0; i--)
            {
                if (arr[i] != 0) newArr[rigIndex--] = arr[i];
            }
            return newArr;
        }

        //向左合并
        private int[] addLef(ref bool canAdd, int[] newArr)
        {
            int length = newArr.Length-1;
            for (int i = 0; i < length; i++)
            {
                if (newArr[i] != 0 && newArr[i] == newArr[i + 1] && canAdd == true)
                {
                    newArr[i] += newArr[i + 1];
                    newArr[i + 1] = 0;
                    canAdd = false;
                }
            }
            return newArr;
        }

        //向右合并
        private int[] addRig(ref bool canAdd, int[] newArr)
        {
            int length = newArr.Length-1;
            for (int i = length; i > 0; i--)
            {
                if (newArr[i] != 0 && newArr[i] == newArr[i - 1] && canAdd == true)
                {
                    newArr[i] += newArr[i - 1];
                    newArr[i - 1] = 0;
                    canAdd = false;
                }
            }
            return newArr;
        }
        //获取对应列的一维数组
        private int[] getCol(int c, int[,] map)
        {
            int[] arr = new int[map.GetLength(0)];
            for (int i = 0; i < map.GetLength(0); i++)
            {
                arr[i] = map[i, c];
            }
            return arr;
        }

        //获取对应行的一维数组
        private int[] getRow(int r, int[,] map)
        {
            int[] arr = new int[map.GetLength(1)];
            for (int i = 0; i < map.GetLength(1); i++)
            {
                arr[i] = map[r, i];
            }
            return arr;
        }

        //处理一维数组
        private int[] getArr(int[] arr,bool isUpRig)
        {
            bool canAdd = true; //合并一次
            int[] array = isUpRig
                ? delZeroToRig(addLef(ref canAdd, delZeroToRig(arr)))
                : delZeroToLef(addRig(ref canAdd, delZeroToLef(arr)));
            return array;
        }

        public void MoveUp(int[,] array)
        {
            for (int c = 0; c < array.GetLength(1); c++)
            {
                int[] arr = getArr(getCol(c, array),true);
                for (int i = 0; i < arr.Length; i++)
                {
                    array[i, c] = arr[i];
                }
            }
        }

        public void MoveDown(int[,] array)
        {
            for (int c = 0; c < array.GetLength(1); c++)
            {
                int[] arr = getArr(getCol(c, array), false);
                for (int i = 0; i < arr.Length; i++)
                {
                    array[i, c] = arr[i];
                }
            }
        }

        public void MoveLeft(int[,] array)
        {
            for (int r = 0; r < array.GetLength(0); r++)
            {
                int[] arr = getArr(getRow(r, array), true);
                for (int i = 0; i < arr.Length; i++)
                {
                    array[r, i] = arr[i];
                }
            }
        }

        public void MoveRight(int[,] array)
        {
            for (int r = 0; r < array.GetLength(0); r++)
            {
                int[] arr = getArr(getRow(r, array), false);
                for (int i = 0; i < arr.Length; i++)
                {
                    array[r, i] = arr[i];
                }
            }
        }
    }
}