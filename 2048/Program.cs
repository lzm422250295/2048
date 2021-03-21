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
            //去零
            int[] delZero(int[] arr)
            {
                int lefIndex = 0; //左下标
                int rigIndex = arr.Length - 1; //右下标
                int[] newArr = new int[arr.Length];
                for (int i = 0; i < arr.Length; i++)
                {
                    if (arr[i] == 0) newArr[rigIndex--] = 0;
                    else newArr[lefIndex++] = arr[i];
                }
                return newArr;
            }

            //合并
            int[] add(ref bool canAdd, int[] newArr)
            {
                int length = newArr.Length;
                for (int i = 0; i < length; i++)
                {
                    if (i == length - 1) continue;
                    if (newArr[i] == newArr[i + 1] && canAdd == true)
                    {
                        newArr[i] += newArr[i + 1];
                        newArr[i + 1] = 0;
                        canAdd = false;
                    }
                }



                return newArr;
            }

            //获取对应列的一维数组
            int[] getCol(int index, int[,] map)
            {
                int[] arr = new int[map.GetLength(0)];
                for (int i = 0; i < map.GetLength(0); i++)
                {
                    arr[i] = map[i, index];
                }
                return arr;
            }

            //处理一维数组
            int[] getArr(int[] arr)
            {
                bool canAdd = true; //合并一次
                int[] array = delZero(add(ref canAdd, delZero(arr)));
                //Array.Reverse(array);

                //合并
                return array;
            }


            int[,] map = new int[4, 4] {
                {2,0,4,16},
                {2,4,0,16},
                {4,4,2,0},
                {8,4,2,16},
            };

            foreach (int item in getArr(getCol(3, map)))
            {
                Console.WriteLine(item);
            }

            Console.WriteLine();
            for (int c = 0; c < map.GetLength(1); c++)
            {
                for (int r = 0; r < map.GetLength(0); r++)
                {
                    //Console.WriteLine(map[r, c]);
                    Console.WriteLine(getArr(getCol(c, map))[r] + "\t");
                }
                    Console.WriteLine();
            }

            //for (int r = 0; r < map.GetLength(0); r++)
            //{
            //    for (int c = 0; c < map.GetLength(1); c++)
            //    {
            //        Console.Write(map[r, c] + "\t");
            //    }
            //    Console.WriteLine();
            //}

        }


    }
}