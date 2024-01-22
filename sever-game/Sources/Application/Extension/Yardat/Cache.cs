using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TienKiemV2Remastered.Application.Extension.Yardat
{
    public class Cache
    {
        public static List<int> YaradatsMap = new List<int> {131, 132, 133 };
        public static List<int> YardatsEntity = new List<int> { 64, 80, 81 };

        public static int TYPE_TANBINH_1 = 64;
        public static int TYPE_TANBINH_2 = 80;
        public static int TYPE_TANBINH_3 = 81;
        public static int TYPE_TANBINH_4 = 45;
        public static List<List<List<int>>> Posistion = new List<List<List<int>>>()
        {
            new List<List<int>>(){
            new List<int>() {127, 456},
            new List<int>() {345, 456},
            new List<int>() {529, 360},
            new List<int>() {713, 456},
            new List<int>() {1099, 456},
            new List<int>() {1299, 456},
            },

            new List<List<int>>(){
            new List<int>() {157, 456},
            new List<int>() {352, 456},
            new List<int>() {519, 360},
            new List<int>() {639, 432},
            new List<int>() {799, 432},
            new List<int>() {937, 312},
            new List<int>() {1107, 456},
            },

              new List<List<int>>(){
            new List<int>() {271, 456},
            new List<int>() {443, 456},
            new List<int>() {635, 456},
            new List<int>() {823, 456},
            new List<int>() {1003, 456},
            new List<int>() {1195, 456},
            },
        };
    }
}
