// (c)2025 RedUp152.All rights reserved.

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Sys = Cosmos.System;

namespace PiOS
{
    public class Kernel : Sys.Kernel
    {
        const String OS = "PiOS v. 08.25 - 1";
        Sys.FileSystem.CosmosVFS FileSystem;
        String Directory = @"0:\";
        bool FileManagerIsOpen = false;
        const String Username = "User";
        Random Randomizer = new Random();
        Byte RecursiveCounter;
        Dictionary<string, int> Notes = new Dictionary<string, int>
        {
            {"C",131}, {"D",147}, {"E",165}, {"F",175}, {"G",196}, {"A",220}, { "H",247},
            {"C1",262}, {"D1",294}, {"E1",330}, {"F1",349}, {"G1",392}, {"A1",440}, { "H1",494},
            {"C2",523}, {"D2",587}, {"E2",659}, {"F2",698}, {"G2",784}, {"A2",880}, {"H2",988},
            {"C3",1047}, {"D3",1175}, {"E3",1319}, {"F3",1398}, {"G3",1568}, {"A3",1760}, {"H3",1976}
        };
        protected override void BeforeRun()
        {
            try{
                for (byte Counter = 1; Counter <= 4; Counter++)
                {
                    Console.Clear();
                    Console.WriteLine("\n" + @"
                        ____     __      ____     _____
                       |  __ \  |__|   / ____\   / ____|
                       | |__) |  __   | |   | |  | (___
                       |  ___/  |  |  | |   | |  \___ \  
                       |  |     |  |  | |___| |   ___) | 
                       |__|     |__|   \ ___ /   |____/  " + "\n\n\n\n\n\n\n\n");
                    Console.WriteLine("                              " + OS);
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write("                             "+ "||||");
                    for (byte Counter2 = 1; Counter2 <= Counter; Counter2++) { Console.Write("||||"); }
                    Console.ForegroundColor = ConsoleColor.White;
                    switch (Counter)
                    {
                        case 1: Console.WriteLine("\n\n\n\n\n\nOS Integrity Check..."); break;
                        case 2: Console.WriteLine("\n\n\n\n\n\nPreparing the file system..."); 
                            FileSystem = new Sys.FileSystem.CosmosVFS();
                            Sys.FileSystem.VFS.VFSManager.RegisterVFS(FileSystem); break;
                        case 3: Console.WriteLine("\n\n\n\n\n\nChecking system files...");
                            try { if (GetFileInfo(@"0:\Kudzu.txt") != null) {GetFileInfo(@"0:\Kudzu.txt").Delete(); }} catch { }
                            try { if (GetFileInfo(@"0:\Root.txt") != null) {GetFileInfo(@"0:\Root.txt").Delete(); }} catch { }
                            try { if (GetDirectoryInfo(@"0:\TEST") != null) { GetDirectoryInfo(@"0:\TEST").Delete(true); }} catch { }
                            try { if (GetDirectoryInfo(@"0:\Dir Testing") != null) { GetDirectoryInfo(@"0:\Dir Testing").Delete(true); }} catch { }
                            break;
                        case 4: Console.WriteLine("\n\n\n\n\n\nOS launch..."); break;
                    }
                    Thread.Sleep(Randomizer.Next(350, 1500));
                }

                Thread.Sleep(Randomizer.Next(1500, 3000));
                Console.Clear();
                Console.WriteLine(OS + "\n(c)2025 RedUp152. All rights reserved.");
                Console.WriteLine("\nWelcome! Type \"help\" to see the command list.");
            }

            catch (Exception Error){
                BlueScreen(Error, "An error occurred while preparing the operating system.");
            }
        }

