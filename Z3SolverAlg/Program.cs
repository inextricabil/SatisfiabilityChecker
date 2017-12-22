/*++
Copyright (c) 2012 Microsoft Corporation

Module Name:

    Program.cs

Abstract:

    Z3 Managed API: Example program

Author:

    Christoph Wintersteiger (cwinter) 2012-03-16

Notes:

--*/

using System;
using System.Collections;
using System.Collections.Generic;

using Microsoft.Z3;
using static SatisfiabilityCheckerLibrary.Z3Solver;

namespace test_mapi
{
    class Program
    {
        
        static void Main(string[] args)
        {
            try
            {
                Global.ToggleWarningMessages(true);
                Log.Open("test.log");

                Console.Write("Z3 Major Version: ");
                Console.WriteLine(Microsoft.Z3.Version.Major.ToString());
                Console.Write("Z3 Full Version: ");
                Console.WriteLine(Microsoft.Z3.Version.ToString());
                Console.Write("Z3 Full Version String: ");
                Console.WriteLine(Microsoft.Z3.Version.FullVersion);

                
                using (Context ctx = new Context(new Dictionary<string, string>() { { "model", "true" } }))
                {
                    //Example1(ctx);
                    //Example2(ctx);
                    //Example3(ctx);
                    //Example4(ctx);
                    //Example5(ctx);
                }

                Log.Close();
                
                if (Log.isOpen())
                    Console.WriteLine("Log is still open!");
            }
            catch (Z3Exception ex)
            {
                Console.WriteLine("Z3 Managed Exception: " + ex.Message);
                Console.WriteLine("Stack trace: " + ex.StackTrace);
            }
            catch (TestFailedException ex)
            {
                Console.WriteLine("TEST CASE FAILED: " + ex.Message);
            }

            Console.ReadKey();
        }
    }
}