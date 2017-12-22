//using System;
//using System.Collections;
//using System.Collections.Generic;
//using Microsoft.Z3;

//namespace test_mapi
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            try
//            {

//                Console.Write("Z3 Major Version: ");
//                Console.WriteLine(Microsoft.Z3.Version.Major.ToString());
//                Console.Write("Z3 Full Version: ");
//                Console.WriteLine(Microsoft.Z3.Version.ToString());
//                Console.Write("Z3 Full Version String: ");
//                Console.WriteLine(Microsoft.Z3.Version.FullVersion);


//                // These examples need model generation turned on.
//                using (Context ctx = new Context(new Dictionary<string, string>() { { "model", "true" } }))
//                {
//                    //QuantifierExample1(ctx);
//                    //QuantifierExample2(ctx);
//                    //LogicExample(ctx);
//                    //ParOrExample(ctx);
//                    ParserExample1(ctx);
//                    //ParserExample2(ctx);
//                    //ParserExample5(ctx);
//                }

//                // These examples need proof generation turned on.
//                //using (Context ctx = new Context(new Dictionary<string, string>() { { "proof", "true" } }))
//                //{
//                //    ProveExample1(ctx);
//                //    ProveExample2(ctx);
//                //    ParserExample3(ctx);
//                //    UnsatCoreAndProofExample(ctx);
//                //    UnsatCoreAndProofExample2(ctx);
//                //}

//                // These examples need proof generation turned on and auto-config set to false.
//                //using (Context ctx = new Context(new Dictionary<string, string>()
//                //    { {"proof", "true" },
//                //      {"auto-config", "false" } }))
//                //{
//                //    QuantifierExample3(ctx);
//                //    QuantifierExample4(ctx);
//                //}

//                //TranslationExample();

//                Log.Close();
//                if (Log.isOpen())
//                    Console.WriteLine("Log is still open!");
//            }
//            catch (Z3Exception ex)
//            {
//                Console.WriteLine("Z3 Managed Exception: " + ex.Message);
//                Console.WriteLine("Stack trace: " + ex.StackTrace);
//            }
//            catch (TestFailedException ex)
//            {
//                Console.WriteLine("TEST CASE FAILED: " + ex.Message);
//            }
//        }

//        public static void ParserExample1(Context ctx)
//        {
//            Console.WriteLine("ParserExample1");

//            var fml = ctx.ParseSMTLIB2String("(declare-const x Int) (declare-const y Int) (assert (> x y)) (assert (> x 0))");
//            Console.WriteLine("formula {0}", fml);

//            Model m = Check(ctx, fml, Status.SATISFIABLE);
//        }

//        static Model Check(Context ctx, BoolExpr f, Status sat)
//        {
//            Solver s = ctx.MkSolver();
//            s.Assert(f);
//            if (s.Check() != sat)
//                throw new TestFailedException();
//            if (sat == Status.SATISFIABLE)
//                return s.Model;
//            else
//                return null;
//        }

//        class TestFailedException : Exception
//        {
//            public TestFailedException() : base("Check FAILED") { }
//        };
//    }
//}