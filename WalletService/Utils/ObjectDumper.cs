using System;
using System.Reflection;
namespace WalletService.Utils
{
    public static class ObjectDumper
    {
        public static void Dump(object obj)
        {
            Dump(obj, 0);
        }

        private static void Dump(object obj, int indent)
        {
            if (obj == null)
            {
                Write(indent, "null");
                return;
            }

            Type type = obj.GetType();

            if (type.IsPrimitive || type == typeof(string))
            {
                Write(indent, obj.ToString());
                return;
            }

            PropertyInfo[] properties = type.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                object value = property.GetValue(obj);
                Write(indent, $"{property.Name}:");
                Dump(value, indent + 1);
            }
        }

        private static void Write(int indent, string message)
        {
            Console.WriteLine($"{new string(' ', indent * 2)}{message}");
        }
    }

}