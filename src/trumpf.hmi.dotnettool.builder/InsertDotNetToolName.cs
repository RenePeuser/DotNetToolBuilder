using System;
using System.Drawing;
using Pastel;

namespace trumpf.hmi.dotnettool.builder
{
    public class InsertDotNetToolName : CollectInfoStep
    {
        private static readonly string title =
            "Please enter the name of the DotNetTool: (Sample: 'dotnet')".AsInput();

        public InsertDotNetToolName() : base(title)
        {
        }
    }
}