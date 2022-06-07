using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 톨게이트프로그램
{
    class QuickSort
    {
        List<car> Quick_list;
        int left;
        int right;
        int tmp;
        public int size
        {
            get; set;
        }
        public QuickSort()
        {
            Quick_list = new List<car>();
            left = 0;
            right = size;
        }
        public void sort(List<car> Quick_list, int left, int right)
        {
            int first_left = left, first_right = right; ;                       // 최초 left와 right를 기억
            int pivot = Quick_list[left].car_count;                                        // 최초 피봇은 left부터 시작

            // left와 right가 만날 때까지 실행
            while (left < right)        // right와 피봇을 비교하여 right가 피봇보다 값이 작을 경우 피봇에 left에 right 값을 넣어줌
            {
                while ((pivot <= Quick_list[right].car_count) && (left < right))
                    right--;
                if (left != right)      // left와 피봇을 비교하여 left가 피봇보다 값이 높을 경우 피봇에 right에 left 값을 넣어줌
                {
                    tmp = Quick_list[right].car_count;
                    Quick_list[right].car_count = Quick_list[left].car_count;
                    Quick_list[left].car_count = tmp;
                    tmp = Quick_list[right].road_num;
                    Quick_list[right].road_num = Quick_list[left].road_num;
                    Quick_list[left].road_num = tmp;
                }
                while ((pivot >= Quick_list[left].car_count) && (left < right))
                    left++;
                if (left != right)
                {
                    tmp = Quick_list[right].car_count;
                    Quick_list[right].car_count = Quick_list[left].car_count;
                    Quick_list[left].car_count = tmp;
                    tmp = Quick_list[right].road_num;
                    Quick_list[right].road_num = Quick_list[left].road_num;
                    Quick_list[left].road_num = tmp;
                    right--;            // right의 위치를 왼쪽으로 이동하여 다시 반복
                }
            }

            // 1. left와 right가 같아지는 순간 해당 위치는 피봇으로 값을 변환해준다.
            // 2. 이렇게 되면 변경된 피봇의 위치를 기준으로 피봇보다 작으면 왼쪽, 
            //    피봇보다 크면 오른쪽으로 이동하게 만들었다!
            // 우리는 피봇의 자리를 정확하게 잡았다. 이제 값을 다시 정리를 해준다.
            Quick_list[left].car_count = pivot;
            pivot = left;
            left = first_left;
            right = first_right;
            // 이 과정을 재귀호출을 하여 정렬이 될 때까지 반복을 한다.
            if (left < pivot)
                sort(Quick_list, left, pivot - 1);
            if (right > pivot)
                sort(Quick_list, pivot + 1, right);
        }
        public void PrintArray(List<car> Quick_list, int q_size)
        {
            for (int i = 0; i < q_size; i++)
            {
                Console.Write((Quick_list[i].road_num + 1).ToString() + "번 도로 차량수 >>  ");
                Console.WriteLine(Quick_list[i].car_count.ToString());
            }
            Console.WriteLine("");
        }

    }

    public class car
    {
        public int car_count
        {
            get; set;
        }
        public int road_num
        {
            get; set;
        }
    }
}