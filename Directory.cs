using System;
using System.Collections.Generic;
using System.IO;

namespace Thorium.IO
{
    public static class Directory
    {
        public static void CopyDirectory(string source, string destination)
        {
            var stack = new Stack<Tuple<string, string>>();
            stack.Push(new Tuple<string, string>(source, destination));

            while(stack.Count > 0)
            {
                var sourceAndDestination = stack.Pop();
                System.IO.Directory.CreateDirectory(sourceAndDestination.Item2);
                foreach(var file in System.IO.Directory.GetFiles(sourceAndDestination.Item1, "*.*", SearchOption.TopDirectoryOnly))
                {
                    File.Copy(file, Path.Combine(sourceAndDestination.Item2, Path.GetFileName(file)));
                }

                foreach(var folder in System.IO.Directory.GetDirectories(sourceAndDestination.Item1))
                {
                    stack.Push(new Tuple<string, string>(folder, Path.Combine(sourceAndDestination.Item2, Path.GetFileName(folder))));
                }
            }
        }
    }
}
