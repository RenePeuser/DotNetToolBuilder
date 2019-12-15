using System.Drawing;
using Pastel;
using trumpf.hmi.dotnettool.builder.Extensions;

namespace trumpf.hmi.dotnettool.builder
{
    public class InsertProjectName : CollectInfoStep
    {
        public InsertProjectName() : base(
            "Please enter the name of your project: (Sample: Trumpf.Hmi.New.Submarine)".AsInput())
        {
        }
    }
}