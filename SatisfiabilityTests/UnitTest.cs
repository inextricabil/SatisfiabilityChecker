using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Z3;

namespace SatisfiabilityTests
{
    [TestClass]
    public class UnitTests
    {
        public static Context ctx = new Context(new Dictionary<string, string>() { { "model", "true" } });

        [TestMethod]
        public void Example1()
        {
            Sort[] types = new Sort[3];
            IntExpr[] xs = new IntExpr[3];
            Symbol[] names = new Symbol[3];
            IntExpr[] vars = new IntExpr[3];


            for (uint j = 0; j < 3; j++)
            {
                types[j] = ctx.IntSort;
                names[j] = ctx.MkSymbol(String.Format("x_{0}", j));
                xs[j] = (IntExpr)ctx.MkConst(names[j], types[j]);
                vars[j] = (IntExpr)ctx.MkBound(2 - j, types[j]); // reversed
            }

            Expr body_vars = ctx.MkAnd(ctx.MkGe(ctx.MkAdd(vars[0], ctx.MkInt(1)), ctx.MkInt(2)),
                                        ctx.MkLe(ctx.MkAdd(-vars[1], ctx.MkInt(2)),
                                                       ctx.MkAdd(vars[2], ctx.MkInt(3))));

            Expr x = ctx.MkForall(types, names, body_vars, 1, null, null);
            Console.WriteLine("Formula: " + x.ToString());

            Solver s = ctx.MkSolver();
            Status q = s.Check();

            Assert.IsNotNull(q);
            Assert.AreEqual(q.ToString(), Status.SATISFIABLE.ToString());
        }


        [TestMethod]
        public void Example2()
        {
            //types
            Sort[] types = new Sort[1];
            types[0] = ctx.IntSort;

            //names
            Symbol[] names = new Symbol[1];
            names[0] = ctx.MkSymbol("x");

            IntExpr[] xs = new IntExpr[1];
            xs[0] = (IntExpr)ctx.MkConst(names[0], types[0]);


            IntExpr[] constants = new IntExpr[2];
            constants[0] = ctx.MkInt(5);
            constants[1] = ctx.MkInt(3);

            var expr = ctx.MkImplies(ctx.MkLe(xs[0], constants[0]), ctx.MkGe(xs[0], constants[1]));

            var x = ctx.MkForall(types, names, expr);
            Console.WriteLine("Formula: " + x.ToString());

            Solver s = ctx.MkSolver();
            Status q = s.Check();


            Assert.IsNotNull(q);
            Assert.AreEqual(q.ToString(), Status.SATISFIABLE.ToString());
        }


        [TestMethod]
        public void Example3()
        {
            //types
            Sort[] types = new Sort[1];
            types[0] = ctx.IntSort;

            //names
            Symbol[] names = new Symbol[1];
            names[0] = ctx.MkSymbol("x");

            IntExpr[] xs = new IntExpr[1];
            xs[0] = (IntExpr)ctx.MkConst(names[0], types[0]);


            IntExpr[] constants = new IntExpr[2];
            constants[0] = ctx.MkInt(5);
            constants[1] = ctx.MkInt(3);

            var expr = ctx.MkImplies(ctx.MkLe(xs[0], constants[0]), ctx.MkGe(xs[0], constants[1]));

            var x = ctx.MkExists(types, names, expr);
            Console.WriteLine("Formula: " + x.ToString());

            Solver s = ctx.MkSolver();
            Status q = s.Check();

            Assert.IsNotNull(q);
            Assert.AreEqual(q.ToString(), Status.SATISFIABLE.ToString());
        }


        [TestMethod]
        public void Example4()
        {
            //types
            Sort[] types = new Sort[1];
            types[0] = ctx.IntSort;

            //names
            Symbol[] names = new Symbol[2];
            names[0] = ctx.MkSymbol("x");
            names[1] = ctx.MkSymbol("y");

            IntExpr[] xs = new IntExpr[2];
            xs[0] = (IntExpr)ctx.MkConst(names[0], types[0]);
            xs[1] = (IntExpr)ctx.MkConst(names[1], types[0]);

            var expr = ctx.MkEq(xs[0], xs[1]);

            var exists = ctx.MkExists(types, new[] { names[1] }, expr);

            var any = ctx.MkForall(types, new[] { names[0] }, exists);

            Console.WriteLine("Formula: " + any.ToString());

            Solver s = ctx.MkSolver();
            Status q = s.Check();

            Assert.IsNotNull(q);
            Assert.AreEqual(q.ToString(), Status.SATISFIABLE.ToString());
        }


        [TestMethod]
        public void Example5()
        {
            //types
            Sort[] types = new Sort[1];
            types[0] = ctx.IntSort;

            //names
            Symbol[] names = new Symbol[1];
            names[0] = ctx.MkSymbol("x");

            IntExpr[] xs = new IntExpr[1];
            xs[0] = (IntExpr)ctx.MkConst(names[0], types[0]);


            IntExpr[] constants = new IntExpr[1];
            constants[0] = ctx.MkInt(2);

            var expr = ctx.MkLe(ctx.MkMod(xs[0], constants[0]), ctx.MkMul(xs[0], constants[0]));
            Console.WriteLine("Formula: " + expr.ToString());

            Solver s = ctx.MkSolver();
            Status q = s.Check();

            Assert.IsNotNull(q);
            Assert.AreEqual(q.ToString(), Status.SATISFIABLE.ToString());
        }
    }
}
