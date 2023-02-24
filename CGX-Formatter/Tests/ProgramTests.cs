using FluentAssertions;
using Xunit;

namespace Codingame;

public class ProgramTests
{
    [Fact]
    public void Example1()
    {
        Solution.Solve(new string[] {
            "( 'user'= (",
            "'key'='1= t(c)(';",
            "'valid'=false",
            ");",
            "'user'= (",
            "   'key'=' = ; '; ",
            "   'valid'= true",
            "); ()",
            ")"
        }).Should().BeEquivalentTo(
            new string[] {
        "(",
        "    'user'=",
        "    (",
        "        'key'='1= t(c)(';",
        "        'valid'=false",
        "    );",
        "    'user'=",
        "    (",
        "        'key'=' = ; ';",
        "        'valid'=true",
        "    );",
        "    (",
        "    )",
        ")"});
    }

    [Fact]
    public void Example2()
    {
        Solution.Solve(new string[] {
            "( 'user'= (",
            "'key'='1= t(c)(';",
            "'valid'=false",
            ");",
            "'user'= (",
            "   'key'=' = ; '; ",
            "   'valid'= true",
            "); (    true    )",
            ")"
        }).Should().BeEquivalentTo(
            new string[] {
        "(",
        "    'user'=",
        "    (",
        "        'key'='1= t(c)(';",
        "        'valid'=false",
        "    );",
        "    'user'=",
        "    (",
        "        'key'=' = ; ';",
        "        'valid'=true",
        "    );",
        "    (",
        "        true",
        "    )",
        ")"});
    }
}