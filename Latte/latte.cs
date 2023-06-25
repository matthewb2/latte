using System;
using System.IO;

using System.Text;

namespace Latte
{
    class Program
    {
        static void make_folder(string folder){
			if (Directory.Exists(folder))
            {
                Console.WriteLine("That path exists already.");
                return;
            }
			
            // Try to create the directory.
            try{
				Directory.CreateDirectory(folder);
			Console.WriteLine(folder+" created");
            } catch{
				Console.WriteLine("failed");
			}
			

		}
		
        static void Main(string[] args)
        {
            //Console.WriteLine(Environment.CurrentDirectory);
            //Console.WriteLine(Directory.GetCurrentDirectory());
            int argsCount = args.Length;

            if (argsCount < 1)
            {
                Console.WriteLine("Usage: latte.exe {projectname}");
                Console.WriteLine("   ex) latte.exe test");
                return;
            }

            string projectname = args[0];
            string bin_folder = @"C:\Program Files (x86)\Latte";

            string path = Directory.GetCurrentDirectory() + @"\" + projectname;
            //DirectoryInfo di = new DirectoryInfo(path);
            make_folder(path);
		    //src
            string src_folder = path + @"\src";
         	make_folder(src_folder);

            //oata package
			
            string package_folder = src_folder + @"\oata";
            /*
			DirectoryInfo di_package = new DirectoryInfo(package_folder);  //Create Directoryinfo value by sDirPath  

            if (di_package.Exists == false)   //If New Folder not exits  
            {
                di_package.Create();             //create Folder  
            }
			*/
			make_folder(package_folder);

            //build.xml
            System.IO.File.Copy(Path.Combine(bin_folder, "build.xml"), Path.Combine(path, "build.xml"), true);
            //set project name
            // Read a file
            string readText = File.ReadAllText(Path.Combine(path, "build.xml"));
            //Console.WriteLine(readText);
            File.WriteAllText(Path.Combine(path, "build.xml"), readText.Replace("Apple", projectname));


            // Create the file, or overwrite if the file exists.
            string filename = path + @"\.project";
            string content1 = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\r\n"
                + "<projectDescription>\r\n"
                + "	<name>"+projectname+"</name>\r\n"
                + "	<comment></comment>\r\n"
                + "	<projects>\r\n"
                + "	</projects>\r\n"
                + "	<buildSpec>\r\n"
                + "		<buildCommand>\r\n"
                + "			<name>org.eclipse.jdt.core.javabuilder</name>\r\n"
                + "			<arguments>\r\n"
                + "			</arguments>\r\n"
                + "		</buildCommand>\r\n"
                + "	</buildSpec>\r\n"
                + "	<natures>\r\n"
                + "		<nature>org.eclipse.jdt.core.javanature</nature>\r\n"
                + "	</natures>\r\n"
                + "</projectDescription>\r\n";

            using (FileStream fs = File.Create(filename))
            {
                byte[] info = new UTF8Encoding(true).GetBytes(content1);
                // Add some information to the file.
                fs.Write(info, 0, info.Length);
            }
            
            System.IO.File.Copy(Path.Combine(bin_folder, ".classpath"), Path.Combine(path, ".classpath"), true);
            
            //
            string setting = path + @"\.settings";
            make_folder(setting);
			
            // Read a file
            readText = File.ReadAllText(Path.Combine(bin_folder, "Apple.java"));
            //Console.WriteLine(readText);
            File.WriteAllText(Path.Combine(package_folder, projectname+".java"), readText.Replace("Apple", projectname));



            //
            System.IO.File.Copy(Path.Combine(bin_folder, "org.eclipse.jdt.core.prefs"), Path.Combine(setting, "org.eclipse.jdt.core.prefs"), true);
            System.IO.File.Copy(Path.Combine(bin_folder, "org.eclipse.core.resources.prefs"), Path.Combine(setting, "org.eclipse.core.resources.prefs"), true);
            
        }
        //

    }
}
