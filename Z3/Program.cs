using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.SolverFoundation.Solvers;

namespace test_mapi
{

    class Program
    {
        //Example: \/ X . X < 5 => X >= 3
        // X + 1 < X + 2
        // Any.X X < 5 => X >= 3
        //Dictionary<string, Regex> Tokens = new Dictionary<string, Regex>
        //{
        //    "Any" = 
        //};


        //    { Id , Int, Plus, Minus, Multiply, Modulo,
        //LT, LET, Equals, GT, GET, Different, And , Or, Not, Implies, Any, Exists};
        //Dictionary<int, Tokens> FoundTokens = new Dictionary<int, Tokens>();

        public class MyCspDomain : CspDomain
        {

            //
            // Summary:
            //     Return the kind of values in this Domain.
            public  ValueKind Kind { get; }
            //
            // Summary:
            //     How many distinct choices is this restriction allowing?
            public  int Count { get; }
            //
            // Summary:
            //     The first value in the restriction otherSet
            public  object FirstValue { get; }
            //
            // Summary:
            //     The last value in the restriction otherSet
            public  object LastValue { get; }

            //
            // Summary:
            //     Check if the given value is an element of the domain.
            public  bool ContainsValue(object val);
            //
            // Summary:
            //     Check if this Domain and the other Domain have identical contents.
            public  bool SetEqual(CspDomain otherDomain);
            //
            // Summary:
            //     Enumerate all values in this domain
            public  IEnumerable<object> Values();

            //
            // Summary:
            //     Valid kinds allowed in domains.
            [Flags]
            public enum ValueKind
            {
                //
                // Summary:
                //     Integer values
                Integer = 1,
                //
                // Summary:
                //     Decimal values
                Decimal = 2,
                //
                // Summary:
                //     Symbolic values
                Symbol = 4
            }
        }

        static void Main(string[] args)
        {
            ConstraintSystem s1 = ConstraintSystem.CreateSolver();

            // () and ()
            CspTerm p1 = s1.CreateBoolean("p1");
            CspTerm p2 = s1.CreateBoolean("p2");
            CspTerm p3 = s1.CreateBoolean("p3");
            CspTerm p4 = s1.CreateBoolean("p4");

            var x = new CspDomain();

            CspTerm test = s1.And(s1.Or(p1, s1.And(s1.Neg(p3)),s1.Neg(p1)), s1.And(p2, s1.Neg(s1.Difference(p1,p2))));

            CspTerm tOr12 = s1.Or(s1.Neg(t1), s1.Neg(t2));
            CspTerm tOr13 = s1.Or(s1.Neg(t1), s1.Neg(t3));
            CspTerm tOr14 = s1.Or(s1.Neg(t1), s1.Neg(t4));

            CspTerm tOr23 = s1.Or(s1.Neg(t2), s1.Neg(t3));
            CspTerm tOr24 = s1.Or(s1.Neg(t2), s1.Neg(t4));

            CspTerm tOr34 = s1.Or(s1.Neg(t3), s1.Neg(t4));

            CspTerm tOr = s1.Or(t1, t2, t3, t4);

            s1.AddConstraints(tOr12);
            s1.AddConstraints(tOr13);
            s1.AddConstraints(tOr14);
            s1.AddConstraints(tOr23);
            s1.AddConstraints(tOr24);
            s1.AddConstraints(tOr34);
            s1.AddConstraints(tOr);

            ConstraintSolverSolution solution1 = s1.Solve();

            if (solution1.HasFoundSolution)
            {
                Console.WriteLine("Is Satisfiable");
            }
            else
            {
                Console.WriteLine("Not satisfiable");
            }

            Console.ReadKey();

        }

        class TestFailedException : Exception
        {
            public TestFailedException() : base("Check FAILED") { }
        };
    }
}