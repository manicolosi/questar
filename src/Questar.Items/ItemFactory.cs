/*******************************************************************************
 *  ItemFactory.cs: A factory that creates Item objects.
 *
 *  Copyright (C) 2007
 *  Written by Mark A. Nicolosi <mark.a.nicolosi@gmail.com>
 ******************************************************************************/

using Boo.Lang.Compiler;
using Boo.Lang.Compiler.IO;
using Boo.Lang.Compiler.Pipelines;
using System;
using System.IO;
using System.Reflection;

using Questar.Base;

namespace Questar.Items
{
    public static class ItemFactory
    {
        private const string namespce = "Questar.Content.Items.";
        private const string directory = "../boo/Questar.Content.Items";

        private static Assembly items_assembly = null;

        public static Item Create (string item_type)
        {
            if (items_assembly == null)
                Load ();

            if (item_type == null)
                throw new ArgumentNullException ("item_type must not be null.");

            Item item = items_assembly.CreateInstance (
                namespce + item_type) as Item;

            if (item == null)
                throw new ArgumentException (
                    "item_type is invalid or is not an Item.");

            return item;
        }

        public static void Load ()
        {
            BooCompiler compiler = new BooCompiler ();
            compiler.Parameters.Input.Add (new FileInput (
                directory + Path.DirectorySeparatorChar + "Potions.boo"));
            compiler.Parameters.Pipeline = new CompileToMemory ();
            compiler.Parameters.Ducky = true;

            CompilerContext context = compiler.Run ();

            items_assembly = context.GeneratedAssembly;
            if (items_assembly == null) {
                foreach (CompilerError error in context.Errors)
                    Console.WriteLine (error);

                throw new ApplicationException ("Error in a boo file.");
            }
        }
    }
}

