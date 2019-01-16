using System;
using System.Threading.Tasks;

namespace FlexValidator {
    /// <summary>
    /// Simple helper class to expand or group variables
    /// </summary>
    internal class Helper {
        public static object[] Pack(params object[] objs) {
            return objs;
        }

        public static void UnPack<T>(Action<T> method, object[] objs) {
            method((T)objs[0]);
        }

        public static void UnPack<T1, T2>(Action<T1, T2> method, object[] objs) {
            method((T1)objs[0], (T2)objs[1]);
        }

        public static void UnPack<T1, T2, T3>(Action<T1, T2, T3> method, object[] objs) {
            method((T1)objs[0], (T2)objs[1], (T3)objs[2]);
        }

        public static void UnPack<T1, T2, T3, T4>(Action<T1, T2, T3, T4> method, object[] objs) {
            method((T1)objs[0], (T2)objs[1], (T3)objs[2], (T4)objs[3]);
        }

        public static Task UnPackAsync<T>(Func<T, Task> method, object[] objs) {
            return method((T)objs[0]);
        }

        public static Task UnPackAsync<T1, T2>(Func<T1, T2, Task> method, object[] objs) {
            return method((T1)objs[0], (T2)objs[1]);
        }

        public static Task UnPackAsync<T1, T2, T3>(Func<T1, T2, T3, Task> method, object[] objs) {
            return method((T1)objs[0], (T2)objs[1], (T3)objs[2]);
        }

        public static Task UnPackAsync<T1, T2, T3, T4>(Func<T1, T2, T3, T4, Task> method, object[] objs) {
            return method((T1)objs[0], (T2)objs[1], (T3)objs[2], (T4)objs[3]);
        }

        public static Action<object[]> Convert<T>(Action<T> action) {
            return x => action((T)x[0]);
        }

        public static Action<object[]> Convert<T1, T2>(Action<T1, T2> action) {
            return x => UnPack(action, x);
        }

        public static Action<object[]> Convert<T1, T2, T3>(Action<T1, T2, T3> action) {
            return x => UnPack(action, x);
        }

        public static Action<object[]> Convert<T1, T2, T3, T4>(Action<T1, T2, T3, T4> action) {
            return x => UnPack(action, x);
        }

        public static Func<object[], Task> Convert<T>(Func<T, Task> action) {
            return x => UnPackAsync(action, x);
        }

        public static Func<object[], Task> Convert<T1, T2>(Func<T1, T2, Task> action) {
            return x => UnPackAsync(action, x);
        }

        public static Func<object[], Task> Convert<T1, T2, T3>(Func<T1, T2, T3, Task> action) {
            return x => UnPackAsync(action, x);
        }

        public static Func<object[], Task> Convert<T1, T2, T3, T4>(Func<T1, T2, T3, T4, Task> action) {
            return x => UnPackAsync(action, x);
        }
    }
}