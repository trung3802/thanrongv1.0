using System;

namespace TienKiemV2Remastered.Application.Helper
{
    public class MyRandom
    {
        public Random R;

        public MyRandom()
        {
            R = new Random();
        }

        public int NextInt()
        {
            return R.Next();
        }

        public int NextInt(int a)
        {
            return R.Next(a);
        }

        public int NextInt(int a, int b)
        {
            return R.Next(a, b);
        }
    }
}