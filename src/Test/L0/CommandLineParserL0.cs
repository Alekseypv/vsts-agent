using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.VisualStudio.Services.Agent;
using Xunit;

namespace Microsoft.VisualStudio.Services.Agent.Tests
{
    public sealed class CommandLineParserL0
    {
        [Fact]
        [Trait("Level", "L0")]
        [Trait("Category", "Common")]
        public void CanConstruct()
        {
            using (TestHostContext thc = new TestHostContext("CommandLineParserL0", "CanConstruct"))
            {
                TraceSource trace = thc.GetTrace();

                CommandLineParser clp = new CommandLineParser(thc);
                trace.Info("Constructed");

                Assert.NotNull(clp); 
            }
        }

        [Fact]
        [Trait("Level", "L0")]
        [Trait("Category", "Common")]
        public void ParsesCommands()
        {
            using (TestHostContext thc = new TestHostContext("CommandLineParserL0", "ParsesCommands"))
            {
                TraceSource trace = thc.GetTrace();

                CommandLineParser clp = new CommandLineParser(thc);
                trace.Info("Constructed.");

                clp.Parse(new string[]{"cmd1", "cmd2", "--arg1", "arg1val", "badcmd"});
                trace.Info("Parsed");

                trace.Info("Commands: {0}", clp.Commands.Count);
                Assert.True(clp.Commands.Count == 2);                
            }
        }

        [Fact]
        [Trait("Level", "L0")]
        [Trait("Category", "Common")]
        public void ParsesArgs()
        {
            using (TestHostContext thc = new TestHostContext("CommandLineParserL0", "ParsesArgs"))
            {
                TraceSource trace = thc.GetTrace();

                CommandLineParser clp = new CommandLineParser(thc);
                trace.Info("Constructed.");

                clp.Parse(new string[]{"cmd1", "--arg1", "arg1val", "--arg2", "arg2val"});
                trace.Info("Parsed");

                trace.Info("Args: {0}", clp.Args.Count);
                Assert.True(clp.Args.Count == 2);
                Assert.True(clp.Args.ContainsKey("arg1"));
                Assert.Equal(clp.Args["arg1"], "arg1val");
                Assert.True(clp.Args.ContainsKey("arg2"));
                Assert.Equal(clp.Args["arg2"], "arg2val");                                
            }
        }

        [Fact]
        [Trait("Level", "L0")]
        [Trait("Category", "Common")]
        public void ParsesFlags()
        {
            using (TestHostContext thc = new TestHostContext("CommandLineParserL0", "ParsesFlags"))
            {
                TraceSource trace = thc.GetTrace();

                CommandLineParser clp = new CommandLineParser(thc);
                trace.Info("Constructed.");

                clp.Parse(new string[]{"cmd1", "--flag1", "--arg1", "arg1val", "--flag2"});
                trace.Info("Parsed");

                trace.Info("Args: {0}", clp.Flags.Count);
                Assert.True(clp.Flags.Count == 2);
                Assert.True(clp.Flags.Contains("flag1"));
                Assert.True(clp.Flags.Contains("flag2"));         
            }
        }        
    }
}