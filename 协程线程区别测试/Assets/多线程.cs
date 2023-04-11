


using System;
using System.Threading;

public class 线程 {
    // private static object lockObject = new object();
    private static int n = 5;
    public  void Main() {
        for (int i = 0; i < 10; i++) {
            new Thread(ThreadMain).Start();
        }
    }
     public static void ThreadMain() {
         // lock (lockObject) {
             if (n==5) {
                 n++;
                 Console.WriteLine("n="+n);
             }
             n = 5;
         // }
        
     }
}