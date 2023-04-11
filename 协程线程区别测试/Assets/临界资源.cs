
using UnityEngine;

public static class  临界资源 {
        public static int n = 5;
        public static void ThreadMain() {
                if (n==5) {
                        n++;
                        Debug.Log("n="+n);
                }
                n = 5;
        }
}
