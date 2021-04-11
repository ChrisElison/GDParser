/*
    GDParser by Christopher David Elison
    Version: 1.0

    This program takes a Grim Dawn character savefile as
    an argument and prints out character info to the console.

    Uses code from Grim Dawn Item Assistant by marius00 
        - https://github.com/marius00/iagd

    Date created:  2021-04-08 19:37
    Date modified: 2021-04-09 02:03

    To Do:
        [x] Create required class files
        [x] Run test with a .gdc file
        [ ] Sort out bugs
 */

using System;

namespace GDParser {
    public class Program {
        static void Main(string[] args) {
            // Check a filename is specified as a command line argument
            if (args.Length != 1) {
                Console.WriteLine("Error: No input .gdc file specified!");
                Console.WriteLine("Usage: GDParser.exe <filename.gdc>");
            } else {
                Console.WriteLine("GDParser");
                Console.WriteLine("Attempting to read Grim Dawn savefile\n" + args[0]);
                Console.WriteLine("");

                CharacterReader testChar = new CharacterReader();
                testChar.ReadSummary(args[0]);
            }
        }
    }
}