        protected override void Run()
        {
            try{
                Console.ForegroundColor = ConsoleColor.DarkGreen;

                if (FileManagerIsOpen == true){
                    Console.Write(Directory + " >>> ");
                    string input = Console.ReadLine().ToLower();
                    OSFileManager(input);
                }
                else{
                    Console.Write(@"PiOS\" + Username + " >>> ");
                    string input = Console.ReadLine().ToLower();
                    Commands(input);
                }
                
            }
            catch (Exception Error){BlueScreen(Error, "An error occurred while the operating system was running.");}
        }
        //IMPORTANT: TESTING OS!!!
        //TODO: test everything!

        void Commands(String input)
        {
            try{
                switch (input) {
                    default:
                        if (input == "?") { goto case "help";}
                        else if (input == "cls") { goto case "clear"; }
                        else if (input == "fm") { goto case "filemanager"; }
                        else { Error("Command \"" + input + "\" not found."); }
                        break;
                    /*case "":

                        break;*/

                    case "help":
                        Console.WriteLine(@"help - shows the list of commands,
shutdown - turns off the device,
restart - restarts the device,
echo - repeats what you wrote,
clear - clears everything written,
sysinfo - shows information about the device,
datetime - shows the date and time,
filemanager - opens the file manager,
filemanagerhelp - shows the list of file manager commands,
random - shows a random number,
randomrange - shows a random number in the specified range,
hearmusic - allows you to listen to music files,
musichelp - information about writing music files.");
                        break;

                    case "filemanagerhelp":
                        Console.WriteLine(@"exit - exiting the file manager,
home - selects the directory ""0:\""
newfile - create a new file,
delfile - delete a file,
newdirectory - create a new directory,
deldirectory - delete a directory,
cd - select a directory,
dir - view all objects in the selected directory,
movedirectory - moves the selected directory,
copydirectory - copies the selected directory,
movefile - moves the selected file,
copyfile - copies the selected file,
writetofile - writes text to a file,
appendtofile - adds text to what is written in the file,
readfile - outputs the text written to the file.");
                        break;

                    case "musichelp":
                        Console.WriteLine(@"Music format: note, length; note, length; etc.  
Use 'P' for a pause (instead of a note).  
Length is specified in milliseconds.  
Save this to a file for playback.  ");
                        break;

                    case "shutdown":
                        Cosmos.System.Power.Shutdown();
                        break;

                    case "restart":
                        Cosmos.System.Power.Reboot();
                        break;

                    case "echo":
                        Console.WriteLine("Enter your text");
                        input = Console.ReadLine();
                        Console.WriteLine(input);
                        break;

                    case "clear":
                        Console.Clear();
                        break;

                    case "sysinfo":
                        Console.WriteLine("Processor: " + Cosmos.Core.CPU.GetCPUBrandString() + ".");
                        Console.WriteLine("Processor vendor: " + Cosmos.Core.CPU.GetCPUVendorName()  + ".");
                        Console.WriteLine("RAM: " + ((double)Cosmos.Core.CPU.GetAmountOfRAM()).ToString() + " MB,");
                        Console.WriteLine("Available RAM: " + ((double)Cosmos.Core.GCImplementation.GetAvailableRAM()).ToString() + "MB,");
                        Console.WriteLine("Total memory: " + ((double)DriveInfo.GetDrives()[0].TotalSize / 1048576).ToString() + "MB,");
                        Console.WriteLine("Available disk space: " + ((double)DriveInfo.GetDrives()[0].AvailableFreeSpace / 1048576).ToString() + "MB.");
                        break;

                    case "datetime":
                        Console.WriteLine(DateTime.Now);
                        break;

                    case "filemanager":
                        FileManagerIsOpen = true;
                        Console.WriteLine("Write \"exit\" to exit the file manager.");
                        break;

                    case "random":
                        Console.WriteLine(Randomizer.Next());
                        break;

                    case "randomrange":
                        Console.WriteLine("Enter the minimum value");
                        string Minimum = Console.ReadLine();
                        Console.WriteLine("Enter the maximum value");
                        string Maximum = Console.ReadLine();
                        try
                        {
                            Console.WriteLine(Randomizer.Next(minValue: Convert.ToInt32(Minimum), maxValue: Convert.ToInt32(Maximum)));
                        }
                        catch
                        {
                            Error("The specified values could not be processed. Verify their correctness and ensure no extra characters are present.");
                        }
                        break;

                    case "hearmusic":
                        Console.WriteLine("Enter the file name");
                        string MusicFileHear = Console.ReadLine();
                        FileInfo MusicFileHearInfo = GetFileInfo(MusicFileHear);
                        if (MusicFileHearInfo == null) { Error("The \"" + MusicFileHear + "\" file was not found"); break; }
                        try{
                            String[] FileHearData = File.ReadAllText(MusicFileHearInfo.FullName).Replace(" ","").ToUpper().Split(";");
                            foreach (String InputNotes in FileHearData){
                                String[] Note = InputNotes.Split(",");
                                if (Note.Length == 2){
                                    if (Note[0] == "P") 
                                    { 
                                       Thread.Sleep( System.Convert.ToInt32(Note[1]) ); 
                                    }
                                    else{
                                        try{
                                            Console.Beep( Notes[Note[0]] , System.Convert.ToInt32(Note[1]) );
                                        }
                                        catch{
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine("Note \""+ Note[0] +"\" (length: \"" + Note[1] + "\" ms) could not be processed.");
                                        }
                                    }
                                }
                                else{
                                    Error("Couldn't process the file.");
                                }
                            }
                        }
                        catch { Error("Couldn't process the file."); }
                        break;
                }
            }
            catch (Exception Error){BlueScreen(Error, "An error occurred while the operating system was running.");}
        }
        void OSFileManager(String input) {
            try
            {
                switch (input)
                {
                    default:
                        if (input == "shutdown") { goto case "exit"; }
                        else if (input == "newdir") { goto case "newdirectory"; }
                        else if (input == "deldir") { goto case "deldirectory"; }
                        else if (input == "copydir") { goto case "copydirectory"; }
                        else if (input == "movedir") { goto case "movedirectory"; }
                        else { Error("The \"" + input + "\" command was not found."); }
                        break;

                    case "exit":
                        FileManagerIsOpen = false;
                        break;

                    case "home":
                        Directory = @"0:\";
                        break;

                    case "newfile":
                        Console.WriteLine("Enter the file name");
                        input = Console.ReadLine();
                        if (input.Contains("Root.txt") || input.Contains("Kudzu.txt")) { Error("Access is denied"); break; }
                        if (GetFileInfo(Directory + input) != null) { Error("A file with the same name already exists."); break; }
                        if (input.Contains(".") == false) { Error("The file does not have an extension."); break; }
                        FileSystem.CreateFile(Directory + input);
                        break;

                    case "delfile":
                        Console.WriteLine("Enter the file name");
                        input = Console.ReadLine();
                        FileInfo FileToDelete = GetFileInfo(input);
                        if (FileToDelete == null) { Error("File \"" + input + "\" was not found."); break; }
                        FileToDelete.Delete();
                        break;

                    case "newdirectory":
                        Console.WriteLine("Enter the directory name");
                        input = Console.ReadLine();
                        if (input.Contains("TEST") || input.Contains("Dir Testing")) { Error("Access is denied"); break; }
                        if (GetFileInfo(Directory + @"\" + input) != null) { Error("A directory with the same name already exists."); break; }
                        if (Directory == @"0:\") { FileSystem.CreateDirectory(Directory + input); }
                        else { FileSystem.CreateDirectory(Directory + @"\" + input); }
                        break;

                    case "deldirectory":
                        Console.WriteLine("Enter the directory name");
                        input = Console.ReadLine();
                        DirectoryInfo DirectoryToDelete = GetDirectoryInfo(input);
                        if (DirectoryToDelete == null) { Error("The \"" + input + "\" directory was not found."); break; }
                        if (DirectoryToDelete.FullName == @"0:\") { Error("Access is denied"); break; }
                        DirectoryToDelete.Delete(true);
                        break;

                    case "cd":
                        Console.WriteLine("Enter the full name of the directory");
                        input = Console.ReadLine().Trim();
                        DirectoryInfo CdDir = new DirectoryInfo(input);
                        if (CdDir.Exists) { Directory = input; } else { Error("The \"" + input + "\" directory was not found."); }
                        break;

                    case "copyfile":
                        Console.WriteLine("Enter the file name");
                        string FileCopy = Console.ReadLine();
                        FileInfo FileCopyInfo = GetFileInfo(FileCopy);
                        if (FileCopyInfo == null) { Error("The \"" + FileCopy + "\" file was not found."); break; }
                        Console.WriteLine("Enter the new file location");
                        string NewPatchFileCopy = Console.ReadLine();
                        DirectoryInfo NewPatchFileCopyInfo = GetDirectoryInfo(NewPatchFileCopy);
                        if (NewPatchFileCopyInfo == null) { Error("The \"" + NewPatchFileCopy + "\" directory was not found."); break; }

                        if (NewPatchFileCopyInfo.FullName.EndsWith(@"\")) { if (GetFileInfo(NewPatchFileCopyInfo.FullName + FileCopyInfo.Name) != null) { Error("A file with the same name already exists on this path."); break; } }
                        else { if (GetFileInfo(NewPatchFileCopyInfo.FullName + @"\" + FileCopyInfo.Name) != null) { Error("A file with the same name already exists on this path."); break; } }
                        File.Copy(FileCopyInfo.FullName, NewPatchFileCopyInfo.FullName);
                        break;

                    case "movefile":
                        Console.WriteLine("Enter the file name");
                        string FileMove = Console.ReadLine();
                        FileInfo FileMoveInfo = GetFileInfo(FileMove);
                        if (FileMoveInfo == null) { Error("The \"" + FileMove + "\" file was not found."); break; }
                        Console.WriteLine("Enter the new file location");
                        string NewPatchFileMove = Console.ReadLine();
                        DirectoryInfo NewPatchFileMoveInfo = GetDirectoryInfo(NewPatchFileMove);
                        if (NewPatchFileMoveInfo == null) { Error("The \"" + NewPatchFileMove + "\" directory was not found."); break; }
                        if (GetFileInfo(NewPatchFileMoveInfo.FullName + @"\" + FileMoveInfo.Name) != null) { Error("A file with the same name already exists on this path."); break; }
                        Console.WriteLine(FileMoveInfo.FullName);
                        Console.WriteLine(NewPatchFileMoveInfo.FullName);
                        File.Copy(FileMoveInfo.FullName, NewPatchFileMoveInfo.FullName);
                        File.Delete(FileMoveInfo.FullName);
                        break;

                    case "copydirectory":
                        Console.WriteLine("Enter the directory name");
                        string DirectoryCopy = Console.ReadLine();
                        if (DirectoryCopy == @"0:\") { Error("Access denied."); break; }
                        DirectoryInfo DirectoryCopyInfo = GetDirectoryInfo(DirectoryCopy);
                        if (DirectoryCopyInfo == null) { Error("The \"" + DirectoryCopy + "\" directory was not found."); break; }
                        Console.WriteLine("Enter the new directory location");
                        string NewPatchDirectoryCopy = Console.ReadLine();
                        DirectoryInfo NewPatchDirectoryCopyInfo = GetDirectoryInfo(NewPatchDirectoryCopy);
                        if (NewPatchDirectoryCopyInfo == null) { Error("The \"" + NewPatchDirectoryCopy + "\" directory was not found."); break; }
                        if (GetDirectoryInfo(NewPatchDirectoryCopyInfo.FullName + @"\" + DirectoryCopyInfo.Name) != null) { Error("A directory with the same name already exists on this path."); break; }
                        CopyDirectoryErrorHandling(DirectoryCopyInfo, NewPatchDirectoryCopyInfo.FullName);
                        break;

                    case "movedirectory":
                        Console.WriteLine("Enter the directory name");
                        string DirectoryMove = Console.ReadLine();
                        if (DirectoryMove == @"0:\") { Error("Access denied."); break; }
                        DirectoryInfo DirectoryMoveInfo = GetDirectoryInfo(DirectoryMove);
                        if (DirectoryMoveInfo == null) { Error("The \"" + DirectoryMove + "\" directory was not found."); break; }
                        Console.WriteLine("Enter the new directory location");
                        string NewPatchDirectoryMove = Console.ReadLine();
                        DirectoryInfo NewPatchDirectoryMoveInfo = GetDirectoryInfo(NewPatchDirectoryMove);
                        if (NewPatchDirectoryMoveInfo == null) { Error("The \"" + NewPatchDirectoryMove + "\" directory was not found."); break; }
                        if (GetDirectoryInfo(NewPatchDirectoryMoveInfo.FullName + @"\" + DirectoryMoveInfo.Name) != null) { Error("A directory with the same name already exists on this path."); break; }
                        CopyDirectoryErrorHandling(DirectoryMoveInfo, NewPatchDirectoryMoveInfo.FullName);
                        if (Directory == DirectoryMoveInfo.FullName) { Directory = @"0:\"; }
                        DirectoryMoveInfo.Delete(true);
                        break;

                    case "writetofile":
                        WriteToFile("WriteAllText");
                        break;

                    case "appendtofile":
                        WriteToFile("Append");
                        break;

                    case "readfile":
                        Console.WriteLine("Enter the file name");
                        string FileRead = Console.ReadLine();
                        FileInfo FileReadInfo = GetFileInfo(FileRead);
                        if (FileReadInfo == null) { Error("The \"" + FileRead + "\" file was not found."); break; }
                        try { Console.WriteLine(File.ReadAllText(FileReadInfo.FullName)); }
                        catch { Error("Couldn't read the file."); }
                        break;

                    case "dir":
                        var DirectoryList = Sys.FileSystem.VFS.VFSManager.GetDirectoryListing(Directory);
                        foreach (var DirectoryEntry in DirectoryList)
                        {
                            try
                            {
                                var EntryType = DirectoryEntry.mEntryType;
                                if (EntryType == Sys.FileSystem.Listing.DirectoryEntryTypeEnum.File)
                                {
                                    Console.WriteLine("<File>           " + DirectoryEntry.mName);
                                }
                                if (EntryType == Sys.FileSystem.Listing.DirectoryEntryTypeEnum.Directory)
                                {
                                    Console.WriteLine("<Directory>          " + DirectoryEntry.mName);
                                }
                            }
                            catch { Error("An unexpected error has occurred. Please try again later. Also, ensure the directory is specified correctly."); }
                        }
                        break;
                }
            }
            catch (Exception Error) { BlueScreen(Error, "An error occurred while working with the file system."); }
        }

        private FileInfo GetFileInfo(string Name){
            try
            {
                FileInfo Info;
                if (Name.StartsWith(@"0:\")) { Info = new FileInfo(Name); }
                else
                {
                    if (Directory.EndsWith(@"\")) { Info = new FileInfo(Directory+ Name); }
                    else if (Directory.EndsWith(@"\") == false) { Info = new FileInfo(Directory+ @"\"+ Name); }
                    else { Info = new FileInfo(Directory+ Name); }
                }
                if (Info.Exists) {  return Info; }
                else if (Info.Exists == false) {  return null; }
                else { return null; }
            }
            catch (Exception Error) { BlueScreen(Error, "An error occurred during file processing."); return null; }
        }
        private DirectoryInfo GetDirectoryInfo(string Name) {
            try
            {
                DirectoryInfo DirInfo;
                if (Name == @"0:\") { return new DirectoryInfo(@"0:\"); }

                if (Name.StartsWith(@"0:\")){ DirInfo = new DirectoryInfo(Name); }
                else {
                    if (Directory.EndsWith(@"\")) { DirInfo = new DirectoryInfo(Directory+ Name); }
                    else { DirInfo = new DirectoryInfo(Directory + @"\" + Name); }
                }
                if (DirInfo.Exists) { return DirInfo; }
                else { return null; }
            }
            catch (Exception Error) { BlueScreen(Error, "An error occurred during directory processing."); return null; }
        }

        void CopyDirectoryErrorHandling(DirectoryInfo DirectoryToCopy, String NewPatch){
            try{
                RecursiveCounter = 0;
                CopyDirectory(DirectoryToCopy, NewPatch);
                
            }
            catch(Exception CopyingError){
                if (CopyingError.Message == "StopCopyingRecursion") { Error("Error copying/moving a directory: too many subdirectories."); }
                else { BlueScreen(CopyingError, "An error occurred during directory copying/moving."); }
            }
        }

        void CopyDirectory(DirectoryInfo DirectoryToCopy, String NewPatch)
        {
            RecursiveCounter++;
            if (RecursiveCounter > 100) { throw new Exception("StopCopyingRecursion"); }

            string DirectoryToCopyNewPath = NewPatch + @"\" + DirectoryToCopy.Name;
            DirectoryInfo[] SubDirectories = DirectoryToCopy.GetDirectories();
            FileSystem.CreateDirectory(DirectoryToCopyNewPath);
            foreach (FileInfo File in DirectoryToCopy.GetFiles())
            {
                string NewFilePath = DirectoryToCopyNewPath + @"\" + File.Name;
                File.CopyTo(NewFilePath);
            }
            if (SubDirectories.Length > 0)
            {
                foreach (DirectoryInfo SubDirectory in SubDirectories)
                {
                    string newDestinationDir = DirectoryToCopyNewPath + @"\" + SubDirectory.Name;
                    CopyDirectory(SubDirectory, newDestinationDir);
                }
            }
        }
        void WriteToFile(string Mode) {
            try{
                Console.WriteLine("Enter the file name");
                string FileToWrite = Console.ReadLine();
                FileInfo FileToWriteInfo = GetFileInfo(FileToWrite);
                if (FileToWriteInfo == null) { Error("The \"" + FileToWrite + "\" file was not found."); return; ; }
                Console.WriteLine("Enter the text. To wrap a line, type \"\\n\". To save the text to the file, press \"Enter\".");
                string InputText = Console.ReadLine();
                if (Mode == "WriteAllText") { File.WriteAllText(FileToWriteInfo.FullName, InputText.Replace("\\n", "\n")); }
                if (Mode == "Append") { File.AppendAllText(FileToWriteInfo.FullName, InputText.Replace("\\n", "\n")); }
        }
            catch (Exception Error) {
                BlueScreen(Error, "An error occurred while writing the file.");
    }
}
        
        static void Error(String Error) {
            try
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Beep();
                Console.WriteLine(Error);
            }
            catch(Exception VoidError)
            {
                BlueScreen(VoidError, "An error occurred during error handling.");
            }
            
        }

        static void BlueScreen(Exception Error, string Info) {
            try{
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("A critical error occurred during system operation. Details:");
                Console.WriteLine("OS:" + OS);
                Console.WriteLine("Processor: " + Cosmos.Core.CPU.GetCPUBrandString());
                Console.WriteLine("RAM: " + Cosmos.Core.CPU.GetAmountOfRAM().ToString() + " MB");
                Console.WriteLine("Error: " + Error);
                Console.WriteLine("Information about the error: " + Info);
                if(Error.Message == @"Path part '0:' not found!") { Console.WriteLine("Additional information: The disk must be formatted."); }
                Console.WriteLine("\nPress any key to restart your computer. Please do not turn off the device.");
                Console.ReadKey();
                Cosmos.System.Power.Reboot();
            }
            catch{
                YellowScreen();
            }
        }
        
        static void YellowScreen()
        {
            try{
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("An error occurred during critical error handling.");
                Console.WriteLine("Press any key to restart your computer. Please do not turn off the device.");
                Console.ReadKey();
                Cosmos.System.Power.Reboot();
            }
            catch{
                Cosmos.System.Power.Reboot();
            }
        }
    }
}
