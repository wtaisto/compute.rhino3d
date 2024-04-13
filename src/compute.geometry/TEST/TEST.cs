using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Rhino;

namespace TEST
{
    public static class TEST
    {
        public const string basePath = @"C:\Projects\compute.rhino3d\src\compute.geometry\TEST";

        public static void Initialize()
        {
            DirectoryInfo directory= new DirectoryInfo(basePath);
            if (!directory.Exists)
            {
                throw new Exception("Path " + basePath + " does not exist.");
            }

            foreach(FileInfo file in directory.GetFiles("*.txt"))
            {
                file.Delete();
            }
        }

        public static async Task LoadGLB()
        {
            var fileName = Path.Combine(basePath, "Duck.glb");
            LoadFile(fileName);
        }


        public static async Task Load3DM()
        {
            var fileName = Path.Combine(basePath, "TestCube.3dm");
            LoadFile(fileName);
        }

        public static async Task LoadFBX()
        {
            var fileName = Path.Combine(basePath, "TestCube.fbx");
            LoadFile(fileName);
        }

        private static void LoadFile(string fileName)
        {
            using (var rhinoDoc = RhinoDoc.CreateHeadless(null))
            {
                if (!rhinoDoc.Import(fileName))
                {
                    File.WriteAllText(fileName + " FAIL.txt", $"{DateTime.Now}: Failed to import file");
                    throw new InvalidOperationException("Failed to import file " + fileName);
                }
            }
            File.WriteAllText(fileName + " SUCCESS.txt", $"{DateTime.Now}: Success!!!");
        }
    }
}
